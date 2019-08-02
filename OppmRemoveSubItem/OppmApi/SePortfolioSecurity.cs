using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using NLog;
using wsPortfoliosSecurity;

namespace OppmRemoveSubItem.OppmApi
{
    public class SePortfolioSecurity
    {
        public static Logger PsLogger = LogManager.GetCurrentClassLogger();

        private String _proSightServer;
        public String ProSightServer
        {
            get
            {
                return _proSightServer;
            }
            set
            {
                _proSightServer = value;
                UpdateWebReference();
            }
        }

        private Int32 _proSightServerPort;
        public Int32 ProSightServerPort
        {
            get
            {
                return _proSightServerPort;
            }
            set
            {
                _proSightServerPort = value;
                UpdateWebReference();
            }
        }

        private Boolean _proSightServerUseSsl;
        

        public Boolean ProSightServerUseSsl
        {
            get { return _proSightServerUseSsl; }
            set
            {
                _proSightServerUseSsl = value;
                UpdateWebReference();
            }
        }

        public Boolean UseComApi { get; set; }

        public String User { get; set; }

        public String Password { get; set; }

        public String SecurityToken
        {
            get
            {
                var cookies = CookieContainer.GetCookies(new Uri(Url));
                return cookies[0].Value;
            }
        }

        public Int32 TimeOut { get; set; }

        public Boolean IsLoggedOn { get; set; }

        public CookieContainer CookieContainer
        {
            get { return PortfoliosSecurityWs.CookieContainer; }
            set { PortfoliosSecurityWs.CookieContainer = value; }
        }

        public String Url
        {
            get { return PortfoliosSecurityWs.Url; }
            set { PortfoliosSecurityWs.Url = value; }
        }

        private X509Certificate _certificate;
        public X509Certificate Certificate
        {
            get { return _certificate; }
            set
            {
                _certificate = value;
                PortfoliosSecurityWs.ClientCertificates.Add(_certificate);
            }
        }

        public psPortfoliosSecurity PortfoliosSecurityWs { get; set; }


        public SePortfolioSecurity()
        {
            PortfoliosSecurityWs = new psPortfoliosSecurity();
            CookieContainer = new CookieContainer();
            PortfoliosSecurityWs.CookieContainer = CookieContainer;
            ProSightServer = "localhost";
            ProSightServerPort = 0;
            ProSightServerUseSsl = false;
            TimeOut = 10000;
            User = "";
            Password = "";
            IsLoggedOn = false;
        }

        public SePortfolioSecurity(String user, String password)
            : this()
        {
            User = user;
            Password = password;
            Login();
        }


        public SePortfolioSecurity(String user, String password, String proSightServer)
            : this()
        {
            User = user;
            Password = password;
            ProSightServer = proSightServer;
            Login();
        }

        public SePortfolioSecurity(String user, String password, int timeout)
            : this()
        {
            User = user;
            Password = password;
            TimeOut = timeout;
            Login();
        }

        public Boolean Login()
        {
            IsLoggedOn = false;
            try
            {     
                    PortfoliosSecurityWs.Login(User, Password, TimeOut);
                    IsLoggedOn = true;
            }
            catch (Exception ex)
            {
                IsLoggedOn = false;
                PsLogger.Fatal(string.Format("Unexcpected Login Error: \n{0}\n", ex.Message));
                throw new Exception(ex.Message, ex.InnerException);
            }
            return IsLoggedOn;
        }

        /*
        public psPortfoliosUser GetLoginUser()
        {
            psPortfoliosUserInfo userInfo;
            try
            {
                ///TODO see if we can get the id from the cookie
                //var userID = PsSecurity.GetCurrentUserID(this.Token);
                var loginUser = new psPortfoliosUser { Url = Url.ToString(), CookieContainer = LoginCookie };
                userInfo = loginUser;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Unable to Get UserInfo: \n{0}\n", ex.Message));
                throw new Exception(ex.Message, ex.InnerException);
            }
            return userInfo;
        }
         * */
        /*
        public psPortfoliosUserInfo GetLoginUser(String userName)
        {
            psPortfoliosUserInfo userInfo;
            try
            {
                using (var loginUser = new psPortfoliosUser())
                {
                    //loginUser.SetSecurity(this.Token);
                    userInfo = loginUser.GetUserIDByLogin(userName);
                } 
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Unable to Get UserInfo: \n{0}\n", ex.Message));
                throw new Exception(ex.Message, ex.InnerException);
            }
            return userInfo;
        }
         * */
        /*
        public String GetConnectionString()
        {
            string connectionString;
            try
            {
                //Hijacked this field on the admin account
                connectionString = GetLoginUser("admin").Address;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Unable to Get DB ConnectionString: \n{0}\n", ex.Message));
                throw new Exception(ex.Message, ex.InnerException);
            }
            return connectionString;
        }
         * */

        private void UpdateWebReference()
        {
            var uri = new StringBuilder();
            uri.Append(ProSightServerUseSsl ? "https://" : "http://");
            uri.Append(String.Format("{0}", ProSightServer));
            if (ProSightServerPort > 0)
                uri.Append(String.Format(":{0}", ProSightServerPort));
            uri.Append("/ProSightWS/psPortfoliosSecurity.asmx?wsdl");
            Url = uri.ToString();
        }

        public String GetWebServicesUrLbyType(object psObject)
        {
            var psObjectType = psObject.GetType().Name;
            var webServiceName = Url.Replace("psPortfoliosSecurity", psObjectType);
            return webServiceName;
        }
    }
}
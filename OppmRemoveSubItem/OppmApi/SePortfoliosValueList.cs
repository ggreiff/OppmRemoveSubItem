using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using NLog;
using OppmRemoveSubItem.Utility;
using wsPortfoliosValueList;

namespace OppmRemoveSubItem.OppmApi
{
    public class SePortfoliosValueList
    {
        public static Logger PsLogger = LogManager.GetCurrentClassLogger();
        public SePortfolioSecurity PsSecurityLogin { get; set; }
        public psPortfoliosValueList PsValueList { get; set; }
        public psPortfoliosValueListValueInfo PValueListValueInfo { get; set; }

        public SePortfoliosValueList(SePortfolioSecurity securityLogin)
        {
            PsSecurityLogin = securityLogin;
            PsValueList = new psPortfoliosValueList { CookieContainer = PsSecurityLogin.CookieContainer };
            PsValueList.Url = PsSecurityLogin.GetWebServicesUrLbyType(PsValueList);

            if (!securityLogin.PortfoliosSecurityWs.ClientCertificates.HasItems()) return;
            foreach (var clientCertificate in securityLogin.PortfoliosSecurityWs.ClientCertificates)
            {
                PsValueList.ClientCertificates.Add(clientCertificate);
            }
        }

        public SePortfoliosValueList(SePortfolioSecurity securityLogin, X509Certificate certificate)
            : this(securityLogin)
        {
            PsValueList.ClientCertificates.Add(certificate);
        }

        public List<psPortfoliosValueListValueInfo> GetValueList(String listName)
        {
            try
            {
                return PsValueList.GetValues(listName).ToList();
            }
            catch (Exception ex)
            {
                PsLogger.Warn(ex.Message);
                return new List<psPortfoliosValueListValueInfo>();
            }
        }

        public Boolean ValueListExists(String listName)
        {
            var listValues = GetValueListText(listName);
            return listValues.HasItems();
        }

        public List<String> GetValueListText(String listName)
        {
            return GetValueList(listName).Select(valueInfo => valueInfo.Text).ToList();
        }

        public Boolean HasValue(String listName, String listValue)
        {
            if (listName.IsNullOrEmpty() || listValue.IsNullOrEmpty()) return false;
            return GetValueListText(listName).Contains(listValue);
        }

        public void UpdateValueList(String listName, List<psPortfoliosValueListValueInfo> valueListInfos)
        {
            try
            {
                PsValueList.UpdateValues(listName, valueListInfos.ToArray());
            }
            catch (Exception ex)
            {
                PsLogger.Error(ex.Message);
            }
        }
    }
}
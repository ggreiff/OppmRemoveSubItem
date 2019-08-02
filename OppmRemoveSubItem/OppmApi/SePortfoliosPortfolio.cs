using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using NLog;
using OppmRemoveSubItem.Utility;
using wsPortfoliosItem;
using wsPortfoliosPortfolio;
using psPortfoliosItemInfo = wsPortfoliosPortfolio.psPortfoliosItemInfo;
using psPORTFOLIO_TYPE = wsPortfoliosPortfolio.psPORTFOLIO_TYPE;
using psRETURN_VALUES = wsPortfoliosPortfolio.psRETURN_VALUES;

namespace OppmRemoveSubItem.OppmApi
{
    public class SePortfoliosPortfolio
    {
        public static Logger PsLogger = LogManager.GetCurrentClassLogger();
        public SePortfolioSecurity PsSecurityLogin { get; set; }

        public psPortfoliosPortfolio PsPortfolio { get; set; }

        public psPortfoliosItem PsItem { get; set; }

        public String LastError { get; set; }


        public SePortfoliosPortfolio(SePortfolioSecurity securityLogin)
        {
            PsSecurityLogin = securityLogin;
            PsPortfolio = new psPortfoliosPortfolio {CookieContainer = PsSecurityLogin.CookieContainer};
            PsItem = new psPortfoliosItem {CookieContainer = PsSecurityLogin.CookieContainer};
            PsPortfolio.Url = PsSecurityLogin.GetWebServicesUrLbyType(PsPortfolio);
            PsItem.Url = PsSecurityLogin.GetWebServicesUrLbyType(PsItem);

            if (!securityLogin.PortfoliosSecurityWs.ClientCertificates.HasItems()) return;
            foreach (var clientCertificate in securityLogin.PortfoliosSecurityWs.ClientCertificates)
            {
                PsItem.ClientCertificates.Add(clientCertificate);
                PsPortfolio.ClientCertificates.Add(clientCertificate);
            }
        }

        public SePortfoliosPortfolio(SePortfolioSecurity securityLogin, X509Certificate certificate)
            : this(securityLogin)
        {
            PsPortfolio.ClientCertificates.Add(certificate);
            PsItem.ClientCertificates.Add(certificate);
        }

        public psPortfoliosQueryPart NewQueryPart(String categoryName, psOPERATOR operatorType, psLOGICAL_OPERATOR logicalOperator)
        {
            var queryPart = NewQueryPart();
            queryPart.CategoryName = categoryName;
            queryPart.CellPart = psCELL_PART.CPRT_VALUE;
            queryPart.Operator = operatorType;
            queryPart.LogicalOperator = logicalOperator;
            return queryPart;
        }


        public psPortfoliosQueryPart NewQueryPart(String categoryName, psOPERATOR operatorType, String compareValue, psLOGICAL_OPERATOR logicalOperator)
        {
            var queryPart = NewQueryPart();
            queryPart.CategoryName = categoryName;
            queryPart.CellPart = psCELL_PART.CPRT_VALUE;
            queryPart.Operator = operatorType;
            queryPart.CompareDisplayValue = compareValue;
            queryPart.LogicalOperator = logicalOperator;
            return queryPart;
        }

        public psPortfoliosQueryPart NewQueryPart(String categoryName, psCELL_PART cellPart, psOPERATOR operatorType, String compareValue, psLOGICAL_OPERATOR logicalOperator)
        {
            var queryPart = NewQueryPart();

            queryPart.CategoryName = categoryName;
            queryPart.CellPart = cellPart;
            queryPart.Operator = operatorType;
            queryPart.CompareDisplayValue = compareValue;
            queryPart.LogicalOperator = logicalOperator;
            return queryPart;
        }

        public psPortfoliosQueryPart NewQueryPart()
        {
            var queryPart = new psPortfoliosQueryPart();
            return queryPart;
        }

        public static psPortfoliosQueryPart FactoryQueryPart()
        {
            return new psPortfoliosQueryPart();
        }

        public List<wsPortfoliosItem.psPortfoliosItemInfo> GetPortfolioListByQuery(String portfolioScope, List<psPortfoliosQueryPart> queryParts, psPORTFOLIO_TYPE itemTypes)
        {
            return GetPortfolioListByQuery("", portfolioScope, queryParts, itemTypes);
        }

        public List<wsPortfoliosItem.psPortfoliosItemInfo> GetPortfolioListByQuery(List<psPortfoliosQueryPart> queryParts, psPORTFOLIO_TYPE itemTypes)
        {
            return GetPortfolioListByQuery("", null, queryParts, itemTypes);
        }

        public List<wsPortfoliosItem.psPortfoliosItemInfo> GetPortfolioListByQuery(String commonIdCategory, String portfolioScope, List<psPortfoliosQueryPart> queryPartsList, psPORTFOLIO_TYPE itemTypes)
        {
            var arScope = new psPortfoliosPortfolioIdentifier[1];
            var retPortfolioItemInfoList = new List<wsPortfoliosItem.psPortfoliosItemInfo>();

            try
            {
                if (portfolioScope != null)
                {
                    arScope[0].Name = portfolioScope;
                }

                //Get the QBP
                var retVal = PsPortfolio.GetItemListByQuery(commonIdCategory, arScope, queryPartsList.ToArray(), itemTypes);
                retPortfolioItemInfoList.AddRange(retVal.Select(val => PsItem.GetItemInfo("", val.ProSightID.ToString(CultureInfo.InvariantCulture))));
            }
            catch (Exception ex)
            {
                PsLogger.Error(string.Format("Unexcpected GetPortfolioListByQuery Error: \n{0}\n", ex.Message));
                throw new Exception(ex.Message, ex.InnerException);
            }
            return retPortfolioItemInfoList;

        }

        public List<String> GetPortfolioListIdsByCommondId(int proSightId)
        {
            return GetPortfolioListIdsByCommondId(String.Empty, proSightId.ToString(CultureInfo.InvariantCulture));
        }

        public List<String> GetPortfolioListIdsByCommondId(String proSightId)
        {
            return GetPortfolioListIdsByCommondId(String.Empty, proSightId);
        }

        public List<String> GetPortfolioListIdsByCommondId(String uci, String id)
        {
            var retPortfolioItemInfoId = new List<String>();
            try
            {
                var retVal = PsPortfolio.GetItemListByCommonID(uci, id);
                retPortfolioItemInfoId.AddRange(retVal.Select(psPortfoliosItemInfo => psPortfoliosItemInfo.UCI));
            }
            catch (Exception ex)
            {
                PsLogger.Error(String.Format("Unexcpected GetPortfolioListByCommonID Error: \n{0}\n", ex.Message));
                throw new Exception(ex.Message, ex.InnerException);
            }
            return retPortfolioItemInfoId;
        }

        public List<psPortfoliosItemInfo> GetPortfolioListByCommonId(int proSightId)
        {
            return GetPortfolioListByCommonId(String.Empty, proSightId.ToString(CultureInfo.InvariantCulture));
        }

        public List<wsPortfoliosItem.psPortfoliosItemInfo> GetPortfolioListByCommonId(String proSightId)
        {
            return GetPortfolioInfoListByCommonId(String.Empty, proSightId);
        }

        public List<psPortfoliosItemInfo> GetPortfolioListByCommonId(String uci, String id)
        {
            var retPortfolioItemInfoList = new List<psPortfoliosItemInfo>();
            try
            {
                var retVal = PsPortfolio.GetItemListByCommonID(uci, id);
                return retVal.ToList();
            }
            catch (Exception ex)
            {
                PsLogger.Error(String.Format("Unexcpected GetPortfolioListByCommonID Error: \n{0}\n", ex.Message));
            }
            return retPortfolioItemInfoList;
        }

        public List<wsPortfoliosItem.psPortfoliosItemInfo> GetPortfolioInfoListByCommonId(int proSightId)
        {
            return GetPortfolioInfoListByCommonId(String.Empty, proSightId.ToString(CultureInfo.InvariantCulture));
        }

        public List<wsPortfoliosItem.psPortfoliosItemInfo> GetPortfolioInfoListByCommonId(String proSightId)
        {
            return GetPortfolioInfoListByCommonId(String.Empty, proSightId);
        }

        public List<wsPortfoliosItem.psPortfoliosItemInfo> GetPortfolioInfoListByCommonId(String uci, String id)
        {
            var retPortfolioItemInfoList = new List<wsPortfoliosItem.psPortfoliosItemInfo>();
            try
            {
                var retVal = PsPortfolio.GetItemListByCommonID(uci, id);
                retPortfolioItemInfoList.AddRange(retVal.Select(x => PsItem.GetItemInfo(uci, x.UCI)));
            }
            catch (Exception ex)
            {
                PsLogger.Error(String.Format("Unexcpected GetPortfolioListByCommonID Error: \n{0}\n", ex.Message));
            }
            return retPortfolioItemInfoList;
        }

        public psRETURN_VALUES AddPortfolio(int portfolioId, psPortfoliosItemInfo portfolioItemInfo)
        {
            return AddPortfolio(String.Empty, portfolioId.ToString(CultureInfo.InvariantCulture), portfolioItemInfo);
        }

        public psRETURN_VALUES AddPortfolio(psPortfoliosItemInfo portfolioItemInfo)
        {
            return AddPortfolio(String.Empty, String.Empty, portfolioItemInfo);
        }

        public psRETURN_VALUES AddPortfolio(String portfolioId, psPortfoliosItemInfo portfolioItemInfo)
        {
            return AddPortfolio(String.Empty, portfolioId, portfolioItemInfo);
        }

        public psRETURN_VALUES AddPortfolio(String uci, String id, psPortfoliosItemInfo portfolioItemInfo)
        {
            psRETURN_VALUES retVal;
            try
            {
                //PsPortfolio.AddNewEx(String.Empty, String.Empty, portfolioItemInfo.Name, portfolioItemInfo.LifeCycle, 
                //                    portfolioItemInfo.StartDate, portfolioItemInfo.EndDate, portfolioItemInfo.Description,portfolioItemInfo.Status, 
                //                    portfolioItemInfo.PortfolioType,portfolioItemInfo.Manager,portfolioItemInfo.ContainerUCI);
                PsPortfolio.AddNewEx2(uci, id, portfolioItemInfo);
                retVal = psRETURN_VALUES.ERR_OK;
            }
            catch (Exception ex)
            {
                PsLogger.Error("Unexcpected AddPortfolio Error: \n{0}\n", ex.Message);
                throw new Exception(ex.Message, ex.InnerException);
            }
            return retVal;
        }

        public psRETURN_VALUES UpdatePortfolio(int portfolioId, psPortfoliosItemInfo portfolioItemInfo)
        {
            return UpdatePortfolio("", portfolioId.ToString(CultureInfo.InvariantCulture), portfolioItemInfo);
        }

        public psRETURN_VALUES UpdatePortfolio(String portfolioId, psPortfoliosItemInfo portfolioItemInfo)
        {
            return UpdatePortfolio("", portfolioId, portfolioItemInfo);
        }

        public psRETURN_VALUES UpdatePortfolio(String uci, String id, psPortfoliosItemInfo portfolioItemInfo)
        {
            psRETURN_VALUES retVal;
            try
            {
                PsPortfolio.UpdateEx2(uci, id, portfolioItemInfo);
                retVal = psRETURN_VALUES.ERR_OK;
            }
            catch (Exception ex)
            {
                PsLogger.Error(string.Format("Unexcpected UpdatePortfolio Error: \n{0}\n", ex.Message));
                throw new Exception(ex.Message, ex.InnerException);
            }
            return retVal;
        }

        public psRETURN_VALUES SynchronizePortfolio(int portfolioId, psPortfoliosItemInfo portfolioItemInfo)
        {
            return SynchronizePortfolio("", portfolioId.ToString(CultureInfo.InvariantCulture), portfolioItemInfo);
        }

        public psRETURN_VALUES SynchronizePortfolio(String portfolioId, psPortfoliosItemInfo portfolioItemInfo)
        {
            return SynchronizePortfolio("", portfolioId, portfolioItemInfo);
        }


        public psRETURN_VALUES SynchronizePortfolio(String uci, String id, psPortfoliosItemInfo portfolioItemInfo)
        {
            psRETURN_VALUES retVal;
            try
            {
                PsPortfolio.SynchronizeEx2(uci, id, portfolioItemInfo);
                retVal = psRETURN_VALUES.ERR_OK;
            }
            catch (Exception ex)
            {
                PsLogger.Error(string.Format("Unexcpected SynchronizePortfolio Error: \n{0}\n", ex.Message));
                throw new Exception(ex.Message, ex.InnerException);
            }
            return retVal;
        }

        public psRETURN_VALUES SynchronizeQbp(String uci, String id, String portfolioNameScope, List<psPortfoliosQueryPart> queryPartsList, bool periodicUpdate, psPortfoliosItemInfo portfolioItemInfo)
        {
            var portfolioScope = new List<psPortfoliosPortfolioIdentifier>
                                     {
                                         new psPortfoliosPortfolioIdentifier { Name = portfolioNameScope }
                                     };
            return SynchronizeQbp(uci, id, portfolioScope, queryPartsList, periodicUpdate, portfolioItemInfo);
        }

        public psRETURN_VALUES SynchronizeQbp(String uci, String id, List<psPortfoliosPortfolioIdentifier> portfolioScope, List<psPortfoliosQueryPart> queryPartsList, bool periodicUpdate, psPortfoliosItemInfo portfolioItemInfo)
        {
            psRETURN_VALUES retVal;
            try
            {
                PsPortfolio.SynchronizeQBP2(uci, id, portfolioScope.ToArray(), queryPartsList.ToArray(), periodicUpdate, portfolioItemInfo);
                retVal = psRETURN_VALUES.ERR_OK;
            }
            catch (Exception ex)
            {
                PsLogger.Error(string.Format("Unexcpected SynchronizeQBP Error: \n{0}\n", ex.Message));
                throw new Exception(ex.Message, ex.InnerException);
            }
            return retVal;
        }


        public psPortfoliosItemInfo GetPortfolioInfo(int itemId)
        {
            return GetPortfolioInfo("", itemId.ToString(CultureInfo.InvariantCulture));
        }

        public psPortfoliosItemInfo GetPortfolioInfo(String itemId)
        {
            return GetPortfolioInfo("", itemId);
        }

        public psPortfoliosItemInfo GetPortfolioInfoByName(String itemName)
        {
            return GetPortfolioInfo(GetPortfolioIdByName(itemName));
        }

        public String GetPortfolioIdByName(String itemName)
        {
            return GetPortfolioIdByName("", itemName);
        }

        public Boolean PortfolioNameExists(String portfolioName)
        {
            return !String.IsNullOrEmpty(GetPortfolioIdByName("", portfolioName));
        }

        public String GetPortfolioIdByName(String uci, String uciName)
        {
            String retValue;
            try
            {
                retValue = PsPortfolio.GetIDByName(uci, uciName);
            }
            catch (Exception)
            {
                retValue = null;
            }
            return retValue;
        }

        public psPortfoliosItemInfo GetPortfolioInfo(String uci, String id)
        {
            psPortfoliosItemInfo retVal;
            try
            {
                retVal = PsPortfolio.GetPortfolioInfo(uci, id);
            }
            catch (Exception ex)
            {
                PsLogger.Error(string.Format("Unexcpected GetPortfolioInfo Error: \n{0}\n", ex.Message));
                throw new Exception(ex.Message, ex.InnerException);
            }
            return retVal;
        }

        public psPortfoliosItemInfo NewPortfolioInfo()
        {
            return new psPortfoliosItemInfo();
        }

        private void ClearLastError()
        {
            LastError = String.Empty;
        }

    }
}
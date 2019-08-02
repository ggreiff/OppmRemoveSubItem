using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using NLog;
using OppmRemoveSubItem.Utility;
using wsPortfoliosItem;

namespace OppmRemoveSubItem.OppmApi
{
    public class SePortfolioItem
    {
        public static Logger PsLogger = LogManager.GetCurrentClassLogger();
        public SePortfolioSecurity PsSecurityLogin { get; set; }
        public psPortfoliosItem PsItem { get; set; }

        public SePortfolioItem(SePortfolioSecurity securityLogin)
        {
            PsSecurityLogin = securityLogin;
            PsItem = new psPortfoliosItem { CookieContainer = PsSecurityLogin.CookieContainer };
            PsItem.Url = PsSecurityLogin.GetWebServicesUrLbyType(PsItem);

            if (!securityLogin.PortfoliosSecurityWs.ClientCertificates.HasItems()) return;
            foreach (var clientCertificate in securityLogin.PortfoliosSecurityWs.ClientCertificates)
            {
                PsItem.ClientCertificates.Add(clientCertificate);
            }
        }

        public SePortfolioItem(SePortfolioSecurity securityLogin, X509Certificate certificate)
            : this(securityLogin)
        {
            PsItem.ClientCertificates.Add(certificate);
        }

        public String AddNewEx(String itemName, String containerName)
        {
            return AddNewEx(itemName, String.Empty, GetItemIdByName(containerName));
        }

        public String AddNewEx(String itemName, Int32 containerId)
        {
            return AddNewEx(itemName, String.Empty, String.Empty, String.Empty, String.Empty, psOPEN_CLOSED_STATUS.OCSTS_OPEN, PsSecurityLogin.User, containerId.ToString(CultureInfo.InvariantCulture));
        }

        public String AddNewEx(String itemName, String description, String containerId)
        {
            return AddNewEx(itemName, String.Empty, String.Empty, String.Empty, description, psOPEN_CLOSED_STATUS.OCSTS_OPEN, PsSecurityLogin.User,
                            containerId);
        }

        public String AddNewEx(String itemName, psOPEN_CLOSED_STATUS psStatus, String containerId)
        {
            return AddNewEx(itemName, String.Empty, String.Empty, String.Empty, String.Empty, psStatus, PsSecurityLogin.User, containerId);
        }

        public String AddNewEx(String itemName, String description, psOPEN_CLOSED_STATUS psStatus, String containerId)
        {
            return AddNewEx(itemName, String.Empty, String.Empty, String.Empty, description, psStatus, PsSecurityLogin.User, containerId);
        }

        public string AddNewEx(psPortfoliosItemInfo itemInfo)
        {
            return AddNewEx(itemInfo.Name, itemInfo.LifeCycle, itemInfo.StartDate, itemInfo.EndDate,
                            itemInfo.Description,
                            itemInfo.Status, itemInfo.Manager, itemInfo.ContainerUCI);
        }

        public string AddNewEx(String itemName, String lifeCycle, String startDate, String endDate, String description,
                               psOPEN_CLOSED_STATUS psStatus, String manager, String containerId)
        {

            var itemId = GetItemIdByName(itemName);

            if (itemId.IsNotNullOrEmpty()) return itemId;
            try
            {
                //TODO add psOpenCloseStatus most things are open when created.
                PsItem.AddNewEx("", "", itemName, lifeCycle, startDate, endDate,
                                 description, psStatus, manager, containerId);
                itemId = GetItemIdByName(itemName);
            }
            catch (Exception ex)
            {
                PsLogger.Error(string.Format("Unexcpected AddNewEx Error: \n{0}\n", ex.Message));
                throw new Exception(ex.Message, ex.InnerException);
            }
            return itemId;
        }

        public psRETURN_VALUES UpdateEx(psPortfoliosItemInfo itemInfo, int updateFlags)
        {
            return UpdateEx(itemInfo.ProSightID, itemInfo.Name, itemInfo.LifeCycle, itemInfo.StartDate, itemInfo.EndDate,
                            itemInfo.Description, itemInfo.Status, itemInfo.Manager,
                            itemInfo.ContainerProSightID.ToString(CultureInfo.InvariantCulture), updateFlags);
        }

        public psRETURN_VALUES UpdateEx(int id, String itemName, String lifeCycle, String startDate, String endDate,
                                        String description, psOPEN_CLOSED_STATUS psStatus, String manager,
                                        String containerId, int updateFlags)
        {
            return UpdateEx("", id.ToString(CultureInfo.InvariantCulture), itemName, lifeCycle, startDate, endDate, description, psStatus, manager,
                            containerId, updateFlags);
        }

        public psRETURN_VALUES UpdateEx(String id, String itemName, String lifeCycle, String startDate, String endDate,
                                        String description, psOPEN_CLOSED_STATUS psStatus, String manager,
                                        String containerId, int updateFlags)
        {
            return UpdateEx("", id, itemName, lifeCycle, startDate, endDate, description, psStatus, manager, containerId,
                            updateFlags);
        }

        public psRETURN_VALUES UpdateEx(String uci, String id, String itemName, String lifeCycle, String startDate,
                                        String endDate,
                                        String description, psOPEN_CLOSED_STATUS psStatus, String manager,
                                        String containerId, int updateFlags)
        {
            psRETURN_VALUES retVal;
            try
            {
                PsItem.UpdateEx(uci, id, itemName, lifeCycle, startDate, endDate, description, psStatus, manager, containerId, updateFlags);
                retVal = psRETURN_VALUES.ERR_OK;
            }
            catch (Exception ex)
            {
                PsLogger.Error(string.Format("Unexcpected AddNewEx Error: \n{0}\n", ex.Message));
                throw new Exception(ex.Message, ex.InnerException);
            }
            return retVal;
        }

        public psPortfoliosItemInfo GetItemInfo(int itemId)
        {
            return GetItemInfo("", itemId.ToString(CultureInfo.InvariantCulture));
        }

        public psPortfoliosItemInfo GetItemInfo(String itemId)
        {
            return GetItemInfo("", itemId);
        }

        public psPortfoliosItemInfo GetItemInfoByName(String itemName)
        {
            return GetItemInfo(GetItemIdByName(itemName));
        }

        public Boolean ItemNameExists(String portfolioName)
        {
            return !String.IsNullOrEmpty(GetItemIdByName(String.Empty, portfolioName));
        }

        public String GetItemIdByName(String itemName)
        {
            return GetItemIdByName(String.Empty, itemName);
        }

        public String GetItemIdByName(String uci, String uciName)
        {
            String retValue;
            try
            {
                retValue = PsItem.GetIDByName(uci, uciName);
            }
            // ReSharper disable once UnusedVariable
            catch (Exception ex)
            {
                retValue = null;
                PsLogger.Trace(ex.Message);
            }
            return retValue;
        }

        public psPortfoliosItemInfo GetItemInfo(String uci, String id)
        {
            psPortfoliosItemInfo retVal;
            try
            {
                retVal = PsItem.GetItemInfo(uci, id);
            }
            catch (Exception ex)
            {
                PsLogger.Trace(String.Format("Item doesn't exits: {0}", ex.Message));
                retVal = null;
            }
            return retVal;
        }


        public psRETURN_VALUES AddSupport(String itemId, psPortfoliosDependencyInfo dependencyInfo)
        {
            var dependencyInfoList = new List<psPortfoliosDependencyInfo> { dependencyInfo };
            return AddSupport(itemId, dependencyInfoList);
        }

        public psRETURN_VALUES AddSupport(String itemId, List<psPortfoliosDependencyInfo> dependencyInfoList)
        {
            return AddSupport(String.Empty, itemId, dependencyInfoList);
        }

        public psRETURN_VALUES AddSupport(String uci, String id, List<psPortfoliosDependencyInfo> dependencyInfoList)
        {
            psRETURN_VALUES retVal;
            try
            {
                PsItem.AddSupportsItems(uci, id, dependencyInfoList.ToArray());
                retVal = psRETURN_VALUES.ERR_OK;
            }
            catch (Exception ex)
            {
                PsLogger.Error(string.Format("Unexcpected AddSupportItems Error: \n{0}\n", ex.Message));
                throw new Exception(ex.Message, ex.InnerException);
            }
            return retVal;
        }

        public psRETURN_VALUES AddDependsOn(psPortfoliosItemInfo itemInfo, psPortfoliosItemInfo dependsOnInfo, String dependsOnType)
        {
            var dependencyInfo = new psPortfoliosDependencyInfo
                                     {
                                         UCI = dependsOnInfo.ProSightID.ToString(CultureInfo.InvariantCulture),
                                         PortfolioType = dependsOnInfo.PortfolioType,
                                         DependencyType = dependsOnType
                                     };
            return AddDependsOn(itemInfo.ProSightID.ToString(CultureInfo.InvariantCulture), dependencyInfo);
        }


        public psRETURN_VALUES AddDependsOn(String itemId, psPortfoliosDependencyInfo dependencyInfo)
        {
            var dependencyInfoList = new List<psPortfoliosDependencyInfo> { dependencyInfo };
            return AddDependsOn(String.Empty, itemId, dependencyInfoList);
        }

        public psRETURN_VALUES AddDependsOn(String itemId, List<psPortfoliosDependencyInfo> dependencyInfoList)
        {
            return AddDependsOn(String.Empty, itemId, dependencyInfoList);
        }

        public psRETURN_VALUES AddDependsOn(String uci, String id, List<psPortfoliosDependencyInfo> dependencyInfoList)
        {
            psRETURN_VALUES retVal;
            try
            {
                var newDependencyInfoList = dependencyInfoList;
                var existsDependsOn = GetDependsOnItems(uci, id, String.Empty);

                if (existsDependsOn.HasItems())
                {
                    newDependencyInfoList = new List<psPortfoliosDependencyInfo>();
                    foreach (var depInfo in dependencyInfoList)
                    {
                        var d = depInfo;
                        var newDependsInfo = existsDependsOn.Where(x => x.ProSightID == d.ProSightID).Where(y => !y.DependencyType.IsEqualTo(d.DependencyType, true));
                        newDependencyInfoList.AddRange(newDependsInfo);
                    }
                }

                if (newDependencyInfoList.HasItems()) 
                    PsItem.AddDependsOnItems(uci, id, dependencyInfoList.ToArray());
                retVal = psRETURN_VALUES.ERR_OK;
            }
            catch (Exception ex)
            {
                PsLogger.Error(string.Format("Unexcpected AddDependsOnItems Error: \n{0}\n", ex.Message));
                throw new Exception(ex.Message, ex.InnerException);
            }
            return retVal;
        }

        public List<psPortfoliosDependencyInfo> GetDependsOnItems(int itemId)
        {

            return GetDependsOnItems(String.Empty, itemId.ToString(CultureInfo.InvariantCulture), String.Empty);
        }
        public List<psPortfoliosDependencyInfo> GetDependsOnItems(int itemId, String dependsType)
        {

            return GetDependsOnItems(String.Empty, itemId.ToString(CultureInfo.InvariantCulture), dependsType);
        }

        public List<psPortfoliosDependencyInfo> GetDependsOnItems(String itemId, String dependsType)
        {
            return GetDependsOnItems(String.Empty, itemId, dependsType);
        }

        public List<psPortfoliosDependencyInfo> GetDependsOnItems(String itemId)
        {
            return GetDependsOnItems(String.Empty, itemId, String.Empty);
        }

        public List<psPortfoliosDependencyInfo> GetDependsOnItems(String uci, String id, String dependsType)
        {
            var retVal = new List<psPortfoliosDependencyInfo>();
            try
            {
                var retItems = PsItem.GetDependsOnItems(uci, id, dependsType);
                retVal.AddRange(retItems);
            }
            catch (Exception ex)
            {
                PsLogger.Error(string.Format("Unexcpected AddDependsOnItems Error: \n{0}\n", ex.Message));
                throw new Exception(ex.Message, ex.InnerException);
            }
            return retVal;
        }

        public List<psPortfoliosDependencyInfo> GetSupportItems(int itemId)
        {

            return GetSupportItems(String.Empty, itemId.ToString(CultureInfo.InvariantCulture), String.Empty);
        }
        public List<psPortfoliosDependencyInfo> GetSupportItems(int itemId, String dependsType)
        {

            return GetSupportItems("", itemId.ToString(CultureInfo.InvariantCulture), dependsType);
        }

        public List<psPortfoliosDependencyInfo> GetSupportItems(String itemId, String dependsType)
        {
            return GetSupportItems("", itemId, dependsType);
        }

        public List<psPortfoliosDependencyInfo> GetSupportItems(String itemId)
        {
            return GetSupportItems(String.Empty, itemId, String.Empty);
        }

        public List<psPortfoliosDependencyInfo> GetSupportItems(String uci, String id, String dependsType)
        {
            var retVal = new List<psPortfoliosDependencyInfo>();
            try
            {
                var retItems = PsItem.GetSupportsItems(uci, id, dependsType);
                retVal.AddRange(retItems);
            }
            catch (Exception ex)
            {
                PsLogger.Error(string.Format("Unexcpected AddSupportItems Error: \n{0}\n", ex.Message));
                throw new Exception(ex.Message, ex.InnerException);
            }
            return retVal;
        }


        public Boolean UpdateHomePortfolio(String itemName, String homePortfolioName)
        {
            var itemInfo = GetItemInfoByName(itemName);
            var homePortfolioInfo = GetItemInfoByName(homePortfolioName);
            return itemInfo != null && homePortfolioInfo != null && UpdateHomePorfolio(itemInfo, homePortfolioInfo);
        }

        public Boolean UpdateHomePortfolio(Int32 itemId, Int32 homePortfolioId)
        {
            var itemInfo = GetItemInfo(itemId);
            var homePortfolioInfo = GetItemInfo(homePortfolioId);
            return itemInfo != null && homePortfolioInfo != null && UpdateHomePorfolio(itemInfo, homePortfolioInfo);
        }

        public Boolean UpdateHomePorfolio(psPortfoliosItemInfo itemInfo, psPortfoliosItemInfo homePortfolioInfo)
        {
            var retVal = false;
            if (itemInfo.ContainerProSightID != homePortfolioInfo.ProSightID)
            {
                itemInfo.ContainerProSightID = homePortfolioInfo.ProSightID;
                UpdateEx(itemInfo, 4096);
                retVal = true;
            }
            return retVal;
        }

        public Boolean RenameItem(psPortfoliosItemInfo itemInfo, String newItemName)
        {
            var retVal = false;
            if (!itemInfo.Name.IsEqualTo(newItemName, true))
            {
                itemInfo.Name = newItemName;
                UpdateEx(itemInfo, 1);
                retVal = true;
            }
            return retVal;
        }
    }
}
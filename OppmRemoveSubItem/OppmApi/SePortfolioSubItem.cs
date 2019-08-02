using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Packaging;
using System.Linq;
using NLog;
using OppmRemoveSubItem.Utility;
using wsPortfoliosSubItem;

namespace OppmRemoveSubItem.OppmApi
{
    public class SePortfolioSubItem
    {
        public static Logger PsLogger = LogManager.GetCurrentClassLogger();
        public SePortfolioSecurity PsSecurityLogin { get; set; }
        public psPortfoliosSubItem PsSubItem { get; set; }

        public SePortfolioSubItem(SePortfolioSecurity securityLogin)
        {
            PsSecurityLogin = securityLogin;
            PsSubItem = new psPortfoliosSubItem { CookieContainer = PsSecurityLogin.CookieContainer };
            PsSubItem.Url = PsSecurityLogin.GetWebServicesUrLbyType(PsSubItem);

            if (!securityLogin.PortfoliosSecurityWs.ClientCertificates.HasItems()) return;
            foreach (var clientCertificate in securityLogin.PortfoliosSecurityWs.ClientCertificates)
            {
                PsSubItem.ClientCertificates.Add(clientCertificate);
            }
        }

        public List<psPortfoliosSubItemInfo> GetSubItemListAsOfToday(Int32 sPorSightId, String sSubItemType,
            Int32 lSubItemTypeId, List<String> arCategoryNames, Boolean bShowHiddenSubItems)
        {
            return GetSubItemListAsOf("", sPorSightId.ToString(CultureInfo.InvariantCulture), sSubItemType, lSubItemTypeId, arCategoryNames, bShowHiddenSubItems, DateTime.Now);
        }


        public List<psPortfoliosSubItemInfo> GetSubItemListAsOfToday(String sCommonIdCategory, String sId, String sSubItemType,
            Int32 lSubItemTypeId, List<String> arCategoryNames, Boolean bShowHiddenSubItems)
        {
            return GetSubItemListAsOf(sCommonIdCategory, sId, sSubItemType, lSubItemTypeId, arCategoryNames, bShowHiddenSubItems, DateTime.Now);
        }

        public List<psPortfoliosSubItemInfo> GetSubItemListAsOfToday(String sCommonIdCategory, String sId, Int32 lSubItemTypeId, List<String> arCategoryNames, Boolean bShowHiddenSubItems)
        {
            return GetSubItemListAsOf(sCommonIdCategory, sId, "", lSubItemTypeId, arCategoryNames, bShowHiddenSubItems, DateTime.Now);
        }

        public List<psPortfoliosSubItemInfo> GetSubItemListAsOfToday(String sCommonIdCategory, String sId, String sSubItemType, List<String> arCategoryNames, Boolean bShowHiddenSubItems)
        {
            return GetSubItemListAsOf(sCommonIdCategory, sId, sSubItemType, 0, arCategoryNames, bShowHiddenSubItems, DateTime.Now);
        }

        public List<psPortfoliosSubItemInfo> GetSubItemListAsOf(Int32 sId, String sSubItemType, Int32 lSubItemTypeId,
            List<String> arCategoryNames, Boolean bShowHiddenSubItems, DateTime sAsOf)
        {
            return GetSubItemListAsOf(String.Empty, sId.ToString(), sSubItemType, lSubItemTypeId, arCategoryNames, bShowHiddenSubItems, sAsOf);
        }

        public List<psPortfoliosSubItemInfo> GetSubItemListAsOf(String sCommonIdCategory, String sId, String sSubItemType, Int32 lSubItemTypeId,
            List<String> arCategoryNames, Boolean bShowHiddenSubItems, DateTime sAsOf)
        {

            var subItemList = new List<psPortfoliosSubItemInfo>();
            try
            {
                var retVal = PsSubItem.GetSubItemListAsOf(sCommonIdCategory, sId, sSubItemType, lSubItemTypeId,
                    arCategoryNames.ToArray(), bShowHiddenSubItems, sAsOf.ToString("MM/dd/yyyy"));
                subItemList.AddRange(retVal);
            }
            catch (Exception ex)
            {
                PsLogger.Error(ex.Message);
                subItemList = new List<psPortfoliosSubItemInfo>();
            }

            return subItemList;
        }

        public List<psPortfoliosSubItemUpdateStatus> SyncSubItemsAsOfToday(Int32 sProSightId, Int32 lSubItemTypeId,
            List<psPortfoliosSubItemInfo> arSubItemList, Boolean stopOnAnyError)
        {
            return SyncSubItemsAsOfToday(String.Empty, sProSightId.ToString(CultureInfo.InvariantCulture), String.Empty, lSubItemTypeId, arSubItemList, stopOnAnyError);
        }

        public List<psPortfoliosSubItemUpdateStatus> SyncSubItemsAsOfToday(Int32 sProSightId, String sSubItemType,
                List<psPortfoliosSubItemInfo> arSubItemList, Boolean stopOnAnyError)
        {
            return SyncSubItemsAsOfToday(sProSightId.ToString(CultureInfo.InvariantCulture), sSubItemType, arSubItemList, stopOnAnyError);
        }

        public List<psPortfoliosSubItemUpdateStatus> SyncSubItemsAsOfToday(String sProSightId, String sSubItemType,
                List<psPortfoliosSubItemInfo> arSubItemList, Boolean stopOnAnyError)
        {
            return SyncSubItemsAsOf(String.Empty, sProSightId, sSubItemType, 0, arSubItemList, DateTime.Now, stopOnAnyError);
        }

        public List<psPortfoliosSubItemUpdateStatus> SyncSubItemsAsOfToday(String sCommonIdCategory, String sId, String sSubItemType,
            Int32 lSubItemTypeId, List<psPortfoliosSubItemInfo> arSubItemList, Boolean stopOnAnyError)
        {
            return SyncSubItemsAsOf(sCommonIdCategory, sId, sSubItemType, lSubItemTypeId, arSubItemList, DateTime.Now, stopOnAnyError);
        }

        public List<psPortfoliosSubItemUpdateStatus> SyncSubItemsAsOf(Int32 sProSightId, String sSubItemType, Int32 lSubItemTypeId,
            List<psPortfoliosSubItemInfo> arSubItemList, DateTime sAsOf, Boolean stopOnAnyError)
        {
            return SyncSubItemsAsOf(sProSightId.ToString(CultureInfo.InvariantCulture), sSubItemType, lSubItemTypeId, arSubItemList, sAsOf, stopOnAnyError);
        }

        public List<psPortfoliosSubItemUpdateStatus> SyncSubItemsAsOf(String sId, String sSubItemType, Int32 lSubItemTypeId,
            List<psPortfoliosSubItemInfo> arSubItemList, DateTime sAsOf, Boolean stopOnAnyError)
        {
            return SyncSubItemsAsOf(String.Empty, sId, sSubItemType, lSubItemTypeId, arSubItemList, sAsOf, stopOnAnyError);
        }

        public List<psPortfoliosSubItemUpdateStatus> SyncSubItemsAsOf(String sCommonIdCategory, String sId, String sSubItemType, Int32 lSubItemTypeId,
            List<psPortfoliosSubItemInfo> arSubItemList, DateTime sAsOf, Boolean stopOnAnyError)
        {
            var asOfString = sAsOf.ToString("MM/dd/yyyy");
            if (asOfString.Equals(DateTime.Now.ToString("MM/dd/yyyy"))) asOfString = String.Empty;
            return SyncSubItemsAsOf(sCommonIdCategory, sId, sSubItemType, lSubItemTypeId, arSubItemList, asOfString, stopOnAnyError);
        }

        public List<psPortfoliosSubItemUpdateStatus> SyncSubItemsAsOf(String sCommonIdCategory, String sId, String sSubItemType, Int32 lSubItemTypeId,
    List<psPortfoliosSubItemInfo> arSubItemList, String sAsOf, Boolean stopOnAnyError)
        {
            var retVal = new List<psPortfoliosSubItemUpdateStatus>();
            psPortfoliosSubItemUpdateStatus[] subItems;
            try
            {
                subItems = PsSubItem.SyncSubItemsAsOf(sCommonIdCategory, sId, sSubItemType, lSubItemTypeId,
                                                        arSubItemList.ToArray(), sAsOf, stopOnAnyError);
                retVal = subItems.ToList();
            }
            catch (Exception ex)
            {
                if (PsLogger != null)
                {
                    PsLogger.Error(String.Format("Unexcpected SyncSubItemsAsOf Error: \n{0}\n", ex.Message));
                }
            }

            return retVal.ToList();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ClosedXML.Excel;
using NLog;
using OppmRemoveSubItem.Utility;


namespace OppmRemoveSubItem.Jobs
{
    public class RemoveSubItem
    {
        /// <summary>
        /// The ps logger
        /// </summary>
        public static Logger NLogger = LogManager.GetCurrentClassLogger();

        public X509Certificate Certificate { get; set; }

        public Boolean RunRemoveSubItem(Options options)
        {
            var retVal = false;
            const String serialCategory = "Sub_Item_Serial";
            const Boolean useSsl = true;

            try
            {

                var goodToGo = true;
                var xlsxDataTable = new DataTable();
                using (var wb = new XLWorkbook(options.XlsxDataFileName))
                {
                    NLogger.Info("Processing workbook {0}", options.XlsxDataFileName);

                    var wsData = wb.Worksheets.ToList().Find(w => w.Name.IsEqualTo(options.XlsxDataSheetName, true));
                    if (wsData == null)
                    {
                        NLogger.Error("Unable to find a Data worksheet named {0}", options.XlsxDataSheetName);
                        goodToGo = false;
                    }

                    if (goodToGo) xlsxDataTable = wsData.ToDataTable();
                }

                if (!goodToGo) return false;

                //
                // OK lets log into OPPM
                //
                var oppm = new Oppm(options.OppmUser, options.OppmPassword, options.OppmHost, useSsl);
                if (options.UseCert)
                {
                    oppm = new Oppm(options.OppmUser, options.OppmPassword, options.OppmHost, options.UseCert, Certificate);
                }
                var loggedin = oppm.Login();

                if (!loggedin)
                {
                    NLogger.Error("Unable to log into {0} with {1}", options.OppmHost, options.OppmUser);
                    return false;
                }
                NLogger.Info("Successful login with {0}", options.OppmUser);

                var dynamicValueList = oppm.SeValueList.GetValueList("Dynamic List Types");
                var valueList = dynamicValueList.Find(x => x.Text.IsEqualTo(options.DynamicList.Trim(), true));
                if (valueList == null)
                {
                    NLogger.Error("Unable to find {0}", Properties.Settings.Default.DynamicListName);
                    return false;
                }

                var j = 1;
                var categoryList = new List<String> { serialCategory };

                var subItemDictionary = new Dictionary<String, List<Int32>>();
                foreach (DataRow row in xlsxDataTable.Rows)
                {
                    var itemName = row.Field<String>(0);
                    var subItemId = row.Field<String>(1).ToInt();
                    if (!subItemId.HasValue) continue;
                    NLogger.Trace("{0}\tProcessing {1} subItem {2}",j++, itemName, subItemId);
                    if (!subItemDictionary.ContainsKey(itemName))
                    {
                        subItemDictionary.Add(itemName, new List<Int32>());
                    }
                    subItemDictionary[itemName].Add(subItemId.Value);
                }

                foreach (var keyItemName in subItemDictionary.Keys)
                {
                    var portfolioId = oppm.SePorfolio.GetPortfolioIdByName(keyItemName).ToInt();
                    if (!portfolioId.HasValue)
                    {
                        NLogger.Warn("Unable to find {0}", keyItemName);
                        continue;
                    }

                    var i = 1;
                    var subItemListAsOfToday = oppm.SeSubItem.GetSubItemListAsOfToday( portfolioId.Value, String.Empty, valueList.ID, categoryList, false);
                    var newSubItemList = new List<wsPortfoliosSubItem.psPortfoliosSubItemInfo>();
                    foreach (var psPortfoliosSubItemInfo in subItemListAsOfToday)
                    {
                        if (subItemDictionary[keyItemName].Contains(psPortfoliosSubItemInfo.SubItemProSightID )) continue;
                        psPortfoliosSubItemInfo.SubItemSerial = i;
                        foreach (var psPortfoliosCellInfo in psPortfoliosSubItemInfo.CategoryValues)
                        {
                            if (psPortfoliosCellInfo.CategoryName.IsNotEqualTo(serialCategory, true)) continue;
                            psPortfoliosCellInfo.CellValue = i.ToString();
                        }
                        newSubItemList.Add(psPortfoliosSubItemInfo);
                        i++;
                    }
                    var subItemUpdateStatuses = oppm.SeSubItem.SyncSubItemsAsOfToday(portfolioId.Value, valueList.ID, newSubItemList, false);
                    NLogger.Trace("Updated subItems on {0} with {1} errors", keyItemName,  subItemUpdateStatuses.Count);
                }

                /*
                foreach (DataRow row in xlsxDataTable.Rows)
                {
                    var itemName = row.Field<String>(0);
                    var subItemId = row.Field<String>(1).ToInt();
                    NLogger.Trace("{0}\tProcessing {1} subItem {2}",j++, itemName, subItemId);

                    var portfolioId = oppm.SePorfolio.GetPortfolioIdByName(itemName).ToInt();
                    if (!(portfolioId.HasValue && subItemId.HasValue))
                    {
                        NLogger.Warn("Unable to find {0}", itemName);
                        continue;
                    }

                    var i = 1;
                    var subItemListAsOfToday = oppm.SeSubItem.GetSubItemListAsOfToday( portfolioId.Value, String.Empty, valueList.ID, categoryList, false);
                    var newSubItemList = new List<wsPortfoliosSubItem.psPortfoliosSubItemInfo>();
                    foreach (var psPortfoliosSubItemInfo in subItemListAsOfToday)
                    {
                        if (psPortfoliosSubItemInfo.SubItemProSightID == subItemId.Value) continue;
                        psPortfoliosSubItemInfo.SubItemSerial = i;
                        foreach (var psPortfoliosCellInfo in psPortfoliosSubItemInfo.CategoryValues)
                        {
                            if (psPortfoliosCellInfo.CategoryName.IsNotEqualTo(serialCategory, true)) continue;
                            psPortfoliosCellInfo.CellValue = i.ToString();
                        }
                        newSubItemList.Add(psPortfoliosSubItemInfo);
                        i++;
                    }

                    var subItemUpdateStatuses = oppm.SeSubItem.SyncSubItemsAsOfToday(portfolioId.Value, valueList.ID, newSubItemList, false);
                    NLogger.Trace("Updated subItems on {0} with {1} errors", itemName,  subItemUpdateStatuses.Count);
                    
                }
                */
                retVal = true;
            }
            catch (Exception ex)
            {
                NLogger.Fatal(ex.Message);
            }
            return retVal;
        }
    }
}

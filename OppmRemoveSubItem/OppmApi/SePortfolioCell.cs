using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using NLog;
using OppmRemoveSubItem.Utility;
using wsPortfoliosCell;

namespace OppmRemoveSubItem.OppmApi
{
    public class SePortfolioCell
    {
        private const int ChunkSize = 250;

        public static Logger PsLogger = LogManager.GetCurrentClassLogger();
        public SePortfolioSecurity PsSecurityLogin { get; set; }
        public psPortfoliosCell PsCell { get; set; }

        public SePortfolioCell(SePortfolioSecurity securityLogin)
        {
            PsSecurityLogin = securityLogin;
            PsCell = new psPortfoliosCell { CookieContainer = PsSecurityLogin.CookieContainer };
            PsCell.Url = PsSecurityLogin.GetWebServicesUrLbyType(PsCell);

            if (!securityLogin.PortfoliosSecurityWs.ClientCertificates.HasItems()) return;
            foreach (var clientCertificate in securityLogin.PortfoliosSecurityWs.ClientCertificates)
            {
                PsCell.ClientCertificates.Add(clientCertificate);
            }
        }

        public SePortfolioCell(SePortfolioSecurity securityLogin, X509Certificate certificate)
            : this(securityLogin)
        {
            PsCell.ClientCertificates.Add(certificate);
        }

        public String GetCellValue(int proSightId, String categoryName)
        {
            return GetCellValue("", proSightId.ToString(CultureInfo.InvariantCulture), categoryName);
        }

        public String GetCellValue(String proSightId, String categoryName)
        {
            return GetCellValue("", proSightId, categoryName);
        }

        public String GetCellValue(String uci, String id, String categoryName)
        {
            string retVal;
            try
            {
                retVal = PsCell.getCellValue(uci, id, categoryName);
            }
            catch (Exception ex)
            {
                PsLogger.Error(String.Format("Unexcpected GetCellValue Error: \n{0}\n", ex.Message));
                throw new Exception(ex.Message, ex.InnerException);
            }
            return retVal;
        }

        public void UpdateCellValue(string proSightId, string cellName, string cellValue, int lUpdateFlags)
        {
            UpdateCellValue("", proSightId, cellName, cellValue, lUpdateFlags);
        }


        public void UpdateCellValue(int proSightId, string cellName, string cellValue, int lUpdateFlags)
        {
            UpdateCellValue("", proSightId.ToString(CultureInfo.InvariantCulture), cellName, cellValue, lUpdateFlags);
        }

        public void UpdateCellValue(String uci, String id, string cellName, string cellValue, int lUpdateFlags)
        {
            var cellInfo = new psPortfoliosCellInfo();
            cellInfo.CategoryName = cellName;
            cellInfo.CellDisplayValue = cellValue;
            cellInfo.CellAsOf = String.Empty;
            UpdateCellValue(uci, id, cellInfo, lUpdateFlags);
        }

        public void UpdateCellValue(int proSightId, psPortfoliosCellInfo cellInfo, int lUpdateFlags)
        {
            UpdateCellValue("", proSightId.ToString(CultureInfo.InvariantCulture), cellInfo, lUpdateFlags);
        }

        public void UpdateCellValue(String proSightId, psPortfoliosCellInfo cellInfo, int lUpdateFlags)
        {
            UpdateCellValue("", proSightId, cellInfo, lUpdateFlags);
        }

        public void UpdateCellValue(String uci, String id, psPortfoliosCellInfo cellInfo, int lUpdateFlags)
        {
            try
            {
                PsCell.Update(uci, id, cellInfo.CategoryName, cellInfo.CellDisplayValue,
                              cellInfo.CellIndicator, cellInfo.CellAnnotation, cellInfo.CellAsOf, lUpdateFlags);
            }
            catch (Exception ex)
            {
                PsLogger.Error(String.Format("Unexcpected UpdateCellValue Error: \n{0}\n", ex.Message));
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public List<psPortfoliosCellInfo> GetMultipleCellValues(int proSightId, List<psPortfoliosCellInfo> cellInfoList)
        {
            return GetMultipleCellValues("", proSightId.ToString(CultureInfo.InvariantCulture), cellInfoList);
        }

        public List<psPortfoliosCellInfo> GetMultipleCellValues(String proSightId, List<psPortfoliosCellInfo> cellInfoList)
        {
            return GetMultipleCellValues("", proSightId, cellInfoList);
        }

        public List<psPortfoliosCellInfo> GetMultipleCellValues(String uci, String id, List<psPortfoliosCellInfo> cellInfoList)
        {
            var categoryNameList = new List<String>();
            cellInfoList.ForEach(cellInfo => categoryNameList.Add(cellInfo.CategoryName));
            return GetMultipleCellValues(uci, id, categoryNameList);
        }

        public List<psPortfoliosCellInfo> GetMultipleCellValues(int proSightId, List<String> categoryNameList)
        {
            return GetMultipleCellValues("", proSightId.ToString(CultureInfo.InvariantCulture), categoryNameList);
        }

        public List<psPortfoliosCellInfo> GetMultipleCellValues(String proSightId, List<String> categoryNameList)
        {
            return GetMultipleCellValues("", proSightId, categoryNameList);
        }

        public List<psPortfoliosCellInfo> GetMultipleCellValues(String uci, String id, List<String> categoryNameList)
        {
            var multipleCellValues = new List<psPortfoliosCellInfo>();
            try
            {
                var retVal = PsCell.GetMultipleCellValues(uci, id, categoryNameList.ToArray());
                Array.ForEach(retVal, multipleCellValues.Add);
            }
            catch (Exception ex)
            {
                PsLogger.Error(String.Format("Unexpected GetMultipleCellValues Error: \n{0}\n", ex.Message));
                throw new Exception(ex.Message, ex.InnerException);
            }
            return multipleCellValues;
        }

        public List<psPortfoliosCellInfo> GetChildrenMultipleCellValues(int proSightId, List<psPortfoliosCellInfo> cellInfoList)
        {
            return GetChildrenMultipleCellValues("", proSightId.ToString(CultureInfo.InvariantCulture), cellInfoList);
        }

        public List<psPortfoliosCellInfo> GetChildrenMultipleCellValues(String proSightId, List<psPortfoliosCellInfo> cellInfoList)
        {
            return GetChildrenMultipleCellValues("", proSightId, cellInfoList);
        }

        public List<psPortfoliosCellInfo> GetChildrenMultipleCellValues(String uci, String id, List<psPortfoliosCellInfo> cellInfoList)
        {
            var categoryNameList = new List<String>();
            cellInfoList.ForEach(cellInfo => categoryNameList.Add(cellInfo.CategoryName));
            return GetChildrenMultipleCellValues(uci, id, categoryNameList);
        }

        public List<psPortfoliosCellInfo> GetChildrenMultipleCellValues(int proSightId, List<String> categoryNameList)
        {
            return GetChildrenMultipleCellValues("", proSightId.ToString(CultureInfo.InvariantCulture), categoryNameList);
        }

        public List<psPortfoliosCellInfo> GetChildrenMultipleCellValues(String proSightId, List<String> categoryNameList)
        {
            return GetChildrenMultipleCellValues("", proSightId, categoryNameList);
        }

        public List<psPortfoliosCellInfo> GetChildrenMultipleCellValues(String uci, String id, List<String> categoryNameList)
        {
            var multipleCellValues = new List<psPortfoliosCellInfo>();
            try
            {
                var index = 0;
                while (index < categoryNameList.Count)
                {
                    var count = categoryNameList.Count - index > ChunkSize ? ChunkSize : categoryNameList.Count - index;
                    var retVal = PsCell.GetChildrenMultipleCellValues(uci, id, categoryNameList.GetRange(index, count).ToArray());
                    Array.ForEach(retVal, multipleCellValues.Add);
                    index += ChunkSize;
                }
            }
            catch (Exception ex)
            {
                PsLogger.Error(String.Format("Unexpected GetMultipleCellValues Error: \n{0}\n", ex.Message));
                throw new Exception(ex.Message, ex.InnerException);
            }
            return multipleCellValues;
        }

        public bool UpdateOnlyModifiedCells(int proSightId, List<psPortfoliosCellInfo> cellsToUpdate)
        {
            return UpdateOnlyModifiedCells("", proSightId.ToString(CultureInfo.InvariantCulture), cellsToUpdate);
        }

        public bool UpdateOnlyModifiedCells(String proSightId, List<psPortfoliosCellInfo> cellsToUpdate)
        {
            return UpdateOnlyModifiedCells("", proSightId, cellsToUpdate);
        }

        public bool UpdateOnlyModifiedCells(String uci, String id, List<psPortfoliosCellInfo> cellsToUpdate)
        {
            try
            {
                PsCell.UpdateOnlyModifiedCells(uci, id, cellsToUpdate.ToArray(), "");
            }
            catch (Exception ex)
            {
                PsLogger.Error(String.Format("Unexcpected UpdateOnlyModifiedCells Error: \n{0}\n", ex));
                throw new Exception(ex.Message, ex.InnerException);
            }
            return true;
        }

        public bool UpdateMultipleCells(int proSightId, List<psPortfoliosCellInfo> cellsToUpdate)
        {
            return UpdateMultipleCells("", proSightId.ToString(CultureInfo.InvariantCulture), cellsToUpdate);
        }

        public bool UpdateMultipleCells(String proSightId, List<psPortfoliosCellInfo> cellsToUpdate)
        {
            return UpdateMultipleCells("", proSightId, cellsToUpdate);
        }

        public bool UpdateMultipleCells(String uci, String id, List<psPortfoliosCellInfo> cellsToUpdate)
        {
            try
            {
                var index = 0;
                while (index < cellsToUpdate.Count)
                {
                    var count = cellsToUpdate.Count - index > ChunkSize ? ChunkSize : cellsToUpdate.Count - index;
                    var cells = cellsToUpdate.GetRange(index, count);
                    PsCell.UpdateMultipleCells(uci, id, cells.ToArray(), "");
                    index += ChunkSize;
                }
            }
            catch (Exception ex)
            {
                PsLogger.Error(String.Format("Unexcpected UpdateMultipleCells Error: \n{0}\n", ex));
                throw new Exception(ex.Message, ex.InnerException);
            }
            return true;
        }

        public List<psPortfoliosCellUpdateStatus> UpdateMultipleCellsEx(int proSightId, List<psPortfoliosCellInfo> cellsToUpdate)
        {
            return UpdateMultipleCellsEx("", proSightId.ToString(CultureInfo.InvariantCulture), cellsToUpdate, String.Empty, true, true, false);
        }

        public List<psPortfoliosCellUpdateStatus> UpdateMultipleCellsEx(String proSightId, List<psPortfoliosCellInfo> cellsToUpdate)
        {
            return UpdateMultipleCellsEx("", proSightId, cellsToUpdate, String.Empty, true, true, false);
        }

        
        public List<psPortfoliosCellUpdateStatus> UpdateMultipleCellsEx(String uci, String id, List<psPortfoliosCellInfo> cellsToUpdate, String asOf,
            Boolean modifyOnly, Boolean stopOnSecurity, Boolean stopOnAnyError)
        {
            var retVal = new List<psPortfoliosCellUpdateStatus>();
            try
            {
                var index = 0;
                while (index < cellsToUpdate.Count)
                {
                    var count = cellsToUpdate.Count - index > ChunkSize ? ChunkSize : cellsToUpdate.Count - index;
                    var cells = cellsToUpdate.GetRange(index, count);
                    var cellUpdateStatus = PsCell.UpdateMultipleCellsEx(uci, id, cells.ToArray(), asOf, modifyOnly, stopOnSecurity, stopOnAnyError);
                    retVal.AddRange(cellUpdateStatus);
                    index += ChunkSize;
                }
            }
            catch (Exception ex)
            {
                retVal = null;
                PsLogger.Error("Unexcpected UpdateMultipleCells Error: \n{0}\n", ex.Message);
                if (stopOnAnyError || stopOnSecurity)
                    throw new Exception(ex.Message, ex.InnerException);
            }
            return retVal;
        } 
    }
}
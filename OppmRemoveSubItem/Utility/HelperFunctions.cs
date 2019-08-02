// ***********************************************************************
// Assembly         : OppmUtility
// Author           : ggreiff
// Created          : 08-27-2014
//
// Last Modified By : ggreiff
// Last Modified On : 09-01-2014
// ***********************************************************************
// <copyright file="HelperFunctions.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using NLog;
using wsPortfoliosCategory;
using itemCell = wsPortfoliosCell;
using subItemCell = wsPortfoliosSubItem;


namespace OppmRemoveSubItem.Utility
{
    /// <summary>
    /// Class HelperFunctions.
    /// </summary>
    public static class HelperFunctions
    {
        /// <summary>
        /// The n logger
        /// </summary>
        public static Logger NLogger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Builds the cell infos.
        /// </summary>
        /// <param name="categoryData">The category data.</param>
        /// <returns>CategoryMapList&lt;psPortfoliosCellInfo&gt;.</returns>
        public static List<itemCell.psPortfoliosCellInfo> BuildItemCellInfos(Dictionary<String, String> categoryData)
        {
            return categoryData.Keys.Select(key => new itemCell.psPortfoliosCellInfo { CategoryName = key, CellDisplayValue = categoryData[key] }).ToList();
        }

        /// <summary>
        /// Builds the cell information.
        /// </summary>
        /// <param name="categoryInfo">The category information.</param>
        /// <param name="categoryValue">The category value.</param>
        /// <returns>psPortfoliosCellInfo.</returns>
        public static itemCell.psPortfoliosCellInfo BuildItemCellInfo(psPortfoliosCategoryInfo categoryInfo, String categoryValue)
        {
            if (categoryValue.IsNullOrEmpty()) categoryValue = String.Empty;
            if (categoryInfo.ValueType == psCATEGORY_VALUE_TYPE.CVTYP_DATETIME)
            {
                var dateTimeValue = categoryValue.ToDate();
                if (dateTimeValue.HasValue)
                    categoryValue = dateTimeValue.Value.ToShortDateString();
            }

            if (categoryInfo.ValueType == psCATEGORY_VALUE_TYPE.CVTYP_FLOAT)
            {
                var doubleValue = categoryValue.ToDouble();
                if (doubleValue.HasValue)
                    categoryValue = doubleValue.Value.ToString(CultureInfo.InvariantCulture);
            }

            if (categoryInfo.ValueType == psCATEGORY_VALUE_TYPE.CVTYP_INT)
            {
                var intValue = categoryValue.ToInt();
                if (intValue.HasValue)
                    categoryValue = intValue.Value.ToString(CultureInfo.InvariantCulture);
            }

            return BuildItemCellInfo(categoryInfo.Name, categoryValue);
        }

        /// <summary>
        /// Builds the cell information.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="categoryValue">The category value.</param>
        /// <returns>psPortfoliosCellInfo.</returns>
        public static itemCell.psPortfoliosCellInfo BuildItemCellInfo(String categoryName, String categoryValue)
        {
            return BuildItemCellInfo(categoryName, categoryValue, String.Empty);
        }

        /// <summary>
        /// Builds the cell information.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="categoryValue">The category value.</param>
        /// <param name="cellAsOf">The cell as of.</param>
        /// <returns>psPortfoliosCellInfo.</returns>
        public static itemCell.psPortfoliosCellInfo BuildItemCellInfo(String categoryName, String categoryValue, String cellAsOf)
        {
            var retVal = new itemCell.psPortfoliosCellInfo
            {
                CategoryName = categoryName,
                CellDisplayValue = categoryValue,
                CellAsOf = cellAsOf
            };
            return retVal;
        }

        /// <summary>
        /// Builds the sub item cell infos.
        /// </summary>
        /// <param name="categoryData">The category data.</param>
        /// <returns>List&lt;subItemCell.psPortfoliosCellInfo&gt;.</returns>
        public static List<subItemCell.psPortfoliosCellInfo> BuildSubItemCellInfos(Dictionary<String, String> categoryData)
        {
            return categoryData.Keys.Select(key => new subItemCell.psPortfoliosCellInfo { CategoryName = key, CellDisplayValue = categoryData[key] }).ToList();
        }

        /// <summary>
        /// Builds the cell information.
        /// </summary>
        /// <param name="categoryInfo">The category information.</param>
        /// <param name="categoryValue">The category value.</param>
        /// <returns>psPortfoliosCellInfo.</returns>
        public static subItemCell.psPortfoliosCellInfo BuildSubItemCellInfo(psPortfoliosCategoryInfo categoryInfo, String categoryValue)
        {
            if (categoryValue.IsNullOrEmpty()) categoryValue = String.Empty;
            if (categoryInfo.ValueType == psCATEGORY_VALUE_TYPE.CVTYP_DATETIME)
            {
                var dateTimeValue = categoryValue.ToDate();
                if (dateTimeValue.HasValue)
                    categoryValue = dateTimeValue.Value.ToShortDateString();
            }

            if (categoryInfo.ValueType == psCATEGORY_VALUE_TYPE.CVTYP_FLOAT)
            {
                var doubleValue = categoryValue.ToDouble();
                if (doubleValue.HasValue)
                    categoryValue = doubleValue.Value.ToString(CultureInfo.InvariantCulture);
            }

            if (categoryInfo.ValueType == psCATEGORY_VALUE_TYPE.CVTYP_INT)
            {
                var intValue = categoryValue.ToInt();
                if (intValue.HasValue)
                    categoryValue = intValue.Value.ToString(CultureInfo.InvariantCulture);
            }

            return BuildSubItemCellInfo(categoryInfo.Name, categoryValue);
        }

        /// <summary>
        /// Builds the cell information.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="categoryValue">The category value.</param>
        /// <returns>psPortfoliosCellInfo.</returns>
        public static subItemCell.psPortfoliosCellInfo BuildSubItemCellInfo(String categoryName, String categoryValue)
        {

            return BuildSubItemCellInfo(categoryName, categoryValue, DateTime.Now.ToString("MM/dd/yyyy"));
        }

        /// <summary>
        /// Builds the cell information.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="categoryValue">The category value.</param>
        /// <param name="cellAsOf">The cell as of.</param>
        /// <returns>psPortfoliosCellInfo.</returns>
        public static subItemCell.psPortfoliosCellInfo BuildSubItemCellInfo(String categoryName, String categoryValue, String cellAsOf)
        {
            var retVal = new subItemCell.psPortfoliosCellInfo
            {
                CategoryName = categoryName,
                CellDisplayValue = categoryValue,
                CellAsOf = cellAsOf
            };
            return retVal;
        }

        /// <summary>
        /// Gets the dod cert.
        /// </summary>
        /// <returns>X509Certificate.</returns>
        public static X509Certificate GetDodCert()
        {
            var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            var certificates = store.Certificates;
            var certList = new List<X509Certificate2>();
            foreach (X509Certificate2 x509 in certificates)
            {
                NLogger.Trace("Certificate Issuer: {0}", x509.Issuer);

                //if (!x509.Issuer.Contains("DOD CA") || x509.Issuer.Contains("DOD CA EMAIL")) continue;
                if (!x509.Issuer.Contains("DOD CA")) continue;
                if (x509.NotAfter < DateTime.Now) continue;
                if (x509.NotBefore > DateTime.Now) continue;
                certList.Add(x509);
            }

            var latesNotAfter = certList.Max(y => y.NotAfter);
            var latestX509 = certList.First(x => x.NotAfter == latesNotAfter);
            return latestX509;
        }
    }
}

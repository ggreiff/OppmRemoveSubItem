// ***********************************************************************
// Assembly         : OppmUtility
// Author           : ggreiff
// Created          : 09-01-2014
//
// Last Modified By : ggreiff
// Last Modified On : 09-01-2014
// ***********************************************************************
// <copyright file="ClosedXmlExtensions.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Data;
using ClosedXML.Excel;
using NLog;
using DataTable = System.Data.DataTable;

namespace OppmRemoveSubItem.Utility
{
    /// <summary>
    /// Class ClosedXmlExtensions.
    /// </summary>
    public static class ClosedXmlExtensions
    {
        /// <summary>
        /// The n logger
        /// </summary>
        public static Logger NLogger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// To the data table.
        /// </summary>
        /// <param name="xlWorksheet">The xl worksheet.</param>
        /// <returns>DataTable.</returns>
        public static DataTable ToDataTable(this IXLWorksheet xlWorksheet)
        {
            
            var datatable = new DataTable();
            var errorFound = false;
            try
            {
                var range = xlWorksheet.Range(xlWorksheet.FirstCellUsed(), xlWorksheet.LastCellUsed()).RangeUsed();

                var columnCount = range.ColumnCount();
                var rowCount = range.RowCount();
                datatable.Clear();

                //
                // Create our columns bases on the first row which contains the itemName and category names
                //
                try
                {
                    if (rowCount < 1) return new DataTable();
                    var goodColumnCount = 0;
                    for (var i = 1; i <= columnCount; i++)
                    {
                        if (xlWorksheet.Cell(1, i).Value.ToString().GetClsString().IsNullOrEmpty()) break;
                        goodColumnCount++;

                        var columnName = xlWorksheet.Cell(1, i).Value.ToString().GetClsString();
                        for (var j = 0; j < 10; j++)
                        {
                            if (!datatable.Columns.Contains(columnName)) break;
                            columnName = $"{columnName}_{j}";
                        }

                        datatable.Columns.Add(new DataColumn
                        {
                            DataType = Type.GetType("System.String"),
                            ColumnName = columnName,
                            Caption = xlWorksheet.Cell(1, i).Value.ToString()
                        }
                            );
                    }
                    columnCount = goodColumnCount;
                }
                catch (Exception ex)
                {
                    NLogger.Warn("Unable to create the data table columns. {0}",ex.Message);
                    return new DataTable();
                }

                //
                // Copy cell data into our datatable.
                //         
                if (rowCount < 2) return datatable;
                for (var i = 2; i <= rowCount; i++)
                {
                   if (range.Row(i).Cell(1).Value.ToString().IsNullOrEmpty()) continue;
                   var array = new object[columnCount];
                   for (var y = 1; y <= columnCount; y++)
                    {
                        try
                        {
                            if (range.Row(i).Cell(y).HasFormula)
                            {
                                NLogger.Warn("Unable to process cells which contain a forumlas (to remove formulas: copy -> paste special values). Check on Sheet {0} row {1} column {2}", xlWorksheet.Name, i, y);
                                errorFound = true;
                                continue;
                            }
                            array[y - 1] = range.Row(i).Cell(y).Value.ToString();
                            if (y == 1)
                            {
                               //if (array[y-1].ToString().IsEqualTo("Spurlock - SP03 Bed Ash Silo Chute", true)) System.Diagnostics.Debugger.Break();
                                NLogger.Trace(array[y-1]);
                            }
                        }
                        catch (Exception ex)
                        {
                            errorFound = true;
                            NLogger.Warn("Error on Sheet {0} row {1} column {2}", xlWorksheet.Name, i, y);
                            NLogger.Trace(ex.Message);
                        }
                    }
                    if (ArrayHasValue(array)) datatable.Rows.Add(array);
                }
                if (errorFound) throw new Exception("There are value errors in the spreadsheet.");
            }
            catch (Exception ex)
            {
                NLogger.Warn(ex.Message);
                datatable = new DataTable();
            }
            return datatable;
        }

        private static Boolean ArrayHasValue(Object[] arrayObjects)
        {
            foreach (var t in arrayObjects)
            {
                if (t != null) return true;
            }
            return false;
        }
    }
}

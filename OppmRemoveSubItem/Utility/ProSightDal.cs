using System;
using System.Collections.Generic;
using System.Data;
using NLog;
using OppmRemoveSubItem.Properties;
using Oracle.ManagedDataAccess.Client;

namespace OppmRemoveSubItem.Utility
{
    public class ProSightDal
    {
        public String ConnectString { get; set; }

        /// <summary>
        ///  Property for we have a valid connection string.
        /// </summary>
        public Boolean ValidConnectionString;

        /// <summary>
        /// The NLOG Logger for this class.
        /// </summary>
        public static Logger NLogger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="ProSightDal"/> class.
        /// </summary>
        public ProSightDal()
        {
            ConnectString = Settings.Default.OppmConnectionString;
        }

        /// <summary>
        /// Gets the data table.
        /// </summary>
        /// <param name="commandString">The command string.</param>
        /// <param name="parameterList">The parameter list.</param>
        /// <returns></returns>
        public DataTable GetDataTable(String commandString, List<OracleParameter> parameterList)
        {
            var retVal = new DataTable();
            try
            {
                using (var oracleConnection = new OracleConnection(ConnectString))
                {
                    using (var oracleCommand = new OracleCommand(commandString, oracleConnection))
                    {
                        //
                        // Set up the command object
                        //
                        oracleCommand.CommandType = CommandType.Text;
                        oracleCommand.Parameters.Clear();
                        if (parameterList.Count > 0) oracleCommand.Parameters.AddRange(parameterList.ToArray());

                        //
                        // Open the connection and pull the data
                        //
                        oracleConnection.Open();
                        //NLogger.Trace("\n{0}\n",CommandStringWithParamaters(oracleCommand));
                        var dataReader = oracleCommand.ExecuteReader();
                        retVal.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                retVal = new DataTable();
                NLogger.Error(ex.ToString()); // Something when wrong -- log it for some who cares.
            }
            return retVal;
        }

        /// <summary>
        /// Gets the data table.
        /// </summary>
        /// <param name="commandString">The command string.</param>
        /// <returns></returns>
        public DataTable GetDataTable(String commandString)
        {
            return GetDataTable(commandString, new List<OracleParameter>());
        }

        /// <summary>
        /// Gets the scalar.
        /// </summary>
        /// <param name="commandString">The command string.</param>
        /// <returns>Object.</returns>
        public Object GetScalar(String commandString)
        {
            return GetScalar(commandString, new List<OracleParameter>());
        }

        /// <summary>
        /// Gets the scalar.
        /// </summary>
        /// <param name="commandString">The command string.</param>
        /// <param name="parameterList">The parameter list.</param>
        /// <returns></returns>
        public Object GetScalar(String commandString, List<OracleParameter> parameterList)
        {
            Object retVal;
            try
            {
                using (var oracleConnection = new OracleConnection(ConnectString))
                {
                    using (var oracleCommand = new OracleCommand(commandString, oracleConnection))
                    {
                        //
                        // Set up the command object
                        //
                        oracleCommand.CommandType = CommandType.Text;
                        oracleCommand.Parameters.Clear();
                        if (parameterList.Count > 0) oracleCommand.Parameters.AddRange(parameterList.ToArray());

                        //
                        // Open the connection and pull the data
                        //
                        oracleConnection.Open();
                        //NLogger.Trace("\n{0}\n", CommandStringWithParamaters(oracleCommand));
                        retVal = oracleCommand.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                retVal = new Object();
                NLogger.Error(ex.ToString()); // Something when wrong -- log it for some who cares.
            }
            return retVal;
        }

        /// <summary>
        /// Gets the custom record set.
        /// </summary>
        /// <param name="sqlText">The SQL text.</param>
        /// <returns>DataTable.</returns>
        public DataTable GetCustomRecordSet(String sqlText)
        {
            return GetCustomRecordSet(-1, sqlText);
        }

        /// <summary>
        /// Gets the custom record set.
        /// </summary>
        /// <param name="portfolioId">The portfolio id.</param>
        /// <param name="sqlText">The SQL text.</param>
        /// <returns></returns>
        public DataTable GetCustomRecordSet(Int32 portfolioId, String sqlText)
        {
            DataTable retVal;
            var parameterList = new List<OracleParameter>();
            try
            {
                if (sqlText.Contains("@PortfolioID")) parameterList.Add(new OracleParameter("@PortfolioID", SqlDbType.Int) { Value = portfolioId });
                retVal = GetDataTable(sqlText, parameterList);
            }
            catch (Exception ex)
            {
                NLogger.Error(ex.ToString()); // Something when wrong -- log it for some who cares.
                retVal = new DataTable();
            }
            return retVal;
        }


        /// <summary>
        /// Gets the list of tables.
        /// </summary>
        /// <returns></returns>
        public Dictionary<Int32, Int32> GetPortfolioIdFromSubItemId(Int32 subItemId)
        {
            var retVal = new Dictionary<Int32, Int32>();
            var oracleParameters = new List<OracleParameter>
            {
                new OracleParameter("SubItemId", OracleDbType.Int32) {Value = subItemId},

            };
            
            var dataTable = GetDataTable(Resources.GetItemIdBySubItemId, oracleParameters);
            if (!dataTable.Rows.HasItems()) return retVal;
            foreach (DataRow dataTableRow in dataTable.Rows)
            {
                retVal.Add(dataTableRow.Field<Int32>("SUB_PORTFOLIO_ID"), dataTableRow.Field<Int32>("PORTFOLIO_ID"));
            }

            return retVal;

        }


        /// <summary>
        /// Commands the string with paramaters.
        /// </summary>
        /// <param name="oracleCommand">The SQL command.</param>
        /// <returns>String.</returns>
        public static String CommandStringWithParamaters(OracleCommand oracleCommand)
        {
            var commandText = oracleCommand.CommandText;
            foreach (OracleParameter oracleParameter in oracleCommand.Parameters)
            {
                switch (oracleParameter.OracleDbType)
                {
                    case  OracleDbType.Int32:
                        commandText = commandText.Replace(oracleParameter.ParameterName, $"{oracleParameter.Value}");
                        break;
                    case OracleDbType.Varchar2:
                        commandText = commandText.Replace(oracleParameter.ParameterName, $"'{oracleParameter.Value}'");
                        break;
                    default:
                        commandText = commandText.Replace(oracleParameter.ParameterName, $"'{oracleParameter.Value}'");
                        break;
                }
            }
            return commandText.Replace(":",String.Empty);
        }

        /// <summary>
        /// Performs a left outer joins the specified datatables.
        /// </summary>
        /// <param name="firstDataTable">The name of the first datatable.</param>
        /// <param name="secondDataTable">The name of the second datatable.</param>
        /// <param name="fjc">The first join data column.</param>
        /// <param name="sjc">The second join data column.</param>
        /// <returns>
        /// A datatable that is the results of the left outer join on the two inputted datatables 
        /// </returns>
        public static DataTable LeftOuterJoin(DataTable firstDataTable, DataTable secondDataTable, DataColumn[] fjc, DataColumn[] sjc)
        {
            //Create Empty Table 
            var table = new DataTable("Join");

            // Use a DataSet to leverage DataRelation 
            using (var ds = new DataSet())
            {
                //Add Copy of Tables 
                ds.Tables.AddRange(new[] { firstDataTable.Copy(), secondDataTable.Copy() });

                //Identify Joining Columns from First 
                var parentcolumns = new DataColumn[fjc.Length];

                for (var i = 0; i < parentcolumns.Length; i++)
                {
                    parentcolumns[i] = ds.Tables[0].Columns[fjc[i].ColumnName];
                }

                //Identify Joining Columns from Second 
                var childcolumns = new DataColumn[sjc.Length];

                for (var i = 0; i < childcolumns.Length; i++)
                {
                    childcolumns[i] = ds.Tables[1].Columns[sjc[i].ColumnName];
                }

                //Create DataRelation 
                var r = new DataRelation(string.Empty, parentcolumns, childcolumns, false);
                ds.Relations.Add(r);

                //Create Columns for JOIN table 
                for (var i = 0; i < firstDataTable.Columns.Count; i++)
                {
                    table.Columns.Add(firstDataTable.Columns[i].ColumnName, firstDataTable.Columns[i].DataType);
                }

                for (var i = 0; i < secondDataTable.Columns.Count; i++)
                {
                    //Beware Duplicates 
                    if (!table.Columns.Contains(secondDataTable.Columns[i].ColumnName))
                        table.Columns.Add(secondDataTable.Columns[i].ColumnName, secondDataTable.Columns[i].DataType);
                    else
                        table.Columns.Add(secondDataTable.Columns[i].ColumnName + "1", secondDataTable.Columns[i].DataType);
                }


                //Loop through First table 
                table.BeginLoadData();

                foreach (DataRow firstrow in ds.Tables[0].Rows)
                {
                    //Get "joined" rows 
                    var childrows = firstrow.GetChildRows(r);
                    if (childrows.Length > 0)
                    {
                        var parentarray = firstrow.ItemArray;
                        foreach (var secondrow in childrows)
                        {
                            var secondarray = secondrow.ItemArray;
                            var joinarray = new object[parentarray.Length + secondarray.Length];
                            Array.Copy(parentarray, 0, joinarray, 0, parentarray.Length);
                            Array.Copy(secondarray, 0, joinarray, parentarray.Length, secondarray.Length);
                            table.LoadDataRow(joinarray, true);
                        }
                    }
                    else
                    {
                        var parentarray = firstrow.ItemArray;
                        var joinarray = new object[parentarray.Length];
                        Array.Copy(parentarray, 0, joinarray, 0, parentarray.Length);
                        table.LoadDataRow(joinarray, true);
                    }
                }
                table.EndLoadData();
            }

            return table;
        }

        /// <summary>
        /// Performs a left outer joins the specified datatables.
        /// </summary>
        /// <param name="firstDataTable">The name of the first datatable.</param>
        /// <param name="secondDataTable">The name of the second datatable.</param>
        /// <param name="fjc">The first join data column.</param>
        /// <param name="sjc">The second join data column.</param>
        /// <returns>
        /// A datatable that is the results of the left outer join on the two inputted datatables 
        /// </returns>
        public static DataTable LeftOuterJoin(DataTable firstDataTable, DataTable secondDataTable, DataColumn fjc, DataColumn sjc)
        {
            return LeftOuterJoin(firstDataTable, secondDataTable, new[] { fjc }, new[] { sjc });
        }

        /// <summary>
        /// Performs a left outer joins the specified datatables.
        /// </summary>
        /// <param name="firstDataTable">The name of the first datatable.</param>
        /// <param name="secondDataTable">The name of the second datatable.</param>
        /// <param name="fjc">The first join data column.</param>
        /// <param name="sjc">The second join data column.</param>
        /// <returns>
        /// A datatable that is the results of the left outer join on the two inputted datatables 
        /// </returns>
        public static DataTable LeftOuterJoin(DataTable firstDataTable, DataTable secondDataTable, String fjc, String sjc)
        {
            return LeftOuterJoin(firstDataTable, secondDataTable, new[] { firstDataTable.Columns[fjc] }, new[] { secondDataTable.Columns[sjc] });
        }
    }
}
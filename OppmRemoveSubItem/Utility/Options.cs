// ***********************************************************************
// Assembly         : OppmUtility
// Author           : ggreiff
// Created          : 09-01-2014
//
// Last Modified By : ggreiff
// Last Modified On : 09-01-2014
// ***********************************************************************
// <copyright file="Options.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using CommandLine;
using CommandLine.Text;

namespace OppmRemoveSubItem.Utility
{
    /// <summary>
    /// Class Options.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Gets or sets the name of the XLXS Data file.
        /// </summary>
        /// <value>The name of the XLXS file.</value>
        [Option('x', "xlxsdatafilename", Required = false, HelpText = "The Excel XLXS data filename to use for importing data.")]
        public string XlsxDataFileName { get; set; }

        /// <summary>
        /// Gets or sets the name of the XLXS sheet.
        /// </summary>
        /// <value>The name of the XLXS sheet.</value>
        [Option('d', "dataSheetName", 
            HelpText = "The import worksheet name that contains the data to import. This will default to a sheet named Data if not specified.", DefaultValue = "Data")]
        public string XlsxDataSheetName { get; set; }

        /// <summary>
        /// Gets or sets the opp server.
        /// </summary>
        /// <value>The opp server.</value>
        [Option('h', "oppmHost", HelpText = "The Oppm host name.")]
        public string OppmHost { get; set; }

        /// <summary>
        /// Gets or sets the name of the oppm user.
        /// </summary>
        /// <value>The name of the oppm user.</value>
        [Option('u', "oppmUser", HelpText = "The Oppm username.")]
        public string OppmUser { get; set; }

        /// <summary>
        /// Gets or sets the oppm password.
        /// </summary>
        /// <value>The oppm password.</value>
        [Option('p', "oppmPassword", HelpText = "The Oppm username's password.")]
        public string OppmPassword { get; set; }

        /// <summary>
        /// Gets or sets the Dynamic List name.
        /// </summary>
        /// <value>The Dynamic List name.</value>
        [Option('l', "DynamicList", HelpText = "The dynamic list name.")]
        public string DynamicList { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use SSL].
        /// </summary>
        /// <value><c>true</c> if [use SSL]; otherwise, <c>false</c>.</value>
        [Option('o', "useCert", HelpText = "Use a client certficate for the web services", DefaultValue = false)]
        public bool UseCert { get; set; }

        /// <summary>
        /// Gets the usage.
        /// </summary>
        /// <returns>System.String.</returns>
        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}

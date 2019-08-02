// ***********************************************************************
// Assembly         : OppmUtility
// Author           : ggreiff
// Created          : 08-27-2014
//
// Last Modified By : ggreiff
// Last Modified On : 09-01-2014
// ***********************************************************************
// <copyright file="OPPM.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Security.Cryptography.X509Certificates;
using NLog;
using SePortfolioCategory = OppmRemoveSubItem.OppmApi.SePortfolioCategory;
using SePortfolioCell = OppmRemoveSubItem.OppmApi.SePortfolioCell;
using SePortfolioItem = OppmRemoveSubItem.OppmApi.SePortfolioItem;
using SePortfolioSecurity = OppmRemoveSubItem.OppmApi.SePortfolioSecurity;
using SePortfoliosPortfolio = OppmRemoveSubItem.OppmApi.SePortfoliosPortfolio;
using SePortfolioSubItem = OppmRemoveSubItem.OppmApi.SePortfolioSubItem;
using SePortfoliosValueList = OppmRemoveSubItem.OppmApi.SePortfoliosValueList;

namespace OppmRemoveSubItem.Utility
{
    /// <summary>
    /// Class Oppm.
    /// </summary>
    public class Oppm
    {
        /// <summary>
        /// Gets or sets the se security.
        /// </summary>
        /// <value>The se security.</value>
        public SePortfolioSecurity SeSecurity { get; set; }
        /// <summary>
        /// Gets or sets the se porfolio.
        /// </summary>
        /// <value>The se porfolio.</value>
        public SePortfoliosPortfolio SePorfolio { get; set; }
        /// <summary>
        /// Gets or sets the se item.
        /// </summary>
        /// <value>The se item.</value>
        public SePortfolioItem SeItem { get; set; }

        /// <summary>
        /// Gets or sets the se category.
        /// </summary>
        /// <value>The se category.</value>
        public SePortfolioCategory SeCategory { get; set; }

        /// <summary>
        /// Gets or sets the se value list.
        /// </summary>
        /// <value>The se value list.</value>
        public SePortfoliosValueList SeValueList { get; set; }

        /// <summary>
        /// Gets or sets the se cell.
        /// </summary>
        /// <value>The se cell.</value>
        public SePortfolioCell SeCell { get; set; }
        /// <summary>
        /// Gets or sets the se sub item.
        /// </summary>
        /// <value>The se sub item.</value>
        public SePortfolioSubItem SeSubItem { get; set; }
        /// <summary>
        /// Gets or sets the certificate.
        /// </summary>
        /// <value>The certificate.</value>
        public X509Certificate Certificate { get; set; }
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public String User { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public String Password { get; set; }
        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        public String Server { get; set; }
        /// <summary>
        /// Gets or sets the use SSL.
        /// </summary>
        /// <value>The use SSL.</value>
        public Boolean UseSsl { get; set; }
        /// <summary>
        /// Gets or sets the logged in.
        /// </summary>
        /// <value>The logged in.</value>
        public Boolean LoggedIn { get; set; }


        /// <summary>
        /// The ps logger
        /// </summary>
        public static Logger PsLogger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Initializes a new instance of the <see cref="Oppm"/> class.
        /// </summary>
        public Oppm()
        {
            SeSecurity = new SePortfolioSecurity();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Oppm"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="server">The server.</param>
        /// <param name="useSsl">The use SSL.</param>
        public Oppm(String username, String password, String server, Boolean useSsl)
            : this(username, password, server, useSsl, null)
        {


        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Oppm"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="server">The server.</param>
        public Oppm(String username, String password, String server)
            : this(username, password, server, false, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Oppm"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="server">The server.</param>
        /// <param name="certificate">The certificate.</param>
        /// <param name="useSsl">The use SSL.</param>
        public Oppm(String username, String password, String server, Boolean useSsl, X509Certificate certificate)
            : this()
        {
            SeSecurity.User = username;
            SeSecurity.Password = password;
            SeSecurity.ProSightServer = server;
            if (certificate != null) SeSecurity.PortfoliosSecurityWs.ClientCertificates.Add(certificate);
            SeSecurity.ProSightServerUseSsl = useSsl;
            SePorfolio = new SePortfoliosPortfolio(SeSecurity);
            SeItem = new SePortfolioItem(SeSecurity);
            SeCategory = new SePortfolioCategory(SeSecurity);
            SeValueList = new SePortfoliosValueList(SeSecurity);
            SeCell = new SePortfolioCell(SeSecurity);
            SeSubItem = new SePortfolioSubItem(SeSecurity);
        }

        /// <summary>
        /// Logins this instance.
        /// </summary>
        /// <returns>Boolean.</returns>
        public Boolean Login()
        {
            LoggedIn = SeSecurity.Login();
            if (LoggedIn) return LoggedIn;
            PsLogger.Fatal("Unable to login to {0} with {1}", SeSecurity.ProSightServer, SeSecurity.User);
            return false;
        }
    }
}

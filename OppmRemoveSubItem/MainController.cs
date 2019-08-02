// ***********************************************************************
// Assembly         : OppmUtility
// Author           : ggreiff
// Created          : 08-27-2014
//
// Last Modified By : ggreiff
// Last Modified On : 09-01-2014
// ***********************************************************************
// <copyright file="MainController.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using CommandLine;
using NLog;
using OppmRemoveSubItem.Jobs;
using OppmRemoveSubItem.Properties;
using OppmRemoveSubItem.Utility;

namespace OppmRemoveSubItem
{
    /// <summary>
    /// Class MainController.
    /// </summary>
    public class MainController
    {
        /// <summary>
        /// The n logger
        /// </summary>
        public static Logger NLogger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            NLogger.Info("Start MainController With args: {0}", String.Join(", ", args));

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            X509Certificate cert = null;

            //
            // Set up the command line argument processing
            //
            var options = new Options();
            if (!Parser.Default.ParseArguments(args, options))
            {
                //NLogger.Fatal("Invalid arguements");
                Environment.Exit(Parser.DefaultExitCodeFail);
            }

            //
            // If the user didn't enter the Oppm arguments check the property file for values
            //
            if (options.OppmHost.IsNullOrEmpty() && Settings.Default.Properties["OppmHost"] != null)
                options.OppmHost = Settings.Default.OppmHost;

            if (options.OppmUser.IsNullOrEmpty() && Settings.Default.Properties["OppmUser"] != null)
                options.OppmUser = Settings.Default.OppmUser;

            if (options.OppmPassword.IsNullOrEmpty() && Settings.Default.Properties["OppmPassword"] != null)
                options.OppmPassword = Settings.Default.OppmPassword;

            if (options.DynamicList.IsNullOrEmpty() && Settings.Default.Properties["DynamicListName"] != null)
                options.DynamicList = Settings.Default.DynamicListName;

            if (options.UseCert) cert = HelperFunctions.GetDodCert();

            //
            // Exit if we don't have the oppm login values and the user's wants to commit
            //
            if (options.OppmUser.IsNullOrEmpty())
            {
                NLogger.Fatal("A Oppm username is required to commit."); Environment.Exit(-1);
            }

            if (options.OppmPassword.IsNullOrEmpty())
            {
                NLogger.Fatal("A Oppm password is required to commit."); Environment.Exit(-1);
            }

            if (options.OppmHost.IsNullOrEmpty())
            {
                NLogger.Fatal("A Oppm hostname is required to commit."); Environment.Exit(-1);
            }

            if (options.DynamicList.IsNullOrEmpty())
            {
                NLogger.Fatal("A Oppm DynamicList is required to commit."); Environment.Exit(-1);
            }

            //
            // Check to see that the xlsxfilename has a .xlsx extension
            //
            if (!Path.GetExtension(options.XlsxDataFileName).IsEqualTo(".xlsx", true))
                {
                    Console.WriteLine(options.GetUsage());
                    NLogger.Fatal("{0} is not a valid xlxs filename", Path.GetExtension(options.XlsxDataFileName));
                    Environment.Exit(-1);
                }

                //
                // Now let's make sure it exits.
                //
                if (!File.Exists(options.XlsxDataFileName))
                {
                    NLogger.Fatal("{0} does not exists", Path.GetExtension(options.XlsxDataFileName));
                    Environment.Exit(-1);
                }

                //
                // OK it appears we have everything we need to do the import.
                //
                try
                {
                    var removeSubItem = new RemoveSubItem {Certificate = cert};
                    removeSubItem.RunRemoveSubItem(options);
                }
                catch (Exception ex)
                {
                    NLogger.Error(ex.Message);
                }
            


            NLogger.Info("Stop MainController");

            Environment.Exit(0);    
        }
    }
}
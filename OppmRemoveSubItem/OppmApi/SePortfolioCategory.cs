using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using NLog;
using OppmRemoveSubItem.Utility;
using wsPortfoliosCategory;

namespace OppmRemoveSubItem.OppmApi
{
    public class SePortfolioCategory
    {
        public static Logger PsLogger = LogManager.GetCurrentClassLogger();

        public SePortfolioSecurity PsSecurityLogin { get; set; }

        public psPortfoliosCategory PsCategory { get; set; }

        public SePortfolioCategory(SePortfolioSecurity securityLogin)
        {
            PsSecurityLogin = securityLogin;
            PsCategory = new psPortfoliosCategory {CookieContainer = PsSecurityLogin.CookieContainer};
            PsCategory.Url = PsSecurityLogin.GetWebServicesUrLbyType(PsCategory);

            if (!securityLogin.PortfoliosSecurityWs.ClientCertificates.HasItems()) return;
            foreach (var clientCertificate in securityLogin.PortfoliosSecurityWs.ClientCertificates)
            {
                PsCategory.ClientCertificates.Add(clientCertificate);
            }
        }

        public SePortfolioCategory(SePortfolioSecurity securityLogin, X509Certificate certificate)
            : this(securityLogin)
        {
            PsCategory.ClientCertificates.Add(certificate);
        }


        public String GetCategoryIdentifierByName(String categoryName)
        {
            String retVal;
            try
            {
                retVal = PsCategory.GetCategoryIdentifierByName(categoryName);
            }
            // ReSharper disable once UnusedVariable
            catch (Exception ex)
            {
                PsLogger.Warn("Category not found {0}", categoryName);
                PsLogger.Trace(ex.Message);
                retVal = null;
            }
            return retVal;
        }

        public psPortfoliosCategoryInfo GetCategoryInfo(String categoryName)
        {
            psPortfoliosCategoryInfo retVal;
            try
            {
                retVal = PsCategory.GetCategoryInfo(categoryName);
            }
            // ReSharper disable once UnusedVariable
            catch (Exception ex)
            {
                //PsLogger.Warn("Category not found {}", categoryName);
                retVal = null;
                PsLogger.Trace(ex.Message);
            }
            return retVal;
        }

        public Boolean CategoryExists(String categoryName)
        {
            Boolean retVal;
            try
            {
                PsCategory.GetCategoryInfo(categoryName);
                retVal = true;
            }
            catch
            {
                retVal = false;
            }
            return retVal;
        }

        public List<psPortfoliosCategoryInfo> GetAllCategories()
        {
            var allCategories = new List<psPortfoliosCategoryInfo>();
            try
            {
                var retVal = PsCategory.GetAllCategories();
                Array.ForEach(retVal, allCategories.Add);
            }
            catch (Exception ex)
            {
                PsLogger.Error(string.Format("Unexcpected GetAllCategories Error: \n{0}\n", ex));
                throw new Exception(ex.ToString());
            }
            return allCategories;

        }

        public List<String> GetCategoriesNamesThatStartWith(String categoryNameStartsWith)
        {
            var retNames = new List<string>();
            var retCategories = GetCategoriesInfoThatStartWith(categoryNameStartsWith);
            retCategories.ForEach(category => retNames.Add(category.Name));
            return retNames;
        }

        public List<String> GetCategoriesNamesThatContain(String categoryNameContains)
        {
            var retNames = new List<string>();
            var retCategories = GetCategoriesInfoThatContain(categoryNameContains);
            retCategories.ForEach(category => retNames.Add(category.Name));
            return retNames;
        }

        public List<String> GetCategoriesNamesThatStartWith(String categoryNameStartsWith, int categoryNameLenght)
        {
            var categories = GetCategoriesNamesThatStartWith(categoryNameStartsWith);
            var retCategories = categories.FindAll(category => category.Length == categoryNameLenght);
            return retCategories;
        }

        public List<psPortfoliosCategoryInfo> GetCategoriesInfoThatStartWith(String categoryNameStartsWith)
        {
            var categories = GetAllCategories();
            var retCategories = categories.FindAll(category => category.Name.StartsWith(categoryNameStartsWith));
            return retCategories;
        }

        public List<psPortfoliosCategoryInfo> GetCategoriesInfoThatStartWith(String categoryNameStartsWith, int categoryNameLenght)
        {
            var categories = GetCategoriesInfoThatStartWith(categoryNameStartsWith);
            var retCategories = categories.FindAll(category => category.Name.Length == categoryNameLenght);
            return retCategories;
        }

        public List<psPortfoliosCategoryInfo> GetCategoriesInfoThatContain(String categoryNameContains)
        {
            var categories = GetAllCategories();
            var retCategories = categories.FindAll(category => category.Name.Contains(categoryNameContains));
            return retCategories;
        }

        public List<psPortfoliosCategoryInfo> GetTypeOfCategories(psCATEGORY_VALUE_TYPE categoryValueType)
        {
            var categories = GetAllCategories();
            var retCategories = categories.FindAll(category => category.ValueType == categoryValueType);
            return retCategories;
        }

        public List<psPortfoliosCategoryInfo> GetCategoriesOfTypeValueList()
        {
            return GetTypeOfCategories(psCATEGORY_VALUE_TYPE.CVTYP_VALUELIST);
        }

        public List<psPortfoliosCategoryInfo> GetCategoriesOfTypeInt()
        {
            return GetTypeOfCategories(psCATEGORY_VALUE_TYPE.CVTYP_INT);
        }

        public List<psPortfoliosCategoryInfo> GetCategoriesOfTypeFloat()
        {
            return GetTypeOfCategories(psCATEGORY_VALUE_TYPE.CVTYP_FLOAT);
        }

        public List<psPortfoliosCategoryInfo> GetCategoriesOfTypeText()
        {
            return GetTypeOfCategories(psCATEGORY_VALUE_TYPE.CVTYP_TEXT);
        }

        public List<psPortfoliosCategoryInfo> GetCategoriesOfTypeUser()
        {
            return GetTypeOfCategories(psCATEGORY_VALUE_TYPE.CVTYP_USER);
        }

        public List<psPortfoliosCategoryInfo> GetCategoriesOfTypeDateTime()
        {
            return GetTypeOfCategories(psCATEGORY_VALUE_TYPE.CVTYP_DATETIME);
        }

    }
}
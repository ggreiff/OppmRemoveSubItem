﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by wsdl, Version=4.0.30319.18020.
// 
namespace wsPortfoliosCategory {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="psPortfoliosCategorySoap", Namespace="http://prosight.com/wsdl/5.0/psPortfoliosCategory/")]
    public partial class psPortfoliosCategory : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback DebugOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetCategoryInfoOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetCategoryIdentifierByNameOperationCompleted;
        
        private System.Threading.SendOrPostCallback UpdateCategoryInfoOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetAllCategoriesOperationCompleted;
        
        /// <remarks/>
        public psPortfoliosCategory() {
            this.Url = "http://localhost/ProSightWs/psPortfoliosCategory.asmx";
        }
        
        /// <remarks/>
        public event DebugCompletedEventHandler DebugCompleted;
        
        /// <remarks/>
        public event GetCategoryInfoCompletedEventHandler GetCategoryInfoCompleted;
        
        /// <remarks/>
        public event GetCategoryIdentifierByNameCompletedEventHandler GetCategoryIdentifierByNameCompleted;
        
        /// <remarks/>
        public event UpdateCategoryInfoCompletedEventHandler UpdateCategoryInfoCompleted;
        
        /// <remarks/>
        public event GetAllCategoriesCompletedEventHandler GetAllCategoriesCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://prosight.com/wsdl/Portfolios/4.0/psPortfoliosCategory/Debug", RequestNamespace="http://prosight.com/type/Portfolios/5.0/psPortfoliosCategory", ResponseNamespace="http://prosight.com/type/Portfolios/5.0/psPortfoliosCategory", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public psRETURN_VALUES Debug() {
            object[] results = this.Invoke("Debug", new object[0]);
            return ((psRETURN_VALUES)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginDebug(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Debug", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public psRETURN_VALUES EndDebug(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((psRETURN_VALUES)(results[0]));
        }
        
        /// <remarks/>
        public void DebugAsync() {
            this.DebugAsync(null);
        }
        
        /// <remarks/>
        public void DebugAsync(object userState) {
            if ((this.DebugOperationCompleted == null)) {
                this.DebugOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDebugOperationCompleted);
            }
            this.InvokeAsync("Debug", new object[0], this.DebugOperationCompleted, userState);
        }
        
        private void OnDebugOperationCompleted(object arg) {
            if ((this.DebugCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DebugCompleted(this, new DebugCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://prosight.com/wsdl/Portfolios/4.0/psPortfoliosCategory/GetCategoryInfo", RequestNamespace="http://prosight.com/type/Portfolios/5.0/psPortfoliosCategory", ResponseNamespace="http://prosight.com/type/Portfolios/5.0/psPortfoliosCategory", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public psPortfoliosCategoryInfo GetCategoryInfo(string sIdentifier) {
            object[] results = this.Invoke("GetCategoryInfo", new object[] {
                        sIdentifier});
            return ((psPortfoliosCategoryInfo)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetCategoryInfo(string sIdentifier, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetCategoryInfo", new object[] {
                        sIdentifier}, callback, asyncState);
        }
        
        /// <remarks/>
        public psPortfoliosCategoryInfo EndGetCategoryInfo(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((psPortfoliosCategoryInfo)(results[0]));
        }
        
        /// <remarks/>
        public void GetCategoryInfoAsync(string sIdentifier) {
            this.GetCategoryInfoAsync(sIdentifier, null);
        }
        
        /// <remarks/>
        public void GetCategoryInfoAsync(string sIdentifier, object userState) {
            if ((this.GetCategoryInfoOperationCompleted == null)) {
                this.GetCategoryInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetCategoryInfoOperationCompleted);
            }
            this.InvokeAsync("GetCategoryInfo", new object[] {
                        sIdentifier}, this.GetCategoryInfoOperationCompleted, userState);
        }
        
        private void OnGetCategoryInfoOperationCompleted(object arg) {
            if ((this.GetCategoryInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetCategoryInfoCompleted(this, new GetCategoryInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://prosight.com/wsdl/Portfolios/4.0/psPortfoliosCategory/GetCategoryIdentifie" +
            "rByName", RequestNamespace="http://prosight.com/type/Portfolios/5.0/psPortfoliosCategory", ResponseNamespace="http://prosight.com/type/Portfolios/5.0/psPortfoliosCategory", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetCategoryIdentifierByName(string sName) {
            object[] results = this.Invoke("GetCategoryIdentifierByName", new object[] {
                        sName});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetCategoryIdentifierByName(string sName, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetCategoryIdentifierByName", new object[] {
                        sName}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndGetCategoryIdentifierByName(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetCategoryIdentifierByNameAsync(string sName) {
            this.GetCategoryIdentifierByNameAsync(sName, null);
        }
        
        /// <remarks/>
        public void GetCategoryIdentifierByNameAsync(string sName, object userState) {
            if ((this.GetCategoryIdentifierByNameOperationCompleted == null)) {
                this.GetCategoryIdentifierByNameOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetCategoryIdentifierByNameOperationCompleted);
            }
            this.InvokeAsync("GetCategoryIdentifierByName", new object[] {
                        sName}, this.GetCategoryIdentifierByNameOperationCompleted, userState);
        }
        
        private void OnGetCategoryIdentifierByNameOperationCompleted(object arg) {
            if ((this.GetCategoryIdentifierByNameCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetCategoryIdentifierByNameCompleted(this, new GetCategoryIdentifierByNameCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://prosight.com/wsdl/Portfolios/4.0/psPortfoliosCategory/UpdateCategoryInfo", RequestNamespace="http://prosight.com/type/Portfolios/5.0/psPortfoliosCategory", ResponseNamespace="http://prosight.com/type/Portfolios/5.0/psPortfoliosCategory", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void UpdateCategoryInfo(string sIdentifier, psPortfoliosCategoryInfo categoryInfo) {
            this.Invoke("UpdateCategoryInfo", new object[] {
                        sIdentifier,
                        categoryInfo});
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginUpdateCategoryInfo(string sIdentifier, psPortfoliosCategoryInfo categoryInfo, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("UpdateCategoryInfo", new object[] {
                        sIdentifier,
                        categoryInfo}, callback, asyncState);
        }
        
        /// <remarks/>
        public void EndUpdateCategoryInfo(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
        
        /// <remarks/>
        public void UpdateCategoryInfoAsync(string sIdentifier, psPortfoliosCategoryInfo categoryInfo) {
            this.UpdateCategoryInfoAsync(sIdentifier, categoryInfo, null);
        }
        
        /// <remarks/>
        public void UpdateCategoryInfoAsync(string sIdentifier, psPortfoliosCategoryInfo categoryInfo, object userState) {
            if ((this.UpdateCategoryInfoOperationCompleted == null)) {
                this.UpdateCategoryInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateCategoryInfoOperationCompleted);
            }
            this.InvokeAsync("UpdateCategoryInfo", new object[] {
                        sIdentifier,
                        categoryInfo}, this.UpdateCategoryInfoOperationCompleted, userState);
        }
        
        private void OnUpdateCategoryInfoOperationCompleted(object arg) {
            if ((this.UpdateCategoryInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateCategoryInfoCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://prosight.com/wsdl/Portfolios/4.0/psPortfoliosCategory/GetAllCategories", RequestNamespace="http://prosight.com/type/Portfolios/5.0/psPortfoliosCategory", ResponseNamespace="http://prosight.com/type/Portfolios/5.0/psPortfoliosCategory", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public psPortfoliosCategoryInfo[] GetAllCategories() {
            object[] results = this.Invoke("GetAllCategories", new object[0]);
            return ((psPortfoliosCategoryInfo[])(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetAllCategories(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetAllCategories", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public psPortfoliosCategoryInfo[] EndGetAllCategories(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((psPortfoliosCategoryInfo[])(results[0]));
        }
        
        /// <remarks/>
        public void GetAllCategoriesAsync() {
            this.GetAllCategoriesAsync(null);
        }
        
        /// <remarks/>
        public void GetAllCategoriesAsync(object userState) {
            if ((this.GetAllCategoriesOperationCompleted == null)) {
                this.GetAllCategoriesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetAllCategoriesOperationCompleted);
            }
            this.InvokeAsync("GetAllCategories", new object[0], this.GetAllCategoriesOperationCompleted, userState);
        }
        
        private void OnGetAllCategoriesOperationCompleted(object arg) {
            if ((this.GetAllCategoriesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetAllCategoriesCompleted(this, new GetAllCategoriesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://prosight.com/type/Portfolios/5.0/psPortfoliosCategory")]
    public enum psRETURN_VALUES {
        
        /// <remarks/>
        ERR_OK,
        
        /// <remarks/>
        ERR_INTERNAL,
        
        /// <remarks/>
        ERR_COMMON_ID_ALREADY_EXISTS,
        
        /// <remarks/>
        ERR_COMMON_ID_UNKNOWN,
        
        /// <remarks/>
        ERR_MULTIPLE_COMMON_ID,
        
        /// <remarks/>
        ERR_NOT_ITEM,
        
        /// <remarks/>
        ERR_NO_NAME_GIVEN,
        
        /// <remarks/>
        ERR_PORTFOLIO_UNKNOWN,
        
        /// <remarks/>
        ERR_PORTFOLIO_NOT_QBP,
        
        /// <remarks/>
        ERR_NAME_ALREADY_EXISTS,
        
        /// <remarks/>
        ERR_NOT_PORTFOLIO,
        
        /// <remarks/>
        ERR_PORTFOLIO_TYPE_MISMATCH,
        
        /// <remarks/>
        ERR_PHASE_NOT_FOUND,
        
        /// <remarks/>
        ERR_NO_SUCH_PHASE_FOR_ITEM,
        
        /// <remarks/>
        ERR_NO_COMMON_ID,
        
        /// <remarks/>
        ERR_ILLEGAL_STATUS,
        
        /// <remarks/>
        ERR_ILLEGAL_PORTFOLIO_TYPE,
        
        /// <remarks/>
        ERR_LIFE_CYCLE_NOT_FOUND,
        
        /// <remarks/>
        ERR_ILLEGAL_GUID,
        
        /// <remarks/>
        ERR_NO_PHASES_FOR_LIFE_CYCLE,
        
        /// <remarks/>
        ERR_ILLEGAL_DATE,
        
        /// <remarks/>
        ERR_ILLEGAL_DESCRIPTION,
        
        /// <remarks/>
        ERR_ITEM_DOES_NOT_EXIST,
        
        /// <remarks/>
        ERR_NO_VALUE_LIST_NAME,
        
        /// <remarks/>
        ERR_VALUE_LIST_DOES_NOT_EXIST,
        
        /// <remarks/>
        ERR_CANNOT_UPDATE_SYSTEM_VALUE_LIST,
        
        /// <remarks/>
        ERR_DUPLICATE_TEXT_IN_VALUE_LIST,
        
        /// <remarks/>
        ERR_DUPLICATE_ID_IN_VALUE_LIST,
        
        /// <remarks/>
        ERR_VALUE_DOES_NOT_EXIST,
        
        /// <remarks/>
        ERR_VALUE_ID_NOT_INTEGER,
        
        /// <remarks/>
        ERR_ILLEGAL_NAME,
        
        /// <remarks/>
        ERR_CATEGORY_NOT_SUPPORTED,
        
        /// <remarks/>
        ERR_CATEGORY_UNKNOWN,
        
        /// <remarks/>
        ERR_ILLEGAL_OPERATOR,
        
        /// <remarks/>
        ERR_VALUE_NOT_INTEGER,
        
        /// <remarks/>
        ERR_VALUE_NOT_NUMERIC,
        
        /// <remarks/>
        ERR_PORTFOLIO_NOT_IN_SCOPE,
        
        /// <remarks/>
        ERR_ILLEGAL_INDICATOR,
        
        /// <remarks/>
        ERR_BAD_PARAMETER,
        
        /// <remarks/>
        ERR_ILLEGAL_DEPENDENCY,
        
        /// <remarks/>
        ERR_ADD_NEW_NEVER_CALLED,
        
        /// <remarks/>
        ERR_SECURITY_NOT_SET,
        
        /// <remarks/>
        ERR_SECURITY_VIOLATION,
        
        /// <remarks/>
        ERR_LIFE_CYCLE_IN_PORTFOLIO,
        
        /// <remarks/>
        ERR_PHASE_IN_LIFE_CYCLE,
        
        /// <remarks/>
        ERR_NO_DELETE_PERMISSION,
        
        /// <remarks/>
        ERR_NO_EDIT_PERMISSION_ON_PARENT,
        
        /// <remarks/>
        ERR_NO_FURTHER_DETAILS_AVAILABLE,
        
        /// <remarks/>
        ERR_DUPLICATE_PHASE_IN_LIFE_CYCLE,
        
        /// <remarks/>
        ERR_PHASE_ACTUAL_START_DATE_LATER_THAN_END_DATE,
        
        /// <remarks/>
        ERR_PHASE_PLANED_START_DATE_LATER_THAN_END_DATE,
        
        /// <remarks/>
        ERR_MORE_THAN_ONE_CURRENT_PHASE_FOR_ITEM,
        
        /// <remarks/>
        ERR_MENU_XML_DOES_NOT_CONFORM_TO_XSD,
        
        /// <remarks/>
        ERR_SETTINGS_DO_NOT_EXIST,
        
        /// <remarks/>
        ERR_SETTINGS_ALREADY_EXIST,
        
        /// <remarks/>
        ERR_OBJECT_IN_USE,
        
        /// <remarks/>
        ERR_EXCEEDED_AUTHORIZED_NUMBER_OF_USERS,
        
        /// <remarks/>
        ERR_PHASE_FRCST_START_DATE_LATER_THAN_END_DATE,
        
        /// <remarks/>
        ERR_SPECIFIC_EMAIL_ALERT_ALREADY_EXISTS,
        
        /// <remarks/>
        ERR_XML_EXPORT_TEMPLATE_ERROR,
        
        /// <remarks/>
        ERR_XML_EXPORT_ERROR,
        
        /// <remarks/>
        ERR_XML_EXPORT_VALIDATION_ERROR,
        
        /// <remarks/>
        ERR_VERSION_NAME_DOES_NOT_EXIST,
        
        /// <remarks/>
        ERR_ATTACHMENT_UNKNOWN,
        
        /// <remarks/>
        NOT_SPECIFIED,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://prosight.com/type/Portfolios/5.0/psPortfoliosCategory")]
    public partial class psPortfoliosCategoryInfo {
        
        private string identifierField;
        
        private int ownerIDField;
        
        private string nameField;
        
        private string descriptionField;
        
        private psCATEGORY_TYPE categoryTypeField;
        
        private psCATEGORY_VALUE_TYPE valueTypeField;
        
        private string valueListNameField;
        
        private string valueUnitsField;
        
        private string referenceCategoryNameField;
        
        private bool isSystemCategoryField;
        
        private psDATA_SOURCE valueDataSourceField;
        
        private psDATA_SOURCE indicatorDataSourceField;
        
        private psDATA_SOURCE summaryValueDataSourceField;
        
        private psDATA_SOURCE summaryIndicatorDataSourceField;
        
        private bool isSecurityDeniedField;
        
        public psPortfoliosCategoryInfo() {
            this.categoryTypeField = psCATEGORY_TYPE.NOT_SPECIFIED;
            this.valueTypeField = psCATEGORY_VALUE_TYPE.NOT_SPECIFIED;
            this.valueDataSourceField = psDATA_SOURCE.NOT_SPECIFIED;
            this.indicatorDataSourceField = psDATA_SOURCE.NOT_SPECIFIED;
            this.summaryValueDataSourceField = psDATA_SOURCE.NOT_SPECIFIED;
            this.summaryIndicatorDataSourceField = psDATA_SOURCE.NOT_SPECIFIED;
        }
        
        /// <remarks/>
        public string Identifier {
            get {
                return this.identifierField;
            }
            set {
                this.identifierField = value;
            }
        }
        
        /// <remarks/>
        public int OwnerID {
            get {
                return this.ownerIDField;
            }
            set {
                this.ownerIDField = value;
            }
        }
        
        /// <remarks/>
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        public string Description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
            }
        }
        
        /// <remarks/>
        [System.ComponentModel.DefaultValueAttribute(psCATEGORY_TYPE.NOT_SPECIFIED)]
        public psCATEGORY_TYPE CategoryType {
            get {
                return this.categoryTypeField;
            }
            set {
                this.categoryTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.ComponentModel.DefaultValueAttribute(psCATEGORY_VALUE_TYPE.NOT_SPECIFIED)]
        public psCATEGORY_VALUE_TYPE ValueType {
            get {
                return this.valueTypeField;
            }
            set {
                this.valueTypeField = value;
            }
        }
        
        /// <remarks/>
        public string ValueListName {
            get {
                return this.valueListNameField;
            }
            set {
                this.valueListNameField = value;
            }
        }
        
        /// <remarks/>
        public string ValueUnits {
            get {
                return this.valueUnitsField;
            }
            set {
                this.valueUnitsField = value;
            }
        }
        
        /// <remarks/>
        public string ReferenceCategoryName {
            get {
                return this.referenceCategoryNameField;
            }
            set {
                this.referenceCategoryNameField = value;
            }
        }
        
        /// <remarks/>
        public bool IsSystemCategory {
            get {
                return this.isSystemCategoryField;
            }
            set {
                this.isSystemCategoryField = value;
            }
        }
        
        /// <remarks/>
        [System.ComponentModel.DefaultValueAttribute(psDATA_SOURCE.NOT_SPECIFIED)]
        public psDATA_SOURCE ValueDataSource {
            get {
                return this.valueDataSourceField;
            }
            set {
                this.valueDataSourceField = value;
            }
        }
        
        /// <remarks/>
        [System.ComponentModel.DefaultValueAttribute(psDATA_SOURCE.NOT_SPECIFIED)]
        public psDATA_SOURCE IndicatorDataSource {
            get {
                return this.indicatorDataSourceField;
            }
            set {
                this.indicatorDataSourceField = value;
            }
        }
        
        /// <remarks/>
        [System.ComponentModel.DefaultValueAttribute(psDATA_SOURCE.NOT_SPECIFIED)]
        public psDATA_SOURCE SummaryValueDataSource {
            get {
                return this.summaryValueDataSourceField;
            }
            set {
                this.summaryValueDataSourceField = value;
            }
        }
        
        /// <remarks/>
        [System.ComponentModel.DefaultValueAttribute(psDATA_SOURCE.NOT_SPECIFIED)]
        public psDATA_SOURCE SummaryIndicatorDataSource {
            get {
                return this.summaryIndicatorDataSourceField;
            }
            set {
                this.summaryIndicatorDataSourceField = value;
            }
        }
        
        /// <remarks/>
        public bool IsSecurityDenied {
            get {
                return this.isSecurityDeniedField;
            }
            set {
                this.isSecurityDeniedField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://prosight.com/type/Portfolios/5.0/psPortfoliosCategory")]
    public enum psCATEGORY_TYPE {
        
        /// <remarks/>
        CTYP_NONE,
        
        /// <remarks/>
        CTYP_VALUE,
        
        /// <remarks/>
        CTYP_INDICATOR,
        
        /// <remarks/>
        CTYP_BOTH,
        
        /// <remarks/>
        NOT_SPECIFIED,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://prosight.com/type/Portfolios/5.0/psPortfoliosCategory")]
    public enum psCATEGORY_VALUE_TYPE {
        
        /// <remarks/>
        CVTYP_NONE,
        
        /// <remarks/>
        CVTYP_INT,
        
        /// <remarks/>
        CVTYP_FLOAT,
        
        /// <remarks/>
        CVTYP_TEXT,
        
        /// <remarks/>
        CVTYP_DATETIME,
        
        /// <remarks/>
        CVTYP_VALUELIST,
        
        /// <remarks/>
        CVTYP_USER,
        
        /// <remarks/>
        NOT_SPECIFIED,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://prosight.com/type/Portfolios/5.0/psPortfoliosCategory")]
    public enum psDATA_SOURCE {
        
        /// <remarks/>
        DSRC_NONE,
        
        /// <remarks/>
        DSRC_MANUAL,
        
        /// <remarks/>
        DSRC_CALCULATED,
        
        /// <remarks/>
        DSRC_IMPORTED,
        
        /// <remarks/>
        NOT_SPECIFIED,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    public delegate void DebugCompletedEventHandler(object sender, DebugCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DebugCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DebugCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public psRETURN_VALUES Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((psRETURN_VALUES)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    public delegate void GetCategoryInfoCompletedEventHandler(object sender, GetCategoryInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetCategoryInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetCategoryInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public psPortfoliosCategoryInfo Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((psPortfoliosCategoryInfo)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    public delegate void GetCategoryIdentifierByNameCompletedEventHandler(object sender, GetCategoryIdentifierByNameCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetCategoryIdentifierByNameCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetCategoryIdentifierByNameCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    public delegate void UpdateCategoryInfoCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    public delegate void GetAllCategoriesCompletedEventHandler(object sender, GetAllCategoriesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetAllCategoriesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetAllCategoriesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public psPortfoliosCategoryInfo[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((psPortfoliosCategoryInfo[])(this.results[0]));
            }
        }
    }
}

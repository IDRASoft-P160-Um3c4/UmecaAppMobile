//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UmecaApp.UmecaWebService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "4.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="UmecaWSSoap11Binding", Namespace="http://ws.umeca.com")]
    public partial class UmecaWS : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        /// <remarks/>
        public UmecaWS() {
			this.Url = "http://www.umeca.com/context";
			this.Timeout = 180000;
        }
        
        public UmecaWS(string url) {
            this.Url = url;
			this.Timeout = 180000;
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:getAssignmentsByUser", RequestNamespace="http://ws.umeca.com", ResponseNamespace="http://ws.umeca.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", IsNullable=true)]
        public ResponseMessage getAssignmentsByUser([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string user, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string guid) {
            object[] results = this.Invoke("getAssignmentsByUser", new object[] {
                        user,
                        guid});
            return ((ResponseMessage)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BegingetAssignmentsByUser(string user, string guid, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("getAssignmentsByUser", new object[] {
                        user,
                        guid}, callback, asyncState);
        }
        
        /// <remarks/>
        public ResponseMessage EndgetAssignmentsByUser(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((ResponseMessage)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:synchronizeMeeting", RequestNamespace="http://ws.umeca.com", ResponseNamespace="http://ws.umeca.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", IsNullable=true)]
        public ResponseMessage synchronizeMeeting([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string user, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string guid, long? assignmentId, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string jsonCase) {
            object[] results = this.Invoke("synchronizeMeeting", new object[] {
                        user,
                        guid,
                        assignmentId,
                        jsonCase});
            return ((ResponseMessage)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginsynchronizeMeeting(string user, string guid, long assignmentId, bool assignmentIdSpecified, string jsonCase, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("synchronizeMeeting", new object[] {
                        user,
                        guid,
                        assignmentId,
                        assignmentIdSpecified,
                        jsonCase}, callback, asyncState);
        }
        
        /// <remarks/>
        public ResponseMessage EndsynchronizeMeeting(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((ResponseMessage)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:loginFromTablet", RequestNamespace="http://ws.umeca.com", ResponseNamespace="http://ws.umeca.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", IsNullable=true)]
        public ResponseMessage loginFromTablet([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string user, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string encodedPass) {
            object[] results = this.Invoke("loginFromTablet", new object[] {
                        user,
                        encodedPass});
            return ((ResponseMessage)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginloginFromTablet(string user, string encodedPass, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("loginFromTablet", new object[] {
                        user,
                        encodedPass}, callback, asyncState);
        }
        
        /// <remarks/>
        public ResponseMessage EndloginFromTablet(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((ResponseMessage)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:synchronizeHearingFormat", RequestNamespace="http://ws.umeca.com", ResponseNamespace="http://ws.umeca.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", IsNullable=true)]
        public ResponseMessage synchronizeHearingFormat([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string user, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string guid, long? assignmentId, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string jsonCase) {
            object[] results = this.Invoke("synchronizeHearingFormat", new object[] {
                        user,
                        guid,
                        assignmentId,
                        jsonCase});
            return ((ResponseMessage)(results[0]));
        } 
        
        /// <remarks/>
        public System.IAsyncResult BeginsynchronizeHearingFormat(string user, string guid, long assignmentId, bool assignmentIdSpecified, string jsonCase, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("synchronizeHearingFormat", new object[] {
                        user,
                        guid,
                        assignmentId,
                        assignmentIdSpecified,
                        jsonCase}, callback, asyncState);
        }
        
        /// <remarks/>
        public ResponseMessage EndsynchronizeHearingFormat(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((ResponseMessage)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:synchronizeSourcesVerification", RequestNamespace="http://ws.umeca.com", ResponseNamespace="http://ws.umeca.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", IsNullable=true)]
        public ResponseMessage synchronizeSourcesVerification([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string user, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string guid, long? assignmentId, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string jsonCase) {
            object[] results = this.Invoke("synchronizeSourcesVerification", new object[] {
                        user,
                        guid,
                        assignmentId,
                        jsonCase});
            return ((ResponseMessage)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginsynchronizeSourcesVerification(string user, string guid, long assignmentId, bool assignmentIdSpecified, string jsonCase, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("synchronizeSourcesVerification", new object[] {
                        user,
                        guid,
                        assignmentId,
                        assignmentIdSpecified,
                        jsonCase}, callback, asyncState);
        }
        
        /// <remarks/>
        public ResponseMessage EndsynchronizeSourcesVerification(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((ResponseMessage)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:confirmReceivedAssignment", RequestNamespace="http://ws.umeca.com", ResponseNamespace="http://ws.umeca.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", IsNullable=true)]
        public ResponseMessage confirmReceivedAssignment([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string user, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string guid, long assignmentId, [System.Xml.Serialization.XmlIgnoreAttribute()] bool assignmentIdSpecified) {
            object[] results = this.Invoke("confirmReceivedAssignment", new object[] {
                        user,
                        guid,
                        assignmentId,
                        assignmentIdSpecified});
            return ((ResponseMessage)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginconfirmReceivedAssignment(string user, string guid, long assignmentId, bool assignmentIdSpecified, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("confirmReceivedAssignment", new object[] {
                        user,
                        guid,
                        assignmentId,
                        assignmentIdSpecified}, callback, asyncState);
        }
        
        /// <remarks/>
        public ResponseMessage EndconfirmReceivedAssignment(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((ResponseMessage)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:getAssignedCaseByAssignmentId", RequestNamespace="http://ws.umeca.com", ResponseNamespace="http://ws.umeca.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", IsNullable=true)]
        public ResponseMessage getAssignedCaseByAssignmentId([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string user, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string guid, long assignmentId, [System.Xml.Serialization.XmlIgnoreAttribute()] bool assignmentIdSpecified) {
            object[] results = this.Invoke("getAssignedCaseByAssignmentId", new object[] {
                        user,
                        guid,
                        assignmentId,
                        assignmentIdSpecified});
            return ((ResponseMessage)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BegingetAssignedCaseByAssignmentId(string user, string guid, long assignmentId, bool assignmentIdSpecified, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("getAssignedCaseByAssignmentId", new object[] {
                        user,
                        guid,
                        assignmentId,
                        assignmentIdSpecified}, callback, asyncState);
        }
        
        /// <remarks/>
        public ResponseMessage EndgetAssignedCaseByAssignmentId(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((ResponseMessage)(results[0]));
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "4.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://model.infrastructure.umeca.com/xsd")]
    public partial class ResponseMessage {
        
        /// <remarks/>
        public bool hasError;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool hasErrorSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string message;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public object returnData;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string title;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string urlToGo;
    }


}

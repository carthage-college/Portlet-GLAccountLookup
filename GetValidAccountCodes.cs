namespace Jenzabar.CRM.Staff.Web.Portlets.GLAccountLookupPortlet {
    
    
    /// <remarks/>
    // XML from the CRMStaff.GLAccountLookup plug-in is used to populate this class.
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public class ValidAccountCodes {
        
        /// <remarks/>
        private ValidAccountCodesGLAccountLookupLedgerAccounts[] _items;
        
        /// This is the comment for _items
        [System.Xml.Serialization.XmlArrayAttribute("GLAccountLookup", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("LedgerAccounts", typeof(ValidAccountCodesGLAccountLookupLedgerAccounts), Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public ValidAccountCodesGLAccountLookupLedgerAccounts[] Items {
            get {
                return this._items;
            }
            set {
                this._items = value;
            }
        }
    }
    
    /// <remarks/>
    public class ValidAccountCodesGLAccountLookupLedgerAccounts {
        
        /// <remarks/>
        private string _accountcode;
        
        /// <remarks/>
        private string _accountname;
        
        /// This is the comment for _accountcode
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AccountCode {
            get {
                return this._accountcode;
            }
            set {
                this._accountcode = value;
            }
        }
        
        /// This is the comment for _accountname
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AccountName {
            get {
                return this._accountname;
            }
            set {
                this._accountname = value;
            }
        }
    }
}

namespace Jenzabar.CRM.Staff.Web.Portlets.GLAccountLookupPortlet {
    
    
    /// <remarks/>
    // XML from the CRMStaff.GLAccountLookup plug-in is used to populate this class.
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public class OtherTranInfo {
        
        /// <remarks/>
        private OtherTranInfoSection[] _items;
        
        /// This is the comment for _items
        [System.Xml.Serialization.XmlElementAttribute("Section", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public OtherTranInfoSection[] Items {
            get {
                return this._items;
            }
            set {
                this._items = value;
            }
        }
    }
    
    /// <remarks/>
    public class OtherTranInfoSection {
        
        /// <remarks/>
        private string _header;
        
        /// <remarks/>
        private OtherTranInfoSectionTranInfo[] _traninfo;
        
        /// This is the comment for _header
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Header {
            get {
                return this._header;
            }
            set {
                this._header = value;
            }
        }
        
        /// This is the comment for _traninfo
        [System.Xml.Serialization.XmlElementAttribute("TranInfo", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public OtherTranInfoSectionTranInfo[] TranInfo {
            get {
                return this._traninfo;
            }
            set {
                this._traninfo = value;
            }
        }
    }
    
    /// <remarks/>
    public class OtherTranInfoSectionTranInfo {
        
        /// <remarks/>
        private string _displaylabel;
        
        /// <remarks/>
        private OtherTranInfoSectionTranInfoDisplayValue[] _displayvalue;
        
        /// This is the comment for _displaylabel
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DisplayLabel {
            get {
                return this._displaylabel;
            }
            set {
                this._displaylabel = value;
            }
        }
        
        /// This is the comment for _displayvalue
        [System.Xml.Serialization.XmlElementAttribute("DisplayValue", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public OtherTranInfoSectionTranInfoDisplayValue[] DisplayValue {
            get {
                return this._displayvalue;
            }
            set {
                this._displayvalue = value;
            }
        }
    }
    
    /// <remarks/>
    public class OtherTranInfoSectionTranInfoDisplayValue {
        
        /// <remarks/>
        private string _email;
        
        /// <remarks/>
        private string _value;
        
        /// This is the comment for _email
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string email {
            get {
                return this._email;
            }
            set {
                this._email = value;
            }
        }
        
        /// This is the comment for _value
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value {
            get {
                return this._value;
            }
            set {
                this._value = value;
            }
        }
    }
}

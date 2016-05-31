namespace Jenzabar.CRM.Deserializer {
    
    
    /// <remarks/>
    // XML from the CRM.AdmissionsOfficer plug-in is used to populate this class.
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public class EmailAddressList {
        
        /// <remarks/>
        private string _id;
        
        /// <remarks/>
        private string _firstname;
        
        /// <remarks/>
        private string _middlename;
        
        /// <remarks/>
        private string _lastname;
        
        /// <remarks/>
        private EmailAddressListEmailAddressInfo[] _emailaddressinfo;
        
        /// This is the comment for _id
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ID {
            get {
                return this._id;
            }
            set {
                this._id = value;
            }
        }
        
        /// This is the comment for _firstname
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FirstName {
            get {
                return this._firstname;
            }
            set {
                this._firstname = value;
            }
        }
        
        /// This is the comment for _middlename
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MiddleName {
            get {
                return this._middlename;
            }
            set {
                this._middlename = value;
            }
        }
        
        /// This is the comment for _lastname
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LastName {
            get {
                return this._lastname;
            }
            set {
                this._lastname = value;
            }
        }
        
        /// This is the comment for _emailaddressinfo
        [System.Xml.Serialization.XmlElementAttribute("EmailAddressInfo", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public EmailAddressListEmailAddressInfo[] EmailAddressInfo {
            get {
                return this._emailaddressinfo;
            }
            set {
                this._emailaddressinfo = value;
            }
        }
    }
    
    /// <remarks/>
    public class EmailAddressListEmailAddressInfo {
        
        /// <remarks/>
        private string _emailaddresskey;
        
        /// <remarks/>
        private string _addrcode;
        
        /// <remarks/>
        private string _addrdesc;
        
        /// <remarks/>
        private string _emailaddress;
        
        /// This is the comment for _emailaddresskey
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EmailAddressKey {
            get {
                return this._emailaddresskey;
            }
            set {
                this._emailaddresskey = value;
            }
        }
        
        /// This is the comment for _addrcode
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AddrCode {
            get {
                return this._addrcode;
            }
            set {
                this._addrcode = value;
            }
        }
        
        /// This is the comment for _addrdesc
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AddrDesc {
            get {
                return this._addrdesc;
            }
            set {
                this._addrdesc = value;
            }
        }
        
        /// This is the comment for _emailaddress
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EmailAddress {
            get {
                return this._emailaddress;
            }
            set {
                this._emailaddress = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public class NewDataSet {
        
        /// <remarks/>
        private EmailAddressList[] _items;
        
        /// This is the comment for _items
        [System.Xml.Serialization.XmlElementAttribute("EmailAddressList")]
        public EmailAddressList[] Items {
            get {
                return this._items;
            }
            set {
                this._items = value;
            }
        }
    }
}

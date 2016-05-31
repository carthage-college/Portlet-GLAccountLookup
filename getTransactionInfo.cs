namespace Jenzabar.CRM.Staff.Web.Portlets.GLAccountLookupPortlet {
    
    
    /// <remarks/>
    // XML from the ERPStaff.GLAccountLookup plug-in is used to populate this class.
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public class TranLookupInfo {
        
        /// <remarks/>
        private string _accountnumber;
        
        /// <remarks/>
        private string _accountdesc;
        
        /// <remarks/>
        private string _period;
        
        /// <remarks/>
        private TranLookupInfoKeyHeader[] _keyheader;
        
        /// <remarks/>
        private TranLookupInfoTransactions[] _transactions;
        
        /// This is the comment for _accountnumber
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AccountNumber {
            get {
                return this._accountnumber;
            }
            set {
                this._accountnumber = value;
            }
        }
        
        /// This is the comment for _accountdesc
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AccountDesc {
            get {
                return this._accountdesc;
            }
            set {
                this._accountdesc = value;
            }
        }
        
        /// This is the comment for _period
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Period {
            get {
                return this._period;
            }
            set {
                this._period = value;
            }
        }
        
        /// This is the comment for _keyheader
        [System.Xml.Serialization.XmlElementAttribute("KeyHeader", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public TranLookupInfoKeyHeader[] KeyHeader {
            get {
                return this._keyheader;
            }
            set {
                this._keyheader = value;
            }
        }
        
        /// This is the comment for _transactions
        [System.Xml.Serialization.XmlElementAttribute("Transactions", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public TranLookupInfoTransactions[] Transactions {
            get {
                return this._transactions;
            }
            set {
                this._transactions = value;
            }
        }
    }
    
    /// <remarks/>
    public class TranLookupInfoKeyHeader {
        
        /// <remarks/>
        private string _keydesc;
        
        /// This is the comment for _keydesc
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string KeyDesc {
            get {
                return this._keydesc;
            }
            set {
                this._keydesc = value;
            }
        }
    }
    
    /// <remarks/>
    public class TranLookupInfoTransactionsKey {
        
        /// <remarks/>
        private string _keyvalue;
        
        /// This is the comment for _keyvalue
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string KeyValue {
            get {
                return this._keyvalue;
            }
            set {
                this._keyvalue = value;
            }
        }
    }
    
    /// <remarks/>
    public class TranLookupInfoTransactions {
        
        /// <remarks/>
        private string _date;
        
        /// <remarks/>
        private string _desc;
        
        /// <remarks/>
        private string _amount;
        
        /// <remarks/>
        private string _status;
        
        /// <remarks/>
        private string _highlight;
        
        /// <remarks/>
        private TranLookupInfoTransactionsKey[] _key;
        
        /// This is the comment for _date
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Date {
            get {
                return this._date;
            }
            set {
                this._date = value;
            }
        }
        
        /// This is the comment for _desc
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Desc {
            get {
                return this._desc;
            }
            set {
                this._desc = value;
            }
        }
        
        /// This is the comment for _amount
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Amount {
            get {
                return this._amount;
            }
            set {
                this._amount = value;
            }
        }
        
        /// This is the comment for _status
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Status {
            get {
                return this._status;
            }
            set {
                this._status = value;
            }
        }
        
        /// This is the comment for _highlight
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Highlight {
            get {
                return this._highlight;
            }
            set {
                this._highlight = value;
            }
        }
        
        /// This is the comment for _key
        [System.Xml.Serialization.XmlElementAttribute("Key", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public TranLookupInfoTransactionsKey[] Key {
            get {
                return this._key;
            }
            set {
                this._key = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public class TranLookupInfoNewDataSet {
        
        /// <remarks/>
        private TranLookupInfo[] _items;
        
        /// This is the comment for _items
        [System.Xml.Serialization.XmlElementAttribute("TranLookupInfo")]
        public TranLookupInfo[] Items {
            get {
                return this._items;
            }
            set {
                this._items = value;
            }
        }
    }
}

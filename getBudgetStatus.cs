namespace Jenzabar.CRM.Staff.Web.Portlets.GLAccountLookupPortlet {
    
    
    /// <remarks/>
    // XML from the CRMStaff.GLAccountLookup plug-in is used to populate this class.
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public class BudgetLookupInfo {
        
        /// <remarks/>
        private string _errortext;
        
        /// <remarks/>
        private string _budrange;
        
        /// <remarks/>
        private string _beginperiod;
        
        /// <remarks/>
        private string _endperiod;
        
        /// <remarks/>
        private string _period;
        
        /// <remarks/>
        private string _unpostbaltotal;
        
        /// <remarks/>
        private string _postedbaltotal;
        
        /// <remarks/>
        private string _encumbrancetotal;
        
        /// <remarks/>
        private string _totalagainstbudgettotal;
        
        /// <remarks/>
        private string _annualbudgettotal;
        
        /// <remarks/>
        private string _periodbudgettotal;
        
        /// <remarks/>
        private string _overundertotal;
        
        /// <remarks/>
        private BudgetLookupInfoAccounts[] _accounts;
        
        /// This is the comment for _errortext
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ErrorText {
            get {
                return this._errortext;
            }
            set {
                this._errortext = value;
            }
        }
        
        /// This is the comment for _budrange
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string BudRange {
            get {
                return this._budrange;
            }
            set {
                this._budrange = value;
            }
        }
        
        /// This is the comment for _beginperiod
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string BeginPeriod {
            get {
                return this._beginperiod;
            }
            set {
                this._beginperiod = value;
            }
        }
        
        /// This is the comment for _endperiod
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EndPeriod {
            get {
                return this._endperiod;
            }
            set {
                this._endperiod = value;
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
        
        /// This is the comment for _unpostbaltotal
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string UnpostBalTotal {
            get {
                return this._unpostbaltotal;
            }
            set {
                this._unpostbaltotal = value;
            }
        }
        
        /// This is the comment for _postedbaltotal
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PostedBalTotal {
            get {
                return this._postedbaltotal;
            }
            set {
                this._postedbaltotal = value;
            }
        }
        
        /// This is the comment for _encumbrancetotal
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EncumbranceTotal {
            get {
                return this._encumbrancetotal;
            }
            set {
                this._encumbrancetotal = value;
            }
        }
        
        /// This is the comment for _totalagainstbudgettotal
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TotalAgainstBudgetTotal {
            get {
                return this._totalagainstbudgettotal;
            }
            set {
                this._totalagainstbudgettotal = value;
            }
        }

		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string OtherAgainstBudgetTotal { set; get; }
        
        /// This is the comment for _annualbudgettotal
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AnnualBudgetTotal {
            get {
                return this._annualbudgettotal;
            }
            set {
                this._annualbudgettotal = value;
            }
        }
        
        /// This is the comment for _periodbudgettotal
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PeriodBudgetTotal {
            get {
                return this._periodbudgettotal;
            }
            set {
                this._periodbudgettotal = value;
            }
        }
        
        /// This is the comment for _overundertotal
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string OverUnderTotal {
            get {
                return this._overundertotal;
            }
            set {
                this._overundertotal = value;
            }
        }
        
        /// This is the comment for _accounts
        [System.Xml.Serialization.XmlElementAttribute("Accounts", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public BudgetLookupInfoAccounts[] Accounts {
            get {
                return this._accounts;
            }
            set {
                this._accounts = value;
            }
        }
    }
    
    /// <remarks/>
    public class BudgetLookupInfoAccounts {
        
        /// <remarks/>
        private string _accountnumber;
        
        /// <remarks/>
        private string _accountdesc;
        
        /// <remarks/>
        private string _unpostedbal;
        
        /// <remarks/>
        private string _postedbal;
        
        /// <remarks/>
        private string _encumbrance;
        
        /// <remarks/>
        private string _totalagainstbudget;
        
        /// <remarks/>
        private string _beginpostbal;
        
        /// <remarks/>
        private string _endbal;
        
        /// <remarks/>
        private string _annualbudget;
        
        /// <remarks/>
        private string _periodbudget;
        
        /// <remarks/>
        private string _remainingbudget;
        
        /// <remarks/>
        private string _overbudget;
        
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
        
        /// This is the comment for _unpostedbal
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string UnpostedBal {
            get {
                return this._unpostedbal;
            }
            set {
                this._unpostedbal = value;
            }
        }
        
        /// This is the comment for _postedbal
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PostedBal {
            get {
                return this._postedbal;
            }
            set {
                this._postedbal = value;
            }
        }
        
        /// This is the comment for _encumbrance
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Encumbrance {
            get {
                return this._encumbrance;
            }
            set {
                this._encumbrance = value;
            }
        }
        
        /// This is the comment for _totalagainstbudget
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TotalAgainstBudget {
            get {
                return this._totalagainstbudget;
            }
            set {
                this._totalagainstbudget = value;
            }
        }

		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string OtherAgainstBudget { set; get; }
        
        /// This is the comment for _beginpostbal
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string BeginPostBal {
            get {
                return this._beginpostbal;
            }
            set {
                this._beginpostbal = value;
            }
        }
        
        /// This is the comment for _endbal
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EndBal {
            get {
                return this._endbal;
            }
            set {
                this._endbal = value;
            }
        }
        
        /// This is the comment for _annualbudget
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AnnualBudget {
            get {
                return this._annualbudget;
            }
            set {
                this._annualbudget = value;
            }
        }
        
        /// This is the comment for _periodbudget
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PeriodBudget {
            get {
                return this._periodbudget;
            }
            set {
                this._periodbudget = value;
            }
        }
        
        /// This is the comment for _remainingbudget
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RemainingBudget {
            get {
                return this._remainingbudget;
            }
            set {
                this._remainingbudget = value;
            }
        }
        
        /// This is the comment for _overbudget
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string OverBudget {
            get {
                return this._overbudget;
            }
            set {
                this._overbudget = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public class NewDataSet {
        
        /// <remarks/>
        private BudgetLookupInfo[] _items;
        
        /// This is the comment for _items
        [System.Xml.Serialization.XmlElementAttribute("BudgetLookupInfo")]
        public BudgetLookupInfo[] Items {
            get {
                return this._items;
            }
            set {
                this._items = value;
            }
        }
    }
}

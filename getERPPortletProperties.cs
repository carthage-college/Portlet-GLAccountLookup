namespace Jenzabar.CRM.Staff.Web.Portlets.GLAccountLookupPortlet {
    
    
    /// <remarks/>
    // XML from the REQENT plug-in is used to populate this class.
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public class ERP {
        
        /// <remarks/>
        private ERPProperties[] _items;
        
        /// This is the comment for _items
        [System.Xml.Serialization.XmlElementAttribute("Properties", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ERPProperties[] Items {
            get {
                return this._items;
            }
            set {
                this._items = value;
            }
        }
    }
    
    /// <remarks/>
    public class ERPProperties {
        
        /// <remarks/>
        private ERPPropertiesDeliverToCode[] _delivertocode;
        
        /// <remarks/>
        private ERPPropertiesDeliverToName[] _delivertoname;
        
        /// <remarks/>
        private ERPPropertiesItemRequested[] _itemrequested;
        
        /// <remarks/>
        private ERPPropertiesUnit[] _unit;
        
        /// <remarks/>
        private ERPPropertiesUnitPrice[] _unitprice;
        
        /// <remarks/>
        private ERPPropertiesCatalogNumber[] _catalognumber;
        
        /// <remarks/>
        private ERPPropertiesProject[] _project;
        
        /// <remarks/>
        private ERPPropertiesNotOnApprovalTrack[] _notonapprovaltrack;
        
        /// <remarks/>
        private ERPPropertiesApprovalNotAPO[] _approvalnotapo;
        
        /// <remarks/>
        private ERPPropertiesApprovalIsAPO[] _approvalisapo;
        
        /// <remarks/>
        private ERPPropertiesComments[] _comments;

		/// <summary>
		/// The array of General Ledger account ledger objects.
		/// </summary>
		private ERPPropertiesGLAccountLedger[] _glAccountLedger;


		/// This is the comment for _delivertocode
		[System.Xml.Serialization.XmlElementAttribute("GLAccountLedger", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public ERPPropertiesGLAccountLedger[] GLAccountLedger 
		{
			get 
			{
				return this._glAccountLedger;
			}
			set 
			{
				this._glAccountLedger = value;
			}
		}
        
        
        /// This is the comment for _delivertocode
        [System.Xml.Serialization.XmlElementAttribute("DeliverToCode", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ERPPropertiesDeliverToCode[] DeliverToCode {
            get {
                return this._delivertocode;
            }
            set {
                this._delivertocode = value;
            }
        }
        
        /// This is the comment for _delivertoname
        [System.Xml.Serialization.XmlElementAttribute("DeliverToName", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ERPPropertiesDeliverToName[] DeliverToName {
            get {
                return this._delivertoname;
            }
            set {
                this._delivertoname = value;
            }
        }
        
        /// This is the comment for _itemrequested
        [System.Xml.Serialization.XmlElementAttribute("ItemRequested", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ERPPropertiesItemRequested[] ItemRequested {
            get {
                return this._itemrequested;
            }
            set {
                this._itemrequested = value;
            }
        }
        
        /// This is the comment for _unit
        [System.Xml.Serialization.XmlElementAttribute("Unit", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ERPPropertiesUnit[] Unit {
            get {
                return this._unit;
            }
            set {
                this._unit = value;
            }
        }
        
        /// This is the comment for _unitprice
        [System.Xml.Serialization.XmlElementAttribute("UnitPrice", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ERPPropertiesUnitPrice[] UnitPrice {
            get {
                return this._unitprice;
            }
            set {
                this._unitprice = value;
            }
        }
        
        /// This is the comment for _catalognumber
        [System.Xml.Serialization.XmlElementAttribute("CatalogNumber", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ERPPropertiesCatalogNumber[] CatalogNumber {
            get {
                return this._catalognumber;
            }
            set {
                this._catalognumber = value;
            }
        }
        
        /// This is the comment for _project
        [System.Xml.Serialization.XmlElementAttribute("Project", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ERPPropertiesProject[] Project {
            get {
                return this._project;
            }
            set {
                this._project = value;
            }
        }
        
        /// This is the comment for _notonapprovaltrack
        [System.Xml.Serialization.XmlElementAttribute("NotOnApprovalTrack", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ERPPropertiesNotOnApprovalTrack[] NotOnApprovalTrack {
            get {
                return this._notonapprovaltrack;
            }
            set {
                this._notonapprovaltrack = value;
            }
        }
        
        /// This is the comment for _approvalnotapo
        [System.Xml.Serialization.XmlElementAttribute("ApprovalNotAPO", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ERPPropertiesApprovalNotAPO[] ApprovalNotAPO {
            get {
                return this._approvalnotapo;
            }
            set {
                this._approvalnotapo = value;
            }
        }
        
        /// This is the comment for _approvalisapo
        [System.Xml.Serialization.XmlElementAttribute("ApprovalIsAPO", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ERPPropertiesApprovalIsAPO[] ApprovalIsAPO {
            get {
                return this._approvalisapo;
            }
            set {
                this._approvalisapo = value;
            }
        }
        
        /// This is the comment for _comments
        [System.Xml.Serialization.XmlElementAttribute("Comments", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ERPPropertiesComments[] Comments {
            get {
                return this._comments;
            }
            set {
                this._comments = value;
            }
        }
    }
    
    /// <remarks/>
    public class ERPPropertiesDeliverToCode {
        
        /// <remarks/>
        private string _visible;
        
        /// This is the comment for _visible
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Visible {
            get {
                return this._visible;
            }
            set {
                this._visible = value;
            }
        }
    }
    
    /// <remarks/>
    public class ERPPropertiesComments {
        
        /// <remarks/>
        private string _visible;
        
        /// This is the comment for _visible
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Visible {
            get {
                return this._visible;
            }
            set {
                this._visible = value;
            }
        }
    }
    
    /// <remarks/>
    public class ERPPropertiesApprovalIsAPO {
        
        /// <remarks/>
        private string _visible;
        
        /// This is the comment for _visible
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Visible {
            get {
                return this._visible;
            }
            set {
                this._visible = value;
            }
        }
    }
    
    /// <remarks/>
    public class ERPPropertiesApprovalNotAPO {
        
        /// <remarks/>
        private string _visible;
        
        /// This is the comment for _visible
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Visible {
            get {
                return this._visible;
            }
            set {
                this._visible = value;
            }
        }
    }
    
    /// <remarks/>
    public class ERPPropertiesNotOnApprovalTrack {
        
        /// <remarks/>
        private string _visible;
        
        /// This is the comment for _visible
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Visible {
            get {
                return this._visible;
            }
            set {
                this._visible = value;
            }
        }
    }
    
    /// <remarks/>
    public class ERPPropertiesProject {
        
        /// <remarks/>
        private string _visible;
        
        /// This is the comment for _visible
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Visible {
            get {
                return this._visible;
            }
            set {
                this._visible = value;
            }
        }
    }
    
    /// <remarks/>
    public class ERPPropertiesCatalogNumber {
        
        /// <remarks/>
        private string _visible;
        
        /// This is the comment for _visible
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Visible {
            get {
                return this._visible;
            }
            set {
                this._visible = value;
            }
        }
    }
    
    /// <remarks/>
    public class ERPPropertiesUnitPrice {
        
        /// <remarks/>
        private string _editable;
        
        /// This is the comment for _editable
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Editable {
            get {
                return this._editable;
            }
            set {
                this._editable = value;
            }
        }
    }
    
    /// <remarks/>
    public class ERPPropertiesUnit {
        
        /// <remarks/>
        private string _editable;
        
        /// This is the comment for _editable
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Editable {
            get {
                return this._editable;
            }
            set {
                this._editable = value;
            }
        }
    }
    
    /// <remarks/>
    public class ERPPropertiesItemRequested {
        
        /// <remarks/>
        private string _editable;
        
        /// <remarks/>
        private string _required;
        
        /// This is the comment for _editable
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Editable {
            get {
                return this._editable;
            }
            set {
                this._editable = value;
            }
        }
        
        /// This is the comment for _required
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Required {
            get {
                return this._required;
            }
            set {
                this._required = value;
            }
        }
    }
    
    /// <remarks/>
    public class ERPPropertiesDeliverToName {
        
        /// <remarks/>
        private string _visible;
        
        /// This is the comment for _visible
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Visible {
            get {
                return this._visible;
            }
            set {
                this._visible = value;
            }
        }
    }

	/// <summary>
	/// The General Ledger Account ledger class
	/// </summary>
	public class ERPPropertiesGLAccountLedger 
	{
        
		/// <remarks/>
		private string _visible;
        
		/// This is the comment for _visible
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string Visible 
		{
			get 
			{
				return this._visible;
			}
			set 
			{
				this._visible = value;
			}
		}
	}
}

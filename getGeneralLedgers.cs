
namespace Jenzabar.CRM.Staff.Web.Portlets.GLAccountLookupPortlet 
{
	/// <summary>
	/// Summary description for getGeneralLedgers.
	/// </summary>
	[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
	public class GeneralLedgers
	{
		private AcctLedger[]_ledgers;

		
		/// <summary>
		/// This is an array of ledger objects
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute("Ledger", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public AcctLedger[]Ledger
		{
			get
			{
				return _ledgers;
			}
			set
			{
				_ledgers = value;
			}
		}

	}


	/// <summary>
	/// The ledger class that will contain the information for each
	/// ledger object.
	/// </summary>
	public class AcctLedger
	{
		private string _LedgerName;
		private string _LedgerID;

		/// <summary>
		/// The ledger account's ID
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string ID
		{
			get
			{
				return _LedgerID;
			}
			set
			{
				_LedgerID = value;
			}
		}

		
		/// <summary>
		/// The ledger account's name
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string Name
		{
			get
			{
				return _LedgerName;
			}
			set
			{
				_LedgerName = value;
			}
		}


	}
}

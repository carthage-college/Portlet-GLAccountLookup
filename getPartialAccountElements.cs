namespace Jenzabar.CRM.Staff.Web.Portlets.GLAccountLookupPortlet 
{
	/// <remarks/>
	// XML from the ERPStaff.GLAccountLookup plug-in is used to populate this class.
	[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
	public class PartialAccountElements 
	{
		private Element[] _accountElement;

		[System.Xml.Serialization.XmlElementAttribute("Element", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public Element[] Elements
		{
			get
			{
				return _accountElement;
			}
			set
			{
				_accountElement = value;
			}
		}
	}


	/// <summary>
	/// This class will be be populated by the data for each element.
	/// </summary>
	[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
	public class Element
	{
		private string _elementID;
		private string _elementName;
		private string _elementSequence;
		private int _elementMaxLength;


		//The element ID
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string ID
		{

			get
			{
				return _elementID;
			}
			set
			{
				_elementID = value;
			}	
		}

		//The Element Name
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string Name
		{

			get
			{
				return _elementName;
			}
			set
			{
				_elementName = value;
			}	
		}

		//The Element's Maximum Length
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public int MaximumLength
		{

			get
			{
				return _elementMaxLength;
			}
			set
			{
				_elementMaxLength = value;
			}	
		}

		//The Element's Sequence Number
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string Sequence
		{

			get
			{
				return _elementSequence;
			}
			set
			{
				_elementSequence = value;
			}	
		}

	}
}

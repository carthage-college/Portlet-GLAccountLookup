using System.Collections;
using System.Xml.Serialization;
using Jenzabar.CRM.Deserializers;
using Jenzabar.CRM.Utility;
using Jenzabar.Portal.Framework;

namespace Jenzabar.CRM.Staff.Web.Portlets.GLAccountLookupPortlet
{
	/// <summary>
	/// Provides data and default values for the Item Approval Preferences screen.
	/// </summary>
	public class GLAccountLookupNameValueProvider : INameValueProvider
	{
		public GLAccountLookupNameValueProvider()
		{
		}
		#region INameValueProvider Members

		public NameValueDataSource[] RetrieveItems(string name, object key)
		{
			ArrayList arrayList = new ArrayList();
			NameValueDataSource myNVDS = null;
			NameValueDataSource[] myNVDSs = null;

			switch (name)
			{
				case "GLAccountSortOrder":
					myNVDS = NameValueDataSource.Create(GLALPMessages.TXT_ACCOUNT_NUMBER, GLALPMessages.TXT_ACCOUNT_NUMBER);
					arrayList.Add(myNVDS);
					myNVDS = NameValueDataSource.Create(GLALPMessages.TXT_ACCOUNT_DESCRIPTION, GLALPMessages.TXT_ACCOUNT_DESCRIPTION);
					arrayList.Add(myNVDS);
					break;

				case "DefaultBudgetLookup":
					myNVDS = NameValueDataSource.Create(GLALPMessages.TXT_FULL_ANNUAL, GLALPMessages.TXT_FULL_ANNUAL);
					arrayList.Add(myNVDS);
					myNVDS = NameValueDataSource.Create(GLALPMessages.TXT_PERIOD_RANGE, GLALPMessages.TXT_PERIOD_RANGE);
					arrayList.Add(myNVDS);
					myNVDS = NameValueDataSource.Create(GLALPMessages.TXT_NO_BUDGET_INFORMATION, GLALPMessages.TXT_NO_BUDGET_INFORMATION);
					arrayList.Add(myNVDS);
					break;

				//TTP 6579 functionality for future version
				case "DefaultSelectedYear":
					string strXML = null;
					string strError = null;
					object[] PlParam= new object[1];
					PlParam[0] = "GLAccountLookup";
					myNVDS = NameValueDataSource.Create("Select a Year","-1");
					arrayList.Add(myNVDS);
					try
					{
						//HACK: To make this portlet fully StructureMap-compatible, this should call GetInstance<ICommon>().
						new Jenzabar.ERP.Common()
							.GetERPPortletListBox(PlParam[0].ToString(), ref strXML, ref strError);
						strXML = strXML.Replace("<ListBoxes>","<ListBoxes><GLAccountLookup>");
						strXML = strXML.Replace("</ListBoxes>","</GLAccountLookup></ListBoxes>");
						ListBoxes lb = (ListBoxes)PlugIn.MapXMLToObject(strXML, new XmlSerializer(typeof(ListBoxes)));
				
						if (lb.Items != null)
						{	
							for (int i=0; i<lb.Items.Length;i++)
							{
								if(lb.Items[i].Years != null)
								{
									
									for (int j=0; j<lb.Items[i].Years.Length;j++)
									{
										myNVDS = NameValueDataSource.Create(lb.Items[i].Years[j].Year,lb.Items[i].Years[j].Year);
										arrayList.Add(myNVDS);
										//ListItem li = new ListItem(lb.Items[i].Years[j].Year,lb.Items[i].Years[j].Year);
										//this.ddlYear.Items.Add(li);
									}
								}
							}
						}
					}
					catch
					{
						myNVDS = NameValueDataSource.Create("--","");
						arrayList.Add(myNVDS);
					}
					
					//myNVDS = NameValueDataSource.Create("Current", "C");
					break;


			}
			myNVDSs = (NameValueDataSource[])arrayList.ToArray(typeof(NameValueDataSource));
			return myNVDSs;
		}

		public NameValueDataSource[] RetrieveDefaultItems(string name, object key)
		{
			ArrayList arrayList = new ArrayList();
			NameValueDataSource myNVDS = null;
			NameValueDataSource[] myNVDSs = null;

			switch(name)       
			{         
				case "GLAccountSortOrder":
					myNVDS = NameValueDataSource.Create(GLALPMessages.TXT_ACCOUNT_DESCRIPTION, GLALPMessages.TXT_ACCOUNT_DESCRIPTION);
					arrayList.Add(myNVDS);
					break;

				case "DefaultBudgetLookup":
					myNVDS = NameValueDataSource.Create(GLALPMessages.TXT_FULL_ANNUAL, GLALPMessages.TXT_FULL_ANNUAL);
					arrayList.Add(myNVDS);
					break;

				default:
					myNVDS = NameValueDataSource.Create(GLALPMessages.TXT_ACCOUNT_DESCRIPTION, GLALPMessages.TXT_ACCOUNT_DESCRIPTION);
					arrayList.Add(myNVDS);
					break;
			}

			myNVDSs = (NameValueDataSource[])arrayList.ToArray(typeof(NameValueDataSource));
			return myNVDSs;
		}

		#endregion
	}
}

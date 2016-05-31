using System;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using Jenzabar.CRM.Utility;
using Jenzabar.Portal.Framework.Web.UI;

namespace Jenzabar.CRM.Staff.Web.Portlets.GLAccountLookupPortlet
{
	public class ViewOtherInformation : PortletViewBase
	{
		protected System.Web.UI.WebControls.Label lblHeader;
		protected System.Web.UI.WebControls.DataGrid dgOtherTransInfo;
		protected System.Web.UI.WebControls.Label lblError;
		protected OtherTranInfo ot;
	
		public override string ViewName 
		{
			get
			{
				return GLALPConstants.VIEW_OTHER_INFORMATION_SCREEN;
			}
		}

		#region Web Form Designer generated code

		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dgOtherTransInfo.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgOtherTransInfo_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(this.IsFirstLoad)
			{
				Initialize_Objects();
			}
		
		}

		private void Initialize_Objects()
		{
			object[] PluginParam= new object[1];
			string strXML = "";
			string strError = "";

			PluginParam[0] = this.ParentPortlet.PortletViewState["TransKey"].ToString();

			// Serialize XML form plugin
			try
			{
				this
					.GetInstance<IStaff>()
					.GetOtherTransactionInfo(this.ParentPortlet.PortletViewState["TransKey"].ToString(), ref strXML, ref strError);

				int nPos1 = strXML.IndexOf( "<ErrorText>", 0 );
				if(nPos1 >= 0)
				{
					int nPos2 = strXML.IndexOf( "</ErrorText>", 0 );
					this.lblError.Text = strXML.Substring(nPos1+11,nPos2); //Get the ErrorText
				}
				else
				{
					if(strXML.IndexOf( "<Header>", 0 )>0)
					{

						ot = (OtherTranInfo)PlugIn.MapXMLToObject(strXML, new XmlSerializer(typeof(OtherTranInfo)));
					
						if (ot.Items != null)
						{	
							this.lblHeader.Text = ot.Items[0].Header.ToString();
							this.lblHeader.Font.Bold = true;
							this.dgOtherTransInfo.DataSource = ot.Items[0].TranInfo;
							this.dgOtherTransInfo.DataBind();

						}
					}
				}
			}
			catch (System.Exception ex)
			{
				this.lblError.Text = ex.GetBaseException().Message;
			}
		}


		private void dgOtherTransInfo_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{

			//First, make sure we are dealing with an Item or AlternatingItem
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				if(Convert.ToString(System.Web.UI.DataBinder.Eval(e.Item.DataItem,"DisplayValue[0].email")).Length > 0)
				{
					((HyperLink)e.Item.FindControl("hlnkDisplayValue")).Visible = true;
				}
				else
				{
					((Label)e.Item.FindControl("lblDisplayValue")).Visible = true;				
				}
			}

		
		}
	}
}

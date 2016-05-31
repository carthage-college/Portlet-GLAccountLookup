using System;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using Jenzabar.CRM;
using Jenzabar.Common;
using Jenzabar.Common.Encryption;
using Jenzabar.CRM.Staff.Web.Portlets.GLAccountLookupPortlet;
using Jenzabar.CRM.Utility;
using Jenzabar.Portal.Framework;


namespace Jenzabar.Portal.Web.Applications.CRM.Portlets.Staff.GLAccountLookupPortlet
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class ViewOtherInfo : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblHeader;
		protected System.Web.UI.WebControls.DataGrid dgOtherTransInfo;
		protected System.Web.UI.WebControls.Label lblError;
		protected OtherTranInfo ot;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
			    CheckSecurity();
				Initialize_Objects();
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
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.dgOtherTransInfo.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgOtherTransInfo_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

        private void CheckSecurity()
        {
            var encryptor = new Encryptor(new KeyProvider());
            var token = encryptor.Decrypt(Request.QueryString["x"]).Split('|');
            var username = token[0];
            var timestamp = DateTime.Parse(token[1]);

            if (username != PortalUser.Current.Username ||
                timestamp.AddMinutes(20) < DateTime.Now)
            {
                Response.Write("Access denied.");
                Response.End();
            }
        }

		private void Initialize_Objects()
		{
			object[] PluginParam= new object[1];
			string strXML = "";
			string strError = "";

			PluginParam[0] = Convert.ToString(Request.QueryString["TransKey"]);		//strTransKey.ToString(); //this.ParentPortlet.PortletViewState["TransKey"].ToString();

			// Serialize XML form plugin
			try
			{
				//HACK: For testing purposes, we should ideally use StructureMap, without having to worry about versioning issues.
				new Jenzabar.ERP.Staff()
					.GetOtherTransactionInfo(Convert.ToString(Request.QueryString["TransKey"]), ref strXML, ref strError);

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

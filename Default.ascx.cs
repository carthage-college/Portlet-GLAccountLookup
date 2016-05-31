using System;
using Jenzabar.Portal.Framework.Web.UI;


namespace Jenzabar.CRM.Staff.Web.Portlets.GLAccountLookupPortlet
{
	public class Default : PortletViewBase
	{
		protected System.Web.UI.WebControls.LinkButton lnkLookup;
	
		public override string ViewName 
		{
			get
			{
				return "";
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
			this.lnkLookup.Click += new System.EventHandler(this.lnkLookup_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			Initialize_Globalization();
		}

		private void lnkLookup_Click(object sender, System.EventArgs e)
		{
			this.ParentPortlet.State = PortletState.Maximized;
			this.ParentPortlet.ChangeScreen(GLALPConstants.ACCOUNT_SELECTION_SCREEN);
		}

		private void Initialize_Globalization()
		{
			this.lnkLookup.Text = GLALPMessages.TXT_LOOKUP_GL_ACCOUNT_INFORMATION;
		}

	}
}

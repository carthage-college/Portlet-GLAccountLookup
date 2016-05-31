using System;
using Jenzabar.Portal.Framework;
using Jenzabar.Portal.Framework.Web.UI;
using Jenzabar.Portal.Framework.Web.UI.Controls.MetaControls;
using Jenzabar.Portal.Framework.Web.UI.Controls.MetaControls.Attributes;

namespace Jenzabar.CRM.Staff.Web.Portlets.GLAccountLookupPortlet
{
	#region Operations
        
	#endregion

	#region Preferences

//	[DropDownListMetaControl(0, "GLAccountSortOrder","LBL_GL_ACCOUNT_LOOKUP_GLAccountSortOrder", "LBL_GL_ACCOUNT_LOOKUP_GL_Account_Sort_Order", false, 
//		 "Portlet.GLAccountLookup,Jenzabar.CRM.Staff.Web.Portlets.GLAccountLookupPortlet.GLAccountLookupNameValueProvider",
//		 "Portlet.GLAccountLookup,Jenzabar.CRM.Staff.Web.Portlets.GLAccountLookupPortlet.GLAccountLookupNameValueProvider",
//		 NameValueDataSourceType.Dynamic, NameValueType.PortletPreference, "MetaControl")]
//
//	[TextBoxMetaControl(1,"GLAccountFilterStartingPosition","LBL_GL_ACCOUNT_LOOKUP_GLAccountFilterStartingPosition","LBL_GL_ACCOUNT_LOOKUP_GL_Account_Filter_Starting_Position",false,
//		@"<NameValueDataSources><NameValueDataSource Name='GLAccountFilterStartingPosition' Value='' /></NameValueDataSources>",
//		NameValueDataSourceType.Static,NameValueType.PortletPreference, "MetaControl")]
//
//	[TextBoxMetaControl(2,"GLAccountFilterLength","LBL_GL_ACCOUNT_LOOKUP_GLAccountFilterLength","LBL_GL_ACCOUNT_LOOKUP_GL_Account_Filter_Length",false,
//		 @"<NameValueDataSources><NameValueDataSource Name='GLAccountFilterLength' Value='' /></NameValueDataSources>",
//		 NameValueDataSourceType.Static,NameValueType.PortletPreference, "MetaControl")]
//
//	[TextBoxMetaControl(3,"GLAccountFilterComponentValue","LBL_GL_ACCOUNT_LOOKUP_GLAccountFilterComponentValue","LBL_GL_ACCOUNT_LOOKUP_GL_Account_Filter_Component_Value",false,
//		 @"<NameValueDataSources><NameValueDataSource Name='GLAccountFilterComponentValue' Value='' /></NameValueDataSources>",
//		 NameValueDataSourceType.Static,NameValueType.PortletPreference, "MetaControl")]

	[DropDownListMetaControl(4,"DefaultBudgetLookup","LBL_GL_ACCOUNT_LOOKUP_DefaultBudgetLookup", "LBL_GL_ACCOUNT_LOOKUP_Default_Budget_Lookup", false, 
		 "Portlet.GLAccountLookup,Jenzabar.CRM.Staff.Web.Portlets.GLAccountLookupPortlet.GLAccountLookupNameValueProvider",
		 "Portlet.GLAccountLookup,Jenzabar.CRM.Staff.Web.Portlets.GLAccountLookupPortlet.GLAccountLookupNameValueProvider",
		 NameValueDataSourceType.Dynamic, NameValueType.PortletPreference, "MetaControl")]

//TTP 6579 functionality for future version
	[DropDownListMetaControl(4,"DefaultSelectedYear","LBL_GL_ACCOUNT_PREF_DEF_YEAR", "LBL_GL_ACCOUNT_PREF_DEF_YEAR_DESC", false, 
		 "Portlet.GLAccountLookup,Jenzabar.CRM.Staff.Web.Portlets.GLAccountLookupPortlet.GLAccountLookupNameValueProvider",
		 "Portlet.GLAccountLookup,Jenzabar.CRM.Staff.Web.Portlets.GLAccountLookupPortlet.GLAccountLookupNameValueProvider",
		 NameValueDataSourceType.Dynamic, NameValueType.PortletPreference, "MetaControl")]

    /*[CheckBoxListMetaControl(1, "DefaultBudgetLookup", "Default Budget Lookup", "",false,@"<NameValueDataSources></NamValueDataSources>",@"<NameValueDataSources></NamValueDataSources>",NameValueDataSourceType.Static, NameValueType.PortletPreference, "MetaControl")] 
    [CheckBoxListMetaControl(2, "GLAccountFilterComponentValue", "GL Account Filter Component Value", "",false,@"<NameValueDataSources></NamValueDataSources>",@"<NameValueDataSources></NamValueDataSources>",NameValueDataSourceType.Static, NameValueType.PortletPreference, "MetaControl")] 
    [CheckBoxListMetaControl(3, "GLAccountSortOrder", "GL Account Sort Order", "",false,@"<NameValueDataSources></NamValueDataSources>",@"<NameValueDataSources></NamValueDataSources>",NameValueDataSourceType.Static, NameValueType.PortletPreference, "MetaControl")] 
    [CheckBoxListMetaControl(4, "GLAccountFilterComponentSequence", "GL Account Filter Component Sequence", "",false,@"<NameValueDataSources></NamValueDataSources>",@"<NameValueDataSources></NamValueDataSources>",NameValueDataSourceType.Static, NameValueType.PortletPreference, "MetaControl")] */

	#endregion

	#region Settings

    /*[CheckBoxListMetaControl(1, "GLAccountDelimiter", "GL Account Delimiter", "",false,@"<NameValueDataSources></NamValueDataSources>",@"<NameValueDataSources></NamValueDataSources>",NameValueDataSourceType.Static, NameValueType.PortletPreference, "MetaControl")] 

	
	[RadioButtonListMetaControl(0, "PortletType", "Portlet Type", 
		"Choose whether the portlet is used for personal or shared .",
		false,
		@"<NameValueDataSources>
			<NameValueDataSource Name='Personal' Value='Personal' />
		</NameValueDataSources>",
		@"<NameValueDataSources>
			<NameValueDataSource Name='Personal' Value='Personal' />
			<NameValueDataSource Name='Shared' Value='Shared' />
		</NameValueDataSources>",
		NameValueDataSourceType.Static, NameValueType.PortletSetting, "MetaControl")]
	*/
	#endregion
	
	public class GLAccountLookupPortlet : CRMPortletBase, ICssProvider
	{

		/// <summary>
		/// Constructor for GLAccountLookup class.
		/// </summary>
		public GLAccountLookupPortlet()
		{
			
		}
		
		/// <summary>
		/// The portlet's views, overlays, help status, and other content are 
		/// loaded into place during the OnInit() method.  If it is overridden in 
		/// a child class, base.OnInit(e) must be called to ensure proper operation.
		/// </summary>
		protected override void OnInit(EventArgs e)
		{
			this.Toolbar.ItemCommand += new System.Web.UI.WebControls.CommandEventHandler(Toolbar_ItemCommand);
			base.OnInit (e);
		}		

		/// <summary>
		/// The GetCurrentScreen method returns the current screen.
		/// </summary>
		/// <returns>PortletViewBase</returns>
		protected override PortletViewBase GetCurrentScreen()
		{
			PortletViewBase targetScreen = null;

			switch(this.CurrentPortletScreenName)
			{
	            case GLALPConstants.BALANCE_TRANSACTION_DETAIL_SCREEN:
	                   targetScreen = this.LoadPortletView("Staff/Portlet.GLAccountLookup/BalanceTransactionDetail.ascx");
	                   break;
	
	            case GLALPConstants.DEFAULT_SCREEN:
	                   targetScreen = this.LoadPortletView("Staff/Portlet.GLAccountLookup/Default.ascx");
	                   break;
	
	            case GLALPConstants.BUDGET_TO_ACTUAL_SCREEN:
	                   targetScreen = this.LoadPortletView("Staff/Portlet.GLAccountLookup/BudgetToActual.ascx");
	                   break;
	
	            case GLALPConstants.ACCOUNT_SELECTION_SCREEN:
	                   targetScreen = this.LoadPortletView("Staff/Portlet.GLAccountLookup/AccountSelection.ascx");
	                   break;
	
	            case GLALPConstants.VIEW_OTHER_INFORMATION_SCREEN:
	                   targetScreen = this.LoadPortletView("Staff/Portlet.GLAccountLookup/ViewOtherInformation.ascx");
	                   break;
	
				default:
					// TODO: Set this to be your default screen name.
					targetScreen = this.LoadPortletView("Staff/Portlet.GLAccountLookup/Default.ascx");
					break;
			}
			return targetScreen;
		}

		#region Sample Menu Item Code
		/// <summary>
		/// The PopulateToolbar() method will create the toolbar and add menu items. 
		/// </summary>
		/// <returns>bool</returns>
		/*protected override bool PopulateToolbar(Jenzabar.Common.Web.UI.Controls.Toolbar toolbar)
		{
			#region Sample Menu Item Code
			
			Jenzabar.Common.Web.UI.Controls.MenuItem manage = toolbar.MenuItems.Add( Messages.TXT_MANAGE );
			Jenzabar.Common.Web.UI.Controls.MenuItem menuItem = new Jenzabar.Common.Web.UI.Controls.MenuItem(GLAccountLookupMessages.TXT_MENU_PROGRAM, GLAccountLookupConstants.CMD_PROGRAM);
			Jenzabar.Common.Web.UI.Controls.MenuItem subMenuItem = new Jenzabar.Common.Web.UI.Controls.MenuItem();
			subMenuItem.Text = "";
			subMenuItem.CommandName = "";
			menuItem.MenuItems.Add(subMenuItem);
			manage.MenuItems.Add(menuItem);
			
			#endregion
	
			return true;

		}*/	
		#endregion

		/// <summary>
		/// The Toolbar_ItemCommand() event responds to clicked toolbar items. 
		/// </summary>		
		private void Toolbar_ItemCommand(object sender, System.Web.UI.WebControls.CommandEventArgs e)
		{
			#region Sample Toolbar Item Code
			/*
			MaximizeCheck();
			this.ForceReload = true;
			// Go to selected screen
			this.ChangeScreen(e.CommandName);
			*/
			#endregion	
		}

		#region Event Handlers
		#endregion
		
		#region ICssProvider Members

		public string CssClass { get{ return "GLAccountLookup"; } }

		public string CssFileLocation { get{ return "Portlets/CRM/Staff/Portlet.GLAccountLookup/GLAccountLookupPortlet.css"; } }

		#endregion
	}
}


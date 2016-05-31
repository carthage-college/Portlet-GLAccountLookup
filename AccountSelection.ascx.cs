using System;
using System.Collections;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using Jenzabar.Common.Globalization;
//using Jenzabar.Common.Configuration;
using Jenzabar.CRM.Deserializers;
using Jenzabar.CRM.Utility;
using Jenzabar.Portal.Framework;
//using Jenzabar.Portal.Framework.Web.Configuration;
using Jenzabar.Portal.Framework.Web.UI;
using Jenzabar.Portal.Web.UI.Controls;
using Settings = Jenzabar.Portal.Framework.Configuration.Settings;

namespace Jenzabar.CRM.Staff.Web.Portlets.GLAccountLookupPortlet
{
	public class AccountSelection : PortletViewBase
	{
		protected System.Web.UI.WebControls.Label lblYear;
		protected System.Web.UI.WebControls.Label lblBeginPeriod;
		protected System.Web.UI.WebControls.Label lblBudget;
		protected System.Web.UI.WebControls.DropDownList ddlYear;
		//protected System.Web.UI.WebControls.DropDownList ddlBegPeriod;
		protected System.Web.UI.WebControls.DropDownList ddlEndPeriod;
		protected System.Web.UI.WebControls.Button btnLookup;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.DropDownList ddlBudget;
		protected Jenzabar.Common.Web.UI.Controls.ContentTabGroup MainScreenTabs;
		protected Jenzabar.Common.Web.UI.Controls.ContentTab tbFull;
		protected Jenzabar.Common.Web.UI.Controls.ContentTab tbPartial;
		protected Jenzabar.Common.Web.UI.Controls.ContentTab tbSelect;
		protected System.Web.UI.WebControls.Label lblAcctNumRange;
		protected System.Web.UI.WebControls.Label lblBegAcctNum;
		protected System.Web.UI.WebControls.Label lblEnAcctNum;
		protected System.Web.UI.WebControls.Label lblAcctNumSel;
		protected System.Web.UI.WebControls.Label lblDRForTrans;

		protected System.Web.UI.WebControls.TextBox txtBeginAcctNum;
		protected System.Web.UI.WebControls.TextBox txtEndAcctNum;
		protected System.Web.UI.WebControls.TextBox txtFund;
		protected System.Web.UI.WebControls.TextBox txtDept;
		protected System.Web.UI.WebControls.TextBox txtObject;
		protected Hashtable htSearchSavedValues;
		
		protected System.Web.UI.HtmlControls.HtmlTable tblDRControls;


		protected System.Web.UI.WebControls.DropDownList ddlLedger;
		protected System.Web.UI.WebControls.Label lblLedger;
		protected System.Web.UI.WebControls.Label lblTabIns;
		protected Jenzabar.Common.Web.UI.Controls.Hint hntFullAcct;
		protected Jenzabar.Common.Web.UI.Controls.Hint hntPartAcct;

		protected System.Web.UI.WebControls.Label lblResultsPerPage;
		protected System.Web.UI.WebControls.DropDownList ddlResultsPerPage;
		protected Jenzabar.Portal.Web.UI.Controls.JenzabarGLAccountLookup JenzabarGLAccountLookup;

		//Hashtable htPartAcctControls; 
        //The maximum number of rows we will dynamically create for the partial search
		//controls.
		private const int MAX_PART_ACCT_CTRL_ROWS = 3;
		private const int TAB_FULL = 0;
		private const int TAB_PART = 1;
		private const int TAB_SEL = 2;

		const string SEARCH_TYPE_RANGE = "RANGE";
		const string SEARCH_TYPE_PARTIAL = "PARTIAL";
		const string SEARCH_TYPE_ACCOUNTS = "ACCOUNTS";
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnSelSort;
		protected System.Web.UI.WebControls.DropDownList ddlBegPeriod;
		protected System.Web.UI.WebControls.Label lblEndPeriod;

		string strShowGLLedger = null;
		string strRefineSearch = null;
		
		
		public override string ViewName 
		{
			get
			{
				return GLALPConstants.ACCOUNT_SELECTION_SCREEN;
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
			this.btnLookup.Click += new System.EventHandler(this.btnLookup_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new System.EventHandler(this.AccountSelection_PreRender);
			if(this.ParentPortlet.Session["glAlPartLookupData"]==null)
			{
				htSearchSavedValues = new Hashtable();
			}
			else
			{
				htSearchSavedValues = (Hashtable)this.ParentPortlet.Session["glAlPartLookupData"];
			}

		}
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			string strTotalAccounts = null;
			

			//We use this flag to tell us whether the user clicked the "start new search" button on
			//the BudgetToActual screen.
			strRefineSearch = ((this.ParentPortlet.PortletViewState[GLALPConstants.VS_GL_REFINE_SEARCH]!=null)?this.ParentPortlet.PortletViewState[GLALPConstants.VS_GL_REFINE_SEARCH].ToString():"N");
			

			GenerateJavascript();
			
			//Set Labels and Text
			Initialize_Globalization();

				
			//Populate the partial account number search 
			//controls.
			CreatePartSearchControls();

			
			
			//Initialize the account control
			InitAccountControl();


			
			if (this.IsFirstLoad)
			{
						
				try
				{
					//if (CheckForNumberOfAccounts() > ERPSettings.MaximumGLAccountsToDisplay)
					//Uncomment the line above once the framework team implements it.

					//Get the number of accounts and save it in a viewstate value
					strTotalAccounts = (CheckForNumberOfAccounts()).ToString();
					this.ParentPortlet.PortletViewState[GLALPConstants.VS_GL_TOTAL_ACCT_COUNT]
						= strTotalAccounts;
					

					CallGetERPPortletProperties();

					//DataBind for ddls
					Initialize_Controls();

					//Initialize_Objects();

//					//If this is CX then call
//					if(Settings.Current.ERPType.ToString()=="CX")
//					{
//						PopulateLedger();
//					}

					//If the ERP General Ledger visible property is set
					//to "Y" then show the dropdown.
					strShowGLLedger = ((this.ParentPortlet.PortletViewState["ERP_GL_ACCT_LEDGER"]!=null)?this.ParentPortlet.PortletViewState["ERP_GL_ACCT_LEDGER"].ToString():"Y");
					if(strShowGLLedger.ToUpper().Trim()=="Y")
					{
						PopulateLedger();
					}

					//Populate dropdown
					PopulateResultsPerPageDropdown();

					//Initialize the account control
					//InitAccountControl();

				}
				catch (System.Exception ex)
				{
					this.lblError.Text = ex.GetBaseException().Message;
				}
			}
			else
			{
				strTotalAccounts = ((this.ParentPortlet.PortletViewState[GLALPConstants.VS_GL_TOTAL_ACCT_COUNT]!=null)?this.ParentPortlet.PortletViewState[GLALPConstants.VS_GL_TOTAL_ACCT_COUNT].ToString():"0");
				strShowGLLedger = ((this.ParentPortlet.PortletViewState["ERP_GL_ACCT_LEDGER"]!=null)?this.ParentPortlet.PortletViewState["ERP_GL_ACCT_LEDGER"].ToString():"Y");
	
			}

			//Set the default tab-I am keeping this code outside of the first decision block for
			//isFirstLoad just in case I need to comment the first decision statement out on a
			//later date.
			if(this.MainScreenTabs.ContentTabs.Count > 0 && this.IsFirstLoad ==true)
			{
				string strSelTab = ((this.ParentPortlet.PortletViewState[GLALPConstants.VS_GL_SELECTED_TAB_VAL]!=null)? this.ParentPortlet.PortletViewState[GLALPConstants.VS_GL_SELECTED_TAB_VAL].ToString(): System.Convert.ToString(TAB_FULL));
				SetSelectedTab(strSelTab);
				if(strRefineSearch.ToUpper()=="Y")
				{
					//If the user had selected a tab previously then we will use that value
//					string strSelTab = ((this.ParentPortlet.PortletViewState[GLALPConstants.VS_GL_SELECTED_TAB_VAL]!=null)? this.ParentPortlet.PortletViewState[GLALPConstants.VS_GL_SELECTED_TAB_VAL].ToString(): System.Convert.ToString(TAB_FULL));
					//SetSelectedTab(strSelTab);
					//reset the value
					strRefineSearch = "N";
				}
				else
				{
					//this.MainScreenTabs.ContentTabs[TAB_FULL].Selected = true;
				}
			}
					
			//if (CheckForNumberOfAccounts() > ERPSettings.MaximumGLAccountsToDisplay)
			//Uncomment the line above once the framework team implements it.

			//Get the number of accounts and either display or hide
			//the third tab.
			if (System.Convert.ToInt32(strTotalAccounts) > Settings.Current.MaxGLAcctsToDisplay)
			{
				this.tbSelect.Visible = false;
				//TTP 13130
				this.JenzabarGLAccountLookup.AutoPopulateAccountCodes = false;
			}
			else
			{
				this.tbSelect.Visible = true;
				//TTP 13130
				this.JenzabarGLAccountLookup.AutoPopulateAccountCodes = true;
				
			}

			//Get the ERP property values
			//strShowGLLedger = ((this.ParentPortlet.PortletViewState["ERP_GL_ACCT_LEDGER"]!=null)?this.ParentPortlet.PortletViewState["ERP_GL_ACCT_LEDGER"].ToString():"Y");
			if(strShowGLLedger.ToUpper() =="N")
			{
				this.lblLedger.Visible = false;
				this.ddlLedger.Visible = false;
			}

			else
			{
				this.lblLedger.Visible = true;
				this.ddlLedger.Visible = true;
			}

			//AddJavascriptEvents();
		}

	
		private void AccountSelection_PreRender(object sender, System.EventArgs e)
		{
			SortedList slSavedValues = (SortedList)this.ParentPortlet.Session["glAlSelLookupData"];
			if(slSavedValues !=null )
			{
				if(slSavedValues.Count > 0)
				{
					JenzabarGLAccountLookup.AccountsSelected = slSavedValues;
					slSavedValues.Clear();
				}
				this.ParentPortlet.Session.Remove("glAlSelLookupData");
			}
		}

	
		#region Custom Methods

		private void GenerateJavascript()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<script language=\"JavaScript\">");

			//This function will save the selected sort by value in a hidden field.
			sb.Append("function SetSelectedSort (hdnCtlID, sortByID) {" + Environment.NewLine);
			sb.Append("var hdnField = document.getElementById(hdnCtlID); "+ Environment.NewLine);
			sb.Append("var sortBy = document.getElementById(sortByID); "+ Environment.NewLine);
			sb.Append("if(hdnField != null && sortBy!=null){ hdnField.value = sortBy.value; }"+ Environment.NewLine);
			sb.Append("}"+ Environment.NewLine);

//			//The Validation Functions
//			sb.Append("function ValidateAcctNumData(tabCtlID, strTabNum) { " + Environment.NewLine);
//			sb.Append("var begAcctNum = document.getElementById(tabCtlID + '_txtBeginAcctNum'); "+ Environment.NewLine);
//			
//			//The validation logic for the Full Account Search tab controls
//			sb.Append("if(strTabNum == 'FULL'){" + Environment.NewLine);
//			sb.Append("if(begAcctNum == null){ return true;}"+ Environment.NewLine);
//			
//			sb.Append("	if(begAcctNum.value =='' )" + Environment.NewLine);
//			sb.Append("{ alert('Please enter a Beginning Account Number'); return false;}"+ Environment.NewLine);
//			sb.Append("else{return true;}"+ Environment.NewLine);
//			sb.Append("}"+ Environment.NewLine);
//
//			
//			sb.Append("}"+ Environment.NewLine);
//
//
//			//The function to set the hidden field value for the selected tab
//			sb.Append("function SetSelectedTab (hdnCtlID, strTabNum) {" + Environment.NewLine);
//			sb.Append("var hdnField = document.getElementById(hdnCtlID); "+ Environment.NewLine);
//			sb.Append("if(hdnField != null){ hdnField.value = strTabNum;}"+ Environment.NewLine);
//			sb.Append("}"+ Environment.NewLine);
			
			

			sb.Append("</script>"+ Environment.NewLine);
			this.Page.RegisterStartupScript("AcctJavaScript",sb.ToString());

		}


		private void AddJavascriptEvents()
		{
			if (this.MainScreenTabs.ContentTabs.Count > 0 &&
				this.MainScreenTabs.ContentTabs[TAB_FULL].Selected ==true)
			{
				this.btnLookup.Attributes.Add("onClick","return ValidateAcctNumData('"+ this.tbFull.ClientID +"', 'FULL');");
			}
			else if(this.MainScreenTabs.ContentTabs.Count > 1 &&
				this.MainScreenTabs.ContentTabs[TAB_PART].Selected ==true)
			{
				this.btnLookup.Attributes.Add("onClick","return ValidateAcctNumData('"+ this.tbFull.ClientID +"', 'PART');");
			}

			else if(this.MainScreenTabs.ContentTabs.Count > 2 &&
				this.MainScreenTabs.ContentTabs[TAB_SEL].Selected ==true)
			{
				this.btnLookup.Attributes.Add("onClick","return ValidateAcctNumData('"+ this.tbFull.ClientID +"', 'SEL');");
			}
		}


		/// <summary>
		/// This method will check for the number of accounts the user ID has access to.
		/// </summary>
		/// <returns></returns>
		private int CheckForNumberOfAccounts()
		{
			
			int intRetVal = 0;
			string strXML = string.Empty;
			string strError = string.Empty;
			Jenzabar.CRM.Deserializers.ValidAccountCodes objAccountInfo ;
			
			//TTP 8923
			string strHostID = ((PortalUser.Current.HostID !=null)?PortalUser.Current.HostID.ToString():"");

			try
			{
				if (this
					.GetInstance<IStaff>()
					.GetValidAccountCodes(strHostID, true, "GLAccountLookup", ref strXML, ref strError) != PlugInConstants.SUCCESS)
				{
					//Do something here to display an error
					intRetVal = PlugInConstants.RETURN_TYPE_NONE;
				}
				else
				{
					objAccountInfo = (Jenzabar.CRM.Deserializers.ValidAccountCodes)PlugIn.MapXMLToObject(strXML, new XmlSerializer(typeof(Jenzabar.CRM.Deserializers.ValidAccountCodes)));

					if(objAccountInfo != null && objAccountInfo.TotalAccounts !=null )
					{
						intRetVal = System.Convert.ToInt32(objAccountInfo.TotalAccounts) ;
					}
				}
			}
			catch(System.Exception ex)
			{
				this.lblError.Text = ex.Message.ToString();
				this.lblError.Visible = true;
			}

			return intRetVal;
		}



		/// <summary>
		/// This method will populate the ledger control
		/// </summary>
		private void PopulateLedger()
		{
			string strXML = null;
			string strError =  null;

			//TTP 8923
			string strHostID = ((PortalUser.Current.HostID !=null)?PortalUser.Current.HostID.ToString():"");

			//Call the plug in to get the general ledger data
			if(this.GetInstance<IStaff>().GetGeneralLedgers(strHostID,
				ref strXML,ref strError)== PlugInConstants.SUCCESS)
			{
				if (strXML !=null && strXML !=string.Empty)
				{
					GeneralLedgers gl = (GeneralLedgers)PlugIn.MapXMLToObject(strXML, new XmlSerializer(typeof(GeneralLedgers)));
					if(gl !=null && gl.Ledger.Length > 0)
					{
						//If we have any ledger items then add them to the dropdown list
						for(int i= 0; i<gl.Ledger.Length; i++)
						{
							ListItem li = new ListItem(gl.Ledger[i].Name,gl.Ledger[i].ID);
							this.ddlLedger.Items.Add(li);
						}
					}
				}
			}

		}

		/// <summary>
		/// This method will call the getERPPortletProperties plug in
		/// and set the visibility of various controls based on the
		/// retrieved values.
		/// </summary>
		/// <returns></returns>
		private int CallGetERPPortletProperties()
		{
			string strXML	=	null;
			string strError	=	null;
			string lsGLAccountLedger = null;

			try
			{
				if (this.GetInstance<IStaff>().GetERPPortletProperties(ref strXML, ref strError) != PlugInConstants.SUCCESS)
				{
					this.lblError.Visible = true ;
					this.lblError.Text = GLALPMessages.TXT_GLP_ERROR_NO_DATA_RETRIEVED ;
					return -1;
				}
				
				strXML = strXML.Replace("<Properties>","<ERP><Properties>");
				strXML = strXML.Replace("</Properties>","</Properties></ERP>");

				ERP ERPProperties = (ERP)PlugIn.MapXMLToObject(strXML, new XmlSerializer(typeof(ERP)));
				if (ERPProperties.Items == null || ERPProperties.Items.Length == 0 )
				{
					this.lblError.Visible = true ;
					this.lblError.Text = GLALPMessages.TXT_GLP_ERROR_NO_DATA_RETRIEVED ;
					return -1;
				}
				if(ERPProperties.Items[0].GLAccountLedger!=null && ERPProperties.Items[0].GLAccountLedger.Length > 0)
				{
					lsGLAccountLedger = ERPProperties.Items[0].GLAccountLedger[0].Visible.ToString();
				}
				else
				{
					lsGLAccountLedger = "Y";
				}

				//Set the Values into PortLet View State
				this.ParentPortlet.PortletViewState["ERP_GL_ACCT_LEDGER"] =	lsGLAccountLedger;

				
			}
			catch(System.Exception ex)
			{
				this.lblError.Visible = true;
				this.lblError.Text = ex.Message.ToString();
			}

			return 1;
		}

		/// <summary>
		/// This method will build the XML parameter string for the GetBudgetStatus
		/// plug in call.
		/// </summary>
		/// <param name="strSearchType"></param>
		/// <param name="ht"></param>
		/// <returns></returns>
		private string BuildBudgetStatusSearchParamString(string strSearchType, Hashtable ht)
		{
			
			CRMStringUtility sb = new CRMStringUtility(); 

			//Make sure the user sent us a hashtable with at least one element
			if(ht != null && ht.Count > 0)
			{
				try
				{
					//Build the XML parameter string for the Account Range search.
					if(strSearchType.ToUpper() == SEARCH_TYPE_RANGE)
					{
						sb.AppendString("<SearchParameters>"); 
						sb.AppendXML(ht["SearchType"].ToString(), "SearchType");
						sb.AppendXML(ht["RangeBeginAcct"].ToString(),"RangeBeginAccount");
						sb.AppendXML(ht["RangeEndAcct"].ToString(),"RangeEndAccount");
						sb.AppendString("</SearchParameters>"); 

					}
					if(strSearchType.ToUpper() == SEARCH_TYPE_PARTIAL)
					{
						sb.AppendString("<SearchParameters>"); 
						sb.AppendXML(ht["SearchType"].ToString(), "SearchType");
						sb.AppendString(ht["SearchParams"].ToString());
						sb.AppendString("</SearchParameters>"); 

					
					}





					//Build the XML parameter string for the Account Select search.
					if(strSearchType.ToUpper() == SEARCH_TYPE_ACCOUNTS)
					{
						
						sb.AppendString("<SearchParameters>"); 
						sb.AppendXML(ht["SearchType"].ToString(), "SearchType");
						SortedList sl = (SortedList)ht["SelectedAccounts"];
						sb.AppendString("<Accounts>");
						if(sl != null && sl.Count > 0)
						{
							for (int i= 0; i< sl.Count; i++)
							{
								//string strVal = sl.GetByIndex(i).ToString();
								//string strDelimit = "-";

								//Get the key value from the sorted list.
								//The key will always contain a delimited
								//string with the account number as the last
								//parameter(i.e. acctNum;acctDesc;acctNum or
								//acctDesc;acctNum;acctNum).
								string strVal = sl.GetKey(i).ToString();
								string strDelimit = ";";
								char [] delimiter = strDelimit.ToCharArray();

								string[] strAcctNum = strVal.Split(delimiter);

								//We must have at least 3 elements and we always use the last
								//element to retrieve the account number.
								if(strAcctNum.Length > 2)
								{
									sb.AppendXML(strAcctNum[2].ToString().Trim(), "AccountNumber");
								}
								else
								{
									sb.AppendXML("", "AccountNumber");
								}
							}
						}
						sb.AppendString("</Accounts>");
						sb.AppendString("</SearchParameters>"); 

					}

				}
				catch
				{
					sb.AppendString("");
				}
			}
			return sb.myString.ToString();
		}


		/// <summary>
		/// This method will call the GetBudgetStatus plug in.
		/// </summary>
		/// <returns>An integer indicating success or failure.</returns>
		private int CallGetBudgetStatus()
		{
			object[] PluginParam= new object[7];
			string strXML = "";
			string strError = "";
			BudgetLookupInfo bl;
			int intRetValue = 1;
			string strSearchParams = string.Empty;

			//TTP 8923
			string strHostID = ((PortalUser.Current.HostID !=null)?PortalUser.Current.HostID.ToString():"");

			//Get all the search values stored in the viewstate variables.
			string strLedger = ((this.ParentPortlet.PortletViewState["Ledger"] !=null)?this.ParentPortlet.PortletViewState["Ledger"].ToString():"");
			string strYear = ((this.ParentPortlet.PortletViewState["Year"] !=null)?this.ParentPortlet.PortletViewState["Year"].ToString():"");
			string strBudRange = ((this.ParentPortlet.PortletViewState["BudRange"] !=null)?this.ParentPortlet.PortletViewState["BudRange"].ToString():"");

			string strBeginPeriod = ((this.ParentPortlet.PortletViewState["BeginPeriod"] !=null)?this.ParentPortlet.PortletViewState["BeginPeriod"].ToString():"");
			string strEndPeriod = ((this.ParentPortlet.PortletViewState["EndPeriod"] !=null)?this.ParentPortlet.PortletViewState["EndPeriod"].ToString():"");
	
//			PluginParam[0] = PortalUser.Current.HostID.ToString();
//			PluginParam[1] = this.ParentPortlet.PortletViewState["BeginAcctNum"].ToString();
//			PluginParam[2] = this.ParentPortlet.PortletViewState["EndAcctNum"].ToString();

//			PluginParam[1] = this.ParentPortlet.PortletViewState["Ledger"].ToString();
//			PluginParam[2] = this.ParentPortlet.PortletViewState["Year"].ToString();
//			PluginParam[3] = this.ParentPortlet.PortletViewState["BeginPeriod"].ToString();
//			PluginParam[4] = this.ParentPortlet.PortletViewState["EndPeriod"].ToString();
//			PluginParam[5] = this.ParentPortlet.PortletViewState["BudRange"].ToString();

		
			//Create the search parameter string for the FULL Account Search 
			if(MainScreenTabs.ContentTabs.Count > 0 &&
				MainScreenTabs.ContentTabs[TAB_FULL].Selected)
			{

				string strBeginAcct = ((this.ParentPortlet.PortletViewState["BeginAcctNum"] !=null)?this.ParentPortlet.PortletViewState["BeginAcctNum"].ToString():"");
				string strEndAcct = ((this.ParentPortlet.PortletViewState["EndAcctNum"] !=null)?this.ParentPortlet.PortletViewState["EndAcctNum"].ToString():"");

				Hashtable htSearchParams =  new Hashtable();
				
				htSearchParams.Add("SearchType",SEARCH_TYPE_RANGE);
				htSearchParams.Add("RangeBeginAcct",strBeginAcct);
				htSearchParams.Add("RangeEndAcct",strEndAcct);

				//Create the string.
				 strSearchParams = BuildBudgetStatusSearchParamString(SEARCH_TYPE_RANGE, htSearchParams);
			}


			//Create the search parameter string for the Partial Account Number Search 
//			if(MainScreenTabs.ContentTabs.Count > 0 &&
//				MainScreenTabs.ContentTabs[TAB_SEL].Selected)
//			{
//				//Create the hashtable to store the parameters
//				Hashtable htSearchParams =  new Hashtable();
//
//				htSearchParams.Add("SearchType",SEARCH_TYPE_PARTIAL);
//				//htSearchParams.Add("SelectedAccounts",slAccounts);
//
//				//Create the string.
//				strSearchParams = BuildBudgetStatusSearchParamString(SEARCH_TYPE_ACCOUNTS, htSearchParams);
//			}

			//Create the search parameter string for the Part Account Number Search 
			if(MainScreenTabs.ContentTabs.Count > 0 &&
				MainScreenTabs.ContentTabs[TAB_PART].Selected)
			{
				Hashtable htSearchParams =  new Hashtable();
				HtmlTable tbl =  (HtmlTable)this.FindControl("MainScreenTabs").FindControl("tbPartial").FindControl("tblSearchByAcctPart");
				CRMStringUtility sb = new CRMStringUtility();
				string strHiddenFields = ((this.ParentPortlet.PortletViewState["AS_HIDDEN_CTL_ID"]!=null)?this.ParentPortlet.PortletViewState["AS_HIDDEN_CTL_ID"].ToString():"");

				sb.AppendString("<PartialElements>");
				if(tbl !=null && strHiddenFields !=null && strHiddenFields != string.Empty)
				{
					char[]aryDelimiter = {';'};
					string[]aryHiddenFieldIDs = strHiddenFields.Split(aryDelimiter);
					if(aryHiddenFieldIDs.Length > 0)
					{
						//For each row we retrieve the hidden control and the text box control
						//to get the required search parameters
						//for(int i=0; i< tbl.Rows.Count; i++)
						if(tbl.Rows.Count >= MAX_PART_ACCT_CTRL_ROWS)
						{
							//Get the last row in the table which should always contain
							//the hidden fields with their associated data (ID, Name, Sequence Number).
							int i = tbl.Rows.Count-1;
							for(int j = 0; j<aryHiddenFieldIDs.Length; j++)
							{
								//Now we'll get the hidden text box which should contain the values for 
								//the sequence number, control name and text box ID.
								TextBox tbHidden = (TextBox)tbl.Rows[i].FindControl(aryHiddenFieldIDs[j]);

								if(tbHidden !=null && tbHidden.Text != string.Empty)
								{
									string strHiddenVal = tbHidden.Text;
									string[]aryHiddenVals = strHiddenVal.Split(aryDelimiter);
																	
									if(aryHiddenVals.Length > 2 && aryHiddenFieldIDs[j]!=string.Empty )
									{
										sb.AppendString("<Element>");

										sb.AppendXML(aryHiddenVals[0].ToString(),"ID");
										sb.AppendXML(aryHiddenVals[2].ToString(),"Name");
										sb.AppendXML(aryHiddenVals[1].ToString(),"Sequence");
										

										//Now find the data entry textbox and get the value the user entered
										//if there is any.
										TextBox tbVal = (TextBox)tbl.Rows[i].FindControl("AcctSelPartial_txt" + aryHiddenVals[0]);
										if(tbVal != null)
										{
											sb.AppendXML(tbVal.Text.Trim(),"Value");
										}

										//Append the closing tag
										sb.AppendString("</Element>");

									 }
									
								}

												
							}//End of aryHiddenFieldIDs FOR loop
							
							


						}//End of  tbl.Rows FOR loop

					}
					
					
				}

				sb.AppendString("</PartialElements>");
				htSearchParams.Add("SearchType",SEARCH_TYPE_PARTIAL);
				htSearchParams.Add("SearchParams",sb.myString.ToString());
				

				//Create the string.
				strSearchParams = BuildBudgetStatusSearchParamString(SEARCH_TYPE_PARTIAL, htSearchParams);
			}


			//Create the search parameter string for the Select Account Search 
			if(MainScreenTabs.ContentTabs.Count > 0 &&
				MainScreenTabs.ContentTabs[TAB_SEL].Selected)
			{
				Hashtable htSearchParams =  new Hashtable();

				//Create a sorted list for the account numbers
				SortedList slAccounts =  new SortedList();

				if( this.JenzabarGLAccountLookup!=null 
					&& this.JenzabarGLAccountLookup.AccountsSelected !=null)
				{
					slAccounts = this.JenzabarGLAccountLookup.AccountsSelected;
				}

				htSearchParams.Add("SearchType",SEARCH_TYPE_ACCOUNTS);
				htSearchParams.Add("SelectedAccounts",slAccounts);

				//Create the string.
				strSearchParams = BuildBudgetStatusSearchParamString(SEARCH_TYPE_ACCOUNTS, htSearchParams);
			}

			
			// Serialize XML form plugin
			try
			{
				this
					.GetInstance<IStaff>()
					.GetBudgetStatus(strHostID, strLedger, strYear, strBeginPeriod ,strEndPeriod, strBudRange,strSearchParams, true, ref strXML, ref strError);
				
				bl = (BudgetLookupInfo)PlugIn.MapXMLToObject(strXML, new XmlSerializer(typeof(BudgetLookupInfo)));

				//Make sure we have at least one account.	
				if (bl.Accounts != null)
                {	
					intRetValue = bl.Accounts.Length;

					//Save the account Search information - TTP 7803
					this.ParentPortlet.PortletViewState["GLBudgetStatusXML"] = strXML;

					//this.Page.Cache.Insert("GLBudgetStatusXML",bl);

					//		this.lblPeriod.Text = bl.Period.ToString();
					//		this.lblPeriod.Font.Bold = true;
					//		this.ParentPortlet.PortletViewState["BeginPeriodDate"] = bl.BeginPeriod.ToString();
					//		this.ParentPortlet.PortletViewState["EndPeriodDate"] = bl.EndPeriod.ToString();
				}
				else
				{
					intRetValue = -1;
				}
			}
			catch(System.Exception ex)
			{
				this.lblError.Visible = true;
				this.lblError.Text = ex.Message.ToString();
			}

			return intRetValue;
		}

        
		private void PopulateResultsPerPageDropdown()
		{
			if(this.ddlResultsPerPage !=null)
			{
				this.ddlResultsPerPage.Items.Clear();
				this.ddlResultsPerPage.Items.Add(new ListItem("50","50"));
				this.ddlResultsPerPage.Items.Add(new ListItem("100","100"));
				this.ddlResultsPerPage.Items.Add(new ListItem("200","200"));
				this.ddlResultsPerPage.Items.Add(new ListItem("All Results","ALL"));
				this.ddlResultsPerPage.SelectedValue = "ALL";


			}
		}

		/// <summary>
		/// This method will remove certain characters from a string so the portlet
		/// can correctly handle special character data.
		/// </summary>
		/// <param name="strInput"></param>
		/// <returns></returns>
		private string CleanUpString(string strInput)
		{
			string strOutput = null;

			//The unicode representation for a non-breaking space
			char chrSpace = '\u00A0';
		
			if(strInput !=null && strInput != string.Empty)
			{
				string str1  = strInput.Replace('"',chrSpace);
				string str2 = str1.Replace('<',chrSpace);
				string str3 = str2.Replace('>',chrSpace);
				string str4 = str3.Replace(';',chrSpace);
				strOutput = str4;

				//strOutput = Regex.Replace(strInput, @"[^\w\s\-\'\:\,\.\/\\\&\+ @-]", "");
			}
			else
			{
				strOutput = strInput;
			}
			return strOutput;
		}


		/// <summary>
		/// This method will dynamically populate the controls into the Part Account Number
		/// search tab
		/// </summary>
		private void CreatePartSearchControls()
		{
			string strXML = null;
			bool blnRestoreSavedValues = false;
			//string strError = null;

			//Clear the viewstate 
			this.ParentPortlet.PortletViewState["AS_HIDDEN_CTL_ID"] = "";
			if(htSearchSavedValues !=null && htSearchSavedValues.Count > 0)
			{
				blnRestoreSavedValues  = true;
			}
			try
			{
				HtmlTable tbl =  (HtmlTable)this.FindControl("MainScreenTabs").FindControl("tbPartial").FindControl("tblSearchByAcctPart");
				if(tbl !=null)
				{
					//Now create the controls

					//First call the plug in to get the control information
					this.GetInstance<IStaff>().GetPartialAccountElements(ref strXML);
					if(strXML !=null)
					{
						PartialAccountElements partElements = (PartialAccountElements)PlugIn.MapXMLToObject(strXML, new XmlSerializer(typeof(PartialAccountElements)));
						
						//Get each element's data and create the control in the table which will contain
						//all the part search controls.
						HtmlTableRow trLabels = new HtmlTableRow();
						HtmlTableRow trInput = new HtmlTableRow();
						HtmlTableRow trHidden = new HtmlTableRow();

						trLabels.Attributes.Add("runat","server");
						trInput.Attributes.Add("runat","server");
						
						if(partElements.Elements !=null 
							&& partElements.Elements.Length > 0)
						{
							for (int i=0; i< partElements.Elements.Length; i++)
							{
								HtmlTableCell tcLabel = new HtmlTableCell();
								HtmlTableCell tcText = new HtmlTableCell();
								HtmlTableCell tcHidden = new HtmlTableCell();

								//Create the Label
								Label lbl = new Label();
								lbl.Attributes.Add("runat","server");
								lbl.ID = "AcctSelPartial_lbl" + CleanUpString(partElements.Elements[i].ID);
								lbl.Text = CleanUpString(partElements.Elements[i].Name) + ": ";

								//Create the textbox
								TextBox tb = new TextBox();
								tb.Attributes.Add("runat","server");
								tb.EnableViewState = true;
								tb.ID = "AcctSelPartial_txt" + CleanUpString(partElements.Elements[i].ID);
								if(blnRestoreSavedValues)
								{
									if(htSearchSavedValues.ContainsKey(tb.ID))
									{
										tb.Text = htSearchSavedValues[tb.ID].ToString();
									}
								}
								else
								{
									htSearchSavedValues.Add(tb.ID,tb.Text);

								}
								if(partElements.Elements[i].MaximumLength > 0)
								{
									tb.MaxLength = partElements.Elements[i].MaximumLength;
								}

								//Now add a hidden text field which will contain the sequence
								//number, the control name and the text box's ID delimited with a ";";
								TextBox tbHidden = new TextBox();
								tbHidden.EnableViewState = true;
								tbHidden.Attributes.Add("runat","server");
								tbHidden.ID = "txtHidden" + partElements.Elements[i].ID; 

								//Save the hidden field's ID value to a viewstate variable
								string strPrevVal = ((this.ParentPortlet.PortletViewState["AS_HIDDEN_CTL_ID"] !=null)?this.ParentPortlet.PortletViewState["AS_HIDDEN_CTL_ID"].ToString():"");
								this.ParentPortlet.PortletViewState["AS_HIDDEN_CTL_ID"] = strPrevVal + tbHidden.ID + ";";

//								tbHidden.Text = tb.ID.ToString() + ";" + 
//									partElements.Elements[i].Sequence + ";" + partElements.Elements[i].Name;
								
								tbHidden.Text = CleanUpString(partElements.Elements[i].ID) + ";" + 
									CleanUpString(partElements.Elements[i].Sequence) + ";" + CleanUpString(partElements.Elements[i].Name);
								tbHidden.Visible = false;
								tbHidden.Width = System.Convert.ToInt32(1);
								tbHidden.Height = System.Convert.ToInt32(1);
								
								//tcHidden.Attributes.Add("style","visibility:hidden");

								tcLabel.Controls.Add(lbl);
								tcText.Controls.Add(tb);
								tcHidden.Controls.Add(tbHidden);

								trLabels.Cells.Add(tcLabel);
								trInput.Cells.Add(tcText);
								trHidden.Cells.Add(tcHidden);
								//tr.Cells.Add(tcLabel);
								//tr.Cells.Add(tcText);
								//tr.Cells.Add(tcHidden);

							}
						}
						tbl.Rows.Add(trLabels);
						tbl.Rows.Add(trInput);
						tbl.Rows.Add(trHidden);
						//tbl.Rows.Add(tr);
					}

				}
				//tbl.Border = Convert.ToInt32(1);

			
			}
			catch(System.Exception ex)
			{
				this.lblError.Visible = true;
				this.lblError.Text = ex.Message.ToString();
			}
			if(blnRestoreSavedValues)
			{
				this.htSearchSavedValues.Clear();
				this.ParentPortlet.Session.Remove("glAlPartLookupData");
				
			}
			
		}

//		/// <summary>
//		/// This method will retrieve all the controls in the tblSearchByAcctPart table and 
//		/// store the user entered values in a hashtable.
//		/// </summary>
//		/// <returns></returns>
//		private Hashtable StorePartAcctSearchControlValues()
//		{
//			HtmlTable tbl =  (HtmlTable)this.FindControl("MainScreenTabs").FindControl("tbPartial").FindControl("tblSearchByAcctPart");
//			if(tbl !=null && tbl.Rows.Count > 0)
//			{
//				for (int i=0; i < tbl.Rows.Count; i++)
//				{
//					
//
//				}
//			}
//		}

		/// <summary>
		/// This method will contains the user data entry validation
		/// logic.
		/// </summary>
		/// <returns></returns>
		private bool ValidateSearchData()
		{
			bool blnValid = true;

			//Validation for controls on TAB 1
			if (this.MainScreenTabs.ContentTabs.Count > 0 &&
				this.MainScreenTabs.ContentTabs[TAB_FULL].Selected ==true)
			{
				if(this.txtBeginAcctNum.Text.Trim().Length <=0 )
				{
					this.lblError.Text = "Please enter a valid Beginning Account Number.";
					this.lblError.Visible = true;
					blnValid = false;
				}
			}
			
			//Validation for controls on TAB 2
			if (this.MainScreenTabs.ContentTabs.Count > 0 &&
				this.MainScreenTabs.ContentTabs[TAB_PART].Selected ==true)
			{

				if(!ValidatePartAcctControlVals())
				{
					this.lblError.Text = "Please enter at least one Partial Account Number value.";
					this.lblError.Visible = true;
					blnValid = false;
				}

			}

			//Validation for controls on TAB 3
			if (this.MainScreenTabs.ContentTabs.Count > 0 &&
				this.MainScreenTabs.ContentTabs[TAB_SEL].Selected ==true)
			{
				if(this.JenzabarGLAccountLookup !=null 
					&& this.JenzabarGLAccountLookup.AccountsSelected.Count <= 0)
				{
					SortedList sl = this.JenzabarGLAccountLookup.AccountsSelected;
					this.lblError.Text = "Please select at least one Account Number.";
					this.lblError.Visible = true;
					blnValid = false;
				}
			}

			//Validation for common controls
			//If everything is valid up to this point then we'll validate the year.
			if(blnValid)
			{
				if(this.ddlYear.SelectedValue == "-1" )
				{
					this.lblError.Text = "Please select a Year.";
					this.lblError.Visible = true;
					blnValid = false;
				}
			}

			if(blnValid)
			{
				if(this.ddlLedger.SelectedValue == "-1" && this.ddlLedger.Visible == true)
				{
					this.lblError.Text = "Please select at Ledger.";
					this.lblError.Visible = true;
					blnValid = false;
				}
			}

			return blnValid;

		}

		/// <summary>
		/// This method will retrieve all the Part Account # search controls on the tab and validate
		/// whether the user entered any data.
		/// </summary>
		/// <returns></returns>
		private bool ValidatePartAcctControlVals()
		{
			bool blnValid = false;
			HtmlTable tbl =  (HtmlTable)this.FindControl("MainScreenTabs").FindControl("tbPartial").FindControl("tblSearchByAcctPart");
			string strHiddenFields = ((this.ParentPortlet.PortletViewState["AS_HIDDEN_CTL_ID"]!=null)?this.ParentPortlet.PortletViewState["AS_HIDDEN_CTL_ID"].ToString():"");

			if(tbl!=null)
			{
				//We will retrieve the controls for each row.
				for(int i=0; i< tbl.Rows.Count; i++)
				{
					char[]aryDelimiter = {';'};
					string[]aryHiddenFieldIDs = strHiddenFields.Split(aryDelimiter);

					if(aryHiddenFieldIDs.Length > 0)
					{
						for(int j = 0; j < aryHiddenFieldIDs.Length; j++)
						{
							//Now we'll get the hidden text box which should contain the values for 
							//the sequence number, control name and text box ID.
							TextBox tbHidden = (TextBox)tbl.Rows[i].FindControl(aryHiddenFieldIDs[j]);
							if(tbHidden !=null && tbHidden.Text != string.Empty)
							{
								string strHiddenVal = tbHidden.Text;
								string[]aryHiddenVals = strHiddenVal.Split(aryDelimiter);
																	
								if(aryHiddenVals.Length > 2)
								{
									//Now find the data entry textbox and get the value the user entered
									//if there is any.
									TextBox tbVal = (TextBox)tbl.Rows[i].FindControl("AcctSelPartial_txt"+ aryHiddenVals[0]);
									if(tbVal != null)
									{
										if(tbVal.Text.Length >0 || tbVal.Text != string.Empty)
										{
											blnValid = true;
										}
									}
			
								}

							}
						}//End of inner FOR loop
					}


				}//End of outer FOR loop
			}


			return blnValid;
		}


		#endregion
	
		private void Initialize_Globalization()
		{
//			this.lblBeginAcctNum.Text = GLALPMessages.LBL_BEGIN_ACCOUNT_NUMBER;
//			this.lblEndAcctNum.Text = GLALPMessages.LBL_END_ACCOUNT_NUMBER;
			//this.lblYear.Text = GLALPMessages.LBL_YEAR;
			this.lblBeginPeriod.Text = GLALPMessages.LBL_BEGIN_PERIOD+ ":";
			this.lblEndPeriod.Text = GLALPMessages.LBL_END_PERIOD + ":";
			this.lblBudget.Text = Globalizer.GetGlobalizedString("TXT_GL_BUDGET_INFO_INS");
			this.lblLedger.Text = Globalizer.GetGlobalizedString("TXT_GL_GEN_LEDGER_INFO_INS");
			this.btnCancel.Text = GLALPMessages.TXT_CANCEL;
			//this.btnLookup.Text = GLALPMessages.TXT_SEARCH;

			this.btnLookup.Text = Globalizer.GetGlobalizedString("TXT_GO");
			this.lblAcctNumSel.Text = Globalizer.GetGlobalizedString("TXT_GL_ACCT_NUM_SEL");
			this.lblTabIns.Text = Globalizer.GetGlobalizedString("TXT_GL_SEL_ONE_OF_THREE");
            
			this.lblDRForTrans.Text = Globalizer.GetGlobalizedString("TXT_GL_DR_FOR_TRANS");

			//Tabs and controls
			this.tbFull.Text = Globalizer.GetGlobalizedString("TXT_GL_FULL_ACCT_NUM");
			this.lblAcctNumRange.Text = Globalizer.GetGlobalizedString("TXT_GL_ACCT_NUM_RANGE");
			this.lblBegAcctNum.Text = Globalizer.GetGlobalizedString("TXT_GL_BEGIN_ACCT_NUM");
			this.lblEnAcctNum.Text =   Globalizer.GetGlobalizedString("TXT_GL_END_ACCT_NUM");
			this.lblResultsPerPage.Text = Globalizer.GetGlobalizedString("TXT_GL_RESULTS_PER_PAGE");
		
			this.tbPartial.Text = Globalizer.GetGlobalizedString("TXT_GL_PARTIAL_ACCT_NUM");

			this.tbSelect.Text = Globalizer.GetGlobalizedString("TXT_GL_SELECT_FROM_LIST");
//			this.hntFullAcct.Text = "Enter full account numbers in the boxes above<br/>";
//			this.hntFullAcct.Text = this.hntFullAcct.Text + "<strong>Note:</strong> If both begin and end values are entered, then all accounts in that<br/>";
//			this.hntFullAcct.Text = this.hntFullAcct.Text + "range will be returned. If only one value is entered, then only that account </br>";
//			this.hntFullAcct.Text = this.hntFullAcct.Text + "will be returned.";
			
			this.hntFullAcct.Text = Globalizer.GetGlobalizedString("TXT_GL_ACCT_LOOKUP_AS_HINT");
			this.hntPartAcct.Text = Globalizer.GetGlobalizedString("TXT_GL_ACCT_LOOKUP_AS_HINT_PART");
			
			this.lblError.Text = "";
			
			//Set Visibility
			this.lblError.Visible = true;
		
		
		
		}

		
		
		/// <summary>
		/// This method will initiate the controls in the GL Account Lookup control.
		/// </summary>
		private void InitAccountControl()
		{
//			SortedList slSavedValues;
			JenzabarGLAccountLookup.AddItemsToDropDown(new ListItem("Account Number", "A"));
			JenzabarGLAccountLookup.AddItemsToDropDown(new ListItem("Description", "D"));
	
			//GenerateJavascript();
			
			//Replace these with globals
			JenzabarGLAccountLookup.LabelSearchByAcctNumText = "Account#/Description";
			JenzabarGLAccountLookup.LabelFundText = "Fund: ";
			JenzabarGLAccountLookup.LabelDeptText = "Dept: ";
			JenzabarGLAccountLookup.LabelObjectText = "Object: ";
			JenzabarGLAccountLookup.DropDownLabelText = "Sort By: ";
//			JenzabarGLAccountLookup.RadioButtonAcctNumText = "Search by Account Number";
//			JenzabarGLAccountLookup.RadioButtonAcctCompText = "Search by Account Component";
//			JenzabarGLAccountLookup.LabelSearchByAcctNumInsText = "Enter a full or partial account number";
			JenzabarGLAccountLookup.ButtonSearchText = "Search";
			JenzabarGLAccountLookup.ButtonSelectText = "Select Account";
			JenzabarGLAccountLookup.HintControlText = Globalizer.GetGlobalizedString("TXT_GL_ACCT_LOOKUP_ACCT_CONTROL_HINT");
			JenzabarGLAccountLookup.HideSearchControls = true;
			//JenzabarGLAccountLookup.AccountsToDisplayLimit = -1;
			//JenzabarGLAccountLookup.DisplaySearchButton = false;
			JenzabarGLAccountLookup.DisplaySelectButton = false;
			JenzabarGLAccountLookup.ListBoxSelectionMode = ListSelectionMode.Multiple;
			JenzabarGLAccountLookup.SortBy = this.hdnSelSort.Value;
			JenzabarGLAccountLookup.CallingPortletName = "GLAccountLookup";
						
			//This is temporary until I figure out a way to get the correct clientID from the server control.
			JenzabarGLAccountLookup.ParentControlClientID = this.FindControl("MainScreenTabs").FindControl("tbSelect").ClientID.ToString();
			

			

		}
	

		/// <summary>
		/// This method will initialize the screen controls
		/// </summary>
		private void Initialize_Controls()
		{
			string strXML = "";
			string strError = "";
			object[] PlParam= new object[1];
			PlParam[0] = "GLAccountLookup";
			NameValueDataSource[] nvdsDefault;
			NameValueDataSource[] nvdsYear;
			
			
			try
			{
				nvdsDefault = this.ParentPortlet.Preferences["DefaultBudgetLookup"];
				nvdsYear = this.ParentPortlet.Preferences["DefaultSelectedYear"];
				//The budget dropdown
				ListItem liA = new ListItem(GLALPMessages.TXT_FULL_ANNUAL,GLALPMessages.TXT_FULL_ANNUAL);
				this.ddlBudget.Items.Add(liA);
				ListItem liB = new ListItem(GLALPMessages.TXT_PERIOD_RANGE,GLALPMessages.TXT_PERIOD_RANGE);
				this.ddlBudget.Items.Add(liB);
				ListItem liC = new ListItem(GLALPMessages.TXT_NO_BUDGET_INFORMATION,GLALPMessages.TXT_NO_BUDGET_INFORMATION);
				this.ddlBudget.Items.Add(liC);
				
				//Set the budget dropdown selected value
				if(nvdsDefault != null && nvdsDefault.Length > 0) ddlBudget.SelectedValue = nvdsDefault[0].Value.ToString();

				//General Ledger dropdown
				ListItem liLedger = new ListItem("Select Ledger", "-1");
				this.ddlLedger.Items.Add(liLedger);
				
				//Year dropdown
				ListItem liSelect = new ListItem("Select a Year", "-1");
				this.ddlYear.Items.Add(liSelect);	

				//Call the plug in and get the listbox data
				this
					.GetInstance<ICommon>()
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
								ListItem li = new ListItem(lb.Items[i].Years[j].Year,lb.Items[i].Years[j].Year);
								this.ddlYear.Items.Add(li);
							}
						}
						if(lb.Items[i].Periods != null)
						{
							for (int j=0; j<lb.Items[i].Periods.Length;j++)
							{
								ListItem lpBeg = new ListItem(lb.Items[i].Periods[j].Period,lb.Items[i].Periods[j].Period);
								ListItem lpEnd = new ListItem(lb.Items[i].Periods[j].Period,lb.Items[i].Periods[j].Period);
								this.ddlBegPeriod.Items.Add(lpBeg);
								this.ddlEndPeriod.Items.Add(lpEnd);
							}
						}
					}
				}
				//TTP 6579 functionality for future version
//				//Now set the default Year by getting the preference value

				//TTP 8923 added a FindByValue check because if the user logs in as an admin
				//the returned preference value is invalid and we get an error.
				if(nvdsYear != null && nvdsYear.Length > 0 && nvdsYear[0].Value.ToString()!="" 
					&& ddlYear.Items.FindByValue(nvdsYear[0].Value.ToString())!=null  )
				{
						ddlYear.SelectedValue = nvdsYear[0].Value.ToString();
				}


			}
			catch (System.Exception ex)
			{
				this.lblError.Text = ex.GetBaseException().Message;
			}
		}

		private void btnLookup_Click(object sender, System.EventArgs e)
		{
			string strBeginAcctNum = this.txtBeginAcctNum.Text;
			string strEndAcctNum = this.txtEndAcctNum.Text;

			this.ParentPortlet.PortletViewState["BeginAcctNum"] = strBeginAcctNum;

			//If we don't have an ending account number we'll just set it to the beginning
			//account number value.
			if(strEndAcctNum == string.Empty)
			{
				strEndAcctNum = strBeginAcctNum;
			}
			if(ValidateSearchData())
			{
			
				//Save the selected tab value
				SaveSelectedTabAndData();
				this.ParentPortlet.PortletViewState["EndAcctNum"] = strEndAcctNum;
				this.ParentPortlet.PortletViewState["Budget"]=(this.ddlBudget.Items.Count > 0 ? this.ddlBudget.SelectedItem.Value.ToString() : string.Empty);
				this.ParentPortlet.PortletViewState["Year"] = (this.ddlYear.Items.Count > 0 ? this.ddlYear.SelectedItem.Value.ToString() : string.Empty);
				this.ParentPortlet.PortletViewState["BeginPeriod"] = (this.ddlBegPeriod.Items.Count > 0 ? this.ddlBegPeriod.SelectedItem.Value.ToString() : string.Empty);
				this.ParentPortlet.PortletViewState["EndPeriod"] = (this.ddlEndPeriod.Items.Count > 0 ? this.ddlEndPeriod.SelectedItem.Value.ToString() : string.Empty);
				this.ParentPortlet.PortletViewState["Ledger"] = (this.ddlLedger.Items.Count > 0 ? this.ddlLedger.SelectedItem.Value.ToString():string.Empty);
				this.ParentPortlet.PortletViewState["GLResultsPerPage"] = (this.ddlResultsPerPage.Items.Count > 0 ? this.ddlResultsPerPage.SelectedItem.Value.ToString():string.Empty);

				switch(this.ddlBudget.SelectedValue.ToString())
				{
					case "Full Annual":
						this.ParentPortlet.PortletViewState["BudRange"] = "Annual";
						break;
					case "Period Range":
						this.ParentPortlet.PortletViewState["BudRange"] = "Period";
						break;
					case "No Budget Information":
						this.ParentPortlet.PortletViewState["BudRange"] = "NoShow";
						break;
				}

				//Call the plug in to get the budget status data.
				if(CallGetBudgetStatus() != -1)
				{
					//Clear out the viewstate variable
					this.ParentPortlet.PortletViewState["AS_HIDDEN_CTL_ID"]="";
					this.ParentPortlet.ChangeScreen(GLALPConstants.BUDGET_TO_ACTUAL_SCREEN);
				}
				else
				{
					this.lblError.Text = "No accounts exist that match these criteria.  Please try again."  ;
					this.lblError.Visible = true;
				}
				
			}
			//this.ParentPortlet.ChangeScreen(GLALPConstants.BUDGET_TO_ACTUAL_SCREEN);
		}

		private void SaveSelectedTabAndData()
		{
			try
			{
				if(this.MainScreenTabs.ContentTabs[TAB_FULL].Selected==true)
				{
					this.ParentPortlet.PortletViewState[GLALPConstants.VS_GL_SELECTED_TAB_VAL]=TAB_FULL;
				}
				else if(this.MainScreenTabs.ContentTabs[TAB_PART].Selected)
				{
					this.ParentPortlet.PortletViewState[GLALPConstants.VS_GL_SELECTED_TAB_VAL]=TAB_PART;
					SavePartSearchControlData();


				}
				else if(this.MainScreenTabs.ContentTabs[TAB_SEL].Selected)
				{
					this.ParentPortlet.PortletViewState[GLALPConstants.VS_GL_SELECTED_TAB_VAL]=TAB_SEL;
					SortedList sl = this.JenzabarGLAccountLookup.AccountsSelected;

					this.ParentPortlet.Session["glAlSelLookupData"] = sl;
				}
			}
			catch
			{
				this.ParentPortlet.PortletViewState[GLALPConstants.VS_GL_SELECTED_TAB_VAL]=TAB_FULL;
			}
		}

		/// <summary>
		/// This method will save the search data from the partial account number
		/// search controls and store the values in a hashtable.
		/// </summary>
		private void SavePartSearchControlData()
		{
			//htSearchSavedValues.Clear();
			HtmlTable tbl =  (HtmlTable)this.FindControl("MainScreenTabs").FindControl("tbPartial").FindControl("tblSearchByAcctPart");
			if(tbl !=null)
			{
				foreach(Object obj in tbl.Controls)
				{
					if(obj is HtmlTableRow)
					{
						HtmlTableRow tr = (HtmlTableRow)obj;
						for(int i=0; i< tr.Cells.Count; i++)
						{
							HtmlTableCell tc = (HtmlTableCell)tr.Cells[i];
							for(int n=0; n< tc.Controls.Count; n++)
							{
								if(tr.Cells[i].Controls[n] is TextBox)
								{
									TextBox tb = (TextBox)tr.Cells[i].Controls[n];
									if(htSearchSavedValues.ContainsKey(tb.ID))
									{
										//Create the hashtable key
										htSearchSavedValues[tb.ID]= tb.Text;
									}
								}
							}

						}

					}
				}
			}
			this.ParentPortlet.Session["glAlPartLookupData"]= htSearchSavedValues;									
								
		}

		
		/// <summary>
		/// This method will repopulate the Partial Account search controls with \
		/// any previously stored data.
		/// </summary>
		private void RepopulatePartSearchControlData()
		{
			HtmlTable tbl =  (HtmlTable)this.FindControl("MainScreenTabs").FindControl("tbPartial").FindControl("tblSearchByAcctPart");
			if(tbl !=null)
			{
				foreach(HtmlControl ctl in tbl.Controls)
				{
					if(ctl is HtmlTableRow)
					{
						HtmlTableRow tr = (HtmlTableRow)ctl;
						for(int i=0; i< tr.Cells.Count; i++)
						{
							for(int n=0; n< tr.Cells[i].Controls.Count; n++)
							{
								if(tr.Cells[i].Controls[n] is TextBox)
								{
									TextBox tb = (TextBox)tr.Cells[i].Controls[n];
									tb.Text = htSearchSavedValues[tb.ID].ToString();
								}
							}

						}

					}
				}
			}
			this.ParentPortlet.Session.Remove("glAlPartLookupData");
		
		}
		
		
		
		
		/// <summary>
		/// This method will set the selected tab using the value
		/// passed into the method:
		/// </summary>
		/// <param name="strTabVal"></param>
		private void SetSelectedTab(string strTabVal)
		{
			try
			{
				this.MainScreenTabs.ContentTabs[TAB_FULL].Selected = false;
				this.MainScreenTabs.ContentTabs[TAB_PART].Selected = false;
				this.MainScreenTabs.ContentTabs[TAB_SEL].Selected = false;

				if(strTabVal !=null && strTabVal != string.Empty)
				{
					int intTabVal = System.Convert.ToInt32(strTabVal);
					switch(intTabVal)
					{
						case TAB_FULL:
							this.MainScreenTabs.ContentTabs[TAB_FULL].Selected = true;
							break;
						case TAB_PART:
							this.MainScreenTabs.ContentTabs[TAB_PART].Selected = true;
							break;
						case TAB_SEL:
							this.MainScreenTabs.ContentTabs[TAB_SEL].Selected = true;
							break;

					}
					//Now repopulate all the saved search parameters
					RepopulateSavedViewstateData(intTabVal);
				}
			}
			catch
			{
				this.MainScreenTabs.ContentTabs[TAB_FULL].Selected = true;
			}
		}

		/// <summary>
		/// This method will repopulate certain controls with the saved viewstate data.
		/// It is mainly to be used when the user wants to start a new search without having
		/// to re-enter all the search parameters.
		/// </summary>
		private void RepopulateSavedViewstateData(int intSelTab)
		{

			//Set the budget value
			//Set the Year 
			if(this.ddlBudget.Items!=null && this.ddlBudget.Items.Count >0)
			{
				string strSelVal = (this.ParentPortlet.PortletViewState["Budget"]!=null)?this.ParentPortlet.PortletViewState["Budget"].ToString():"";
				if(strSelVal!=string.Empty)
				{
					this.ddlBudget.SelectedValue =strSelVal;
				}
			}

			//Set the Year 
			if(this.ddlYear.Items!=null && this.ddlYear.Items.Count >0)
			{   string strSelVal = (this.ParentPortlet.PortletViewState["Year"]!=null)?this.ParentPortlet.PortletViewState["Year"].ToString():"";
				if(strSelVal!=string.Empty)
				{
					this.ddlYear.SelectedValue =strSelVal;
				}
			}
			//Set the Begin Period
			if(this.ddlBegPeriod.Items!=null && this.ddlBegPeriod.Items.Count >0)
			{
				string strSelVal = (this.ParentPortlet.PortletViewState["BeginPeriod"]!=null)?this.ParentPortlet.PortletViewState["BeginPeriod"].ToString():"";
				if(strSelVal!=string.Empty)
				{
					ddlBegPeriod.SelectedValue = strSelVal;
				}
			}
			//Set the End Period
			if(this.ddlEndPeriod.Items!=null && this.ddlEndPeriod.Items.Count >0)
			{
				string strNewVal = (this.ParentPortlet.PortletViewState["EndPeriod"]!=null)?this.ParentPortlet.PortletViewState["EndPeriod"].ToString():"";
				if(strNewVal!=string.Empty)
				{
					ddlEndPeriod.SelectedValue = strNewVal;
				}
			}

			//Only set the General Ledger if it is visible
			if(this.ddlLedger.Items!=null && this.ddlLedger.Items.Count >0 &&
				this.ddlLedger.Visible ==true)
			{
				string strSelVal = (this.ParentPortlet.PortletViewState["Ledger"]!=null)?this.ParentPortlet.PortletViewState["Ledger"].ToString():"";
				if(strSelVal!=string.Empty)
				{
					this.ddlLedger.SelectedValue =strSelVal;
				}
			}

			//Results per page
			if(this.ddlResultsPerPage.Items!=null && this.ddlResultsPerPage.Items.Count >0 )
			{
				string strSelVal = (this.ParentPortlet.PortletViewState["GLResultsPerPage"]!=null)?this.ParentPortlet.PortletViewState["GLResultsPerPage"].ToString():"";
				if(strSelVal!=string.Empty)
				{
					this.ddlResultsPerPage.SelectedValue =strSelVal;
				}
			}



			//Now restore any tab specific selection data for the first tab
			//if it was selected previously. Also, remove any session object
			//data that may have been saved for the other tab searches.
			switch(intSelTab)
			{
				case TAB_FULL:
					if(strRefineSearch.ToUpper()=="Y")
					{
						this.txtBeginAcctNum.Text  = ((this.ParentPortlet.PortletViewState["BeginAcctNum"]!=null)?this.ParentPortlet.PortletViewState["BeginAcctNum"].ToString():"");
						this.txtEndAcctNum.Text =  ((this.ParentPortlet.PortletViewState["EndAcctNum"]!=null)?this.ParentPortlet.PortletViewState["EndAcctNum"].ToString():"");
					}
					//Clear out any data saved for the other tab searches
					//and remove any objects from the session state.
					htSearchSavedValues.Clear();
					this.ParentPortlet.Session.Remove("glAlPartLookupData");
					this.ParentPortlet.Session.Remove("glAlSelLookupData");

					break;
				case TAB_PART:
					this.ParentPortlet.Session.Remove("glAlSelLookupData");
					break;
				case TAB_SEL:
					htSearchSavedValues.Clear();
					this.ParentPortlet.Session.Remove("glAlPartLookupData");
					break;

			}


		
		}

		private void ddlBeginAcctNum_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//this.ddlEndAcctNum.SelectedValue = this.ddlBeginAcctNum.SelectedItem.Value;
			//AddRemoveItem(this.ddlBeginAcctNum,this.ddlEndAcctNum);
		}

		private void AddRemoveItem(DropDownList aSource, DropDownList aTarget)
		{
			ListItemCollection licCollection;
			try
			{
				licCollection = new ListItemCollection();
				aTarget.Items.Clear();
				for(int intCount=aSource.SelectedIndex;intCount < aSource.Items.Count;intCount++)
				{
					licCollection.Add(aSource.Items[intCount]); 
				}
				for(int intCount=0;intCount < licCollection.Count;intCount++)
				{
					aTarget.Items.Add(licCollection[intCount]);
				}
			}
			catch(Exception expException)
			{
				this.lblError.Text = expException.GetBaseException().Message;				
			}
			finally
			{
				licCollection = null;
			}
		}
//		private void CreateTabs()
//		{
//			MainScreenTabs.Tabs.Add("FULL","Full Acct #","TXT_CUSTOM");
//			MainScreenTabs.Tabs.Add("PARTIAL","Partial Acct #","TXT_CUSTOM");
//			MainScreenTabs.Tabs.Add("SELECT","Select From List","TXT_CUSTOM");
//
//		}

		private void AddControlsToTab(string strTabName)
		{
			if(strTabName !=null && strTabName != string.Empty)
			{
			}
		}
	}
}

using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using Jenzabar.Common.ApplicationBlocks.ExceptionManagement;
using Jenzabar.CRM.Utility;
using Jenzabar.Portal.Framework;
using Jenzabar.Portal.Framework.Configuration;
//using Jenzabar.Portal.Framework.Web.Configuration;
using Jenzabar.Portal.Framework.Web.UI;


namespace Jenzabar.CRM.Staff.Web.Portlets.GLAccountLookupPortlet
{
	public class BudgetToActual : PortletViewBase
	{
		protected System.Web.UI.WebControls.Label lblPeriod;
		protected System.Web.UI.WebControls.Button btnNewSearch;
		protected System.Web.UI.WebControls.Button btnCancel;
		//protected Jenzabar.Common.Web.UI.Controls.GroupedGrid dgBudgetToActual;
		protected System.Web.UI.WebControls.DataGrid dgBudgetToActual;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.PlaceHolder phEndPeriod;
		protected System.Web.UI.WebControls.LinkButton lbPrev;
		protected System.Web.UI.WebControls.LinkButton lbNext;
		protected System.Web.UI.WebControls.LinkButton lbPos1;
		protected System.Web.UI.WebControls.LinkButton lbPos2;
		protected System.Web.UI.WebControls.LinkButton lbPos4;
		protected System.Web.UI.WebControls.LinkButton lbPos5;
		protected System.Web.UI.WebControls.LinkButton lbPos6;
		protected System.Web.UI.WebControls.LinkButton lbPos7;
		protected System.Web.UI.WebControls.LinkButton lbPos8;
		protected System.Web.UI.WebControls.LinkButton lbPos3;
		protected BudgetLookupInfo bl;
		const int INT_MAX_PAGE_POS = 8;
		const int INT_MIN_PAGE_POS = 1;
		private string strResultsPerPage;
		private int intResultsPerPage;
		private int intTotalPages;
		private string strCurERPType;
		//private int intStartNum;
				
	
		public override string ViewName 
		{
			get
			{
				return GLALPConstants.BUDGET_TO_ACTUAL_SCREEN;
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
			this.dgBudgetToActual.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgBudgetToActual_ItemCommand);
			this.dgBudgetToActual.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgBudgetToActual_ItemDataBound);
			this.btnNewSearch.Click += new System.EventHandler(this.btnNewSearch_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.lbPos1.Click +=new EventHandler(lbPos1_Click);
			this.lbPos2.Click +=new EventHandler(lbPos2_Click);
			this.lbPos3.Click +=new EventHandler(lbPos3_Click);
			this.lbPos4.Click +=new EventHandler(lbPos4_Click);
			this.lbPos5.Click +=new EventHandler(lbPos5_Click);
			this.lbPos6.Click +=new EventHandler(lbPos6_Click);
			this.lbPos7.Click +=new EventHandler(lbPos7_Click);
			this.lbPos8.Click +=new EventHandler(lbPos8_Click);
			this.lbPrev.Click +=new EventHandler(lbPrev_Click);
			this.lbNext.Click +=new EventHandler(lbNext_Click);

		}
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
		
			string strBudgetLookupXML = null;

			//TTP 7803 - Retrieve the previously stored search XML
			strBudgetLookupXML = ((this.ParentPortlet.PortletViewState["GLBudgetStatusXML"]!=null)?this.ParentPortlet.PortletViewState["GLBudgetStatusXML"].ToString():"");
			
			//If we have previously stored search data then use it.
			if(strBudgetLookupXML !=null && strBudgetLookupXML != string.Empty)
			{
				bl = (BudgetLookupInfo)PlugIn.MapXMLToObject(strBudgetLookupXML, new XmlSerializer(typeof(BudgetLookupInfo)));
			}
			//bl = (BudgetLookupInfo)this.Session["GLBudgetStatusXML"];
			//bl = (BudgetLookupInfo)this.Page.Cache["GLBudgetStatusXML"];
			strResultsPerPage = ((this.ParentPortlet.PortletViewState["GLResultsPerPage"] != null)? this.ParentPortlet.PortletViewState["GLResultsPerPage"].ToString():"");

			if(bl !=null && bl.Accounts !=null)
			{
				//Convert the results per page setting value to an actual number
				if(strResultsPerPage !=null && strResultsPerPage !=string.Empty )
				{
					if(strResultsPerPage =="ALL")
					{
						intResultsPerPage = bl.Accounts.Length;
					}
					else
					{
						intResultsPerPage =  System.Convert.ToInt32(strResultsPerPage);
					}
				}

				//Calculate the total pages
				if(bl.Accounts!=null && bl.Accounts.Length >= intResultsPerPage
					&& strResultsPerPage.ToUpper() !="ALL")
				{
					intTotalPages = bl.Accounts.Length/intResultsPerPage;

					if((bl.Accounts.Length % intResultsPerPage) > 0)
					{
						intTotalPages +=1;
					}
				}
				else
				{
					intTotalPages = 1;
				}
			}

			if(this.IsFirstLoad)
			{
				Initialize_Globalization();

				//string strXML = ((this.ParentPortlet.PortletViewState["GLBudgetStatusXML"] != null)? this.ParentPortlet.PortletViewState["GLBudgetStatusXML"].ToString():"");

				//Clear out the cache since we don't need this anymore.
				//this.Page.Cache.Remove("GLBudgetStatusXML");
				
				//bl = (BudgetLookupInfo)PlugIn.MapXMLToObject(strXML, new XmlSerializer(typeof(BudgetLookupInfo)));

				Initialize_Objects();
			}
		}

		#region Events
		//These are the click events for the pagination link buttons
		private void lbPos1_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.lbPrev.Visible = false;
				LinkButton lb = (LinkButton)sender;
				if(lb !=null)
				{
					string strPosVal =lb.Text.Trim();
					if(strPosVal == intTotalPages.ToString())
					{
						lbNext.Visible = false;
					}
					else
					{
						lbNext.Visible = true;
					}
					SaveCurrentPageNum(strPosVal);
					//Enable the previous link buttons.
					EnablePrevPositionNumbers(System.Convert.ToInt32(strPosVal));
					this.lbPos2.Enabled = false;
					if(System.Convert.ToInt32(strPosVal) > 0)
					{
						int intStartPos = (System.Convert.ToInt32(strPosVal)- 1 ) * this.intResultsPerPage;
						PopulateAccountData(intStartPos);
						SetSelectedPage(System.Convert.ToInt32(strPosVal));
					}
					
				}
			}
			catch(System.Exception ex)
			{
				ExceptionManager.Publish(ex);
				this.lblError.Visible = true;
				this.lblError.Text = ex.Message.ToString();
			}
			
		}

		private void lbPos2_Click(object sender, System.EventArgs e)
		{
			
			try
			{
				this.lbPrev.Visible = true;
				LinkButton lb = (LinkButton)sender;
				if(lb !=null)
				{
					string strPosVal =lb.Text.Trim();
					if(strPosVal == intTotalPages.ToString())
					{
						lbNext.Visible = false;
					}
					else
					{
						lbNext.Visible = true;
					}
					SaveCurrentPageNum(strPosVal);
					//Enable the previous link buttons.
					EnablePrevPositionNumbers(System.Convert.ToInt32(strPosVal));
					this.lbPos2.Enabled = false;
					if(System.Convert.ToInt32(strPosVal) > 0)
					{
						int intStartPos = (System.Convert.ToInt32(strPosVal)- 1 ) * this.intResultsPerPage;
						PopulateAccountData(intStartPos);
						SetSelectedPage(System.Convert.ToInt32(strPosVal));
					}
					
				}
			}
			catch(System.Exception ex)
			{
				ExceptionManager.Publish(ex);
				this.lblError.Visible = true;
				this.lblError.Text = ex.Message.ToString();
			}
		}

		private void lbPos3_Click(object sender, System.EventArgs e)
		{
			
			try
			{
				this.lbPrev.Visible = true;
				LinkButton lb = (LinkButton)sender;
				if(lb !=null)
				{
					string strPosVal =lb.Text.Trim();
					if(strPosVal == intTotalPages.ToString())
					{
						lbNext.Visible = false;
					}
					else
					{
						lbNext.Visible = true;
					}
					SaveCurrentPageNum(strPosVal);
					//Enable the previous link buttons.
					EnablePrevPositionNumbers(System.Convert.ToInt32(strPosVal));
					this.lbPos3.Enabled = false;
					if(System.Convert.ToInt32(strPosVal) > 0)
					{
						int intStartPos = (System.Convert.ToInt32(strPosVal)- 1 ) * this.intResultsPerPage;
						PopulateAccountData(intStartPos);
						SetSelectedPage(System.Convert.ToInt32(strPosVal));
					}
					
				}
			}
			catch(System.Exception ex)
			{
				ExceptionManager.Publish(ex);
				this.lblError.Visible = true;
				this.lblError.Text = ex.Message.ToString();
			}
		}

		private void lbPos4_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.lbPrev.Visible = true;
				LinkButton lb = (LinkButton)sender;
				if(lb !=null)
				{
					string strPosVal =lb.Text.Trim();
					if(strPosVal == intTotalPages.ToString())
					{
						lbNext.Visible = false;
					}
					else
					{
						lbNext.Visible = true;
					}
					SaveCurrentPageNum(strPosVal);
					//Enable the previous link buttons.
					EnablePrevPositionNumbers(System.Convert.ToInt32(strPosVal));
					this.lbPos4.Enabled = false;
					if(System.Convert.ToInt32(strPosVal) > 0)
					{
						int intStartPos = (System.Convert.ToInt32(strPosVal)- 1 ) * this.intResultsPerPage;
						PopulateAccountData(intStartPos);
						SetSelectedPage(System.Convert.ToInt32(strPosVal));
					}
					
				}
			}
			catch(System.Exception ex)
			{
				ExceptionManager.Publish(ex);
				this.lblError.Visible = true;
				this.lblError.Text = ex.Message.ToString();
			}
		}

		private void lbPos5_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.lbPrev.Visible = true;
				LinkButton lb = (LinkButton)sender;
				if(lb !=null)
				{
					string strPosVal =lb.Text.Trim();
					if(strPosVal == intTotalPages.ToString())
					{
						lbNext.Visible = false;
					}
					else
					{
						lbNext.Visible = true;
					}
					SaveCurrentPageNum(strPosVal);
					//Enable the previous link buttons.
					EnablePrevPositionNumbers(System.Convert.ToInt32(strPosVal));
					this.lbPos5.Enabled = false;
					if(System.Convert.ToInt32(strPosVal) > 0)
					{
						int intStartPos = (System.Convert.ToInt32(strPosVal)- 1 ) * this.intResultsPerPage;
						PopulateAccountData(intStartPos);
						SetSelectedPage(System.Convert.ToInt32(strPosVal));
					}
					
				}
			}
			catch(System.Exception ex)
			{
				ExceptionManager.Publish(ex);
				this.lblError.Visible = true;
				this.lblError.Text = ex.Message.ToString();
			}
			
		}

		private void lbPos6_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.lbPrev.Visible = true;
				LinkButton lb = (LinkButton)sender;
				if(lb !=null)
				{
					string strPosVal =lb.Text.Trim();
					if(strPosVal == intTotalPages.ToString())
					{
						lbNext.Visible = false;
					}
					else
					{
						lbNext.Visible = true;
					}
					SaveCurrentPageNum(strPosVal);
					//Enable the previous link buttons.
					EnablePrevPositionNumbers(System.Convert.ToInt32(strPosVal));
					this.lbPos6.Enabled = false;
					if(System.Convert.ToInt32(strPosVal) > 0)
					{
						int intStartPos = (System.Convert.ToInt32(strPosVal)- 1 ) * this.intResultsPerPage;
						PopulateAccountData(intStartPos);
						SetSelectedPage(System.Convert.ToInt32(strPosVal));
					}
				}
			}
			catch(System.Exception ex)
			{
				ExceptionManager.Publish(ex);
				this.lblError.Visible = true;
				this.lblError.Text = ex.Message.ToString();
			}
			
		}


		private void lbPos7_Click(object sender, System.EventArgs e)
		{
			
			try
			{
				this.lbPrev.Visible = true;
				LinkButton lb = (LinkButton)sender;
				if(lb !=null)
				{
					string strPosVal =lb.Text.Trim();
					if(strPosVal == intTotalPages.ToString())
					{
						lbNext.Visible = false;
					}
					else
					{
						lbNext.Visible = true;
					}
					SaveCurrentPageNum(strPosVal);
					//Enable the previous link buttons.
					EnablePrevPositionNumbers(System.Convert.ToInt32(strPosVal));
					this.lbPos7.Enabled = false;
					if(System.Convert.ToInt32(strPosVal) > 0)
					{
						int intStartPos = (System.Convert.ToInt32(strPosVal)- 1 ) * this.intResultsPerPage;
						PopulateAccountData(intStartPos);
						SetSelectedPage(System.Convert.ToInt32(strPosVal));
					}
					
				}
			}
			catch(System.Exception ex)
			{
				ExceptionManager.Publish(ex);
				this.lblError.Visible = true;
				this.lblError.Text = ex.Message.ToString();
			}
			
		}


		private void lbPos8_Click(object sender, System.EventArgs e)
		{
			
			try
			{
				this.lbPrev.Visible = true;
				LinkButton lb = (LinkButton)sender;
				if(lb !=null)
				{
					string strPosVal =lb.Text.Trim();
					if(strPosVal == intTotalPages.ToString())
					{
						lbNext.Visible = false;
					}
					else
					{
						lbNext.Visible = true;
					}
					SaveCurrentPageNum(strPosVal);
					//Enable the previous link buttons.
					EnablePrevPositionNumbers(System.Convert.ToInt32(strPosVal));
					this.lbPos8.Enabled = false;
					if(System.Convert.ToInt32(strPosVal) > 0)
					{
						int intStartPos = (System.Convert.ToInt32(strPosVal)- 1 ) * this.intResultsPerPage;
						PopulateAccountData(intStartPos);
						SetSelectedPage(System.Convert.ToInt32(strPosVal));
					}
					
				}
			}
			catch(System.Exception ex)
			{
				ExceptionManager.Publish(ex);
				this.lblError.Visible = true;
				this.lblError.Text = ex.Message.ToString();
			}
			
		}


		private void lbNext_Click(object sender, System.EventArgs e)
		{
			int intLastPos ;
			string strLastPos = null;
			LinkButton lb;
			string strCurPage = ((this.ParentPortlet.PortletViewState["GL_BUD_TO_ACTUAL_PAGE"]!=null)?this.ParentPortlet.PortletViewState["GL_BUD_TO_ACTUAL_PAGE"].ToString():"0");

			
			try
			{
				//Get the last visible page number link button on the screen
				for(int i= INT_MIN_PAGE_POS; i<=INT_MAX_PAGE_POS; i++)
				{
					LinkButton lbPage =  (LinkButton)this.FindControl("lbPos" + i.ToString());
					if(lbPage !=null)
					{
						if(lbPage.Visible == false)
						{
							strLastPos = "lbPos" + i.ToString();
							i =INT_MAX_PAGE_POS;
						}
					}
				}
				int intCurPage = System.Convert.ToInt32(strCurPage);
				int intNextPageNum = intCurPage + 1;

				if(strLastPos !=null && strLastPos !=string.Empty)
				{
					lb =  (LinkButton)this.FindControl(strLastPos);
				}
				else
				{
					lb =  (LinkButton)this.FindControl("lbPos8");
				}
				 
				if(lb !=null)
				{
					if(lb.Text != string.Empty)
					{
						intLastPos = System.Convert.ToInt32(lb.Text);
					}
					else
					{
						intLastPos = INT_MAX_PAGE_POS ;
					}
					if (System.Convert.ToInt32(strCurPage)==intLastPos)
					{
						
						EnablePagination((intLastPos + 1),this.bl.Accounts.Length);
						PopulateAccountData((intLastPos  * intResultsPerPage) );
						SaveCurrentPageNum(intNextPageNum.ToString());
						SetSelectedPage(intNextPageNum);
						
					}
					else
					{
						//string strPage = ((this.ParentPortlet.PortletViewState["GL_BUD_TO_ACTUAL_PAGE"]!=null)?this.ParentPortlet.PortletViewState["GL_BUD_TO_ACTUAL_PAGE"].ToString():"");
						if (strCurPage !="")
						{
							
							//EnablePagination((intCurPage),this.bl.Accounts.Length);
							PopulateAccountData((intCurPage * intResultsPerPage) );
							SaveCurrentPageNum(System.Convert.ToString(intNextPageNum));
							SetSelectedPage(intNextPageNum);
						}

					}
				}
			}
			catch(System.Exception ex)
			{
				ExceptionManager.Publish(ex);
				this.lblError.Visible = true;
				this.lblError.Text = ex.Message.ToString();
			}
		}


		private void lbPrev_Click(object sender, System.EventArgs e)
		{
			int intFirstPos;
			try
			{
				string strCurPage = ((this.ParentPortlet.PortletViewState["GL_BUD_TO_ACTUAL_PAGE"]!=null)?this.ParentPortlet.PortletViewState["GL_BUD_TO_ACTUAL_PAGE"].ToString():"0");
				int intCurPage = System.Convert.ToInt32(strCurPage);
				int intPrevPageNum = intCurPage - 1;

				LinkButton lb =  (LinkButton)this.FindControl("lbPos1");
				if(lb !=null)
				{
					intFirstPos = System.Convert.ToInt32(lb.Text);
					if (System.Convert.ToInt32(intPrevPageNum)< intFirstPos)
					{
						EnablePagination(1, this.bl.Accounts.Length);
					}
					if (strCurPage !="")
					{
						PopulateAccountData((intPrevPageNum - 1)* intResultsPerPage );
						SaveCurrentPageNum(System.Convert.ToString(intPrevPageNum));
						SetSelectedPage(intPrevPageNum);
					}

				}
			}
			catch(System.Exception ex)
			{
				ExceptionManager.Publish(ex);
				this.lblError.Visible = true;
				this.lblError.Text = ex.Message.ToString();
			}
		}

		#endregion

		private void Initialize_Globalization()
		{
			this.btnCancel.Text = GLALPMessages.TXT_CANCEL;
			this.btnNewSearch.Text = GLALPMessages.TXT_NEW_SEARCH;
		}

		private void Initialize_Objects()
		{
			object[] PluginParam = new object[7];
			string strXML = "";
			string strError = "";
			strCurERPType = Settings.Current.ERPType.ToUpper();

			//TTP 8923
			string strHostID = ((PortalUser.Current.HostID !=null)?PortalUser.Current.HostID.ToString():"");
			this.lbPos1.Enabled = false;

			//Set the Pagination controls
			if(intTotalPages > 1)
			{
				EnablePagination(1,this.bl.Accounts.Length);
				SaveCurrentPageNum("1");
			}

			try
			{
				//Make sure we have the budget info object
				if(bl ==null )
				{
					PluginParam[0] = strHostID;
					PluginParam[1] = this.ParentPortlet.PortletViewState["BeginAcctNum"].ToString();
					PluginParam[2] = this.ParentPortlet.PortletViewState["EndAcctNum"].ToString();
					PluginParam[3] = this.ParentPortlet.PortletViewState["Year"].ToString();
					PluginParam[4] = this.ParentPortlet.PortletViewState["BeginPeriod"].ToString();
					PluginParam[5] = this.ParentPortlet.PortletViewState["EndPeriod"].ToString();
					PluginParam[6] = this.ParentPortlet.PortletViewState["BudRange"].ToString();

					// Serialize XML form plugin
					this.GetInstance<IStaff>().GetBudgetStatus(strHostID, this.ParentPortlet.PortletViewState["BeginAcctNum"].ToString(), this.ParentPortlet.PortletViewState["EndAcctNum"].ToString(), this.ParentPortlet.PortletViewState["Year"].ToString(), this.ParentPortlet.PortletViewState["BeginPeriod"].ToString(), this.ParentPortlet.PortletViewState["EndPeriod"].ToString(), this.ParentPortlet.PortletViewState["BudRange"].ToString(), ref strXML, ref strError);
					bl = (BudgetLookupInfo)PlugIn.MapXMLToObject(strXML, new XmlSerializer(typeof(BudgetLookupInfo)));
				}

				PopulateAccountData(0);
			}
			catch(System.Exception ex)
			{
				ExceptionManager.Publish(ex);
				this.lblError.Visible = true;
				this.lblError.Text = ex.GetBaseException().Message;
			}
		}

		/// <summary>
		/// This method will populate the account data based on a starting number. This method
		/// is used for pagination so we only display a certain amount of accounts per page.
		/// </summary>
		/// <param name="intStartNum"></param>
		private void PopulateAccountData(int intStartNum)
		{
			int intMaxPerPage = intResultsPerPage;
			try
			{
				//bl = (BudgetLookupInfo)PlugIn.MapXMLToObject(strXML, new XmlSerializer(typeof(BudgetLookupInfo)));
					
				if (bl.Accounts != null)
				{	
					//this.lblPeriod.Text = bl.Period.ToString();
                    this.lblPeriod.Text = String.Format("Fiscal Year {0} {1}", this.ParentPortlet.PortletViewState["Year"].ToString(), bl.Period.ToString());
					this.lblPeriod.Font.Bold = true;
					this.ParentPortlet.PortletViewState["BeginPeriodDate"] = bl.BeginPeriod.ToString();
					this.ParentPortlet.PortletViewState["EndPeriodDate"] = bl.EndPeriod.ToString();

					//Create the table from the accounts collection of the BudgetLookupInfo object
					DataTable dt = this.CreateDataTable(intStartNum, intResultsPerPage);

					this.dgBudgetToActual.Columns[9].HeaderText = "<span class=\"GLAccountLookupPortlet_AccountOverBudget\">Over</span>/<span class=\"GLAccountLookupPortlet_AccountUnderBudget\">Under</span> Budget";

					//Change the headers depending on whether there are any "Other" budgets.
					this.dgBudgetToActual.Columns[6].Visible =
						(!String.IsNullOrEmpty(bl.OtherAgainstBudgetTotal)) ||
						(bl.Accounts.Any(a => !String.IsNullOrEmpty(a.OtherAgainstBudget)));
					if (this.dgBudgetToActual.Columns[6].Visible)
					{
						this.dgBudgetToActual.Columns[5].HeaderText = "This Account Against Budget";
						this.dgBudgetToActual.Columns[7].HeaderText = "Total Period Pooled Budget";
						this.dgBudgetToActual.Columns[8].HeaderText = "Total Annual Pooled Budget";
					}

					bool bOverUnder = false;

					//We do this check to make sure the intResultsPerPage variable never exceeds the number
					//of accounts we have in the data table
					if(dt.Rows.Count <  intResultsPerPage)
					{
						intMaxPerPage = dt.Rows.Count;
					}

					//Get each row in the data table and set the appropriate variable values and column
					//visibility.

					for(int i=0;i<intMaxPerPage;i++)
					{
						if(dt.Rows[i]["OverBudget"] !=null && dt.Rows[i]["OverBudget"].ToString() != string.Empty)
						{
							bOverUnder = true;
						}
						else
						{
							if(dt.Rows[i]["RemainingBudget"] != null && dt.Rows[i]["RemainingBudget"].ToString() != string.Empty)
								bOverUnder = true;
						}
	
						if(!bOverUnder) this.dgBudgetToActual.Columns[9].Visible = false;

						//this.dgBudgetToActual.DataSource = bl.Accounts;
						this.dgBudgetToActual.DataSource = dt;
						this.dgBudgetToActual.DataBind();
						
						//Disable Unposted Balance if its not there in the plugin
						//if (bl.Accounts[0].UnpostedBal == null) this.dgBudgetToActual.Columns[2].Visible = false;
						if (dt.Rows[i]["UnpostedBal"]== null ||strCurERPType=="CX") this.dgBudgetToActual.Columns[2].Visible = false;
					
						//Disable Budget Range columns
						switch (bl.BudRange.ToUpper())
						{
							case "PERIOD":
								this.dgBudgetToActual.Columns[7].Visible = true;
								this.dgBudgetToActual.Columns[8].Visible = false;
								break;
							case "ANNUAL":
								this.dgBudgetToActual.Columns[7].Visible = false;
								this.dgBudgetToActual.Columns[8].Visible = true;
								break;
							default:
								this.dgBudgetToActual.Columns[7].Visible = false;
								this.dgBudgetToActual.Columns[8].Visible = false;
								break;
						}
					}
				}
				else
				{
					this.lblError.Visible = true;
					this.lblError.Text = "No results were found for your search criteria";
				}
			}
			catch (System.Exception ex)
			{
				ExceptionManager.Publish(ex);
				this.lblError.Visible = true;
				this.lblError.Text = ex.GetBaseException().Message;
			}
		}

		
		/// <summary>
		/// This method will create a data table and populate it with values from teh BudgetLookupInfo
		/// object. It will only populate the table with the number of items specified by the StartingNum
		/// and ItemLimit number values.
		/// </summary>
		/// <param name="intStartingNum"></param>
		/// <param name="intItemLimit"></param>
		/// <returns></returns>
		private DataTable CreateDataTable(int intStartingNum, int intItemLimit)
		{
			DataTable dtAccts = new DataTable();
			dtAccts.Columns.Add("AccountNumber", typeof(String));
			dtAccts.Columns.Add("AccountDesc", typeof(String));
			dtAccts.Columns.Add("UnpostedBal", typeof(String));
			dtAccts.Columns.Add("PostedBal", typeof(String));
			dtAccts.Columns.Add("Encumbrance", typeof(String));
			dtAccts.Columns.Add("TotalAgainstBudget", typeof(String));
			dtAccts.Columns.Add("OtherAgainstBudget", typeof(String));
			dtAccts.Columns.Add("BeginPostBal", typeof(String));
			dtAccts.Columns.Add("EndBal", typeof(String));
			dtAccts.Columns.Add("AnnualBudget", typeof(String));
			dtAccts.Columns.Add("PeriodBudget", typeof(String));
			dtAccts.Columns.Add("RemainingBudget", typeof(String));
			dtAccts.Columns.Add("OverBudget", typeof(String));

			int intEndingNum = intItemLimit + intStartingNum;

			if(intEndingNum > bl.Accounts.Length)
			{
				intEndingNum = bl.Accounts.Length;
			}
			if(bl.Accounts !=null  && bl.Accounts.Length > 0)
			{
				for(int i = intStartingNum; i< intEndingNum; i++)
				{
					DataRow dr = dtAccts.NewRow();
					dr["AccountNumber"]= bl.Accounts[i].AccountNumber;
					dr["AccountDesc"]= bl.Accounts[i].AccountDesc;
					dr["UnpostedBal"]= bl.Accounts[i].UnpostedBal;
					dr["PostedBal"]= bl.Accounts[i].PostedBal;
					dr["Encumbrance"]= bl.Accounts[i].Encumbrance;
					dr["TotalAgainstBudget"] = bl.Accounts[i].TotalAgainstBudget;
					dr["OtherAgainstBudget"] = bl.Accounts[i].OtherAgainstBudget;
					dr["BeginPostBal"] = bl.Accounts[i].BeginPostBal;
					dr["EndBal"]= bl.Accounts[i].EndBal;
					dr["AnnualBudget"]= bl.Accounts[i].AnnualBudget;
					dr["PeriodBudget"]= bl.Accounts[i].PeriodBudget;
					dr["RemainingBudget"]= bl.Accounts[i].RemainingBudget;
					dr["OverBudget"]= bl.Accounts[i].OverBudget;
					dtAccts.Rows.Add(dr);
				}
			}

			
		return dtAccts;
		}


		/// <summary>
		/// This method will set the visibility for the pagination link buttons
		/// </summary>
		/// <param name="intStartPos">The starting link button position number</param>
		/// <param name="intCount">The number of link buttons to display</param>
		private void EnablePagination(int intStartPos, int intCount)
		{
			//const int INT_PAGE_POS_MAX = 8;
			//const int INT_PAGE_POS_MIN = 1;

			this.lbPrev.Text = "< Prev";

			if (intCount > 0 && this.strResultsPerPage !="ALL")
			{
				//Make sure we have a value for the total number of pages we will
				//need to access all of the account data.
				if (intTotalPages > 0)
				{
					//Make sure we always start the loop at the minimum position
					//link button count we can have on the screen(always 1) and never
					//exceed the maximum number of link buttons we can display on the screen.
					for(int i=INT_MIN_PAGE_POS; i<=INT_MAX_PAGE_POS; i++)
					{
						LinkButton lb =  (LinkButton)this.FindControl("lbPos" + i.ToString());
						if(lb !=null)
						{
							if(intStartPos > INT_MAX_PAGE_POS)
							{
								int intRelativePageNumber = i + INT_MAX_PAGE_POS;
								if(intRelativePageNumber > intTotalPages)
								{
									lb.Visible = false;
								}
								else
								{
									lb.Text = System.Convert.ToString(i + INT_MAX_PAGE_POS);
									lb.Visible = true;
									lbPos1.Enabled = false;
								}
							}
							else
							{
								if(intStartPos > intTotalPages
									|| i > intTotalPages)
								{
									lb.Visible = false;
								}
								else
								{
									lb.Text = i.ToString();
									lb.Visible = true;
								}
							}
						}
					}


					if(intTotalPages > 1)
					{
						LinkButton lb =  (LinkButton)this.FindControl("lbNext");
						if(lb!=null)
						{
							lb.Text = "Next > ";
							lb.Visible = true;
						}

					}
				}


			}
		}


		/// <summary>
		/// This method will enable the previous pagination link buttons
		/// based on the current position number value the user enters.
		/// </summary>
		/// <param name="?"></param>
		private void EnablePrevPositionNumbers(int intPos)
		{
			intPos -= 1;
			if(intPos >1)
			{
				for (int i = intPos; i >= 1; i--)
				{
					LinkButton lb =  (LinkButton)this.FindControl("lbPos" + i.ToString());
					if(lb !=null)
					{
						lb.Enabled = true;
					}
				}
			}
		}


		/// <summary>
		/// This method will set the selected page number link button
		/// by disabling the button so that it appears that the user
		/// clicked on that page link.
		/// </summary>
		/// <param name="intPageNum"></param>
		private void SetSelectedPage(int intPageNum)
		{
			//const int INT_MAX_PAGE_POS = 8;
			try
			{
				//If this is the last page then make the
				//next button invisible.
				if(intPageNum == intTotalPages)
				{
					this.lbNext.Visible = false;
				}
				else
				{
					this.lbNext.Visible = true;
				}

				//If this is the first page then make the
				//previous button invisible.
				if(intPageNum > 1)
				{
					this.lbPrev.Visible = true;
				}
				else
				{
					this.lbPrev.Visible = false;
				}

				if(intPageNum > INT_MAX_PAGE_POS)
				{
					intPageNum = intPageNum - INT_MAX_PAGE_POS;
				}
			
				for (int i=1; i<=INT_MAX_PAGE_POS; i++)
				{
					LinkButton lb =  (LinkButton)this.FindControl("lbPos" + i.ToString());
					if(i == intPageNum)
					{
						lb.Enabled = false;
					}
					else
					{
						lb.Enabled = true;
					}

				}
			}
			catch(System.Exception ex)
			{
				ExceptionManager.Publish(ex);
				this.lblError.Visible = true;
				this.lblError.Text = ex.Message.ToString();
			}
		}


		private void SaveCurrentPageNum(string strPageNum)
		{
			this.ParentPortlet.PortletViewState["GL_BUD_TO_ACTUAL_PAGE"] = strPageNum;
		}

		//This search button functionality has actually been changed to "Refine Search"
		private void btnNewSearch_Click(object sender, System.EventArgs e)
		{
			this.Page.Cache.Remove("GLBudgetStatusXML");
			this.ParentPortlet.PortletViewState[GLALPConstants.VS_GL_REFINE_SEARCH]="Y";
			this.ParentPortlet.ChangeScreen(GLALPConstants.ACCOUNT_SELECTION_SCREEN);
		}

		private void dgBudgetToActual_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			//First, make sure we are dealing with an Item or AlternatingItem
			//First, make sure we are dealing with an Item or AlternatingItem
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{

				if (e.Item.Cells[9].Text == "&nbsp;") e.Item.Cells[9].Text ="";

				if (e.Item.Cells[9].Text == null || e.Item.Cells[9].Text=="") 
				{ 
					e.Item.Cells[9].Text = e.Item.Cells[10].Text;
					e.Item.Cells[9].CssClass = "GLAccountLookupPortlet_AccountUnderBudget";
				}
				else
				{
					double total;
					if (Double.TryParse(e.Item.Cells[9].Text, out total))
					{
						e.Item.Cells[9].CssClass =
							(total < 0.0)
								? "GLAccountLookupPortlet_AccountOverBudget"
								: "GLAccountLookupPortlet_AccountUnderBudget";
					}
				}
			}

			if (e.Item.ItemType == ListItemType.Footer)
			{
				e.Item.Cells[1].Text = "Totals";		//Todo
				e.Item.Cells[1].Font.Bold = true;
				if(bl.UnpostBalTotal != null) e.Item.Cells[2].Text = bl.UnpostBalTotal;
				e.Item.Cells[2].HorizontalAlign = HorizontalAlign.Right;
				e.Item.Cells[2].Font.Bold = true;
				e.Item.Cells[2].Wrap = false;
				if(bl.PostedBalTotal != null) e.Item.Cells[3].Text = bl.PostedBalTotal;
				e.Item.Cells[3].HorizontalAlign = HorizontalAlign.Right;
				e.Item.Cells[3].Font.Bold = true;
				e.Item.Cells[3].Wrap = false;
				if (bl.EncumbranceTotal != null) e.Item.Cells[4].Text = bl.EncumbranceTotal;
				e.Item.Cells[4].HorizontalAlign = HorizontalAlign.Right;
				e.Item.Cells[4].Font.Bold = true;
				e.Item.Cells[4].Wrap = false;
				if (bl.TotalAgainstBudgetTotal != null) e.Item.Cells[5].Text = bl.TotalAgainstBudgetTotal;
				e.Item.Cells[5].HorizontalAlign = HorizontalAlign.Right;
				e.Item.Cells[5].Font.Bold = true;
				e.Item.Cells[5].Wrap = false;
				if (bl.OtherAgainstBudgetTotal != null) e.Item.Cells[6].Text = bl.OtherAgainstBudgetTotal;
				e.Item.Cells[6].HorizontalAlign = HorizontalAlign.Right;
				e.Item.Cells[6].Font.Bold = true;
				e.Item.Cells[6].Wrap = false;
				if (bl.PeriodBudgetTotal != null) e.Item.Cells[7].Text = bl.PeriodBudgetTotal;
				e.Item.Cells[7].HorizontalAlign = HorizontalAlign.Right;
				e.Item.Cells[7].Font.Bold = true;
				e.Item.Cells[7].Wrap = false;
				if(bl.AnnualBudgetTotal != null) e.Item.Cells[8].Text = bl.AnnualBudgetTotal;
				e.Item.Cells[8].HorizontalAlign = HorizontalAlign.Right;
				e.Item.Cells[8].Font.Bold = true;
				e.Item.Cells[8].Wrap = false;
				if(bl.OverUnderTotal != null) e.Item.Cells[9].Text = bl.OverUnderTotal;

				double total;
				if ((bl.UnpostBalTotal != null) && (Double.TryParse(bl.OverUnderTotal, out total)))
				{
					e.Item.Cells[9].CssClass =
						(total < 0.0)
							? "GLAccountLookupPortlet_AccountOverBudget"
							: "GLAccountLookupPortlet_AccountUnderBudget";
				}
					
				e.Item.Cells[9].HorizontalAlign = HorizontalAlign.Right;
				e.Item.Cells[9].Font.Bold = true;
				e.Item.Cells[9].Wrap = false;
			}
		}

		protected void Constituent_Click(object O, System.Web.UI.WebControls.CommandEventArgs E) 
			{
				string [] strAcct = E.CommandArgument.ToString().Split(new Char [] {';'});
				this.ParentPortlet.PortletViewState["AcctNum"] = strAcct[0];
				this.ParentPortlet.PortletViewState["Status"] = strAcct[1];
				this.ParentPortlet.PortletViewState["AcctDesc"] = strAcct[2];
				//this.ParentPortlet.PortletViewState["BeginPeriod"] = this.ParentPortlet.PortletViewState["BeginPeriod"].ToString();
				//this.ParentPortlet.PortletViewState["EndPeriod"] = this.ParentPortlet.PortletViewState["EndPeriod"].ToString();
				this.ParentPortlet.ChangeScreen(GLALPConstants.BALANCE_TRANSACTION_DETAIL_SCREEN);
			}


		private void dgBudgetToActual_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//Get the Account Number from DataKeys
			string strAcctNum = this.dgBudgetToActual.DataKeys[e.Item.ItemIndex].ToString();

			string [] strAcct = e.CommandArgument.ToString().Split(new Char [] {';'});
			this.ParentPortlet.PortletViewState["AcctNum"] = strAcctNum.ToString();
			this.ParentPortlet.PortletViewState["Status"] = strAcct[1];
			this.ParentPortlet.PortletViewState["AcctDesc"] = strAcct[2];
			if(strAcct[3] == "" || strAcct[3] == null) strAcct[3]="0.00";
			this.ParentPortlet.PortletViewState["BegPostedBal"] = strAcct[3];
			if(strAcct[4] == "" || strAcct[4] == null) strAcct[4]="0.00";
			this.ParentPortlet.PortletViewState["EndingBal"] = strAcct[4];

			switch(e.CommandName)
			{
				case "UnpostedBalance":
				case "PostedBal":
				case "Encumbrance":
				case "TotBud":
					this.ParentPortlet.ChangeScreen(GLALPConstants.BALANCE_TRANSACTION_DETAIL_SCREEN);
					break;
			}

		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			//Remove all the cache objects because we do not need them 
			//at this point.
			this.Page.Cache.Remove("GLBudgetStatusXML");
			//this.ParentPortlet.ChangeScreen(GLALPConstants.DEFAULT_SCREEN);
			this.ParentPortlet.Session.Remove("glAlPartLookupData");
			this.ParentPortlet.Session.Remove("glAlSelLookupData");
			this.ParentPortlet.PortletViewState[GLALPConstants.VS_GL_REFINE_SEARCH]="N";
			this.ParentPortlet.ChangeScreen(GLALPConstants.ACCOUNT_SELECTION_SCREEN);

		}

	}
}

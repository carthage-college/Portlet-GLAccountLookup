using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using Jenzabar.Common;
using Jenzabar.Common.Encryption;
using Jenzabar.CRM.Utility;
using Jenzabar.Portal.Framework;
using Jenzabar.Portal.Framework.Configuration;
//using Jenzabar.Portal.Framework.Web.Configuration;
using Jenzabar.Portal.Framework.Web.UI;
using CUS.OdbcConnectionClass3;

namespace Jenzabar.CRM.Staff.Web.Portlets.GLAccountLookupPortlet
{
	public class BalanceTransactionDetail : PortletViewBase
	{
        /****************************       Carthage modification       *******************************/

        protected OdbcConnectionClass3 cxConn = new OdbcConnectionClass3("ERPDataConnection.config");
        
        /**********************************************************************************************/

        protected System.Web.UI.WebControls.Label lblAcctNum;
		protected System.Web.UI.WebControls.Label lblAcctDescLabel;
		protected System.Web.UI.WebControls.Label lblAcctDesc;
		protected System.Web.UI.WebControls.Label lblBegPosBalLabel;
		protected System.Web.UI.WebControls.Label lblBegPosBal;
		protected System.Web.UI.WebControls.Label lblEndBalLabel;
		protected System.Web.UI.WebControls.Label lblEndBal;
		protected System.Web.UI.WebControls.Label lblPeriod;
		protected System.Web.UI.WebControls.DataGrid dgBalanceTranDetail;
		protected System.Web.UI.WebControls.Button btnBack;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.Label lblAcctNumLabel;
		protected System.Web.UI.WebControls.Label lblError;
		protected TranLookupInfo tl;
		protected double dbTotal;
		protected TemplateColumn tc;
		protected ColumnTemplate ItemTemp;
		protected bool blnUseDefaultBalTransColumns;
		protected string strCurErpType;

		public override string ViewName 
		{
			get
			{
				return GLALPConstants.BALANCE_TRANSACTION_DETAIL_SCREEN;
			}
		}

		#region Web Form Designer generated code

		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//

			strCurErpType = Settings.Current.ERPType.Trim().ToUpper();
			if(strCurErpType=="CX" || strCurErpType=="PX")
			{
				blnUseDefaultBalTransColumns = false;
			}
			else
			{
				blnUseDefaultBalTransColumns = true;
			}
			InitializeComponent();
			this.dgBalanceTranDetail.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgBalanceTranDetail_ItemCommand);
			this.dgBalanceTranDetail.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgBalanceTranDetail_ItemDataBound);
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void Page_Load(object sender, System.EventArgs e)
		{
			if(this.IsFirstLoad)
			{
				Initialize_Globalization();
			}
			Initialize_Objects();
		
		}

		private void Initialize_Globalization()
		{
			this.btnCancel.Text = GLALPMessages.TXT_CANCEL;
			this.btnBack.Text = GLALPMessages.TXT_BACK;
			this.lblAcctNumLabel.Text = GLALPMessages.TXT_ACCOUNT_NUMBER;
			this.lblAcctDescLabel.Text = GLALPMessages.TXT_ACCOUNT_DESCRIPTION;
			this.lblBegPosBalLabel.Text = GLALPMessages.TXT_BEGINNING_POSTED_BALANCE;
			this.lblEndBalLabel.Text = GLALPMessages.TXT_ENDING_BALANCE;
		}

		private void Initialize_Objects()
		{
			object[] PluginParam= new object[6];
			string strXML = "";
			string strError = "";

			PluginParam[0] = this.ParentPortlet.PortletViewState["Year"].ToString();
			PluginParam[1] = this.ParentPortlet.PortletViewState["AcctNum"].ToString();
			PluginParam[2] = this.ParentPortlet.PortletViewState["AcctDesc"];
			PluginParam[3] = this.ParentPortlet.PortletViewState["Status"].ToString();
			PluginParam[4] = this.ParentPortlet.PortletViewState["BeginPeriodDate"].ToString();
			PluginParam[5] = this.ParentPortlet.PortletViewState["EndPeriodDate"].ToString();

			// Serialize XML form plugin
			try
			{

				this.lblAcctNum.Text = this.ParentPortlet.PortletViewState["AcctNum"].ToString();
				this.lblAcctDesc.Text = this.ParentPortlet.PortletViewState["AcctDesc"].ToString();
				this.lblBegPosBal.Text = System.Convert.ToDouble(this.ParentPortlet.PortletViewState["BegPostedBal"]).ToString("c");
				this.lblEndBal.Text = System.Convert.ToDouble(this.ParentPortlet.PortletViewState["EndingBal"]).ToString("c");

				this
					.GetInstance<IStaff>()
					.GetTransactionInfo(this.ParentPortlet.PortletViewState["Year"].ToString(), this.ParentPortlet.PortletViewState["AcctNum"].ToString(), this.ParentPortlet.PortletViewState["AcctDesc"].ToString(), this.ParentPortlet.PortletViewState["Status"].ToString(), this.ParentPortlet.PortletViewState["BeginPeriodDate"].ToString(), this.ParentPortlet.PortletViewState["EndPeriodDate"].ToString(), ref strXML, ref strError);
                //this.ParentPortlet.ShowFeedback(FeedbackType.Message, String.Format("<pre>{0}</pre>", strXML));

				int nPos1 = strXML.IndexOf( "<Transactions>", 0 );
				if(nPos1 >= 0)
				{
					string[] strTemp = new string[2];
					strTemp[0] = strXML.Substring(0, nPos1); // get first column string
					int nPos2 = nPos1 + 1;
					strTemp[1] = strXML.Substring(nPos1); // get last column string
					strTemp[0] = strTemp[0].Replace("<Key>","<KeyHeader>");
					strTemp[0] = strTemp[0].Replace("</Key>","</KeyHeader>");
					strXML = strTemp[0]+strTemp[1];
				}
				tl = (TranLookupInfo)PlugIn.MapXMLToObject(strXML, new XmlSerializer(typeof(TranLookupInfo)));
					
				if (tl.Transactions != null)
				{
					//this.lblPeriod.Text = tl.Period.ToString();
                    /**************************     Carthage modifications      ***********************/
                    this.lblPeriod.Text = String.Format("Fiscal Year {0} {1}", this.ParentPortlet.PortletViewState["Year"].ToString(), tl.Period.ToString());

                    //this.dgBalanceTranDetail.DataSource = CreateDataSource();
                    this.dgBalanceTranDetail.DataSource = CreateDataSource(
                        this.ParentPortlet.PortletViewState["Status"].ToString(),
                        this.ParentPortlet.PortletViewState["AcctNum"].ToString(),
                        this.ParentPortlet.PortletViewState["Year"].ToString(),
                        this.ParentPortlet.PortletViewState["BeginPeriodDate"].ToString(),
                        this.ParentPortlet.PortletViewState["EndPeriodDate"].ToString()
                    );
                    /**********************************************************************************/
					this.dgBalanceTranDetail.DataBind();
					
//					//Update the headers to match what was returned in the XML.
//					if ((tl.KeyHeader[0].KeyDesc != null) && (tl.KeyHeader[0].KeyDesc.Trim() != ""))
//					{
//						this.dgBalanceTranDetail.Columns[0].HeaderText = tl.KeyHeader[0].KeyDesc.Trim();
//					}
//					if ((tl.KeyHeader[1].KeyDesc != null) && (tl.KeyHeader[1].KeyDesc.Trim() != ""))
//					{
//		
					this.dgBalanceTranDetail.Columns[1].HeaderText = tl.KeyHeader[1].KeyDesc.Trim();
//					}
//					if ((tl.KeyHeader[2].KeyDesc != null) && (tl.KeyHeader[2].KeyDesc.Trim() != ""))
//					{
//						this.dgBalanceTranDetail.Columns[2].HeaderText = tl.KeyHeader[2].KeyDesc.Trim();
//					}
				}
			}
			catch (System.Exception ex)
			{
				this.lblError.Text = ex.GetBaseException().Message;
			}
		}

		private void btnBack_Click(object sender, System.EventArgs e)
		{
			this.ParentPortlet.ChangeScreen(GLALPConstants.BUDGET_TO_ACTUAL_SCREEN);

		}

        /**************************     Carthage modifications      ***********************/
        ICollection CreateDataSource(string status, string accountNumber, string fiscalYear, string beginPeriod, string endingPeriod)
        {
            DataTable dtTransactions = CreateTableColumns();
            Exception exTransactions = null;
            string sqlTransactions = "";
            try
            {
                //Replicate logic from /opt/carsi/web/cgi/services/staff/getTransactions.pl
                string glWhere = BuildGLWhere(accountNumber.Split('-')),
                        begPeriod = beginPeriod.Split(' ')[0],
                        endPeriod = endingPeriod.Split(' ')[0],
                        statusType = "";
                //Depending on the status, use either one or both amt_type values
                switch (status)
                {
                    case "P":
                        statusType = "('ACT')";
                        break;
                    case "E":
                        statusType = "('ENC')";
                        break;
                    case "A":
                        statusType = "('ACT','ENC')";
                        break;
                }

                sqlTransactions = String.Format(@"
                    SELECT
	                    a.jrnl_ref, a.jrnl_no, a.ent_no, a.amt, TO_CHAR(c.jrnl_date,'%m/%d/%Y') AS jrnl_date, b.descr,
                        CASE b.stat WHEN 'P' THEN 'Actual' WHEN 'E' THEN 'Encumbered' ELSE TRIM(b.stat) END AS stat,
                        c.amt_type, c.fscl_mo, TRIM(d.fullname) AS vendor_name, b.doc_ref AS document_ref, b.doc_no AS document_no, b.doc_id AS vendor_id
                    FROM
	                    gltr_rec    a   INNER JOIN  gle_rec b   ON  a.jrnl_ref  =   b.jrnl_ref
									                            AND a.jrnl_no   =   b.jrnl_no
									                            AND a.ent_no    =   b.gle_no
				                        INNER JOIN  vch_rec c   ON  b.jrnl_ref  =   c.vch_ref
										                        AND b.jrnl_no   =   c.jrnl_no
				                        LEFT JOIN   id_rec  d   ON  b.doc_id    =   d.id
                    WHERE
	                    c.amt_type in {1}
                    AND
	                    (a.stat = 'P' OR a.stat = 'D')
                    AND
	                    c.fscl_yr = '{2}'
                    AND
	                    c.fscl_mo IN (
		                    SELECT UNIQUE fscl_mo 
		                    FROM    fscl_cal_rec 
		                    WHERE   subs = 'G/L' 
		                    AND    	fscl_yr = '{2}'
		                    AND	prd_no BETWEEN {3} AND {4}
	                    )
                    {0}
                ", glWhere, statusType, fiscalYear, begPeriod, endPeriod);
                dtTransactions = cxConn.ConnectToERP(sqlTransactions, ref exTransactions);
                if (exTransactions != null) { throw exTransactions; }
                
                //In the original Jenzabar logic, the total was calculated in the PopulateDataTable() method. Since it is not being used, perform the calculations here.
                dbTotal = 0;
                foreach (DataRow dr in dtTransactions.Rows)
                {
                    dbTotal += double.Parse(dr["amt"].ToString());
                }
            }
            catch (Exception ex)
            {
                this.lblError.Text = ex.GetBaseException().Message;
                this.ParentPortlet.ShowFeedback(FeedbackType.Error, String.Format("An exception occurred while retrieving transactions:<br />{0}<br />{1}<br /><pre>{2}</pre><br /><pre>{3}</pre>", ex.Message, sqlTransactions, ex.InnerException, ex.StackTrace));
            }
            return new DataView(dtTransactions);
        }

        private DataTable CreateTableColumns()
        {
            DataTable dtBalDetail = new DataTable();

            //Create the column in the data table and add bound column to the data grid
            dtBalDetail.Columns.Add(new DataColumn("jrnl_ref", typeof(string)));
            this.dgBalanceTranDetail.Columns.Add(CreateBoundColumn("jrnl_ref", "Journal Ref.", HorizontalAlign.Left, HorizontalAlign.Left, true, false, true));

            dtBalDetail.Columns.Add(new DataColumn("jrnl_no", typeof(string)));
            this.dgBalanceTranDetail.Columns.Add(CreateBoundColumn("jrnl_no", "Journal No.", HorizontalAlign.Left, HorizontalAlign.Left, true, false, true));

            dtBalDetail.Columns.Add(new DataColumn("ent_no", typeof(string)));
            this.dgBalanceTranDetail.Columns.Add(CreateBoundColumn("ent_no", "Entry No.", HorizontalAlign.Left, HorizontalAlign.Left, true, false, true));

            dtBalDetail.Columns.Add(new DataColumn("document_ref", typeof(string)));
            this.dgBalanceTranDetail.Columns.Add(CreateBoundColumn("document_ref", "Document Ref.", HorizontalAlign.Center, HorizontalAlign.Left, true, false, true));

            dtBalDetail.Columns.Add(new DataColumn("document_no", typeof(string)));
            this.dgBalanceTranDetail.Columns.Add(CreateBoundColumn("document_no", "Document No.", HorizontalAlign.Center, HorizontalAlign.Left, true, false, true));

            dtBalDetail.Columns.Add(new DataColumn("fscl_mo", typeof(string)));
            this.dgBalanceTranDetail.Columns.Add(CreateBoundColumn("fscl_mo", "Fiscal Mo.", HorizontalAlign.Left, HorizontalAlign.Left, true, false, false));

            dtBalDetail.Columns.Add(new DataColumn("jrnl_date", typeof(string)));
            this.dgBalanceTranDetail.Columns.Add(CreateBoundColumn("jrnl_date", "Date", HorizontalAlign.Center, HorizontalAlign.Center, true, false, true));

            dtBalDetail.Columns.Add(new DataColumn("vendor_id", typeof(string)));
            this.dgBalanceTranDetail.Columns.Add(CreateBoundColumn("vendor_id", "Vendor ID", HorizontalAlign.Center, HorizontalAlign.Left, true, false, true));

            dtBalDetail.Columns.Add(new DataColumn("vendor_name", typeof(string)));
            this.dgBalanceTranDetail.Columns.Add(CreateBoundColumn("vendor_name", "Vendor Name", HorizontalAlign.Center, HorizontalAlign.Left, true, false, true));

            dtBalDetail.Columns.Add(new DataColumn("descr", typeof(string)));
            this.dgBalanceTranDetail.Columns.Add(CreateBoundColumn("descr", "Description", HorizontalAlign.Center, HorizontalAlign.Left, true, false, true));

            dtBalDetail.Columns.Add(new DataColumn("amt", typeof(string)));
            this.dgBalanceTranDetail.Columns.Add(CreateBoundColumn("amt", "Amount", HorizontalAlign.Right, HorizontalAlign.Right, true, false, true));

            dtBalDetail.Columns.Add(new DataColumn("stat", typeof(string)));
            this.dgBalanceTranDetail.Columns.Add(CreateBoundColumn("stat", "Status", HorizontalAlign.Left, HorizontalAlign.Left, true, false, true));

            //Add template column to the grid
            this.dgBalanceTranDetail.Columns.Add(CreateTemplateColumn());
            return dtBalDetail;
        }

        private string BuildGLWhere(string[] acctNumberParts)
        {
            //Extracted logic from /opt/carsi/web/cgi/services/staff/getTransactions.pl
            string sql = "";
            DataTable dtColumns = null;
            Exception exColumns = null;
            string sqlGetColumns = @"
                SELECT   colname
                FROM     syscolumns
                WHERE    tabid = (SELECT tabid FROM systables WHERE tabname = 'glacct_rec')
                ORDER BY colno
            ";
            try
            {
                dtColumns = cxConn.ConnectToERP(sqlGetColumns, ref exColumns);
                if (exColumns != null) { throw exColumns; }

                //Depending how many partial parts of the account number were provided, create a corresponding number of "AND" conditions using the column names extracted above.
                for (int ii = 0; ii < acctNumberParts.Length; ii++)
                {
                    //Do not create "AND" condition if the account number part is whitespace or blank
                    if (!String.IsNullOrWhiteSpace(acctNumberParts[ii]))
                    {
                        sql = String.Format("{0} AND a.{1} = '{2}'", sql, dtColumns.Rows[ii]["colname"].ToString(), acctNumberParts[ii]);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ParentPortlet.ShowFeedback(FeedbackType.Error, String.Format("An exception occurred while building the GL query:<br />{0}<pre>{1}</pre><br /><pre>{2}</pre>", ex.Message, ex.StackTrace, ex.InnerException));
            }
            return sql;
        }
        /**********************************************************************************/

		ICollection CreateDataSource() 
		{
			DataTable dt = new DataTable();
			try
			{
				//DataRow dr;
				if((tl.KeyHeader !=null && tl.KeyHeader.Length > 0)
					&& blnUseDefaultBalTransColumns == false)
				{
					//We will dynamically add columns to the data table and
					//data grid.
					dt = DynamicallyCreateDataTableColumns(false);
					PopulateDataTable(ref dt,false);
				}
				else
				{
					//If we get here then we will just populate the 
					//data table and data grid with the default
					//columns.
					dt = DynamicallyCreateDataTableColumns(true);
					PopulateDataTable(ref dt,true);
					
				}

			}

			catch (System.Exception ex)
			{
				this.lblError.Text = ex.GetBaseException().Message;
			}
 
			DataView dv = new DataView(dt);
			return dv;
		}


		//This is the old data method which is no longer used.
		ICollection CreateDataSourceOLD() 
		{
			DataTable dt = new DataTable();
			DataRow dr;

			dt.Columns.Add(new DataColumn("SourceCode", typeof(string)));
			dt.Columns.Add(new DataColumn("GroupNum", typeof(string)));
			dt.Columns.Add(new DataColumn("LineNum", typeof(string)));
			dt.Columns.Add(new DataColumn("Date", typeof(string)));
			dt.Columns.Add(new DataColumn("Description", typeof(string)));
			dt.Columns.Add(new DataColumn("Amount", typeof(string)));
			dt.Columns.Add(new DataColumn("Status", typeof(string)));
			dt.Columns.Add(new DataColumn("Highlight", typeof(string)));

			// populate datatable
			try
			{
				if (tl.Transactions != null)
				{
					for (int i=0; i<tl.Transactions.Length;i++)
					{
						dr = dt.NewRow();
						for(int j=0; j<tl.Transactions[i].Key.Length;j++)
						{
							dr[j] = tl.Transactions[i].Key[j].KeyValue.ToString();
						}

						
						dr[3] = tl.Transactions[i].Date.ToString();
						dr[4] = CleanUpString(tl.Transactions[i].Desc.ToString());
						dr[5] = tl.Transactions[i].Amount.ToString();
						dr[6] = tl.Transactions[i].Status.ToString();
						dr[7] = tl.Transactions[i].Highlight.ToString();
						dt.Rows.Add(dr);

					}
				}
			}

			catch (System.Exception ex)
			{
				this.lblError.Text = ex.GetBaseException().Message;
			}
 
			DataView dv = new DataView(dt);
			return dv;
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

		private void dgBalanceTranDetail_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName == "OtherTranInfo")
			{

				//Update Viewstate with keyvalues
				string strTransKey;
				strTransKey = "<OtherTranInfo>";
				strTransKey += "<Key><KeyValue>"+ e.Item.Cells[0].Text.ToString() +"</KeyValue></Key>";
				strTransKey += "<Key><KeyValue>"+ e.Item.Cells[1].Text.ToString() +"</KeyValue></Key>";
				strTransKey += "<Key><KeyValue>"+ e.Item.Cells[2].Text.ToString() +"</KeyValue></Key>";
				strTransKey += "</OtherTranInfo>";

				/*
				  <script language="JavaScript"><!--
					function centerWindow() {
						sb.Append("if (document.all)" + Environment.NewLine);
							sb.Append("var xMax = screen.width, yMax = screen.height;" + Environment.NewLine);
						sb.Append("else" + Environment.NewLine);
							sb.Append("if (document.layers)" + Environment.NewLine);
								sb.Append("var xMax = window.outerWidth, yMax = window.outerHeight;" + Environment.NewLine);
							sb.Append("else" + Environment.NewLine);
								sb.Append("var xMax = 640, yMax=480;" + Environment.NewLine + Environment.NewLine);

						sb.Append("var xOffset = (xMax - 200)/2, yOffset = (yMax - 200)/2;" + Environment.NewLine);

						window.open('testpage.htm','myExample7',
						'width=200,height=200,screenX='+xOffset+',screenY='+yOffset+',
						top='+yOffset+',left='+xOffset+'');
					}

					centerWindow();
					//--></script>
				*/
				//ToDo Create a Constant for ViewOtherInfo.aspx
				string applicationName = this.ParentPortlet.PortletDisplay.PortletTemplate.Application.BasePath;
				string filePath = this.Request.ApplicationPath.ToString()+"/Portlets/" + applicationName + "/Staff/Portlet.GLAccountLookup/ViewOtherInfo.aspx";	//Todo
				//filePath = "/ICSNET/Applications/CRM/Portlets/Staff/Portlet.GLAccountLookup/ViewOtherInfo.aspx";
				string winArgs = "height=300, width=350, location=no, menubar=no, status=no, toolbar=no, scrollbars=yes, resizable=yes";

			    var token = BuildSecurityToken();
                
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
				sb.Append("<script language=\"javascript\"><!-- " + Environment.NewLine);
				sb.Append("if (document.all)" + Environment.NewLine);
				sb.Append("var xMax = screen.width, yMax = screen.height;" + Environment.NewLine);
				sb.Append("else" + Environment.NewLine);
				sb.Append("if (document.layers)" + Environment.NewLine);
				sb.Append("var xMax = window.outerWidth, yMax = window.outerHeight;" + Environment.NewLine);
				sb.Append("else" + Environment.NewLine);
				sb.Append("var xMax = 640, yMax=480;" + Environment.NewLine + Environment.NewLine);
				sb.Append("var xOffset = (xMax - 200)/2, yOffset = (yMax - 200)/2;" + Environment.NewLine);
				sb.Append("window.open('"+ filePath.ToString() +"?x=" + token + "&TransKey=" + strTransKey.ToString() + "','" + this.ParentPortlet.PortletDisplay.Name.Replace(" ","_").Replace("-","_") + "', config='" + winArgs + ",screenX='+xOffset+',screenY='+yOffset+',top='+yOffset+',left='+xOffset+'');" + Environment.NewLine);
				sb.Append(" //--></script>" + Environment.NewLine);
				//this.Page.RegisterStartupScript("LaunchPopup",sb.ToString());
				this.Page.RegisterClientScriptBlock("LaunchPopup",sb.ToString());

				//this.ParentPortlet.PortletViewState["TransKey"] = strTransKey;
				//this.ParentPortlet.ChangeScreen(GLALPConstants.VIEW_OTHER_INFORMATION_SCREEN);

			}
		}

        private string BuildSecurityToken()
        {
            return EncryptForUrl(
                PortalUser.Current.Username + "|" +
                DateTime.Now);
        }

        private string EncryptForUrl(string text)
        {
            return HttpUtility.UrlEncode(
                new Encryptor(new KeyProvider())
                .Encrypt(text));
        }


		public string getPopupURL()
		{
			/*//string applicationName = this.ParentPortlet.PortletDisplay.ParentPage.Path;
			//applicationName = this.ParentPortlet.PortletDisplay.Path;
			//applicationName = this.ParentPortlet.PortletDisplay.Portlet.URL;
			string applicationName = this.ParentPortlet.PortletDisplay.PortletTemplate.Application.BasePath;
			
			//string filePath = "/Applications/" + applicationName + "/Portlets/" + "Staff/Portlet.GLAccountLookup/ViewOtherInfo.aspx";

			string filePath = "/ICSNET/Applications/"+"CRM/Portlets/Staff/GLAccountLookupPortlet" + "/ViewOtherInfo.aspx";

			return "Javascript: window.open('"+filePath.ToString()+"','GL_ACCOUNT_LOOKUP','height=300,width=400,toolbar=no,menubar=no,location=no,resizable=yes,scrollbars=no,status=no')";*/
			return "";

		}

        private void dgBalanceTranDetail_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            //First, make sure we are dealing with an Item or AlternatingItem
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //TTP 8543
                //				try
                //				{
                //					dbTotal += System.Convert.ToDouble(e.Item.Cells[5].Text.ToString());
                //				}
                //				catch
                //				{
                //					dbTotal += 0;
                //				}

                if (e.Item.Cells[8].Text != null && e.Item.Cells[8].Text.ToUpper() == "Y") e.Item.BackColor = System.Drawing.Color.LightYellow;

            }
            else
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    /**************************     Carthage modifications      ***********************/
                    //e.Item.Cells[4].Text = "Totals";		//Todo
                    //e.Item.Cells[4].Font.Bold = true;
                    //e.Item.Cells[5].Text = dbTotal.ToString("c");
                    //e.Item.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    //e.Item.Cells[5].Font.Bold = true;
                    //e.Item.Cells[5].Wrap = false;
                    e.Item.Cells[6].Text = "Totals";
                    e.Item.Cells[6].Font.Bold = true;
                    e.Item.Cells[7].Text = dbTotal.ToString("c");
                    e.Item.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    e.Item.Cells[7].Font.Bold = true;
                    e.Item.Cells[7].Wrap = false;
                    /**********************************************************************************/
                }
        }

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			//this.ParentPortlet.ChangeScreen(GLALPConstants.DEFAULT_SCREEN);
			this.Page.Cache.Remove("GLBudgetStatusXML");
			this.ParentPortlet.PortletViewState[GLALPConstants.VS_GL_REFINE_SEARCH]="N";
			this.ParentPortlet.ChangeScreen(GLALPConstants.ACCOUNT_SELECTION_SCREEN);
		}


		#region Dynamic Column Creation functions
		
		/// <summary>
		/// This method will create a bound column for the datagrid.
		/// </summary>
		/// <param name="strDataField"></param>
		/// <param name="strHeaderText"></param>
		/// <param name="HeaderHorizAlign"></param>
		/// <param name="ItemHorizAlign"></param>
		/// <param name="HeaderWrap"></param>
		/// <param name="ItemWrap"></param>
		/// <returns></returns>
		private BoundColumn CreateBoundColumn(string strDataField, string strHeaderText,
			HorizontalAlign HeaderHorizAlign,  HorizontalAlign ItemHorizAlign, bool HeaderWrap, bool ItemWrap, bool blnVisible )
		{
			BoundColumn bc = new BoundColumn();
			if(strDataField != string.Empty)
			{
				bc.DataField = strDataField;
			}
			
			bc.ItemStyle.Wrap = ItemWrap;
			bc.ItemStyle.HorizontalAlign = ItemHorizAlign;
			bc.HeaderStyle.HorizontalAlign = HeaderHorizAlign;
			bc.HeaderStyle.Wrap = HeaderWrap;
			bc.Visible = blnVisible;
			if(strHeaderText !=string.Empty)
			{
				bc.HeaderText = strHeaderText;
			}

			return bc;

		}

		
		/// <summary>
		/// This method created an item template column to be used
		/// for the datagrid.
		/// </summary>
		private TemplateColumn CreateTemplateColumn()
		{
			//Create a new template column
			tc = new TemplateColumn();
			ItemTemp =  new ColumnTemplate();
				
			//Set the template column properties
			tc.HeaderText = "Other Transaction Information";
			tc.ItemStyle.Wrap = false;
			tc.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
			tc.HeaderStyle.Wrap = true;
			tc.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
			tc.ItemTemplate = ItemTemp;
			return tc;
		}
		
		/// <summary>
		/// This method will dynamically create the data table columns.
		/// </summary>
		/// <param name="blnUseDefaultColumns"></param>
		/// <returns></returns>
		private DataTable DynamicallyCreateDataTableColumns(bool blnUseDefaultColumns)
		{
			DataTable dtBalDetail = new DataTable();
			try
			{
				if(blnUseDefaultColumns)
				{
					//Add the default columns if we do not want to
					//add the dynamic columns.
					AddDefaultColumns(ref dtBalDetail);
				
				}
				else
				{
					//Add the dynamic columns if we have a KeyHeader and its
					//associated data.
					if(tl.KeyHeader !=null && tl.KeyHeader.Length > 0)
					{
						for(int n=0;n<tl.KeyHeader.Length; n++)
						{
							//If the column is not already there then add it.
							if(!dtBalDetail.Columns.Contains(tl.KeyHeader[n].KeyDesc))
							{
								string strKeyDesc = CleanUpString(tl.KeyHeader[n].KeyDesc);
								//Create the column in the data table
								dtBalDetail.Columns.Add(new DataColumn(strKeyDesc, typeof(string)));
							
								//Now add the bound column to the data grid
								this.dgBalanceTranDetail.Columns.Add(CreateBoundColumn(strKeyDesc,strKeyDesc,
									HorizontalAlign.Left,HorizontalAlign.Left,true, false,true));
						
							}
						}
					}
					else
					{
						AddDefaultColumns(ref dtBalDetail);
					}
	
				}

				

				//Now add the static columns
				AddStaticColumns(ref dtBalDetail);

				//Create the new item template column object which will
				//be added to the datagrid.
				//CreateTemplateColumn();

				//Now add the template column to the grid
				this.dgBalanceTranDetail.Columns.Add(CreateTemplateColumn());
				

			}
			catch(System.Exception ex)
			{
				this.lblError.Text = ex.GetBaseException().Message;
			}

			return dtBalDetail;
		}

		/// <summary>
		/// This method will add the default data columns in place of the 
		/// dynamic columns.
		/// </summary>
		/// <param name="dt"></param>
		private void AddDefaultColumns(ref DataTable dt)
		{
			if(dt !=null)
			{
				//Create the columns in the data table
				dt.Columns.Add(new DataColumn("SourceCode", typeof(string)));
				this.dgBalanceTranDetail.Columns.Add(CreateBoundColumn("SourceCode","Source Code",
					HorizontalAlign.Left,HorizontalAlign.Left,true,false,true));

				dt.Columns.Add(new DataColumn("GroupNum", typeof(string)));
				this.dgBalanceTranDetail.Columns.Add(CreateBoundColumn("GroupNum","Group Number",
					HorizontalAlign.Left,HorizontalAlign.Left,true,false,true));

				dt.Columns.Add(new DataColumn("LineNum", typeof(string)));
				this.dgBalanceTranDetail.Columns.Add(CreateBoundColumn("LineNum","Line Number",
					HorizontalAlign.Left,HorizontalAlign.Left,true,false,true));
			}
		}

		/// <summary>
		/// This method will add the static columns to the data table.
		/// These columns are always added after the dynamic colums have
		/// been created and added.
		/// </summary>
		/// <param name="dt"></param>
		private void AddStaticColumns(ref DataTable dt)
		{
			if(dt !=null)
			{
				dt.Columns.Add(new DataColumn("Date", typeof(string)));
				this.dgBalanceTranDetail.Columns.Add(CreateBoundColumn("Date","Date",HorizontalAlign.Center,
					HorizontalAlign.Center,false,false,true));

				dt.Columns.Add(new DataColumn("Description", typeof(string)));
				this.dgBalanceTranDetail.Columns.Add(CreateBoundColumn("Description","Description",
					HorizontalAlign.Center,HorizontalAlign.Left,false,true,true));

				dt.Columns.Add(new DataColumn("Amount", typeof(string)));
				this.dgBalanceTranDetail.Columns.Add(CreateBoundColumn("Amount","Amount",HorizontalAlign.Right,
					HorizontalAlign.Right,false,false,true));

				dt.Columns.Add(new DataColumn("Status", typeof(string)));
				this.dgBalanceTranDetail.Columns.Add(CreateBoundColumn("Status","Status",HorizontalAlign.NotSet,
					HorizontalAlign.NotSet,false,false,true));


				dt.Columns.Add(new DataColumn("Highlight", typeof(string)));
				this.dgBalanceTranDetail.Columns.Add(CreateBoundColumn("Highlight","Highlight",HorizontalAlign.NotSet,
					HorizontalAlign.NotSet,false,false,false));
			}
		}


		/// <summary>
		/// This method will create the data table which is used for the data grid.
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="blnUseDefaultColumns"></param>
		private void PopulateDataTable(ref DataTable dt, bool blnUseDefaultColumns)
		{
			DataRow dr = null;

			if (tl.Transactions != null )
			{
				
				for (int i=0; i<tl.Transactions.Length;i++)
				{
					dr = dt.NewRow();
					for(int j=0; j<tl.Transactions[i].Key.Length;j++)
					{
						string str = tl.Transactions[i].Key[j].KeyValue.ToString();
						dr[j] = tl.Transactions[i].Key[j].KeyValue.ToString();

					}

					//dr[3] = tl.Transactions[i].Date.ToString();
					//dr[4] = CleanUpString(tl.Transactions[i].Desc.ToString());
					//dr[5] = tl.Transactions[i].Amount.ToString();
					//dr[6] = tl.Transactions[i].Status.ToString();
					//dr[7] = tl.Transactions[i].Highlight.ToString();
					
					dr["Date"] = tl.Transactions[i].Date.ToString();
					dr["Description"] = CleanUpString(tl.Transactions[i].Desc.ToString());
					dr["Amount"] = tl.Transactions[i].Amount.ToString();
					dr["Status"] = tl.Transactions[i].Status.ToString();
					dr["Highlight"] =tl.Transactions[i].Highlight.ToString();
					
					//Add the data row
					dt.Rows.Add(dr);
	
					//TTP 8543
					//Add to the running total
					try
					{
						dbTotal += System.Convert.ToDouble(tl.Transactions[i].Amount.ToString());
					}
					catch
					{
						dbTotal += 0;
					}
					

					
					
				}
			}
			
		}



		#endregion

	}

	/// <summary>
	/// This class will create a ColumnTemplate object we can use
	/// for data grids.
	/// </summary>
	public class ColumnTemplate : ITemplate, INamingContainer
	{ 
		public void InstantiateIn(Control container) 
		{ 
			LinkButton lb = new LinkButton();
			lb.ID = "lnkOtherTranInfo";
			lb.CommandName = "OtherTranInfo";
			lb.CommandArgument = "OtherTranInfo";
			lb.Text = "View";
			lb.Attributes.Add("runat","server");
			lb.EnableViewState = true;


			//We no longer use this hyperlink.
//			HyperLink hl = new HyperLink();
//			hl.ID = "hlnkOtherTranInfo";
//			//hl.NavigateUrl = getPopupURL();
//			hl.Text = "View";
//			hl.Visible = false;
//			hl.Attributes.Add("Runat","server");
//			hl.Attributes.Add("Target","_new");
			//hl.EnableViewState = true;
			
			container.Controls.Add(lb); 
			//container.Controls.Add(hl); 
		}

		
	}
}

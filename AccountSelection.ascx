<%@ Register TagPrefix="common" assembly="Jenzabar.Common" namespace="Jenzabar.Common.Web.UI.Controls" %>
<%@ Register TagPrefix="cc1" Namespace="Jenzabar.Portal.Web.UI.Controls" Assembly="Jenzabar.Portal.Web" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="AccountSelection.ascx.cs" Inherits="Jenzabar.CRM.Staff.Web.Portlets.GLAccountLookupPortlet.AccountSelection" %>
<%@ Import namespace="Jenzabar.Common.Globalization" %>
<table width="100%">
	<TR>
		<TD width="100%"><INPUT id="hdnSelSort" type="hidden" size="10" runat="server"></TD>
	</TR>
	<TR>
		<TD width="100%"><asp:label id="lblError" ForeColor="#ff0000" Font-Bold="True" runat="server"></asp:label><INPUT id="hdnSelTab" type="hidden" DESIGNTIMEDRAGDROP="81"></TD>
	</TR>
</table>
<TABLE id="Table1" cellSpacing="3" cellPadding="3" width="100%" border="0">
	<TR>
		<TD noWrap colSpan="2">
			<TABLE id="Table2" cellSpacing="3" cellPadding="3" width="300" border="0">
				<TR>
					<TD><asp:dropdownlist id="ddlLedger" runat="server" Width="280px"></asp:dropdownlist></TD>
					<TD noWrap><asp:label id="lblLedger" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD><asp:dropdownlist id="ddlBudget" runat="server" Width="280px"></asp:dropdownlist></TD>
					<TD noWrap><asp:label id="lblBudget" runat="server"></asp:label></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD width="253">
			<asp:button id="btnCancel" runat="server" Visible="False"></asp:button></TD>
		<TD>
		</TD>
	</TR>
	<tr>
		<td colSpan="3" noWrap>
			<h6><asp:label id="lblAcctNumSel" runat="server"></asp:label></h6>
		</td>
	</tr>
	<tr>
		<td colSpan="4"><asp:label id="lblTabIns" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td colspan="2">
			<div id="divTab"><common:contenttabgroup id="MainScreenTabs" runat="server">
					<common:ContentTab id="tbFull" runat="server">
						<table id="tblFull" runat="server" border="0">
							<tr>
								<td colspan="1">
									<asp:Label ID="lblAcctNumRange" runat="server" /></td>
							</tr>
							<tr>
								<td>
									<table id="tblBeginAcctNum" runat="server">
										<tr>
											<td>
												<asp:Label ID="lblBegAcctNum" runat="server" /></td>
											<td align="right">
												<asp:TextBox ID="txtBeginAcctNum" Runat="server" /></td>
										</tr>
										<tr>
											<td>
												<asp:Label ID="lblEnAcctNum" runat="server" /></td>
											<td align="right">
												<asp:TextBox ID="txtEndAcctNum" Runat="server" /></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td>
								</td>
							</tr>
							<tr>
								<td colspan="4">
									<common:Hint id="hntFullAcct" runat="server" /></td>
							</tr>
						</table>
					</common:ContentTab>
					<common:ContentTab id="tbPartial" runat="server">
						<table id="tblPartial" runat="server" border="0">
							<tr>
								<td colspan="4">
									<table id="tblSearchByAcctPart" runat="server" cellpadding="2" cellspacing="2" border="0">
									</table>
								</td>
							</tr>
							<tr>
								<td colspan="4">
									<common:Hint id="hntPartAcct" runat="server" /></td>
							</tr>
						</table>
					</common:ContentTab>
					<common:ContentTab id="tbSelect" runat="server">
						<table id="tblSelect" runat="server" border="0" width='60%'>
							<TR>
								<TD colSpan="4">
									<cc1:jenzabarglaccountlookup id="JenzabarGLAccountLookup" runat="server"></cc1:jenzabarglaccountlookup></TD>
							</TR>
						</table>
					</common:ContentTab>
				</common:contenttabgroup></div>
		</td>
	</tr>
	<tr>
		<TD colSpan="2">
			<H6>
				<asp:label id="lblDRForTrans" runat="server"></asp:label></H6>
		</TD>
	</tr>
	<TR>
		<td colSpan="2">
			<table id="tblDRControls" runat="server">
				<TR>
					<TD><asp:dropdownlist id="ddlYear" runat="server"></asp:dropdownlist></TD>
					<TD>&nbsp;</TD>
					<TD><asp:label id="lblBeginPeriod" runat="server"></asp:label></TD>
          <TD noWrap align=left>
<asp:DropDownList id=ddlBegPeriod runat="server"></asp:DropDownList></TD>
          <TD noWrap align=right>&nbsp;</TD>
					<TD noWrap align=right>
<asp:label id=lblEndPeriod runat="server"></asp:label> 
						<asp:dropdownlist id="ddlEndPeriod" runat="server"></asp:dropdownlist></TD>
				</TR>
				<tr>
					<td colspan="6" align="center">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6" align="left"><asp:label id="lblResultsPerPage" runat="server"></asp:label>&nbsp;<asp:dropdownlist id="ddlResultsPerPage" runat="server"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td colSpan=6>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4" align="center"><asp:button id="btnLookup" runat="server"></asp:button></td>
				</tr>
			</table>
		</td>
	</TR>
</TABLE>

<%@ Control Language="c#" AutoEventWireup="false" Codebehind="BalanceTransactionDetail.ascx.cs" Inherits="Jenzabar.CRM.Staff.Web.Portlets.GLAccountLookupPortlet.BalanceTransactionDetail" %>
<%@ Register TagPrefix="common" namespace="Jenzabar.Common.Web.UI.Controls" assembly="Jenzabar.Common" %>
<TABLE id="Table1" cellSpacing="3" cellPadding="3" width="100%" border="0">
	<TR>
		<TD width="30%"><asp:label id="lblAcctNumLabel" runat="server" Font-Bold="True"></asp:label></TD>
		<TD width="70%"><asp:label id="lblAcctNum" runat="server"></asp:label></TD>
	<TR>
		<TD width="30%"><asp:label id="lblAcctDescLabel" runat="server" Font-Bold="True"></asp:label></TD>
		<TD width="70%"><asp:label id="lblAcctDesc" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<td width="30%"><asp:label id="lblBegPosBalLabel" runat="server" Font-Bold="True"></asp:label></td>
		<TD width="70%"><asp:label id="lblBegPosBal" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD width="30%"><asp:label id="lblEndBalLabel" runat="server" Font-Bold="True"></asp:label></TD>
		<TD width="70%"><asp:label id="lblEndBal" runat="server"></asp:label></TD>
	</TR>
</TABLE>
<table id="Table2" cellSpacing="3" cellPadding="3" width="100%" border="0">
	<tr>
		<TD align="center"><asp:label id="lblPeriod" runat="server" Font-Bold="True"></asp:label></TD>
	</tr>
	<tr>
		<td><asp:datagrid id="dgBalanceTranDetail" runat="server" ShowFooter="True" GridLines="Vertical" Width="100%"
				AutoGenerateColumns="False" AlternatingItemStyle-CssClass="alt" HeaderStyle-CssClass="header"
				FooterStyle-CssClass="header">
				
			</asp:datagrid></td>
	</tr>
	<tr>
		<td>
			<table cellSpacing="5" cellPadding="3">
				<tr>
					<td><asp:button id="btnBack" runat="server" Width="100px"></asp:button></td>
					<td align="right"><asp:button id="btnCancel" runat="server" Width="100px"></asp:button></td>
				</tr>
			</table>
		</td>
	</tr>
	<TR>
		<TD><asp:label id="lblError" runat="server"></asp:label></TD>
	</TR>
</table>

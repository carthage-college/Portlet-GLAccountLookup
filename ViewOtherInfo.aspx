<%@ Register TagPrefix="common" namespace="Jenzabar.Common.Web.UI.Controls" assembly="Jenzabar.Common" %>
<%@ Page language="c#" Codebehind="ViewOtherInfo.aspx.cs" validateRequest="false" AutoEventWireup="false" Inherits="Jenzabar.Portal.Web.Applications.CRM.Portlets.Staff.GLAccountLookupPortlet.ViewOtherInfo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ViewOtherInfo</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="3" cellPadding="3" width="100%" border="0">
				<TR>
					<TD>&nbsp;<asp:label id="lblHeader" runat="server" Width="100%"></asp:label></TD>
				</TR>
				<TR>
					<TD><asp:datagrid class="groupedGrid" id="dgOtherTransInfo" runat="server" AutoGenerateColumns="False"
							Width="100%" GridLines="None" AlternatingItemStyle-CssClass="alt" ShowHeader="False" BorderColor="#999999"
							BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3">
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="DisplayLabel">
									<ItemStyle Font-Bold="True" Wrap="False" BackColor="Gainsboro"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn>
									<ItemStyle Wrap="False" BackColor="WhiteSmoke"></ItemStyle>
									<ItemTemplate>
										<asp:Label ID="lblDisplayValue" Runat="server" Visible="False">
											<%# DataBinder.Eval(Container.DataItem,"DisplayValue[0].Value") %>
										</asp:Label>
										<asp:HyperLink ID="hlnkDisplayValue" Runat="server" Visible="False" NavigateUrl='<%# "mailto:"+DataBinder.Eval(Container.DataItem,"DisplayValue[0].email") %>'>
											<%# DataBinder.Eval(Container.DataItem,"DisplayValue[0].Value") %>
										</asp:HyperLink>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblError" runat="server" Width="100%"></asp:label></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

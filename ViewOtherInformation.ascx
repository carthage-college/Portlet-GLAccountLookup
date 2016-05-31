<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ViewOtherInformation.ascx.cs" Inherits="Jenzabar.CRM.Staff.Web.Portlets.GLAccountLookupPortlet.ViewOtherInformation" %>
<TABLE id="Table1" cellSpacing="3" cellPadding="3" width="100%" border="0">
	<TR>
		<TD>&nbsp;<asp:label id="lblHeader" runat="server" Width="100%"></asp:label></TD>
	</TR>
	<TR>
		<TD><asp:datagrid class="groupedGrid" id="dgOtherTransInfo" runat="server" AutoGenerateColumns="False"
				Width="100%" GridLines="None" AlternatingItemStyle-CssClass="alt" ShowHeader="False">
				<AlternatingItemStyle CssClass="alt"></AlternatingItemStyle>
				<Columns>
					<asp:BoundColumn DataField="DisplayLabel">
						<ItemStyle Font-Bold="True" Wrap="False"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn>
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
			</asp:datagrid></TD>
	</TR>
	<TR>
		<TD><asp:label id="lblError" runat="server" Width="100%"></asp:label></TD>
	</TR>
</TABLE>

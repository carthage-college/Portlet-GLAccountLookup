<%@ Register TagPrefix="common" namespace="Jenzabar.Common.Web.UI.Controls" assembly="Jenzabar.Common"%>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="BudgetToActual.ascx.cs" Inherits="Jenzabar.CRM.Staff.Web.Portlets.GLAccountLookupPortlet.BudgetToActual" %>
<TABLE id="Table1" cellSpacing="3" cellPadding="3" width="100%" border="0">
	<TR>
		<TD><asp:label id="lblError" runat="server" ForeColor="#ff0000"></asp:label></TD>
	</TR>
	<TR>
		<TD align="center"><asp:label id="lblPeriod" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD><asp:datagrid id="dgBudgetToActual" runat="server" DataKeyField="AccountNumber" Width="100%" GridLines="Vertical"
				AutoGenerateColumns="False" ShowFooter="True" FooterStyle-CssClass="header" HeaderStyle-CssClass="header"
				AlternatingItemStyle-CssClass="alt">
				<Columns>
					<asp:BoundColumn DataField="AccountNumber" HeaderText="Account Number">
						<ItemStyle Wrap="False"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="AccountDesc" HeaderText="Description"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Unposted Balance">
						<ItemTemplate>
							<asp:LinkButton ID="lnkUnpostedBalance" CommandName="UnpostedBalance" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"AccountNumber")+";U;"+DataBinder.Eval(Container.DataItem,"AccountDesc")+";"+DataBinder.Eval(Container.DataItem,"BeginPostBal")+";"+DataBinder.Eval(Container.DataItem,"EndBal")%>' Runat="server">
								<%# DataBinder.Eval(Container.DataItem,"UnpostedBal") %>
							</asp:LinkButton>
						</ItemTemplate>
						<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
						<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Posted Balance">
						<ItemTemplate>
							<asp:LinkButton ID="lnkPostedBal" CommandName="PostedBal" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"AccountNumber")+";P;"+DataBinder.Eval(Container.DataItem,"AccountDesc")+";"+DataBinder.Eval(Container.DataItem,"BeginPostBal")+";"+DataBinder.Eval(Container.DataItem,"EndBal")%>' Runat="server">
								<%# DataBinder.Eval(Container.DataItem,"PostedBal") %>
							</asp:LinkButton>
						</ItemTemplate>
						<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
						<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Encumbrance">
						<ItemTemplate>
							<asp:LinkButton ID="lnkEncumbrance" CommandName="Encumbrance" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"AccountNumber")+";E;"+DataBinder.Eval(Container.DataItem,"AccountDesc")+";"+DataBinder.Eval(Container.DataItem,"BeginPostBal")+";"+DataBinder.Eval(Container.DataItem,"EndBal")%>' Runat="server">
								<%# DataBinder.Eval(Container.DataItem,"Encumbrance") %>
							</asp:LinkButton>
						</ItemTemplate>
						<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
						<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Total Against Budget">
						<ItemTemplate>
							<asp:LinkButton ID="lnkTotBud" CommandName="TotBud" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"AccountNumber")+";A;"+DataBinder.Eval(Container.DataItem,"AccountDesc")+";"+DataBinder.Eval(Container.DataItem,"BeginPostBal")+";"+DataBinder.Eval(Container.DataItem,"EndBal")%>' Runat="server">
								<%# DataBinder.Eval(Container.DataItem,"TotalAgainstBudget") %>
							</asp:LinkButton>
						</ItemTemplate>
						<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
						<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="OtherAgainstBudget" HeaderText="Other Accounts Against Budget">
						<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
						<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="PeriodBudget" HeaderText="Total Period Budget">
						<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
						<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="AnnualBudget" HeaderText="Total Annual Budget">
						<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
						<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="OverBudget" HeaderText="Over/Under Budget">
						<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
						<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="RemainingBudget" Visible="False" HeaderText="Remaining Budget">
						<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
						<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
					</asp:BoundColumn>
				</Columns>
			</asp:datagrid></TD>
	</TR>
	<TR>
		<TD align="center">
			<TABLE id="tblPagination" cellSpacing="1" cellPadding="1" width="300" border="0">
				<TR>
					<TD align="center">
						<asp:LinkButton id="lbPrev" runat="server" Visible="False"></asp:LinkButton></TD>
					<TD align="center">
						<asp:LinkButton id="lbPos1" runat="server" Visible="False"></asp:LinkButton></TD>
					<TD align="center">
						<asp:LinkButton id="lbPos2" runat="server" Visible="False"></asp:LinkButton></TD>
					<TD align="center">
						<asp:LinkButton id="lbPos3" runat="server" Visible="False"></asp:LinkButton></TD>
					<TD align="center">
						<asp:LinkButton id="lbPos4" runat="server" Visible="False"></asp:LinkButton></TD>
					<TD align="center">
						<asp:LinkButton id="lbPos5" runat="server" Visible="False"></asp:LinkButton></TD>
					<TD align="center">
						<asp:LinkButton id="lbPos6" runat="server" Visible="False"></asp:LinkButton></TD>
					<TD align="center">
						<asp:LinkButton id="lbPos7" runat="server" Visible="False"></asp:LinkButton></TD>
					<TD align="center">
						<asp:LinkButton id="lbPos8" runat="server" Visible="False"></asp:LinkButton></TD>
					<TD align="center">
						<asp:LinkButton id="lbNext" runat="server" Visible="False"></asp:LinkButton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD>
			<table cellSpacing="3" cellPadding="3">
				<tr>
					<td><asp:button id="btnNewSearch" runat="server"></asp:button></td>
					<td><asp:button id="btnCancel" runat="server"></asp:button></td>
				</tr>
			</table>
		</TD>
	</TR>
</TABLE>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_Sayfalama.ascx.cs"
    Inherits="includes_UC_Sayfalama" %>
<div class="clear">
    <asp:Panel ID="pnlToplam" runat="server" CssClass="sayfalamatoplam">
        <asp:Label ID="lblToplam" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlSayfalama" runat="server" class="sayfalama" Visible="false">
        <asp:Literal ID="lblSayfalama" runat="server" />
    </asp:Panel>
</div>

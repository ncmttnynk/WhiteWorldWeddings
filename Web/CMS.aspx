<%@ Page Title="" Language="C#" MasterPageFile="~/MP_SolSutun.master" AutoEventWireup="true" CodeFile="CMS.aspx.cs" Inherits="CMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_CSS" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Banner" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Sol" runat="Server">
    <div class="widget widget_product_categories">
        <asp:Literal ID="ltlCmsMenu" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPH_Sag" runat="Server">
      <h1>
                <asp:Literal ID="ltlBaslik" runat="server" /></h1>
    <asp:Literal ID="ltlIcerik" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPH_JS" runat="Server">
</asp:Content>


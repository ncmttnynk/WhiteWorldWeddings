<%@ Page Title="" Language="C#" MasterPageFile="~/MP_TekSutun.master" AutoEventWireup="true" CodeFile="Hakkimizda.aspx.cs" Inherits="Hakkimizda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_CSS" runat="Server">
<link rel="stylesheet" type="text/css" href="/Compress.ashx?css=/css/owl.carousel.css">
    <link href="/css/animate.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Mansetler" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Banner" runat="Server">
     <div class="page-banner about-banner">
    	<div class="banner-content">
    		<span class="subtitle">Welcome to Boutique - Let’s shop with us !</span>
            <h2 class="title">BOUTIQUE</h2>
    	</div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPH_TekSutun" runat="Server">
   <asp:Literal ID="ltlHakkimizda" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPH_JS" runat="Server">
</asp:Content>


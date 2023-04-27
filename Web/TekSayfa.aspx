<%@ Page Title="" Language="C#" MasterPageFile="~/MP_TekSutun.master" AutoEventWireup="true" CodeFile="TekSayfa.aspx.cs" Inherits="TekSayfa" %>

<%@ Register Src="~/usercontrols/UC_BlogListesi.ascx" TagName="UC_BlogListesi" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_CSS" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Mansetler" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Banner" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPH_TekSutun" runat="Server">
    <div class="container margin-top-30" style="border-top: 1px solid #eaeaea;">
        <div class="blog-detail margin-top-30 margin-bottom-30">
            <article class="blog-item">
                <h3 class="blog-title"><a href="javascript:void(0)">
                    <asp:Literal ID="ltlBaslik" runat="server" /></a></h3>
                <div class="entry-meta">
                    <span class="post-date">
                        <asp:Literal ID="ltlTarih" runat="server" /></span>
                    <%--<span class="blog-comment"><i class="fa fa-comment"></i><span class="count-comment">36</span></span>--%>
                </div>
                <div class="post-thumbnail">
                    <asp:Image ID="imgKayit" runat="server" />
                </div>
                <div class="blog-short-desc">
                    <asp:Literal ID="ltlIcerik" runat="server" />
                </div>
            </article>
        </div>
    </div>
        <uc3:UC_BlogListesi ID="UC_BlogListesi1" runat="server" />

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPH_JS" runat="Server">
</asp:Content>


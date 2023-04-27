<%@ Page Title="" Language="C#" MasterPageFile="~/MP_SolSutun.master" AutoEventWireup="true" CodeFile="Gelinlik.aspx.cs" Inherits="Gelinlik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_CSS" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Banner" runat="Server">
    <div class="container">
        <div class="shop-banner">
            <img src="/images/kategoribg.jpg" alt="">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Sol" runat="Server">
    <asp:Panel ID="pnlFiltre" runat="server" DefaultButton="btnFiltrele">
        <div class="box-cart-total">
            <h2 class="title"><%=(DilKod == 1) ? "Sezon" : "Season" %></h2>
            <table class="padding-0">
                <tr>
                    <td>
                        <asp:RadioButtonList ID="cbSezon" runat="server" CssClass="col-md-12" />
                    </td>
                </tr>
            </table>
            <h2 class="title"><%=(DilKod == 1) ? "Kesim" : "Kesim" %></h2>
            <table class="padding-0">
                <tr>
                    <td>
                        <asp:RadioButtonList ID="cbKesim" runat="server" CssClass="col-md-12" />
                    </td>
                </tr>
            </table>
            <h2 class="title"><%=(DilKod == 1) ? "Trend" : "Colour" %></h2>
            <table class="padding-0">
                <tr>
                    <td>
                        <asp:RadioButtonList ID="cbBilgi" runat="server" CssClass="col-md-12" />
                    </td>
                </tr>
            </table>
            <%--  <h2 class="title"><%=(DilKod == 1) ? "Renk" : "Colour" %></h2>
            <table class="padding-0">
                <tr>
                    <td>
                        <asp:RadioButtonList ID="cbRenkler" runat="server" CssClass="col-md-12" />
                    </td>
                </tr>
            </table>
            <h2 class="title"><%=(DilKod == 1) ? "Kumaş" : "Material" %></h2>
            <table class="padding-0">
                <tr>
                    <td>
                        <asp:RadioButtonList ID="cbKumas" runat="server" CssClass="col-md-12" />
                    </td>
                </tr>
            </table>
            <h2 class="title"><%=(DilKod == 1) ? "Siluet" : "Siluet" %></h2>
            <table class="padding-0">
                <tr>
                    <td>
                        <asp:RadioButtonList ID="cbSiluet" runat="server" CssClass="col-md-12" />
                    </td>
                </tr>
            </table>
            <h2 class="title"><%=(DilKod == 1) ? "Yaka Tipi" : "Neck Line" %></h2>
            <table class="padding-0">
                <tr>
                    <td>
                        <asp:RadioButtonList ID="cbYakaTipi" runat="server" CssClass="col-md-12" />
                    </td>
                </tr>
            </table>--%>
            <asp:Button ID="btnFiltrele" runat="server" CssClass="button btn-primary medium checkout-button" OnClick="btnFiltrele_Click" />
            <asp:HyperLink ID="hlTemizle" runat="server" CssClass="button medium checkout-button" />
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPH_Sag" runat="Server">
    <asp:Repeater ID="rptGelinlikler" runat="server">
        <HeaderTemplate>
            <ul class="product-list-grid desktop-columns-3 tablet-columns-2 mobile-columns-1 row flex-flow">
        </HeaderTemplate>
        <ItemTemplate>
            <li class="product-item style3 col-sm-6 col-md-4">
                <div class="product-inner">
                    <div class="product-thumb has-back-image">
                        <asp:Literal ID="ltlYeni" runat="server" />
                        <a href='<%#string.Format("GelinlikDetay/{0}/{1},product",Eval("Id"),Eval("Baslik").ToString().ToURL()) %>' title='<%#Eval("Baslik") %>'>
                            <img src='<%#Eval("OnFoto") %>' alt='<%#Eval("OnFotoEtiket") %>' />
                        </a>
                        <a class="back-image" href='<%#string.Format("GelinlikDetay/{0}/{1},product",Eval("Id"),Eval("Baslik").ToString().ToURL()) %>' title='<%#Eval("Baslik") %>'>
                            <img src='<%#Eval("ArkaFoto") %>' alt='<%#Eval("ArkaFotoEtiket") %>'></a>
                        <div class="gorup-button">

                            <a href='<%#string.Format("GelinlikDetay/{0}/{1},product",Eval("Id"),Eval("Baslik").ToString().ToURL()) %>' class="quick-view"><i class="fa fa-search"></i></a>
                        </div>
                    </div>
                    <div class="product-info">
                        <h3 class="product-name"><a href='<%#string.Format("GelinlikDetay/{0}/{1},product",Eval("Id"),Eval("Baslik").ToString().ToURL()) %>' class="tdn"><%#Eval("Baslik") %></a></h3>
                        <a href='<%#string.Format("GelinlikDetay/{0}/{1},product",Eval("Id"),Eval("Baslik").ToString().ToURL()) %>' class="button add_to_cart_button">İNCELE</a>
                    </div>
                </div>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPH_JS" runat="Server">
</asp:Content>


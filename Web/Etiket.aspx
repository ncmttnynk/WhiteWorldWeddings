<%@ Page Title="" Language="C#" MasterPageFile="~/MP_TekSutun.master" AutoEventWireup="true" CodeFile="Etiket.aspx.cs" Inherits="Etiket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_CSS" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Mansetler" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Banner" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPH_TekSutun" runat="Server">
    <div class="container margin-top-30 margin-bottom-30">
        <div class="row">
            <asp:Repeater ID="rptGelinlikler" runat="server">
                <HeaderTemplate>

                    <ul class="product-list owl-carousel" data-nav="false" data-dots="false" data-margin="30" data-loop="true" data-responsive='{"0":{"items":1},"600":{"items":3},"1000":{"items":4}}'>
                </HeaderTemplate>
                <ItemTemplate>
                    <li class="product-item style5">
                        <div class="product-inner">
                            <div class="product-thumb has-back-image">
                                <asp:Literal ID="ltlYeniSezonYeni" runat="server" />
                                <a href='<%#string.Format("GelinlikDetay/{0}/{1},product",Eval("Id"),Eval("Baslik").ToString().ToURL()) %>'>
                                    <img src='<%#Eval("OnFoto") %>' alt='<%#Eval("OnFotoEtiket") %>' />
                                </a>
                                <a class="back-image" href='<%#string.Format("GelinlikDetay/{0}/{1},product",Eval("Id"),Eval("Baslik").ToString().ToURL()) %>' title='<%#Eval("Baslik") %>'>
                                    <img src='<%#Eval("ArkaFoto") %>' alt='<%#Eval("ArkaFotoEtiket") %>'></a>
                                <div class="gorup-button">
                                    <%--<a href="#" class="button add_to_cart_button">ADD TO CART</a>--%>
                                    <a href='<%#string.Format("GelinlikDetay/{0}/{1},product",Eval("Id"),Eval("Baslik").ToString().ToURL()) %>' class="quick-view"><i class="fa fa-search"></i></a>
                                    <%--<a href="#" class="compare"><i class="fa fa-exchange"></i></a>--%>
                                </div>
                            </div>
                            <div class="product-info">
                                <h3 class="product-name"><a href='<%#string.Format("GelinlikDetay/{0}/{1},product",Eval("Id"),Eval("Baslik").ToString().ToURL()) %>' class="tdn"><%#Eval("Baslik") %></a></h3>
                                <%--  <span class="price">
                                            <ins>£85.00</ins>
                                            <del>£95.00</del>
                                        </span>
                                        <a href="#" class="wishlist"><i class="fa fa-heart-o"></i></a>--%>
                            </div>
                        </div>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                  
                </FooterTemplate>
            </asp:Repeater>
            <h1><asp:Literal ID="ltlBulunamadi" runat="server" /></h1>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPH_JS" runat="Server">
</asp:Content>


<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_OzelUrun.ascx.cs" Inherits="usercontrols_UC_UrunListesi_Ozel" %>



<div class="margin-top-50" id="divGelinlikler" runat="server" visible="false">
    <div class="container">
        <div class="tab-product style2 tab-owl-fade-effect">
            <ul class="box-tabs nav-tab">
                <li class="text-head"><%=Dil.Equals("tr") ? "Ö" : "F" %></li>
                <li class="active"><a data-toggle="tab" href="#ozelUrunler"><%=Dil.Equals("tr") ? "Özel Ürünler" : "Featured" %></a></li>
            </ul>
            <div class="tab-content">
                <div class="tab-container">
                    <asp:Repeater ID="rptOzelUrunler" runat="server" OnItemDataBound="rptOzelUrunler_ItemDataBound">
                        <HeaderTemplate>
                            <div id="ozelUrunler" class="tab-panel active">
                                <ul class="product-list owl-carousel" data-nav="false" data-dots="false" data-margin="30" data-loop="true" data-autoplay="true" data-responsive='{"0":{"items":1},"600":{"items":3},"1000":{"items":4}}'>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li class="product-item style5">
                                <div class="product-inner">
                                    <div class="product-thumb has-back-image">
                                        <asp:Literal ID="ltlOzelUrunYeni" runat="server" />
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
                    </div>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</div>

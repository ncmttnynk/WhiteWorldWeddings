<%@ Page Title="" Language="C#" MasterPageFile="~/MP_TekSutun.master" AutoEventWireup="true" CodeFile="GelinlikDetay.aspx.cs" Inherits="GelinlikDetay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_CSS" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Mansetler" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Banner" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPH_TekSutun" runat="Server">
    <div class="container">
        <div class="breadcrumbs style2">
            <a href="/"><%=(DilKod==1) ? "Anasayfa" : "Home" %></a>
            <asp:HyperLink ID="hlGelinlikler" runat="server" />
            <span>
                <asp:Literal ID="ltlGelinlikBas" runat="server" />
            </span>
        </div>
        <div class="row">
            <div class="main-content col-sm-12">
                <div class="row">
                    <div class="col-sm-4">
                        <div class="product-detail-image style2">
                            <div class="main-image-wapper">
                                    <asp:Image ID="imgOnFoto" runat="server" CssClass="main-image" />
                            </div>
                            <div class="thumbnails owl-carousel nav-center-center nav-style4" data-responsive='{"0":{"items":3},"481":{"items":4},"600":{"items":3},"1000":{"items":4}}' data-autoplay="true" data-loop="true" data-items="4" data-dots="false" data-nav="true" data-margin="20">
                                <asp:Repeater ID="rptFotoKucuk" runat="server">
                                    <ItemTemplate>
                                        <a data-url='<%#Eval("FotoBuyuk") %>' class="active" href="#">
                                            <img src='<%#Eval("FotoKucuk") %>' alt=""></a>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-8">
                        <div class="product-details-right style2">
                            <h1 class="product-name">
                                <asp:Literal ID="ltlGelinlikBaslik" runat="server" /></h1>
                            <div class="rating">
                                <i class="fa fa-star"></i>
                                <i class="fa fa-star"></i>
                                <i class="fa fa-star"></i>
                                <i class="fa fa-star"></i>
                                <i class="fa fa-star"></i>
                                <span class="count-review">
                                    <asp:Literal ID="ltlYorum" runat="server" /></span>
                            </div>
                            <div class="meta">
                                <span>Stok Durumu: <span class="text-primary">Mevcut</span></span>
                            </div>
                            <div class="short-descript">
                                <asp:Literal ID="ltlGelinlikAciklama" runat="server" />
                            </div>
                            <table class="group-product table">
                                <tbody>
                                    <%--   <tr>
                                    <td><%=(DilKod == 1) ? "Beden" : "Size" %></td>
                                    <td>
                                        <span class="price">
                                            <ins>
                                                <asp:Literal ID="ltlBeden" runat="server" /></ins>
                                        </span>
                                    </td>
                                </tr>--%>
                                    <%--<tr>
                                        <td><%=(DilKod == 1) ? "Renk" : "Colour" %></td>
                                        <td>
                                            <span class="price">
                                                <ins>
                                                    <asp:Literal ID="ltlRenk" runat="server" /></ins>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><%=(DilKod == 1) ? "Kumaş" : "Material" %></td>
                                        <td>
                                            <span class="price">
                                                <ins>
                                                    <asp:Literal ID="ltlKumas" runat="server" /></ins>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><%=(DilKod == 1) ? "Siluet" : "Colour" %></td>
                                        <td>
                                            <span class="price">
                                                <ins>
                                                    <asp:Literal ID="ltlSiluet" runat="server" /></ins>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><%=(DilKod == 1) ? "Yaka Tipi" : "Yaka Tipi" %></td>
                                        <td>
                                            <span class="price">
                                                <ins>
                                                    <asp:Literal ID="ltlYakaTipi" runat="server" /></ins>
                                            </span>
                                        </td>
                                    </tr>--%>
                                </tbody>
                            </table>
                            <div class="widget widget_product_tag_cloud style2">
                                <h2 class="widget-title"><%=(DilKod ==1 ) ? "Etiketler" : "Tags" %></h2>
                                <div class="tagcloud">
                                    <asp:Repeater ID="rptTag" runat="server">
                                        <ItemTemplate>
                                            <a href='<%#string.Format("/{0}/{1},etiket",Dil,Eval("Baslik").ToString().ToURL()) %>' title='<%#Eval("Baslik") %>'><%#Eval("Baslik") %></a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="tab-details-product padding-40-60">
            <ul class="box-tabs nav-tab">
                <li class="active"><a data-toggle="tab" href="#tab-1">
                    <asp:Literal ID="ltlDetayBaslik" runat="server" /></a></li>
            </ul>
            <div class="tab-container">
                <div id="tab-1" class="tab-panel active">
                    <asp:Literal ID="ltlGelinlikDetay" runat="server" />
                </div>

            </div>
        </div>
        <asp:Panel ID="pnlBenzerUrunlar" runat="server" Visible="false">
            <div class="upsell-products">
                <div class="section-title text-center">
                    <h1>
                        <asp:Literal ID="ltlAltUrunBaslik" runat="server" /></h1>
                </div>
                <asp:Repeater ID="rptBenzerUrunler" runat="server">
                    <HeaderTemplate>
                        <ul class="owl-carousel" data-responsive='{"0":{"items":1},"600":{"items":3},"1000":{"items":4}}' data-autoplay="true" data-loop="true" data-items="4" data-dots="false" data-nav="false" data-margin="30">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li class="product-item">
                            <div class="product-inner">
                                <div class="product-thumb">
                                    <a href='<%#string.Format("/{0}/GelinlikDetay/{1}/{2},product",Dil,Eval("Id"),Eval("Baslik").ToString().ToURL()) %>' title='<%#Eval("Baslik") %>'>
                                        <img src='<%#Eval("Fotograf") %>' alt='<%#Eval("Etiket") %>'></a>
                                    <div class="gorup-button">
                                        <a href='<%#string.Format("/{0}/GelinlikDetay/{1}/{2},product",Dil,Eval("Id"),Eval("Baslik").ToString().ToURL()) %>' class="quick-view"><i class="fa fa-search"></i></a>
                                    </div>
                                </div>
                                <div class="product-info">
                                    <h3 class="product-name"><a href='<%#string.Format("/{0}/GelinlikDetay/{1}/{2},product",Dil,Eval("Id"),Eval("Baslik").ToString().ToURL()) %>'><%#Eval("Baslik") %></a></h3>
                                </div>
                            </div>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </asp:Panel>
    </div>
    <div class="container margin-top-60">
        <div id="comments">
            <asp:Panel ID="pnlYorumlar" runat="server" Visible="false">
                <asp:Repeater ID="rptYorumlar" runat="server" OnItemDataBound="rptYorumlar_ItemDataBound">
                    <HeaderTemplate>
                        <h4 class="title-border"><span class="text"><%=(Dil.Equals("tr") ? "Müşteri Yorumları" : "Customer Comments") %></span><span class="subtext"></span></h4>
                        <ul class="comment-list">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li class="comment even parent">
                            <div class="comment-item">
                                <div class="comment-author">
                                    <img alt="18_blogpost" src="/images/logo.png">
                                </div>
                                <div class="comment-body">
                                    <h5 class="author"><%#Eval("AdSoyad") %></h5>
                                    <span class="date-comment"><%#Eval("Tarih") %></span>
                                    <div class="comment-content">
                                        <p><%#Eval("Aciklama") %></p>
                                    </div>
                                    <%--<asp:LinkButton ID="lbYorumBirak" runat="server" CssClass="comment-reply-link" OnClientClick="return SetFocus()" />--%>
                                </div>
                            </div>
                            <asp:Repeater ID="rptAltYorumlar" runat="server" Visible="false">
                                <ItemTemplate>
                                    <ol class="children">
                                        <li class="comment odd">
                                            <div class="comment-item">
                                                <div class="comment-author">
                                                    <img alt="18_blogpost" src="/images/logo.png">
                                                </div>
                                                <div class="comment-body">
                                                    <h5 class="author"><%#Eval("AdSoyad") %></h5>
                                                    <span class="date-comment"><%#Eval("Tarih") %></span>
                                                    <div class="comment-content">
                                                        <p><%#Eval("Aciklama") %></p>
                                                    </div>
                                                    <asp:LinkButton ID="lbYorumBirak" runat="server" CssClass="comment-reply-link" OnClientClick="return SetFocus()" />
                                                </div>
                                            </div>
                                        </li>
                                    </ol>
                                </ItemTemplate>
                            </asp:Repeater>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
            </asp:Panel>
            <asp:Panel ID="pnlYorum" runat="server" DefaultButton="btnYorumKaydet">
                <div class="comment-form margin-bottom-30" id="yorumlar" runat="server">
                    <h4 class="title-border margin-top-10"><span class="text"><%=(Dil.Equals("tr") ? "Bir yorum bırak!" : "Leave a comment!") %></span></h4>
                    <asp:TextBox ID="txtYorumAdSoyad" runat="server" size="30" ValidationGroup="yorum" />
                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtYorumAdSoyad"
                        ValidationGroup="yorum" SetFocusOnError="true" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtYorumEposta" runat="server" size="30" />
                    <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtYorumEposta"
                        ValidationGroup="yorum" SetFocusOnError="true" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revEPosta" runat="server"
                        ControlToValidate="txtYorumEposta" ValidationGroup="yorum" SetFocusOnError="true"
                        Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                           <span class="text" style="float:left;"><%=(DilKod == 1) ? " Geçersiz e-posta adresi!" : " Invalid e-mail adress!" %></span>
                    </asp:RegularExpressionValidator>
                    <asp:TextBox ID="txtYorumAciklama" runat="server" CssClass="comment-form-comment" TextMode="MultiLine" Rows="8" />
                    <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="txtYorumAciklama"
                        ValidationGroup="yorum" SetFocusOnError="true" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    <div class="clear"></div>
                    <asp:Button ID="btnYorumKaydet" runat="server" CssClass="submit style2" Text="Kaydet" OnClick="btnYorumKaydet_Click" ValidationGroup="yorum" />
                    <input class="button" type="hidden" name="comment_parent" id="comment_parent" value="0">
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPH_JS" runat="Server">
    <script type="text/javascript">
        function SetFocus() {
            document.getElementById('<%=yorumlar.ClientID %>').focus();
            document.getElementById('<%=txtYorumAdSoyad.ClientID %>').focus();
            return false;
        }

    </script>
    <script src="/admin/js/examples/examples.lightboxes.js"></script>
</asp:Content>


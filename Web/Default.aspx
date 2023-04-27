<%@ Page Title="" Language="C#" MasterPageFile="~/MP_TekSutun.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/usercontrols/UC_Mansetler.ascx" TagName="UC_Mansetler" TagPrefix="uc1" %>
<%@ Register Src="~/usercontrols/UC_BlogListesi.ascx" TagName="UC_BlogListesi" TagPrefix="uc3" %>
<%@ Register Src="usercontrols/UC_EnCokSatan.ascx" TagName="UC_EnCokSatan" TagPrefix="uc2" %>
<%@ Register Src="usercontrols/UC_OzelUrun.ascx" TagName="UC_OzelUrun" TagPrefix="uc4" %>
<%@ Register Src="usercontrols/UC_YeniSezon.ascx" TagName="UC_YeniSezon" TagPrefix="uc5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_CSS" runat="Server">
    <link rel="stylesheet" type="text/css" href="/Compress.ashx?css=/css/owl.carousel.css">
    <link href="/css/animate.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Mansetler" runat="Server">
    <uc1:UC_Mansetler ID="UC_Mansetler1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Banner" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPH_TekSutun" runat="Server">
    <div class="container">
        <div class="margin-top-40">
            <div class="row">
                <div class="col-sm-4" style="text-align: right;">
                    <img style="margin-right: 20px;" class=" margin-top-25" src="/images/logo.png" alt="">
                </div>
                <div class="col-sm-7">
                    <blockquote>Değerli Müşterilerimiz; White House olarak bütün dünyayı etkisi altına alan ve ne yazık ki Türkiye'ye de sıçramış olan Corona Virüs (COVID-19) salgıgına dair bütün gelişmeleri ilk günden itibaren yakından takip ediyor ve gerekli bütün önlemleri alıyoruz. 23 Mart itibariyle toptan, perakende mağazalarımızdaki ve üretim merkezlerimizdeki hizmetlerimize geçici olarak ara vermiş bulunmaktayız. Sağlıklı günlerde buluşmak dileğiyle...</blockquote>
                </div>
            </div>
        </div>
    </div>
    <%--<div class="owl-carousel nav-style4 nav-center-center" data-nav="false" data-dots="false" data-loop="false" data-autoplay="false" data-margin="0" data-responsive='{"0":{"items":1},"600":{"items":2},"1000":{"items":3}}'>
        <div class="banner-text style4">
            <div class="image">
                <a href="Gelinlik?Ara=|kesim|1">
                    <img src="/images/category/A-Kesim.jpg" alt="">
                </a>
            </div>
            <div class="content-text">
                <h4 class="subtitle">A Kesim</h4>
                <a class="title" href="Gelinlik?Ara=|kesim|1">İNCELE</a>
            </div>
        </div>
        <div class="banner-text style4">
            <div class="image">
                <a href="Gelinlik?Ara=|kesim|2">
                    <img src="/images/category/Prenses-Kesim.jpg" alt=""></a>
            </div>
            <div class="content-text">
                <h4 class="subtitle">Prenses Kesim</h4>
                <a class="title" href="Gelinlik?Ara=|kesim|2">İNCELE</a>
            </div>
        </div>
        <div class="banner-text style4">
            <div class="image">
                <a href="Gelinlik?Ara=|kesim|3">
                    <img src="/images/category/Balik-Kesim.jpg" alt=""></a>
            </div>
            <div class="content-text">
                <h4 class="subtitle">Balik Kesim</h4>
                <a class="title" href="Gelinlik?Ara=|kesim|3">İNCELE</a>
            </div>
        </div>
    </div>--%>
    <uc2:UC_EnCokSatan ID="UC_EnCokSatan1" runat="server" />
    <div class="block-paralax3">
        <div class="container">
            <div class="section-content text-center">
                <h2 class="title"><strong>KATALOG</strong> 2020</h2>
                <h3 class="sub-title text-primary">YENİ KOLEKSİYON</h3>
                <a class="button btn-primary small" href="Gelinlik?Ara=|sezon|2020">İNCELE</a>
            </div>
        </div>
    </div>
    <div class="container">
        <asp:Panel runat="server" ID="pnlMail">
            <div class="margin-top-60">
                <div class="row">
                    <div class="col-sm-12 col-md-7">
                        <div class="video video-lightbox">
                            <img src="/images/video-bg.jpg" alt="">
                            <div class="overlay"></div>
                            <%--<a href="#" class="link-lightbox button-play" data-videoid="kLz3WOH22qU" data-videosite="youtube"></a>--%>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-5">
                        <div class="newsletter">
                            <div class="section-title text-center">
                                <h3>Takip edin!</h3>
                            </div>
                            <i class="newsletter-info">Mail listemize kayıt olmak için formu doldurabilirsiniz.</i>
                            <div class="form-newsletter">
                                <asp:TextBox ID="txtMail" runat="server" placeholder="mailadresiniz@abc.com"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtMail"
                                    ValidationGroup="mail" SetFocusOnError="true" Display="Dynamic">
                                <label><%=(DilKod == 1) ? "Boş geçilemez!" : "!" %></label>
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revEPosta" runat="server"
                                    ControlToValidate="txtMail" ValidationGroup="mail" SetFocusOnError="true"
                                    Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                <label><%=(DilKod == 1) ? "Geçersiz e-posta adresi!" : "Invalid e-mail adress!" %></label>
                                </asp:RegularExpressionValidator>
                                <span>
                                    <asp:LinkButton ID="lbMail" runat="server" CssClass="newsletter-submit" OnClick="lbMail_Click" ValidationGroup="mail">KAYIT OL</asp:LinkButton>
                                </span>
                                <asp:Literal runat="server" ID="ltlMessage" Visible="false"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
    <uc4:UC_OzelUrun ID="UC_OzelUrun1" runat="server" />
    <div class="block-paralax1 bg-parallax">
        <%--<div class="container">
            <div class="row">
                <div class="col-sm-12 col-md-5">
                    <div class="section-title style5 margin-top-230">
                        <h3><span class="text-head">G</span><span>Görüşler</span></h3>
                    </div>
                </div>
                <div class="col-sm-12 col-md-7">
                    <div class="testimonials testimonials-owl-3 margin-top-110">
                        <div class="testimonial-owl">
                            <div class="testimonial">
                                <div class="avatar">
                                    <img src="/images/testimonials/testimonial2.png" alt="" />
                                </div>
                                <div class="inner">
                                    <p class="text-in">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard.</p>
                                    <h6>John Brown</h6>
                                </div>
                            </div>
                            <div class="testimonial">
                                <div class="avatar">
                                    <img src="/images/testimonials/testimonial2.png" alt="" />
                                </div>
                                <div class="inner">
                                    <p class="text-in">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard.</p>
                                    <h6>David Smith</h6>
                                </div>
                            </div>
                            <div class="testimonial">
                                <div class="avatar">
                                    <img src="/images/testimonials/testimonial3.png" alt="" />
                                </div>
                                <div class="inner">
                                    <p class="text-in">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard.</p>
                                    <h6>Thor</h6>
                                </div>
                            </div>
                        </div>
                        <div data-animated="fadeIn" class="testimonial-info"></div>
                    </div>
                </div>
            </div>
        </div>--%>
        <div class="container">
            <div class="row">
                <div class="col-sm-4">
                    <div class="element-icon style3">
                        <div class="icon">
                            <i class="flaticon flaticon-origami28"></i>
                        </div>
                        <div class="content">
                            <h4 class="title">Dünyanın 4 bir <strong>yanına!</strong></h4>
                            <span class="subtite">Sadece Türkiye değil her yere...</span>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="element-icon style3">
                        <div class="icon">
                            <i class="flaticon flaticon-curvearrows9"></i>
                        </div>
                        <div class="content">
                            <h4 class="title">Kişiye Özgü<strong> Tasarım!</strong></h4>
                            <span class="subtite">Tüm ürünlerimizde özel tasarım...</span>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="element-icon style3">
                        <div class="icon">
                            <i class="flaticon flaticon-headphones54"></i>
                        </div>
                        <div class="content">
                            <h4 class="title">İstediğiniz <strong>zaman!</strong></h4>
                            <span class="subtite">Bizimle iletişime geçin...</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc5:UC_YeniSezon ID="UC_YeniSezon1" runat="server" />


    <%--<asp:Panel ID="pnlTekUrun" runat="server" Visible="false">
        <div class="section-block-single-product2">
            <div class="container">
                <div class="block-single-product row">
                    <div class="product-info col-sm-7">
                        <h3 class="product-name">
                            <asp:HyperLink ID="hlGelinlik" runat="server" />
                        </h3>
                        <div class="desc">
                            <p>
                                <asp:Literal ID="ltlGelinlikAciklama" runat="server" />
                            </p>
                            <table class="group-product table anasayfa">
                                <tbody>
                                      <tr>
                                        <td><%=(DilKod == 1) ? "Beden" : "Size" %></td>
                                        <td>
                                            <span class="price">
                                                <ins>
                                                    <asp:Literal ID="ltlBeden" runat="server" /></ins>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
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
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                      <span class="price">£185.00</span
                        <asp:HyperLink ID="hlGelinlikAlt" runat="server" CssClass="quick-view addtocart">
                    <i class="fa fa-search"></i>
                </asp:HyperLink>
                    </div>
                    <div class="product-image col-sm-5">
                        <asp:HyperLink ID="hlGelinlikFotograf" runat="server">
                            <asp:Image ID="imgGelinlik" runat="server" />
                        </asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>--%>
    <%--<div class="owl-carousel nav-style4 nav-center-center " data-nav="true" data-dots="false" data-loop="true" data-autoplay="false" data-margin="0" data-responsive='{"0":{"items":1},"600":{"items":2},"1000":{"items":4}}'>
        <div class="banner-text style4">
            <div class="image">
                <a href="#">
                    <img src="/images/deneme.jpg" alt=""></a>
            </div>
            <div class="content-text">
                <h4 class="subtitle">Trending</h4>
                <h3 class="title">Men fashion</h3>
                <a class="shop-now" href="#">shop now!</a>
            </div>
        </div>
        <div class="banner-text style4">
            <div class="image">
                <a href="#">
                    <img src="/images/deneme.jpg" alt=""></a>
            </div>
            <div class="content-text">
                <h4 class="subtitle">Trending</h4>
                <h3 class="title">Men fashion</h3>
                <a class="shop-now" href="#">shop now!</a>
            </div>
        </div>
        <div class="banner-text style4">
            <div class="image">
                <a href="#">
                    <img src="/images/deneme.jpg" alt=""></a>
            </div>
            <div class="content-text">
                <h4 class="subtitle">Trending</h4>
                <h3 class="title">Men fashion</h3>
                <a class="shop-now" href="#">shop now!</a>
            </div>
        </div>
        <div class="banner-text style4">
            <div class="image">
                <a href="#">
                    <img src="/images/deneme.jpg" alt=""></a>
            </div>
            <div class="content-text">
                <h4 class="subtitle">Trending</h4>
                <h3 class="title">Men fashion</h3>
                <a class="shop-now" href="#">shop now!</a>
            </div>
        </div>
    </div>--%>
    <uc3:UC_BlogListesi ID="UC_BlogListesi1" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPH_JS" runat="Server">
</asp:Content>


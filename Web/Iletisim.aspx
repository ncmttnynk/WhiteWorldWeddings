<%@ Page Title="" Language="C#" MasterPageFile="~/MP_TekSutun.master" AutoEventWireup="true" CodeFile="Iletisim.aspx.cs" Inherits="Iletisim" %>

<%@ Register Src="~/includes/GoogleMap/GoogleMapForASPNet.ascx" TagName="GoogleMapForASPNet"
    TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_CSS" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Mansetler" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Banner" runat="Server">
    <div class="page-banner contact-banner">
        <div class="banner-content">
            <span class="subtitle">
                <asp:Literal ID="ltlUst" runat="server" /></span>
            <h2 class="title">
                <asp:Literal ID="ltlAlt" runat="server" /></h2>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPH_TekSutun" runat="Server">
    <div class="container">
        <div class="row margin-bottom-30">
            <asp:Panel ID="pnlIletisim" runat="server">
                <div class="col-sm-6">
                    <div class="kt-contact-form margin-top-60">
                        <div id="message-box-conact"></div>
                        <h3 class="title">
                            <asp:Literal ID="ltlIletisimFormBaslik" runat="server" /></h3>
                        <p>

                            <asp:TextBox ID="txtIletisimAdSoyad" runat="server" size="30" ValidationGroup="iletisim" />
                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtIletisimAdSoyad"
                                ValidationGroup="iletisim" SetFocusOnError="true" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:TextBox ID="txtIletisimEposta" runat="server" size="30" />
                            <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtIletisimEposta"
                                ValidationGroup="iletisim" SetFocusOnError="true" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEPosta" runat="server"
                                ControlToValidate="txtIletisimEposta" ValidationGroup="iletisim" SetFocusOnError="true"
                                Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                           <span class="text" style="float:left;"><%=(DilKod == 1) ? " Geçersiz e-posta adresi!" : " Invalid e-mail adress!" %></span>
                            </asp:RegularExpressionValidator>
                        </p>
                        <p>
                            <asp:TextBox ID="txtIletisimAciklama" runat="server" CssClass="comment-form-comment" TextMode="MultiLine" Rows="8" />
                            <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="txtIletisimAciklama"
                                ValidationGroup="iletisim" SetFocusOnError="true" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </p>
                        <asp:Button ID="btnYorumKaydet" runat="server" CssClass="submit style2" Text="Kaydet" OnClick="btnYorumKaydet_Click" ValidationGroup="iletisim" />

                    </div>
                </div>
            </asp:Panel>
            <div class="col-sm-6">
                <div class="margin-top-60">
                    <img src="/images/map.jpg" alt="adresimiz, osmanbey, gelinlik, white, world, beyaz, dünya">
                    <asp:Literal ID="ltlIletisimAciklama" runat="server" />
                     </div>
            </div>
        </div>
    </div>
    <%--<div class="margin-top-60 margin-bottom-30">
        <div class="container">
            <div class="row">
                <div class="col-sm-12 col-md-4">
                    <div class="element-icon style2">
                        <div class="icon"><i class="flaticon flaticon-origami28"></i></div>
                        <div class="content">
                            <h4 class="title">FREE SHIPPING WORLD WIDE</h4>
                        </div>
                    </div>
                </div>
                <div class="col-sm-12 col-md-4">
                    <div class="element-icon style2">
                        <div class="icon"><i class="flaticon flaticon-curvearrows9"></i></div>
                        <div class="content">
                            <h4 class="title">MONEY BACK GUARANTEE</h4>
                        </div>
                    </div>
                </div>
                <div class="col-sm-12 col-md-4">
                    <div class="element-icon style2">
                        <div class="icon"><i class="flaticon flaticon-headphones54"></i></div>
                        <div class="content">
                            <h4 class="title">ONLINE SUPPORT 24/7</h4>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
    <%--<div class="container margin-bottom-30 margin-top-30">
        <uc2:GoogleMapForASPNet ID="GoogleMapForASPNet1" runat="server" />
    </div>--%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPH_JS" runat="Server">
</asp:Content>


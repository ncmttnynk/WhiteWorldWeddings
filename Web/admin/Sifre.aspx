<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sifre.aspx.cs" Inherits="admin_Sifre" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <!-- Basic -->
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <!-- Favicon -->

    <!-- Mobile Metas -->
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1.0, shrink-to-fit=no" />

    <!-- Web Fonts  -->
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700,800%7CShadows+Into+Light" rel="stylesheet" type="text/css" />

    <!-- Vendor CSS -->
    <link rel="stylesheet" href="/admin/vendor/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="/admin/vendor/font-awesome/css/fontawesome-all.min.css" />
    <link rel="stylesheet" href="/admin/vendor/animate/animate.min.css" />
    <link rel="stylesheet" href="/admin/vendor/simple-line-icons/css/simple-line-icons.min.css" />
    <link rel="stylesheet" href="/admin/vendor/owl.carousel/assets/owl.carousel.min.css" />
    <link rel="stylesheet" href="/admin/vendor/owl.carousel/assets/owl.theme.default.min.css" />
    <link rel="stylesheet" href="/admin/vendor/magnific-popup/magnific-popup.min.css" />

    <!-- Theme CSS -->
    <link rel="stylesheet" href="/admin/css/theme.css" />
    <link rel="stylesheet" href="/admin/css/theme-elements.css" />
    <link rel="stylesheet" href="/admin/css/theme-blog.css" />
    <link rel="stylesheet" href="/admin/css/theme-shop.css" />

    <!-- Current Page CSS -->
    <link rel="stylesheet" href="/admin/vendor/rs-plugin/css/settings.css" />
    <link rel="stylesheet" href="/admin/vendor/rs-plugin/css/layers.css" />
    <link rel="stylesheet" href="/admin/vendor/rs-plugin/css/navigation.css" />
    <link rel="stylesheet" href="/admin/vendor/circle-flip-slideshow/css/component.css" />
    <!-- Demo CSS -->

    <!-- Skin CSS -->
    <link rel="stylesheet" href="/admin/css/skins/default.css" />

    <!-- Theme Custom CSS -->
    <link rel="stylesheet" href="/admin/css/custom.css" />

    <!-- Head Libs -->
    <script src="/admin/vendor/modernizr/modernizr.min.js"></script>

    <title>Admin | dotaajans.com</title>
</head>
<body style="background-image: url('/admin/img/patterns/honey_im_subtle.png');">
    <form id="form1" runat="server">
        <div class="body" style="background-image: url('/admin/img/patterns/honey_im_subtle.png');">
            <div role="main" class="main">
                <div style="margin: 0px auto; margin-top: 180px; width: 500px;">
                    <div class="alert alert-danger" id="divHata" runat="server" visible="false">
                        <strong>Giriş bilgileriniz hatalı!</strong>
                    </div>
                    <div class="featured-box featured-box-primary text-left mt-5">
                        <asp:Panel ID="pnlContent" runat="server" CssClass="box-content" DefaultButton="btnGiris">
                            <h4 class="heading-primary mb-3">Giriş Bilgileri</h4>
                            <div class="form-row">
                                <div class="form-group col">
                                    <label>Yönetici Kodu:</label>
                                    <asp:TextBox ID="txtKod" runat="server" CssClass="form-control input-lg" />
                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtKod" Display="Dynamic" SetFocusOnError="true" ValidationGroup="giris">
                                    <span class="alert-danger"><i class="fa fa-exclamation"></i> Boş geçemezsiniz</span>    
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="form-group col">
                                    <label>Şifre:</label>
                                    <asp:TextBox ID="txtSifre" runat="server" CssClass="form-control input-lg" TextMode="Password" />
                                    <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtSifre" Display="Dynamic" SetFocusOnError="true" ValidationGroup="giris">
                                    <span class="alert-danger"><i class="fa fa-exclamation"></i> Boş geçemezsiniz</span>    
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="form-group col-lg-6">
                                    <label>Doğrulama Kodu:</label>
                                    <asp:TextBox ID="txtCaptcha" runat="server" AutoCompleteType="Disabled" CssClass="form-control input-lg" />
                                </div>
                                <div class="form-group col-lg-6 mt-4">
                                    <asp:Image ID="imgCaptcha" runat="server" ImageUrl="~/JpegImage.ashx" CssClass="mt-1" />
                                    <div class="alert alert-danger" id="divCaptcha" runat="server" visible="false">
                                        <strong>Giriş bilgileriniz hatalı!</strong>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="form-group col-lg-6">
                                    <div class="form-check form-check-inline">
                                        <label class="form-check-label">
                                            <asp:CheckBox ID="cbBeniHatirla" runat="server" Text="Beni Hatırla" Checked="true" />
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group col-lg-6">
                                    <asp:Button ID="btnGiris" runat="server" Text="Giriş Yap" CssClass="btn btn-primary float-right mb-5" ValidationGroup="giris" OnClick="btnGiris_Click" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <!-- Vendor -->
    <script src="/admin/vendor/jquery/jquery.min.js"></script>
</body>
</html>


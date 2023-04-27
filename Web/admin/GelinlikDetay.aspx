<%@ Page Title="" Language="C#" MasterPageFile="~/admin/MP_Admin.master" AutoEventWireup="true" CodeFile="GelinlikDetay.aspx.cs" Inherits="admin_GelinlikDetay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Sag" runat="Server">
    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnKaydet" EventName="" />
            <asp:AsyncPostBackTrigger ControlID="rptMakineFotograflar" EventName="" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="fotouploading" runat="server" Style="display: none;">
                <div class="popupbox" style="padding: 10px; height: 550px; width: 550px; overflow: auto;">
                    <div style="padding-top: 0px; text-align: center;">
                        <img src="/admin/img/bekle.gif" alt="" />
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlMakineKategori" runat="server" DefaultButton="btnKaydet">
                <div class="row">
                    <div class="col">
                        <section class="card card-admin">
                            <header class="card-header">
                                <h2 class="card-title">
                                    <asp:Literal ID="ltlFormBaslik" runat="server" /></h2>
                            </header>
                            <div class="card-body">
                                <div class="form-horizontal form-bordered">
                                    <div class="form-group row">
                                        <label class="col-lg-2 control-label text-lg-right pt-2">Başlık:</label>
                                        <div class="col-lg-6">
                                            <asp:TextBox ID="txtGelinlikBaslik" runat="server" CssClass="form-control" placeholder="Başlık" />
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-lg-2 control-label text-lg-right pt-2">Sezon:</label>
                                        <div class="col-lg-2">
                                            <asp:DropDownList ID="ddlSezon" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Seçiniz..." Value="0" />
                                                <asp:ListItem Text="2018" Value="2018" />
                                                <asp:ListItem Text="2019" Value="2019" />
                                                <asp:ListItem Text="2020" Value="2020" />
                                                <asp:ListItem Text="2021" Value="2022" />
                                                <asp:ListItem Text="2022" Value="2022" />
                                            </asp:DropDownList>
                                        </div>
                                        <%--   <label class="col-lg-2 control-label text-lg-right pt-2">Beden:</label>
                                        <div class="col-lg-2">
                                            <asp:DropDownList ID="ddlBeden" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Seçiniz..." Value="0" />
                                                <asp:ListItem Text="30" Value="30" />
                                                <asp:ListItem Text="32" Value="32" />
                                                <asp:ListItem Text="34" Value="34" />
                                                <asp:ListItem Text="36" Value="36" />
                                                <asp:ListItem Text="38" Value="38" />
                                            </asp:DropDownList>
                                        </div>--%>
                                        <%-- <label class="col-lg-2 control-label text-lg-right pt-2">Kumaş:</label>
                                        <div class="col-lg-2">
                                            <asp:DropDownList ID="ddlKumas" runat="server" CssClass="form-control" />
                                        </div>
                                        <label class="col-lg-2 control-label text-lg-right pt-2">Renk:</label>
                                        <div class="col-lg-2">
                                            <asp:DropDownList ID="ddlRenk" runat="server" CssClass="form-control" />
                                        </div>--%>
                                    </div>
                                    <div class="form-group row">
                                        <%--<label class="col-lg-2 control-label text-lg-right pt-2">Siluet:</label>
                                        <div class="col-lg-2">
                                            <asp:DropDownList ID="ddlSiluet" runat="server" CssClass="form-control" />
                                        </div>
                                        <label class="col-lg-2 control-label text-lg-right pt-2">Yaka Tipi:</label>
                                        <div class="col-lg-2">
                                            <asp:DropDownList ID="ddlYakaTipi" runat="server" CssClass="form-control" />
                                        </div>--%>
                                        <label class="col-lg-2 control-label text-lg-right pt-2">Kesim:</label>
                                        <div class="col-lg-2">
                                            <asp:DropDownList ID="ddlKesim" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-lg-2 control-label text-lg-right pt-2">Öncelik:</label>
                                        <div class="col-lg-6">
                                            <asp:TextBox ID="txtGelinlikOncelik" runat="server" CssClass="form-control" placeholder="Öncelik (0-9999)" />
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-lg-2 control-label text-lg-right pt-2">Tag:</label>
                                        <div class="col-lg-6">
                                            <asp:TextBox ID="txtGelinlikTag" runat="server" CssClass="form-control" placeholder="Tag (Aralarına boşluk koymayı unutmayınız!)" />
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-lg-2 control-label text-lg-right pt-2">Yeni Sezon:</label>
                                        <div class="col-lg-1 mt-2">
                                            <asp:CheckBox ID="cbYeniSezon" runat="server" CssClass="input-group-lg" />
                                        </div>
                                        <label class="col-lg-2 control-label text-lg-right pt-2">Özel Ürün:</label>
                                        <div class="col-lg-1 mt-2">
                                            <asp:CheckBox ID="cbOzelUrun" runat="server" CssClass="input-group-lg" />
                                        </div>
                                        <label class="col-lg-2 control-label text-lg-right pt-2">En Çok Satan:</label>
                                        <div class="col-lg-1 mt-2">
                                            <asp:CheckBox ID="cbEnCokSatan" runat="server" CssClass="input-group-lg" />
                                        </div>
                                        <label class="col-lg-2 control-label text-lg-right pt-2">Yeni:</label>
                                        <div class="col-lg-1 mt-2">
                                            <asp:CheckBox ID="cbYeni" runat="server" CssClass="input-group-lg" />
                                        </div>

                                    </div>
                                    <div class="form-group row">
                                        <label class="col-lg-2 control-label text-lg-right pt-2">Fotoğraflar:</label>
                                        <div class="col-lg-6">
                                            <ajaxToolkit:AsyncFileUpload ID="fuMakineFotograf" ThrobberID="fotouploading" OnUploadedComplete="fuMakineFotograf_UploadedComplete" runat="server" CssClass="background-color-white form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-lg-2 control-label text-lg-right pt-2"></label>
                                        <div class="col-lg-6">
                                            <asp:TextBox ID="txtFotografEtiket" runat="server" CssClass="form-control" placeholder="Fotoğraf Etiketi" />
                                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtFotografEtiket"
                                                ValidationGroup="gelinlik" SetFocusOnError="true" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-lg-2 control-label text-lg-right pt-2"></label>
                                        <div class="col-lg-6">
                                            <asp:Button ID="btnFotografUpload" runat="server" CssClass="btn btn-primary" ValidationGroup="gelinlik" OnClick="btnFotografUpload_Click" Text="Kaydet" />
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnlFotograflar" runat="server" Visible="false">
                                        <div class="form-group row">
                                            <asp:Repeater ID="rptMakineFotograflar" runat="server" OnItemCommand="rptMakineFotograflar_ItemCommand" OnItemDataBound="rptMakineFotograflar_ItemDataBound">
                                                <HeaderTemplate>
                                                    <label class="col-lg-2 control-label text-lg-right pt-2"></label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="col-md-2 pull-left">
                                                        <asp:HyperLink ID="lbFotograf" runat="server" NavigateUrl='<%#Eval("FotoKucuk") %>' CssClass=" lightbox img-thumbnail d-inline-block" data-plugin-options="{'type':'image'}" ToolTip='<%#Eval("FotoKucuk") %>'>
                                                            <ny:MyImage ID="imgMakineFotograf" runat="server" CssClass="img-fluid" ImageUrl='<%#Eval("FotoKucuk") %>' MaxWidth="200" MaxHeight="200" />
                                                        </asp:HyperLink>
                                                        <br />
                                                        <asp:LinkButton ID="lbFotografSil" runat="server" ToolTip="Sil" CommandArgument='<%#Eval("Id") %>' CommandName="Sil" CssClass="ml-1 tdn">
                                                      <i class="fas fa-trash-alt"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lbOnFoto" runat="server" Visible="true" ToolTip="Ön Fotoğraf Yap" CommandArgument='<%#Eval("Id") %>' CommandName="OnFoto" CssClass="ml-1 tdn">
                                                      <i class="far fa-arrow-alt-circle-left"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lbArkaFoto" runat="server" Visible="true" ToolTip="Arka Fotoğraf Yap" CommandArgument='<%#Eval("Id") %>' CommandName="ArkaFoto" CssClass="ml-1 tdn">
                                                     <i class="far fa-arrow-alt-circle-right"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lbAnasayfa" runat="server" Visible="true" ToolTip="Anasayfa Fotoğraf Yap" CommandArgument='<%#Eval("Id") %>' CommandName="Anasayfa" CssClass="ml-1 tdn">
                                                     <i class="far fa-arrow-alt-circle-up"></i>
                                                        </asp:LinkButton>
                                                    </div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </asp:Panel>
                                    <div class="form-group row">
                                        <label class="col-lg-2 control-label text-lg-right pt-2">Açıklama:</label>
                                        <div class="col-lg-9">
                                            <CKEditor:CKEditorControl ID="ceGelinlikAciklama" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-lg-2 control-label text-lg-right pt-2">Detaylar:</label>
                                        <div class="col-lg-9">
                                            <CKEditor:CKEditorControl ID="ceGelinlikDetay" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-lg-2 control-label text-lg-right pt-2">Göster:</label>
                                        <div class="col-lg-2 mt-2">
                                            <asp:CheckBox ID="cbGelinlikGoster" runat="server" Checked="true" CssClass="input-group-lg" />
                                        </div>
                                        <label class="col-lg-2 control-label text-lg-right pt-2">Anasayfa:</label>
                                        <div class="col-lg-1 mt-2">
                                            <asp:CheckBox ID="cbAnasayfa" runat="server" CssClass="input-group-lg" Checked="true" />
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-lg-9"></div>
                                        <div class="col-lg-3">
                                            <asp:HyperLink ID="hlVazgec" runat="server" Text="Vazgeç" CssClass="btn btn-warning btn-m" NavigateUrl="../admin/Gelinlikler.aspx" />
                                            <asp:Button ID="btnKaydet" runat="server" Text="Kaydet" CssClass="btn btn-success btn-m" OnClick="btnKaydet_Click" ValidationGroup="makine" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


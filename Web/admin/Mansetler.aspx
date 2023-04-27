<%@ Page Title="" Language="C#" MasterPageFile="~/admin/MP_Admin.master" AutoEventWireup="true" CodeFile="Mansetler.aspx.cs" Inherits="admin_Slider" %>

<%@ Register Src="../usercontrols/UC_Sayfalama.ascx" TagName="UC_Sayfalama" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Sag" runat="Server">
    <asp:UpdatePanel ID="up" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnKaydet" EventName="" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel runat="server" CssClass="modal fade show" ID="pnlKayit" role="dialog" aria-labelledby="largeModalLabel" Style="overflow: scroll;" DefaultButton="btnKaydet">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Literal ID="lblKayitBaslik" runat="server" /></h4>
                            <asp:LinkButton ID="btnKapatUst" runat="server" Text="x" CssClass="close" OnClick="btnKapatUst_Click" />
                        </div>
                        <div class="modal-body">
                            <asp:Panel ID="fotouploading" runat="server" Style="display: none;">
                                <div class="popupbox" style="padding: 10px; height: 550px; width: 550px; overflow: auto;">
                                    <div style="padding-top: 0px; text-align: center;">
                                        <img src="/admin/img/bekle.gif" alt="" />
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:UpdatePanel ID="up2" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlFotografEkleme" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="col-md-2">
                                                <blockquote class="blockquote-primary">
                                                    <p>Başlık:</p>
                                                </blockquote>
                                            </div>
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtMansetBaslik" runat="server" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtMansetBaslik" Display="Dynamic" SetFocusOnError="true" ValidationGroup="slider">
                                    <span class="alert-danger"><i class="fa fa-exclamation"></i> Boş geçemezsiniz</span>    
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <blockquote class="blockquote-primary">
                                                    <p>Url:</p>
                                                </blockquote>

                                            </div>
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtMansetUrl" runat="server" CssClass="form-control" />
                                                <span>Yönlendirme istemiyorsanız boş geçebilirsiniz!
                                                </span>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <blockquote class="blockquote-primary">
                                                    <p>Öncelik:</p>
                                                </blockquote>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtMansetOncelik" runat="server" Text="1000" CssClass="form-control" />
                                            </div>
                                            <div class="col-md-2">
                                                <blockquote class="blockquote-primary">
                                                    Göster:
                                                </blockquote>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:CheckBox ID="cbSliderGoster" runat="server" CssClass="form-check mt-2" Checked="true" />
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <blockquote class="blockquote-primary">
                                                    Yeni:
                                                </blockquote>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:CheckBox ID="cbSliderYeni" runat="server" CssClass="form-check mt-2" />
                                                <span>Yeni sekmede açılmasını istiyorsanız işaretleyiniz!</span>
                                            </div>

                                            <div class="col-md-2">
                                                <blockquote class="blockquote-primary">
                                                    <p>Fotoğraf:</p>
                                                </blockquote>
                                            </div>
                                            <div class="col-md-4" id="divFotoUpload" runat="server">
                                                <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Style="visibility: hidden; display: none;" Text="xxx" />
                                                <ajaxToolkit:AsyncFileUpload ID="fu" runat="server" OnClientUploadComplete="btnClick" ThrobberID="fotouploading" OnUploadedComplete="fu_UploadedComplete" CssClass="background-color-white" />
                                            </div>
                                            <div class="col-lg-4" id="divFotografGoster" runat="server" visible="false">
                                                <span class="img-thumbnail d-block">
                                                    <ny:MyImage ID="imgSlider" runat="server" CssClass="img-fluid" MaxHeight="200" Thumbnail="false" Quality="60" Scaled="false" ImageAlign="Middle" />
                                                </span>
                                                <asp:ImageButton ID="btnSil" runat="server" ToolTip="Sil" CssClass="text-center center content-align-center" ImageUrl="/admin/img/Sil.gif" OnClick="btnSil_Click" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-10">
                                            </div>
                                            <div class="col-md-1">
                                                <asp:Button ID="btnKaydet" runat="server" Text="Kaydet" ValidationGroup="slider" CssClass="btn btn-m btn-primary" OnClick="btnKaydet_Click" />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <div class="row">
                <div class="col-md-1 mr-2">
                    <asp:LinkButton ID="btnYeni" runat="server" CssClass="btn btn-success mb-2" OnClick="btnYeni_Click"><i class="fas fa-plus-square"></i> Yeni</asp:LinkButton>
                </div>
                <div class="col-md-5">
                    <div class="alert alert-info alert-sm">
                        <strong>Dikkat!</strong> 1920x650 fotoğraf yüklemeniz tavsiye edilir!
                    </div>
                </div>
            </div>
            <uc1:UC_Sayfalama ID="UC_Sayfalama1" runat="server" ToplamGoster="true" ToplamMetin="{0} kayıt" />
            <div class="table-responsive">
                <asp:GridView ID="gvKayitlar" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered" GridLines="None" OnRowDataBound="gvKayitlar_RowDataBound" OnRowCommand="gvKayitlar_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="İşlemler" ItemStyle-Width="40" HeaderStyle-CssClass="center" ItemStyle-CssClass="center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbSil" runat="server" Text="Sil" CommandName="Sil" CommandArgument='<%#Eval("Id") %>' CssClass="btn btn-warning btn-xs pull-left" OnClientClick="return confirm('Silmek istediğinize emin misiniz?')" />
                                <asp:HyperLink ID="lbFotograf" runat="server" NavigateUrl='<%#Eval("Fotograf") %>' CssClass="d-block lightbox pull-left ml-2" data-plugin-options="{'type':'image'}" ToolTip='<%#Eval("Fotograf") %>'>
                                 <span class="zoom">
                                    <i class="fas fa-search"></i>
                                </span>
                                </asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bilgi" HeaderStyle-CssClass="center" ItemStyle-CssClass="center">
                            <ItemTemplate>
                                <asp:Literal ID="ltlGoster" runat="server" />
                                <asp:Literal ID="ltlDil" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Başlık" DataField="Baslik" ItemStyle-CssClass="center" HeaderStyle-CssClass="center" />
                        <asp:BoundField HeaderText="URL" DataField="Url" ItemStyle-CssClass="center" HeaderStyle-CssClass="center" />
                        <asp:BoundField HeaderText="Öncelik" DataField="Oncelik" ItemStyle-CssClass="center" HeaderStyle-CssClass="center" />
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


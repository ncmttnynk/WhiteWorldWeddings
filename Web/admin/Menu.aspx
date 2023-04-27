<%@ Page Title="" Language="C#" MasterPageFile="~/admin/MP_Admin.master" AutoEventWireup="true" CodeFile="Menu.aspx.cs" Inherits="admin_Menu" %>

<%@ Register Src="../usercontrols/UC_Sayfalama.ascx" TagName="UC_Sayfalama" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Sag" runat="Server">
    <asp:UpdatePanel ID="up" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlLinkTurleri" EventName="" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel runat="server" CssClass="modal fade show" ID="pnlKayit" role="dialog" aria-labelledby="largeModalLabel" Style="display: none; padding-right: 17px;" DefaultButton="btnKayitKaydet">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Literal ID="lblKayitBaslik" runat="server" /></h4>
                            <asp:LinkButton ID="btnKapatUst" runat="server" Text="x" CssClass="close" OnClick="btnKapatUst_Click" />
                        </div>
                        <div class="modal-body">
                            <div class="alert alert-danger" id="divHata" runat="server" visible="false">
                                <asp:Label ID="lblHata" runat="server" />
                                <asp:LinkButton ID="btnHataKapat" runat="server" CssClass="btn btn-danger btn-xs pull-right" OnClick="btnHataKapat_Click">Kapat</asp:LinkButton>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <blockquote class="blockquote-primary">
                                        <p>Hazır:</p>
                                    </blockquote>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList ID="ddlLinkTurleri" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlLinkTurleri_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="(Seçiniz)" />
                                        <asp:ListItem Value="1" Text="Sabit Sayfalar" />
                                        <asp:ListItem Value="2" Text="CMS" />
                                        <asp:ListItem Value="3" Text="Tek Sayfalar" />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList ID="ddlLinkler" runat="server" AutoPostBack="True" Enabled="False"
                                        CssClass="form-control" Visible="false" OnSelectedIndexChanged="ddlLinkler_SelectedIndexChanged" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <blockquote class="blockquote-primary">
                                        <p>Başlık:</p>
                                    </blockquote>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtKayitBaslik" runat="server" CssClass="form-control" ValidationGroup="proje" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <blockquote class="blockquote-primary">
                                        <p>Url:</p>
                                    </blockquote>
                                </div>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtKayitKod" runat="server" CssClass="form-control form-control-inline input-lg" />
                                </div>
                                <div class="col-md-5">
                                    <asp:Button ID="btnKodOlustur" runat="server" Text="Oluştur" CssClass="btn btn-warning btn-m" OnClick="btnKodOlustur_Click" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-2">
                                    <blockquote class="blockquote-primary">
                                        <p>Öncelik:</p>
                                    </blockquote>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtKayitOncelik" runat="server" Text="1000" CssClass="form-control input-lg" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <blockquote class="blockquote-primary">
                                        <p>Göster:</p>
                                    </blockquote>
                                </div>
                                <div class="col-md-10 mt-3">
                                    <asp:CheckBox ID="cbKayitGoster" runat="server" Checked="true" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnKayitKaydet" CssClass="btn btn-success" Text="Kaydet" runat="server" OnClick="btnKayitKaydet_Click" />
                            <asp:Button ID="btnKayitKapat" CssClass="btn btn-danger" Text="Vazgeç" runat="server" OnClick="btnKayitKapat_Click" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:LinkButton ID="btnYeni" runat="server" CssClass="btn btn-success" OnClick="btnYeni_Click"><i class="fas fa-plus-square"></i> Yeni</asp:LinkButton>
            <asp:HyperLink ID="hlUst" runat="server" CssClass="btn btn-info" Visible="false"><i class="fas fa-arrow-up"></i> Üste Git
            </asp:HyperLink>
            <uc1:UC_Sayfalama ID="UC_Sayfalama1" runat="server" ToplamGoster="true" ToplamMetin="{0} kayıt" />
            <div class="table-responsive">
                <asp:GridView ID="gvKayitlar" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered" GridLines="None" OnRowCommand="gvKayitlar_RowCommand" OnRowDataBound="gvKayitlar_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="İşlemler" HeaderStyle-Width="220" ItemStyle-Width="220" ItemStyle-CssClass="center" HeaderStyle-CssClass="center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbSil" runat="server" Text="Sil" CssClass="btn btn-warning btn-xs" OnClientClick="return confirm('Emin misiniz?')" CommandName="Sil" CommandArgument='<%#Eval("Id") %>' />
                                <asp:LinkButton ID="lbGuncelle" runat="server" Text="Düzenle" CssClass="btn btn-danger btn-xs" CommandName="Guncelle" CommandArgument='<%#Eval("Id") %>' />
                                <asp:HyperLink ID="hlDetay" runat="server" CssClass="btn btn-primary btn-xs" NavigateUrl='<%#string.Format("Menu.aspx?Id={0}",Eval("Id")) %>'>
                        Alt Menüler
                                </asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Başlık" DataField="Baslik" HeaderStyle-CssClass="center" ItemStyle-CssClass="center" HeaderStyle-Width="200" />
                        <asp:BoundField HeaderText="Öncelik" DataField="Oncelik" HeaderStyle-CssClass="center" ItemStyle-CssClass="center" HeaderStyle-Width="200" />
                        <asp:TemplateField HeaderText="Dil" HeaderStyle-Width="80" ItemStyle-Width="80" ItemStyle-CssClass="center" HeaderStyle-CssClass="center">
                            <ItemTemplate>
                                <asp:Literal ID="ltlDil" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Göster" HeaderStyle-Width="80" ItemStyle-Width="80" ItemStyle-CssClass="center" HeaderStyle-CssClass="center">
                            <ItemTemplate>
                                <asp:Literal ID="ltlGoster" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <uc1:UC_Sayfalama ID="UC_Sayfalama2" runat="server" ToplamGoster="true" ToplamMetin="{0} kayıt" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


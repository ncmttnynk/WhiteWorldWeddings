<%@ Page Title="" Language="C#" MasterPageFile="~/admin/MP_Admin.master" AutoEventWireup="true" CodeFile="Adminler.aspx.cs" Inherits="admin_Adminler" %>

<%@ Register Src="../usercontrols/UC_Sayfalama.ascx" TagName="UC_Sayfalama" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Sag" runat="Server">
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" CssClass="modal fade show" ID="pnlKayit" role="dialog" aria-labelledby="largeModalLabel" Style="overflow: scroll;" DefaultButton="btnKayitKaydet">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Literal ID="lblKayitBaslik" runat="server" /></h4>
                            <asp:LinkButton ID="btnKapatUst" runat="server" Text="x" CssClass="close" OnClick="btnKapatUst_Click" />
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <blockquote class="blockquote-primary">
                                        <p>Ad Soyad:</p>
                                    </blockquote>
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtKayitAdSoyad" runat="server" CssClass="form-control input-lg" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <blockquote class="blockquote-primary">
                                        <p>Kod:</p>
                                    </blockquote>
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtKayitKod" runat="server" CssClass="form-control" ValidationGroup="proje" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <blockquote class="blockquote-primary">
                                        <p>Şifre:</p>
                                    </blockquote>
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtKayitSifre" runat="server" CssClass="form-control input-lg" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <blockquote class="blockquote-primary">
                                        <p>Hiper:</p>
                                    </blockquote>
                                </div>
                                <div class="col-md-9 mt-3">
                                    <asp:CheckBox ID="cbKayitHiper" runat="server" Checked="true" />
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
            <uc1:UC_Sayfalama ID="UC_Sayfalama1" runat="server" ToplamGoster="true" ToplamMetin="{0} kayıt" />
            <div class="table-responsive">
                <asp:GridView ID="gvKayitlar" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered" GridLines="None" OnRowCommand="gvKayitlar_RowCommand" OnRowDataBound="gvKayitlar_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="İşlemler" HeaderStyle-Width="220" ItemStyle-Width="220" ItemStyle-CssClass="center" HeaderStyle-CssClass="center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbSil" runat="server" Text="Sil" CssClass="btn btn-warning btn-xs" OnClientClick="return confirm('Emin misiniz?')" CommandName="Sil" CommandArgument='<%#Eval("Id") %>' />
                                <asp:LinkButton ID="lbGuncelle" runat="server" Text="Düzenle" CssClass="btn btn-primary btn-xs" CommandName="Guncelle" CommandArgument='<%#Eval("Id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Ad Soyad" DataField="AdSoyad" HeaderStyle-CssClass="center" ItemStyle-CssClass="center" HeaderStyle-Width="200" />
                        <asp:BoundField HeaderText="Kod" DataField="Kod" HeaderStyle-CssClass="center" ItemStyle-CssClass="center" HeaderStyle-Width="200" />
                        <asp:BoundField HeaderText="Son Giriş" DataField="SonGiris" HeaderStyle-CssClass="center" ItemStyle-CssClass="center" HeaderStyle-Width="200" />
                        <asp:TemplateField HeaderText="Hiper" HeaderStyle-Width="80" ItemStyle-Width="80" ItemStyle-CssClass="center" HeaderStyle-CssClass="center">
                            <ItemTemplate>
                                <asp:Literal ID="ltlHiper" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <uc1:UC_Sayfalama ID="UC_Sayfalama2" runat="server" ToplamGoster="true" ToplamMetin="{0} kayıt" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


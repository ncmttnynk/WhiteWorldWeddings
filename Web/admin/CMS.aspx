﻿<%@ Page Title="" Language="C#" MasterPageFile="~/admin/MP_Admin.master" AutoEventWireup="true" CodeFile="CMS.aspx.cs" Inherits="admin_CMS" %>

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
                            <div class="alert alert-info" id="divHata" runat="server" visible="false">
                                <asp:Label ID="lblHata" runat="server" />
                                <asp:LinkButton ID="btnHataKapat" runat="server" CssClass="btn btn-warning btn-xs pull-right" OnClick="btnHataKapat_Click">Kapat</asp:LinkButton>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <blockquote class="blockquote-primary">
                                        <p>Başlık:</p>
                                    </blockquote>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtKayitBaslik" runat="server" CssClass="form-control input-lg" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <blockquote class="blockquote-primary">
                                        <p>Kod:</p>
                                    </blockquote>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtKayitKod" runat="server" CssClass="form-control form-control-inline input-lg" />
                                    <asp:Button ID="btnKodOlustur" runat="server" Text="Oluştur" CssClass="btn btn-warning btn-xs mt-1" Width="25%" OnClick="btnKodOlustur_Click" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <blockquote class="blockquote-primary">
                                        <p>Öncelik:</p>
                                    </blockquote>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtOncelik" runat="server" CssClass="form-control form-control-inline input-lg" Text="1000" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <blockquote class="blockquote-primary">
                                        <p>Göster:</p>
                                    </blockquote>
                                </div>
                                <div class="col-md-10 mt-3">
                                    <asp:CheckBox ID="cbGoster" runat="server" Checked="true" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <blockquote class="blockquote-primary">
                                        <p>Ayrıntı:</p>
                                    </blockquote>
                                </div>
                                <div class="col-md-10 mt-3">
                                    <CKEditor:CKEditorControl ID="ckKayitAyrinti" runat="server" CssClass="form-control input-lg" />
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
            <asp:LinkButton ID="btnYeni" runat="server" Text="Yeni" CssClass="btn btn-success" OnClick="btnYeni_Click"> <i class="fas fa-plus-square"></i> Yeni</asp:LinkButton>
            <asp:HyperLink ID="hlUst" runat="server" CssClass="btn btn-info" Visible="false"><i class="fas fa-arrow-up"></i> Üste Git
            </asp:HyperLink>
            <uc1:UC_Sayfalama ID="UC_Sayfalama1" runat="server" ToplamGoster="true" ToplamMetin="{0} kayıt" />
            <div class="table-responsive">
                <asp:GridView ID="gvKayitlar" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered" GridLines="None" OnRowCommand="gvKayitlar_RowCommand" OnRowDataBound="gvKayitlar_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="İşlemler" HeaderStyle-Width="220" ItemStyle-Width="220" ItemStyle-CssClass="center" HeaderStyle-CssClass="center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbSil" runat="server" Text="Sil" CssClass="btn btn-warning btn-xs" OnClientClick="return confirm('Emin misiniz?')" CommandName="Sil" CommandArgument='<%#Eval("Id") %>' />
                                <asp:LinkButton ID="lbGuncelle" runat="server" Text="Güncelle" CssClass="btn btn-danger btn-xs" CommandName="Guncelle" CommandArgument='<%#Eval("Id") %>' />
                                <asp:HyperLink ID="hlDetay" runat="server" NavigateUrl='<%#string.Format("CMS.aspx?Id={0}",Eval("Id")) %>' CssClass="btn btn-xs btn-primary">
                                 Alt Başlıklar
                                </asp:HyperLink>
                                <asp:HyperLink ID="hlGit" runat="server" NavigateUrl='<%#string.Format("/"+Dil+"/{0}",Eval("Kod"))%>' ToolTip="Göster" Target="_blank"><i class="fa fa-link" aria-hidden="true"></i></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Başlık" DataField="Baslik" HeaderStyle-CssClass="center" ItemStyle-CssClass="center" HeaderStyle-Width="200" />
                        <asp:BoundField HeaderText="Kod" DataField="Kod" HeaderStyle-CssClass="center" ItemStyle-CssClass="center" HeaderStyle-Width="200" />
                        <asp:BoundField HeaderText="Öncelik" DataField="Oncelik" HeaderStyle-CssClass="center" ItemStyle-CssClass="center" HeaderStyle-Width="80" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


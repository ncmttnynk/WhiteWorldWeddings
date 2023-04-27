<%@ Page Title="" Language="C#" MasterPageFile="~/admin/MP_Admin.master" AutoEventWireup="true" CodeFile="Gelinlikler.aspx.cs" Inherits="admin_Gelinlikler" %>

<%@ Register Src="../usercontrols/UC_Sayfalama.ascx" TagName="UC_Sayfalama" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Sag" runat="Server">
    <asp:LinkButton ID="btnYeni" runat="server" CssClass="btn btn-success" OnClick="btnYeni_Click"><i class="fas fa-plus-square"></i> Yeni</asp:LinkButton>
    <uc1:UC_Sayfalama ID="UC_Sayfalama1" runat="server" ToplamGoster="true" ToplamMetin="{0} kayıt" />
    <div class="table-responsive">
        <asp:GridView ID="gvKayitlar" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered" HeaderStyle-CssClass="center" GridLines="None" OnRowCommand="gvKayitlar_RowCommand" OnRowDataBound="gvKayitlar_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="İşlemler" HeaderStyle-Width="220" ItemStyle-Width="220" ItemStyle-CssClass="center" HeaderStyle-CssClass="center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbSil" runat="server" Text="Sil" CssClass="btn btn-warning btn-xs" OnClientClick="return confirm('Emin misiniz?')" CommandName="Sil" CommandArgument='<%#Eval("Id") %>' />
                        <asp:HyperLink ID="lbGuncelle" runat="server" Text="Düzenle" CssClass="btn btn-primary btn-xs" NavigateUrl='<%#string.Format("/admin/GelinlikDetay.aspx?Id={0}",Eval("Id")) %>' />
                        <asp:HyperLink ID="hlGit" runat="server" NavigateUrl='<%#string.Format("/{0}/GelinlikDetay/{1}/{2},product",Dil,Eval("Id"),Eval("Baslik").ToString().ToURL()) %>' ToolTip="Göster" Target="_blank"><i class="fa fa-link" aria-hidden="true"></i></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField HeaderText="Kategori" DataField="Kategori" HeaderStyle-CssClass="center" ItemStyle-CssClass="center" HeaderStyle-Width="200" />--%>
                <asp:BoundField HeaderText="Başlık" DataField="Baslik" HeaderStyle-CssClass="center" ItemStyle-CssClass="center" HeaderStyle-Width="200" />
                <asp:BoundField HeaderText="Öncelik" DataField="Oncelik" HeaderStyle-CssClass="center" ItemStyle-CssClass="center" HeaderStyle-Width="200" />
                <asp:TemplateField HeaderText="Yeni Sezon" HeaderStyle-Width="80" ItemStyle-Width="80" ItemStyle-CssClass="center" HeaderStyle-CssClass="center">
                    <ItemTemplate>
                        <asp:Literal ID="ltlYeniSezon" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Özel Ürün" HeaderStyle-Width="80" ItemStyle-Width="80" ItemStyle-CssClass="center" HeaderStyle-CssClass="center">
                    <ItemTemplate>
                        <asp:Literal ID="ltlOzelUrun" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="En Çok Satan" HeaderStyle-Width="80" ItemStyle-Width="80" ItemStyle-CssClass="center" HeaderStyle-CssClass="center">
                    <ItemTemplate>
                        <asp:Literal ID="ltlEnCokSatan" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Yeni" HeaderStyle-Width="80" ItemStyle-Width="80" ItemStyle-CssClass="center" HeaderStyle-CssClass="center">
                    <ItemTemplate>
                        <asp:Literal ID="ltlYeni" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
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
</asp:Content>


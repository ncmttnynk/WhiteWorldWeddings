<%@ Page Title="" Language="C#" MasterPageFile="~/admin/MP_Admin.master" AutoEventWireup="true" CodeFile="Sabitler.aspx.cs" Inherits="admin_Sabitler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Sag" runat="Server">
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="row">
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlSabitler" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSabitler_SelectedIndexChanged" AutoPostBack="True" />
                        <hr />
                        <div class="alert alert-info alert-sm">
                            <strong>Dikkat!</strong> Kaynak görünümünü kullanınız.
                        </div>
                    </div>
                    <asp:Panel ID="pnlEditor" runat="server" Visible="false" CssClass="col-md-8">
                        <CKEditor:CKEditorControl ID="ckIcerik" runat="server" />
                        <br />
                        <asp:Button ID="btnKaydet" runat="server" Text="Güncelle" CssClass="btn btn-primary btn-xl pull-right" OnClick="btnKaydet_Click" />
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_Buradasiniz.ascx.cs" Inherits="usercontrols_UC_Buradasiniz" %>

<div class="row">
    <div class="col-md-12">
        <asp:Repeater ID="rptUl" runat="server" OnItemDataBound="rptUl_ItemDataBound">
            <HeaderTemplate>
                <ul class="breadcrumb">
            </HeaderTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
            <ItemTemplate>
                <li>
                    <asp:Literal ID="lbl" runat="server" Visible="false" />
                    <asp:HyperLink ID="hl" runat="server" Visible="false"/>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
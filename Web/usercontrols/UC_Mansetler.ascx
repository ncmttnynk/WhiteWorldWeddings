<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_Mansetler.ascx.cs" Inherits="usercontrols_UC_Mansetler" %>

<!-- Home slide -->
<div class="container">
    <asp:Repeater ID="rptMansetler" runat="server">
        <HeaderTemplate>
            <div class="home-slide1 owl-carousel owl-theme owl-loaded nav-style4" data-animeteout="fadeOut" data-animatein="fadeIn" data-items="1" data-nav="true" data-dots="false" data-loop="true" data-autoplay="true" data-margin="0">
        </HeaderTemplate>
        <ItemTemplate>
            <a href='<%#Eval("Url") %>' target='<%# Convert.ToBoolean(Eval("Yeni")) ? "_blank" : "" %>'>
                <ny:MyImage ID="nyImg" runat="server" ImageUrl='<%#Eval("Fotograf") %>' Scaled="false" Thumbnail="false" Quality="100" AlternateText='<%#Eval("Url") %>' MaxHeight="650" ToolTip='<%#Eval("Url") %>' />
            </a>
        </ItemTemplate>
        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>
</div>
<!-- ./Home slide -->

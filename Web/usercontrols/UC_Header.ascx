<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_Header.ascx.cs" Inherits="usercontrols_UC_Header" %>

<%@ Register Src="UC_AnaMenu.ascx" TagName="UC_AnaMenu" TagPrefix="uc1" %>

<div id="box-mobile-menu" class="box-mobile-menu full-height full-width">
    <div class="box-inner">
        <span class="close-menu"><span class="icon pe-7s-close"></span></span>
    </div>
</div>
<div id="header-ontop" class="is-sticky"></div>
<header id="header" class="header style3">
    <div class="container">
        <div class="main-menu-wapper">
            <div class="row">
                <div class="col-sm-12 col-md-3 logo-wapper">
                    <div class="logo">
                        <a href="/">
                            <img src="/images/logo.png" alt="White World Weddings"></a>
                    </div>
                </div>
                <div class="col-sm-12 col-md-9 menu-wapper">
                    <div class="top-header">
                        <span class="mobile-navigation"><i class="fa fa-bars"></i></span>
                        <div class="slogan">
                            <asp:Literal ID="ltlSlogan" runat="server" />
                        </div>
                        <div class="box-control">
                        <%--    <asp:Panel ID="pnlAra" runat="server" DefaultButton="btnAra">
                                <div class="box-search">
                                    <div class="inner">
                                        <asp:TextBox ID="txtAra" runat="server" CssClass="search" placeholder="..." />
                                        <asp:LinkButton ID="btnAra" runat="server" CssClass="button-search tdn" OnClick="btnAra_Click">
                                        <span class="pe-7s-search"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </asp:Panel>--%>
                            <div class="box-settings">
                                <ul class="clearfix" style="list-style-type: none; text-transform: none !important;">
                                    <asp:Literal ID="ltlDil" runat="server" />
                                </ul>
                            </div>
                        </div>
                    </div>
                    <uc1:UC_AnaMenu ID="UC_AnaMenu1" runat="server" />
                </div>
            </div>
        </div>
    </div>
</header>
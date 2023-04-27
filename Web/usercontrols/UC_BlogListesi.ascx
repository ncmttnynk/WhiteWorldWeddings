<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_BlogListesi.ascx.cs" Inherits="usercontrols_UC_BlogListesi" %>

<div class="container">
        <div class="margin-top-30 section-lasttest-blog">
            <div class="section-title text-center">
                <h3>BLOG</h3>
            </div>
            <div class="lastest-blog owl-carousel nav-center-center nav-style7" data-nav="true" data-dots="false" data-loop="true" data-margin="30" data-responsive='{"0":{"items":1},"600":{"items":1},"1000":{"items":2}}'>
                <asp:Repeater ID="rptBlog" runat="server">
                    <ItemTemplate>
                        <div class="item-blog">
                            <div class="left">
                                <div class="blog-date">
                                    <span class="day"><%#Eval("Gun") %></span>
                                    <span class="month"><%#DataBinder.Eval(Container, "DataItem.Ay", "{0:MMM}")%> </span>
                                    <br>
                                    <span class="year"><%#Eval("Yil") %></span>
                                </div>
                                <h3 class="blog-title"><a href='<%#string.Format("/{0}/{1},onepage", Dil, Eval("Baslik").ToString().ToURL()) %>'><%#Eval("Baslik") %></a></h3>
                                <div class="meta">
                                    <span class="author"><%#Eval("Admin") %></span>
                                    <%--<span class="comment"><i class="fa fa-comment"></i>36 comments</span>--%>
                                </div>
                            </div>
                            <div class="right">
                                <a class="banner-border" href='<%#string.Format("/{0}/{1},onepage", Dil, Eval("Baslik").ToString().ToURL()) %>'>
                                    <img src='<%#Eval("Fotograf") %>' alt=""></a>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
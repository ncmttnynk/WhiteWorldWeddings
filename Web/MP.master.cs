using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhiteWorld.Info;
using DA.MetaTagGenerator;

public partial class MP : IMP_Main
{
    public override List<BuradasinizInfo> Buradasiniz { set { } }
    public override string H1 { set { } }
    public string MyTitle
    {
        get
        {
            if (ViewState["TITLE"] == null) return "";
            return ViewState["TITLE"].ToString();
        }
        set { ViewState["TITLE"] = value; }
    }
    public override string Title
    {
        set
        {
            MyTitle = value;
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!MyTitle.IsNullOrEmpty())
        {
            Page.Title = MyTitle + " | " + Config.Title;
        }
        else
            Page.Title = Config.Title;

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        form1.Action = Request.RawUrl;

        if (!IsPostBack)
        {
            MetaTagGenerator gen = MetaTagGenerator.CreateInstance(this.Page);
            gen.MetaTags = new Tags.MetaTagsInfo
            {
                Abstract = "Gelinlik alanında özel tasarım gelinlik satışı yapan White World şirketimizin web sitesidir.",
                Author = "Necmettin Yanık, necmettinyanik@outlook.com",
                Category = "Necmettin Yanık",
                Charset = Constants.UTF8,
                Classification = "Moda",
                ContentType = Constants.UTF8_httpeqiv,
                Copyright = "(c) 2018 whiteworldweddings.com Tüm Hakları Saklıdır!",
                Designer = "Necmettin Yanık",
                Distribution = "global",
                HandheldFriendly = HandheldFriendlyTypes.True,
                IdentifierUrl = "whiteworldweddings.com",
                MobileOptimized = "320",
                Owner = "White World",
                Rating = "General",
                ReplyTo = "info@whiteworldweddings.com",
                RevisitAfter = "7 days",
                Robots = RobotsTypes.ALL,
                Subject = "Özel tasarım gelinlikler",
            };
            gen.GenerateTags();
        }
    }
}

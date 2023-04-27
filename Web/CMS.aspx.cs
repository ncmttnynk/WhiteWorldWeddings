using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhiteWorld.DAL;
using WhiteWorld.Info;
using DA.MetaTagGenerator;

public partial class CMS : IPage
{
    string menuStr = "";
    string kod = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        kod = Request.QueryString["Kod"];
        if (!IsPostBack)
        {
            using (var db = new WhiteWorldEntities())
            {
                var sayfa = db.cms.FirstOrDefault(x => x.Kod == kod && x.DilKod == DilKod && x.Goster);
                if (sayfa == null)
                    Response.Redirect(string.Format("/{0}/Default.aspx", Dil), true);
                base.MP_Main.Title = sayfa.Baslik;
                MetaTagGenerator gen = MetaTagGenerator.CreateInstance(this);
                gen.MetaTags = new Tags.MetaTagsInfo
                {
                    Description = sayfa.Ayrinti.Substring(0,250),
                    Language = Dil,
                    Title = sayfa.Baslik,
                    Topic = sayfa.Baslik,
                };
                gen.GenerateTags();
                ltlIcerik.Text = sayfa.Ayrinti;
                ltlBaslik.Text = sayfa.Baslik;
                ltlCmsMenu.Text = MenuGetir(sayfa.AnaBaslikId);
            }
        }
    }
    int derinlik = 0;
    List<cms> menuler = null;

    public List<cms> AnaMenuGetir()
    {
        using (var db = new WhiteWorldEntities())
        {
            var tumu = db.cms.Where(x => x.Goster).ToList();
            return tumu;
        }
    }

    public string MenuGetir(int SayfaId)
    {
        using (var db = new WhiteWorldEntities())
        {
            menuler = db.cms.ToList();
        }
        var ana = menuler.Where(x => x.BaslikId == 0 && x.Id == SayfaId && x.DilKod == DilKod && x.Goster).OrderBy(x => x.Oncelik).ToList();
        AgacOlustur(ana);
        return menuStr;
    }

    private void AgacOlustur(List<cms> ana)
    {
        derinlik++;
        if (derinlik == 1)
            menuStr += @"<h2 class=""widget-title"">";
        else
            menuStr += @"<ul class=""product-categories"">";

        foreach (var a in ana)
        {
            var alt = menuler.Where(x => x.BaslikId == a.Id).OrderBy(x => x.Oncelik).ToList();
            if (derinlik == 1)
                menuStr += string.Format(@"<li><a href=""{0},content"">{1}</a>", a.Kod, a.Baslik);
            else
                menuStr +=
                    string.Format(@"<li {2}><a href=""{0},content"">{1}</a>", a.Kod, a.Baslik, (string.Format(@"style=""margin-left:{0}px;""", derinlik * 20)));
            if (alt.Count > 0) AgacOlustur(alt);
            menuStr += @"</li>";
        }
        if (derinlik == 1)
            menuStr += @"</h2>";
        else
            menuStr += @"</ul>";
        derinlik--;
    }
}
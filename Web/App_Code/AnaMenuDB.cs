using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WhiteWorld.DAL;

public class AnaMenuDB
{
    string menuStr = "";
    int derinlik = 0;
    List<anamenu> menuler = null;

    public string MenuGetir(int dilKod)
    {
        if (HttpContext.Current.Cache["ANAMENU"] != null)
        {
            menuStr = (string)HttpContext.Current.Cache["ANAMENU"];
        }
        else
        {
            using (var db = new WhiteWorldEntities())
            {
                menuler = db.anamenu.ToList();
            }
            var ana = menuler.Where(x => x.UstMenuId == 0 && x.DilKod == dilKod).OrderBy(x => x.Oncelik).ToList();
            AgacOlustur(ana);
            HttpContext.Current.Cache["ANAMENU"] = menuStr;
        }
        return menuStr;
    }

    private void AgacOlustur(List<anamenu> ana)
    {
        derinlik++;
        if (derinlik == 1)
            menuStr += @"<ul class=""boutique-nav main-menu clone-main-menu"">";
        else
            menuStr += @"<ul class=""sub-menu"">";

        foreach (var a in ana)
        {
            var alt = menuler.Where(x => x.UstMenuId == a.Id).OrderBy(x => x.Oncelik).ToList();

            if (derinlik == 1)
            {
                menuStr += string.Format(@"<li {3}><a href=""{0}"">{1}</a>{2}", a.Url, a.Baslik, alt.Count > 0 ? @"<span class=""arow""></span>" : "", alt.Count > 0 ? @"class=""menu-item-has-children""" : "");
            }
            else
                menuStr +=
                    string.Format(@"<li {2}><a href=""{0}"">{1}</a><span class=""arow""></span>",
                        a.Url, a.Baslik, (alt.Count > 0 ? @"class=""menu-item-has-children""" : ""));

            if (alt.Count > 0) AgacOlustur(alt);

            menuStr += @"</li>";
        }
        if (derinlik == 1)
            menuStr += @"</ul>";
        else
            menuStr += @"</ul>";
        derinlik--;
    }
}
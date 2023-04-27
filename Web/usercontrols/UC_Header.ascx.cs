using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class usercontrols_UC_Header : System.Web.UI.UserControl
{
    int dilKod = 1;
    string dil = "tr";
    protected void Page_Load(object sender, EventArgs e)
    {
        dilKod = (this.Page as IPage).DilKod;
        dil = (this.Page as IPage).Dil;
        if (!IsPostBack)
        {
            SabitlerDB sDB = new SabitlerDB();
            // TODO : dil burada kapatılmıştır
            //ltlDil.Text = (dilKod == 1) ? @"<li><a href=""/Language.aspx?Dil=ar"" title=""Arabia""><img src=""/images/flag2.png"" alt=""Arabia""></a></li><li class=""active""><a href=""javascript:void(0)"" Title=""Türkçe""><img src=""/images/flag1.png"" alt=""Türkçe""></a></li>" :
            //    @"<li><a href=""/Language.aspx?Dil=tr"" title=""Türkçe""><img src=""/images/flag1.png"" alt=""Türkçe""></a></li><li class=""active""><a href=""javascript:void(0)"" title=""Arabia""><img src=""/images/flag2.png"" alt=""Arabia""></a></li>";
            ltlSlogan.Text = (dilKod == 1) ? sDB.TurkceIcerikGetir(Enums.Sabitler.Slogan) : sDB.IngilizceIcerikGetir(Enums.Sabitler.Slogan);
        }
    }

    //protected void btnAra_Click(object sender, EventArgs e)
    //{
    //    var ara = txtAra.Text.ToString().ToURL();
    //    if (!string.IsNullOrEmpty(ara))
    //    {
    //        string url = string.Format("/{0}/{1},etiket", dil, ara);
    //        Response.Redirect(url, true);
    //    }
    //}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhiteWorld.DAL;

public partial class usercontrols_UC_Mansetler : System.Web.UI.UserControl
{
    int dilKod = 1;
    string dil = "tr";
    protected void Page_PreRender(object sender, EventArgs e)
    {
        dilKod = (this.Page as IPage).DilKod;
        dil = (this.Page as IPage).Dil;
        if (!IsPostBack)
        {
            using (var db = new WhiteWorldEntities())
            {
                var kayitlar = from x in db.mansetler
                               where x.Goster && x.DilKod == dilKod
                               select new
                               {
                                   x.Url,
                                   x.Yeni,
                                   x.Fotograf,
                                   x.Oncelik
                               };
                if (kayitlar.Count() > 0)
                {
                    rptMansetler.DataSource = kayitlar.OrderBy(x => x.Oncelik).ToList();
                    rptMansetler.DataBind();
                }
                else
                {
                    this.Visible = false;
                }
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
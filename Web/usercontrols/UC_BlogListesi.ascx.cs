using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhiteWorld.DAL;

public partial class usercontrols_UC_BlogListesi : IUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            using (var db = new WhiteWorldEntities())
            {
                var tekSayfalar = (from x in db.teksayfalar
                                   join k in db.admin on x.AdminId equals k.Id
                                   where x.DilKod == DilKod && x.Goster
                                   orderby x.Oncelik
                                   select new
                                   {
                                       Admin = k.AdSoyad,
                                       Gun = x.KayitTarihi.Day,
                                       Ay = x.KayitTarihi,
                                       Yil = x.KayitTarihi.Year,
                                       Baslik = x.Baslik,
                                       x.Fotograf
                                   }).Take(4).ToList();
                if (tekSayfalar.Count > 0)
                {
                    rptBlog.DataSource = tekSayfalar;
                    rptBlog.DataBind();
                }
                else
                    this.Visible = false;
            }
        }
    }
}
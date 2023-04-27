using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhiteWorld.DAL;
using DA.MetaTagGenerator;

public partial class TekSayfa : IPage
{
    string kod = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            kod = Request.QueryString["Kod"].ToString();
            if (!string.IsNullOrEmpty(kod))
            {
                using (var db = new WhiteWorldEntities())
                {
                    var kayit = db.teksayfalar.FirstOrDefault(x => x.Kod == kod);
                    if (kayit != null)
                    {
                        ltlBaslik.Text = kayit.Baslik;
                        ltlIcerik.Text = kayit.Icerik;
                        imgKayit.ImageUrl = kayit.Fotograf;
                        ltlTarih.Text = kayit.KayitTarihi.ToLongDateString();

                        MetaTagGenerator gen = MetaTagGenerator.CreateInstance(this);
                        gen.MetaTags = new Tags.MetaTagsInfo
                        {
                            Description = kayit.Baslik,
                            Keywords = kayit.Baslik,
                            Language = Dil,
                            Title = kayit.Baslik,
                            Topic = "Özel Dikim Gelinlik",
                        };
                        gen.GenerateTags();

                    }
                    else
                        Response.Redirect("/", true);
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhiteWorld.DAL;
using WhiteWorld.Info;
using DA.MetaTagGenerator;

public partial class Etiket : IPage
{
    string etiket = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            etiket = Request.QueryString["Etiket"].ToString();
            using (var db = new WhiteWorldEntities())
            {
                var etik = db.etiketler.FirstOrDefault(x => x.Baslik == etiket);
                if (etik != null)
                {
                    var g = (from x in db.etiketler
                             where x.Baslik == etik.Baslik
                             join k in db.gelinlikler on x.GelinlikId equals k.Id
                             let onFoto = (from t in db.gelinlikfotograflari
                                           where t.GelinlikId == k.Id && t.OnFoto
                                           select t).FirstOrDefault()
                             let arkaFoto = (from b in db.gelinlikfotograflari
                                             where b.GelinlikId == k.Id && b.ArkaFoto
                                             select b).FirstOrDefault()
                             select new GelinlikInfo
                             {
                                 Id = k.Id,
                                 Baslik = k.Baslik,
                                 OnFotoEtiket = onFoto.Etiket,
                                 ArkaFotoEtiket = arkaFoto.Etiket,
                                 OnFoto = onFoto.FotoKucuk,
                                 ArkaFoto = arkaFoto.FotoKucuk,
                                 Aciklama = k.Aciklama,
                                 Goster = k.Goster
                             }).ToList();

                    base.MP_Main.Title = etik.Baslik;
                    MetaTagGenerator gen = MetaTagGenerator.CreateInstance(this);
                    gen.MetaTags = new Tags.MetaTagsInfo
                    {
                        Description = etik.Baslik,
                        Language = Dil,
                        Title = etik.Baslik,
                        Topic = etik.Baslik,
                    };
                    gen.GenerateTags();
                    rptGelinlikler.DataSource = g.ToList();
                    rptGelinlikler.DataBind();
                }
                else
                {
                    Response.Redirect("/", true);
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhiteWorld.DAL;
using WhiteWorld.Info;
using DA.MetaTagGenerator;

public partial class Gelinlik : IPage
{
    string ara = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            base.MP_Main.Title = (DilKod == 1) ? "Gelinlikler" : "Weddings";
            btnFiltrele.Text = Dil.Equals("tr") ? "Filtrele" : "Filter";
            hlTemizle.Text = Dil.Equals("tr") ? "Temizle" : "Clear";
            hlTemizle.NavigateUrl = "/" + Dil + "/Gelinlik";
            KategorileriGetir();
            AramaSonuclariniGetir();

            MetaTagGenerator gen = MetaTagGenerator.CreateInstance(this);
            gen.MetaTags = new Tags.MetaTagsInfo
            {
                Description = "Gelinliklerimiz",
                Keywords = "gelinlik, osmanbey, özel dikim, abiye, düğün, osmanbeyde gelinlik, işlemeli, dantelli",
                Language = Dil,
                Title = "2018'in Moda Gelinlikleri",
                Topic = "Gelinlikler",
            };
            gen.GenerateTags();
        }
    }

    private void AramaSonuclariniGetir()
    {
        using (var db = new WhiteWorldEntities())
        {
            var g = (from x in db.gelinlikler
                     let onFoto = (from t in db.gelinlikfotograflari
                                   where t.GelinlikId == x.Id && t.OnFoto && x.Goster && x.DilKod == DilKod
                                   select t).FirstOrDefault()
                     let arkaFoto = (from b in db.gelinlikfotograflari
                                     where b.GelinlikId == x.Id && b.ArkaFoto
                                     select b).FirstOrDefault()
                     where x.DilKod == DilKod
                     orderby x.Oncelik descending
                     select new GelinlikInfo
                     {
                         Id = x.Id,
                         Baslik = x.Baslik,
                         OnFotoEtiket = onFoto.Etiket,
                         ArkaFotoEtiket = arkaFoto.Etiket,
                         OnFoto = onFoto.FotoKucuk,
                         ArkaFoto = arkaFoto.FotoKucuk,
                         Renk = x.RenkId,
                         YakaTipi = x.YakaTipiId,
                         Siluet = x.SiluetId,
                         Kumas = x.KumasId,
                         Yeni = x.Yeni,
                         YeniSezon = x.YeniSezon,
                         EnCokSatan = x.EnCokSatan,
                         OzelUrun = x.OzelUrun,
                         Aciklama = x.Aciklama,
                         Kesim = x.KesimId,
                         Sezon = x.Sezon
                     });

            ara = "";
            if (Request.QueryString["Ara"] != null)
            {
                ara = Request.QueryString["Ara"].ToString();
                GelinlikAramaInfo info = new GelinlikAramaInfo();
                info.FromQueryString(ara);

                if (info.Kesim > 0)
                {
                    cbKesim.SelectedValue = info.Kesim.ToString();
                    g = g.Where(x => x.Kesim == info.Kesim);
                }
                //if (info.Renk > 0)
                //{
                //    cbRenkler.SelectedValue = info.Renk.ToString();
                //    g = g.Where(x => x.Renk == info.Renk);
                //}
                //if (info.YakaTipi > 0)
                //{
                //    cbYakaTipi.SelectedValue = info.YakaTipi.ToString();
                //    g = g.Where(x => x.YakaTipi == info.YakaTipi);
                //}
                //if (info.Siluet > 0)
                //{
                //    cbSiluet.SelectedValue = info.Siluet.ToString();
                //    g = g.Where(x => x.Siluet == info.Siluet);
                //}
                //if (info.Kumas > 0)
                //{
                //    cbKumas.SelectedValue = info.Kumas.ToString();
                //    g = g.Where(x => x.Kumas == info.Kumas);
                //}
                if (info.Tip > 0)
                {
                    switch (info.Tip)
                    {
                        case 1: // YENİ
                            g = g.Where(x => x.Yeni);
                            break;
                        case 2: // YENİ SEZON
                            g = g.Where(x => x.Yeni);
                            break;
                        case 3: // EN ÇOK SATAN
                            g = g.Where(x => x.EnCokSatan);
                            break;
                        case 4: // ÖZEL ÜRÜN
                            g = g.Where(x => x.OzelUrun);
                            break;
                        default:
                            break;
                    }
                    cbBilgi.SelectedValue = info.Tip.ToString();
                }

                if (info.Sezon > 0)
                {
                    cbSezon.SelectedValue = info.Sezon.ToString();
                    g = g.Where(x => x.Sezon == info.Sezon);
                }
            }
            rptGelinlikler.DataSource = g.ToList();
            rptGelinlikler.DataBind();
        }
    }
    private void KategorileriGetir()
    {
        KategoriDB kDB = new KategoriDB();
        //var renkler = kDB.RenkleriGetir(DilKod);
        //if (renkler.Count > 0)
        //{
        //    cbRenkler.DataSource = renkler.Where(x => x.Goster).OrderBy(x => x.Oncelik);
        //    cbRenkler.DataTextField = "Baslik";
        //    cbRenkler.DataValueField = "Id";
        //    cbRenkler.DataBind();
        //}

        //var yakaTipleri = kDB.YakaTipiGetir(DilKod);
        //if (yakaTipleri.Count > 0)
        //{
        //    cbYakaTipi.DataSource = yakaTipleri.Where(x => x.Goster).OrderBy(x => x.Oncelik);
        //    cbYakaTipi.DataTextField = "Baslik";
        //    cbYakaTipi.DataValueField = "Id";
        //    cbYakaTipi.DataBind();
        //}

        //var kumaslar = kDB.KumaslariGetir(DilKod);
        //if (kumaslar.Count > 0)
        //{
        //    cbKumas.DataSource = kumaslar.Where(x => x.Goster).OrderBy(x => x.Oncelik);
        //    cbKumas.DataTextField = "Baslik";
        //    cbKumas.DataValueField = "Id";
        //    cbKumas.DataBind();
        //}

        //var siluetler = kDB.SiluetleriGetir(DilKod);
        //if (siluetler.Count > 0)
        //{
        //    cbSiluet.DataSource = siluetler.Where(x => x.Goster).OrderBy(x => x.Oncelik);
        //    cbSiluet.DataTextField = "Baslik";
        //    cbSiluet.DataValueField = "Id";
        //    cbSiluet.DataBind();
        //}

        var kesimler = kDB.KesimleriGetir(DilKod);
        if (kesimler.Count > 0)
        {
            cbKesim.DataSource = kesimler.Where(x => x.Goster).OrderBy(x => x.Oncelik);
            cbKesim.DataTextField = "Baslik";
            cbKesim.DataValueField = "Id";
            cbKesim.DataBind();
        }

        cbSezon.Items.Add(new ListItem("2018", "2018"));
        cbSezon.Items.Add(new ListItem("2019", "2019"));
        cbSezon.Items.Add(new ListItem("2020", "2020"));
        cbSezon.DataBind();

        cbBilgi.Items.Add(new ListItem("Yeni", "1"));
        cbBilgi.Items.Add(new ListItem("Yeni Sezon", "2"));
        cbBilgi.Items.Add(new ListItem("En Çok Satan", "3"));
        cbBilgi.Items.Add(new ListItem("Özel Ürün", "4"));
        cbBilgi.DataBind();
    }

    protected void btnFiltrele_Click(object sender, EventArgs e)
    {
        GelinlikAramaInfo info = new GelinlikAramaInfo();
        //info.Renk = cbRenkler.SelectedValue.ToInt32();
        //info.Siluet = cbSiluet.SelectedValue.ToInt32();
        //info.Kumas = cbKumas.SelectedValue.ToInt32();
        //info.YakaTipi = cbYakaTipi.SelectedValue.ToInt32();
        info.Tip = cbBilgi.SelectedValue.ToInt32();
        info.Kesim = cbKesim.SelectedValue.ToInt32();
        info.Sezon = cbSezon.SelectedValue.ToInt32();
        var qs = info.ToQueryString();
        Response.Redirect("Gelinlik?Ara=" + qs);
    }
}

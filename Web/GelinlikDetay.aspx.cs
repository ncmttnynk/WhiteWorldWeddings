using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WhiteWorld.DAL;
using WhiteWorld.Info;
using DA.MetaTagGenerator;

public partial class GelinlikDetay : IPage
{
    int gelinlikId = 0;
    List<yorumlar> tum = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        gelinlikId = Request.QueryString["Id"].ToInt32();
        if (!IsPostBack)
        {
            if (gelinlikId > 0)
            {
                using (var db = new WhiteWorldEntities())
                {
                    var gelinlik = db.gelinlikler.FirstOrDefault(x => x.Id == gelinlikId && x.DilKod == DilKod);
                    if (gelinlik != null)
                    {
                        GelinlikDB gDB = new GelinlikDB();
                        hlGelinlikler.Text = (DilKod == 1) ? "Gelinlikler" : "Weddings";
                        hlGelinlikler.NavigateUrl = "/" + Dil + "/Gelinlik";
                        ltlDetayBaslik.Text = (Dil.Equals("tr") ? "DETAY" : "DESCRIPTION");
                        ltlAltUrunBaslik.Text = (Dil.Equals("tr") ? "Benzer Ürünler" : "Similiar Products");
                        btnYorumKaydet.Text = (Dil.Equals("tr") ? "Kaydet" : "Submit");
                        txtYorumAdSoyad.Attributes["placeholder"] = (Dil.Equals("tr") ? "Ad Soyad" : "Name Surname");
                        txtYorumEposta.Attributes["placeholder"] = (Dil.Equals("tr") ? "E-Posta" : "E-Mail");
                        txtYorumAciklama.Attributes["placeholder"] = (Dil.Equals("tr") ? "Yorum" : "Comment");
                        ltlGelinlikBaslik.Text = ltlGelinlikBas.Text = gelinlik.Baslik;
                        ltlGelinlikAciklama.Text = gelinlik.Aciklama;
                        ltlGelinlikDetay.Text = gelinlik.Detay;


                        //ltlRenk.Text = gDB.Renk(gelinlikId);
                        //ltlSiluet.Text = gDB.Siluet(gelinlikId);
                        //ltlYakaTipi.Text = gDB.YakaTipi(gelinlikId);
                        //ltlKumas.Text = gDB.Kumas(gelinlikId);
                        //ltlBeden.Text = gelinlik.Beden.ToString();

                        var gelinlikYorumlari = db.yorumlar.Where(x => x.GelinlikId == gelinlikId && x.Goster && x.UstYorumId == 0).ToList();
                        string yorumMesaji = (DilKod == 1) ? string.Format("( {0} <span>Yorum</span> )", gelinlikYorumlari.Count.ToString()) : string.Format("( {0} <span>Reviews</span> )", gelinlikYorumlari.Count.ToString());
                        ltlYorum.Text = (gelinlikYorumlari.Count > 0) ? yorumMesaji : ((Dil.Equals("tr") ? @"<a href=""#comments"">İlk yorumu sen bırak! </a>" : @"<a href=""#comments"">Leave first comment! </a>"));

                        imgOnFoto.ImageUrl = db.gelinlikfotograflari.FirstOrDefault(x => x.GelinlikId == gelinlikId && x.OnFoto).FotoBuyuk;

                        var tag = db.etiketler.Where(x => x.GelinlikId == gelinlikId).ToList();
                        string tags = "";
                        foreach (var t in tag)
                        {
                            tags += t.Baslik + ", ";
                        }

                        rptTag.DataSource = tag;
                        rptTag.DataBind();

                        MetaTagGenerator gen = MetaTagGenerator.CreateInstance(this);
                        gen.MetaTags = new Tags.MetaTagsInfo
                        {
                            Description = gelinlik.Aciklama,
                            Keywords = tags,
                            Language = Dil,
                            Title = gelinlik.Baslik,
                            Topic = "Özel Dikim Gelinlik",
                        };
                        gen.GenerateTags();

                        base.MP_Main.Title = gelinlik.Baslik;


                        var fotoKucuk = from x in db.gelinlikfotograflari
                                        where x.GelinlikId == gelinlikId
                                        orderby x.OnFoto descending
                                        select new FotografInfo
                                        {
                                            FotoBuyuk = x.FotoBuyuk,
                                            FotoKucuk = x.FotoKucuk,
                                            OnFoto = x.OnFoto,
                                            Etiket = x.Etiket
                                        };
                        rptFotoKucuk.DataSource = fotoKucuk.ToList();
                        rptFotoKucuk.DataBind();

                        var benzerGelinlikler = (from x in db.gelinlikler
                                                 let onFoto = (from t in db.gelinlikfotograflari
                                                               where t.GelinlikId == x.Id && t.OnFoto && x.Goster
                                                               select t).FirstOrDefault()
                                                 where x.DilKod == DilKod && x.Goster && x.Id != gelinlikId
                                                 orderby MyDbFunction.Rastgele()
                                                 select new
                                                 {
                                                     x.Id,
                                                     x.Baslik,
                                                     Fotograf = onFoto.FotoKucuk,
                                                     Etiket = onFoto.Etiket
                                                 }).Take(6).ToList();
                        if (benzerGelinlikler.Count > 0)
                        {
                            rptBenzerUrunler.DataSource = benzerGelinlikler.ToList();
                            rptBenzerUrunler.DataBind();
                            pnlBenzerUrunlar.Visible = true;
                        }

                        tum = db.yorumlar.ToList();
                        if (tum.Count > 0)
                        {
                            pnlYorumlar.Visible = true;
                            rptYorumlar.DataSource = tum.Where(x => x.UstYorumId == 0 && x.Goster && x.GelinlikId == gelinlikId).OrderByDescending(x => x.Id);
                            rptYorumlar.DataBind();
                        }
                    }
                    else
                    {
                        Response.Redirect(" / ", true);
                    }
                }
            }
        }
    }

    protected void rptFotoKucuk_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            var kayit = e.Item.DataItem as FotografInfo;
        }
    }

    protected void rptYorumlar_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            var kayit = e.Item.DataItem as yorumlar;
            var alt = tum.Where(x => x.UstYorumId == kayit.Id).OrderBy(x => x.Id).ToList();
            if (alt.Count > 0)
            {
                var rptAltYorumlar = e.Item.FindControl("rptAltYorumlar") as Repeater;
                rptAltYorumlar.Visible = true;
                rptAltYorumlar.DataSource = alt;
                rptAltYorumlar.DataBind();
            }

            //LinkButton lbYorumBirak = e.Item.FindControl("lbYorumBirak") as LinkButton;
            //lbYorumBirak.Text = (Dil.Equals("tr") ? @"Cevapla <i class=""fa fa-mail-reply""></i>" : @"Reply <i class=""fa fa-mail-reply""></i>");
        }
    }

    protected void btnYorumKaydet_Click(object sender, EventArgs e)
    {
        var adSoyad = txtYorumAdSoyad.Text.ToTemizMetin();
        var ePosta = txtYorumEposta.Text.ToTemizMetin();
        var aciklama = txtYorumAciklama.Text.ToTemizMetin();
        var ip = Config.Statistics.IP;

        try
        {
            using (var db = new WhiteWorldEntities())
            {
                var yeniYorum = new yorumlar
                {
                    Aciklama = aciklama,
                    AdSoyad = adSoyad,
                    Eposta = ePosta,
                    Goster = false,
                    Ip = ip,
                    Tarih = DateTime.Now,
                    UstYorumId = 0,
                    GelinlikId = gelinlikId
                };
                db.yorumlar.Add(yeniYorum);
                db.SaveChanges();
                pnlYorum.Enabled = false;
                MessageBox.Show((Dil.Equals("tr") ? @"Yorumunuz başarıyla kayıt edildi! </br> Yönetici onayından sonra gözükecektir." : @"Your review has been successfully saved! </br> It will appear after manager approval."), MessageBox.MesajTipleri.Success);
                pnlYorum.Focus();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, MessageBox.MesajTipleri.Warning);
        }
    }
}
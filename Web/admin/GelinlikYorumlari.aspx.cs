using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhiteWorld.DAL;
using WhiteWorld.Info;

public partial class admin_GelinlikYorumlari : IPage
{
    int GELINLIKKAYITID
    {
        get { return ViewState["GELINLIKKAYITID"].ToInt32(); }
        set { ViewState["GELINLIKKAYITID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        var mp = this.Master as IMP_Main;
        mp.H1 = "Gelinlik Yorumlar";
        mp.Title = "Gelinlik Yorumlar";
        mp.Buradasiniz = new List<BuradasinizInfo>
                {
                    new BuradasinizInfo {Title="Anasayfa", Url="../admin/Anasayfa.aspx"},
                    new BuradasinizInfo {Title="Gelinlik Yorumlar"}
                };
        NYEditor ny = new NYEditor();
        ny.StandartAyarlar(ceServisCevap);
        if (!IsPostBack)
        {
            KayitlariGetir();
        }
    }

    private void KayitlariGetir()
    {
        using (var db = new WhiteWorldEntities())
        {
            var kayitlar = (from x in db.yorumlar
                            join g in db.gelinlikler on x.GelinlikId equals g.Id
                            orderby x.Id descending
                            where x.UstYorumId == 0
                            select new YorumInfo
                            {
                                Id = x.Id,
                                AdSoyad = x.AdSoyad,
                                Aciklama = x.Aciklama,
                                Eposta = x.Eposta,
                                Tarih = x.Tarih,
                                Goster = x.Goster,
                                GelinlikBaslik = g.Baslik,
                                GelinlikId = x.GelinlikId
                            });
            var toplam = kayitlar.Count();
            UC_Sayfalama1.Toplam = toplam;
            UC_Sayfalama1.Adet = 5;
            UC_Sayfalama1.Sayfala();
            gvKayitlar.DataSource = kayitlar.Skip(UC_Sayfalama1.Baslangic).Take(UC_Sayfalama1.Adet).ToList();
            gvKayitlar.DataBind();

            if (gvKayitlar.HeaderRow != null)
                gvKayitlar.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void gvKayitlar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var id = e.CommandArgument.ToInt32();
        if (e.CommandName.Equals("Cevapla"))
        {
            using (var db = new WhiteWorldEntities())
            {
                var kayit = db.yorumlar.FirstOrDefault(x => x.Id == id);
                ltlServisAdSoyad.Text = kayit.AdSoyad;
                ltlServisAciklama.Text = kayit.Aciklama;
                ltlServisEposta.Text = kayit.Eposta;
                ltlServisTarih.Text = kayit.Tarih.ToShortDateString();
                pnlKayit.Style["display"] = "block";
                lblKayitBaslik.Text = "Gelinlik Yorumu Cevaplama";
                GELINLIKKAYITID = id;
            }
        }
        else if (e.CommandName.Equals("Sil"))
        {
            using (var db = new WhiteWorldEntities())
            {
                var kayit = db.yorumlar.FirstOrDefault(x => x.Id == id);
                var altKayitlar = db.yorumlar.Where(x => x.UstYorumId == kayit.Id).ToList();
                if (altKayitlar.Count > 0)
                {
                    foreach (var alt in altKayitlar)
                    {
                        db.yorumlar.Remove(alt);
                    }
                }
                db.yorumlar.Remove(kayit);
                db.SaveChanges();
                KayitlariGetir();
                MessageBox.Show("Yorum ve alt yorumları (varsa) başarıyla silindi!", MessageBox.MesajTipleri.Success);
            }
        }
    }

    protected void btnKapatUst_Click(object sender, EventArgs e)
    {
        pnlKayit.Style["display"] = "none";
        GELINLIKKAYITID = 0;
    }

    protected void btnKayitKapat_Click(object sender, EventArgs e)
    {
        pnlKayit.Style["display"] = "none";
        GELINLIKKAYITID = 0;
    }

    protected void btnServisCevapla_Click(object sender, EventArgs e)
    {
        try
        {
            using (var db = new WhiteWorldEntities())
            {
                var kayit = db.yorumlar.FirstOrDefault(x => x.Id == GELINLIKKAYITID);
                if (kayit != null)
                {
                    yorumlar ustYorum = new yorumlar
                    {
                        Aciklama = ceServisCevap.Text,
                        AdSoyad = "White World",
                        GelinlikId = kayit.GelinlikId,
                        Goster = true,
                        Eposta = "info@whiteworldweddings.com",
                        Ip = Config.Statistics.IP,
                        Tarih = DateTime.Now,
                        UstYorumId = kayit.Id
                    };
                    kayit.Goster = true;
                    db.yorumlar.Add(ustYorum);
                    db.SaveChanges();
                    db.SaveChanges();
                    pnlKayit.Style["display"] = "none";
                    MessageBox.Show("Yoruma cevap başarıyla kayıt edildi!", MessageBox.MesajTipleri.Success);
                    KayitlariGetir();

                }
                // Mail buraya gelecek
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void gvKayitlar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
            var kayit = e.Row.DataItem as YorumInfo;
            LinkButton lbCevapla = e.Row.FindControl("lbCevapla") as LinkButton;
            lbCevapla.Visible = (kayit.Goster) == false;
        }
    }
}
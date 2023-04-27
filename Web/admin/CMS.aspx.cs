using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhiteWorld.DAL;
using WhiteWorld.Info;

public partial class admin_CMS : IPage
{
    int KayitId
    {
        get { return ViewState["KAYITID"].ToInt32(); }
        set { ViewState["KAYITID"] = value; }
    }

    int BaslikId
    {
        get { return ViewState["BASLIKID"].ToInt32(); }
        set { ViewState["BASLIKID"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        var mp = this.Master as IMP_Main;
        mp.H1 = "CMS";
        mp.Title = "CMS";
        mp.Buradasiniz = new List<BuradasinizInfo>
                {
                    new BuradasinizInfo {Title="Anasayfa", Url="../admin/Anasayfa.aspx"},
                    new BuradasinizInfo {Title="CMS"}
                };
        NYEditor editor = new NYEditor();
        //editor.StandartAyarlar(ckKayitAyrinti);
        if (!IsPostBack)
        {
            BaslikId = Request.QueryString["Id"].ToInt32();
            if (BaslikId > 0)
            {
                using (var db = new WhiteWorldEntities())
                {
                    var b = db.cms.FirstOrDefault(x => x.Id == BaslikId);
                    hlUst.NavigateUrl = string.Format("CMS.aspx?Id={0}", b.BaslikId);
                    hlUst.Visible = true;
                }
            }
            KayitlariGetir();
        }
    }

    private void KayitlariGetir()
    {
        using (var db = new WhiteWorldEntities())
        {
            var kayitlar = (from x in db.cms
                            where x.BaslikId == BaslikId && x.DilKod == DilKod
                            orderby x.Oncelik
                            select new CMSInfo
                            {
                                Id = x.Id,
                                Baslik = x.Baslik,
                                Kod = x.Kod,
                                Goster = x.Goster,
                                Oncelik = x.Oncelik,
                                DilKod = x.DilKod
                            }).ToList();
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
        if (e.CommandName.Equals("Sil"))
        {
            using (var db = new WhiteWorldEntities())
            {
                var b = db.cms.FirstOrDefault(x => x.Id == id);
                var tumu = db.cms.Where(x => x.AnaBaslikId == b.AnaBaslikId).ToList();
                if (BaslikId == 0)
                    tumu.ForEach(x => db.cms.Remove(x));
                else
                {
                    db.cms.Remove(b);
                    KayitSil(db, tumu, b.Id);
                }
                db.SaveChanges();
                MessageBox.Show("Sayfa başarıyla silindi!", MessageBox.MesajTipleri.Success);
                KayitlariGetir();
            }
        }
        else if (e.CommandName.Equals("Guncelle"))
        {
            using (var db = new WhiteWorldEntities())
            {
                var k = db.cms.FirstOrDefault(x => x.Id == id);
                txtKayitBaslik.Text = k.Baslik;
                ckKayitAyrinti.Text = k.Ayrinti;
                txtKayitKod.Text = k.Kod;
                txtOncelik.Text = k.Oncelik.ToString();
                KayitId = id;
                pnlKayit.Style["display"] = "block";
                lblKayitBaslik.Text = "Kayıt Güncelleme";
                btnKayitKaydet.Text = "Güncelle";
            }
        }
    }

    private void KayitSil(WhiteWorldEntities db, List<cms> tumu, int id)
    {
        var alt = tumu.Where(x => x.BaslikId == id).ToList();
        foreach (var item in alt)
        {
            db.cms.Remove(item);
            KayitSil(db, tumu, item.Id);
        }
    }

    protected void btnKodOlustur_Click(object sender, EventArgs e)
    {
        var baslik = txtKayitBaslik.Text.ToTemizMetin();
        txtKayitKod.Text = baslik.ToURL();
    }

    protected void btnYeni_Click(object sender, EventArgs e)
    {
        btnKodOlustur.Visible = true;
        txtKayitKod.Enabled = true;
        txtOncelik.Text = "1000";
        txtKayitBaslik.Text = "";
        ckKayitAyrinti.Text = "";
        txtKayitKod.Text = "";
        KayitId = 0;
        pnlKayit.Style["display"] = "block";
        lblKayitBaslik.Text = "Kayıt Ekleme";
        btnKayitKaydet.Text = "Ekle";
    }

    protected void btnKayitKapat_Click(object sender, EventArgs e)
    {
        pnlKayit.Style["display"] = "none";
    }

    protected void btnHataKapat_Click(object sender, EventArgs e)
    {
        divHata.Visible = false;
    }

    protected void btnKapatUst_Click(object sender, EventArgs e)
    {
        pnlKayit.Style["display"] = "none";
    }

    protected void btnKayitKaydet_Click(object sender, EventArgs e)
    {
        var baslik = txtKayitBaslik.Text.ToTemizMetin();
        var kod = txtKayitKod.Text.ToTemizMetin();
        var ayrinti = ckKayitAyrinti.Text;
        if (baslik.IsNullOrEmpty())
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                @"alert('Başlığı girmelisiniz!');", true);
            txtKayitBaslik.Focus();
            // TODO: Burayı kontrol et!
            return;
        }
        if (kod.IsNullOrEmpty())
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                @"alert('URL bilgisini girmelisiniz!');", true);
            txtKayitKod.Focus();
            // TODO: Burayı kontrol et!
            return;
        }
        try
        {
            using (var db = new WhiteWorldEntities())
            {
                cms sayfa = null;
                if (KayitId == 0)
                {
                    sayfa = new cms
                    {
                        AdminId = Session["ADMIN"].ToInt32(),
                        Ayrinti = ayrinti,
                        Baslik = baslik,
                        KayitTarihi = DateTime.Now,
                        Kod = kod,
                        OkunmaSayisi = 0,
                        AnaBaslikId = 0,
                        BaslikId = BaslikId,
                        Goster = cbGoster.Checked,
                        Oncelik = txtOncelik.Text.ToInt32(),
                        DilKod = DilKod
                    };
                    db.cms.Add(sayfa);
                    MessageBox.Show("Yeni sayfa başarıyla oluşturuldu!", MessageBox.MesajTipleri.Success);
                }
                else
                {
                    sayfa = db.cms.FirstOrDefault(x => x.Id == KayitId);
                    sayfa.Ayrinti = ayrinti;
                    sayfa.Kod = kod;
                    sayfa.Baslik = baslik;
                    sayfa.Goster = cbGoster.Checked;
                    sayfa.Oncelik = txtOncelik.Text.ToInt32();
                    MessageBox.Show("Sayfa başarıyla güncellendi!", MessageBox.MesajTipleri.Success);
                }
                db.SaveChanges();
                if (KayitId == 0)
                {
                    if (BaslikId == 0)
                        sayfa.AnaBaslikId = sayfa.Id;
                    else
                    {
                        var b = db.cms.FirstOrDefault(x => x.Id == BaslikId);
                        sayfa.AnaBaslikId = b.AnaBaslikId;
                    }
                    db.SaveChanges();
                }
                pnlKayit.Style["display"] = "none";
                KayitlariGetir();
            }
        }
        catch (Exception ex)
        {
            lblHata.Text = ex.InnerException.InnerException.Message;
            divHata.Visible = true;
        }
    }

    protected void gvKayitlar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
            var kayit = e.Row.DataItem as CMSInfo;
            Literal ltlDil = e.Row.FindControl("ltlDil") as Literal;
            Literal ltlGoster = e.Row.FindControl("ltlGoster") as Literal;
            ltlGoster.Text = (kayit.Goster) ? "<i class='far fa-thumbs-up'></i>" : "<i class='far fa-thumbs-down'></i>";
            ltlDil.Text = (kayit.DilKod == 1) ? @"<img src=""/admin/img/blank.gif"" class=""flag flag-tr"" alt=""Türkçe"">" : @"<img src=""/admin/img/blank.gif"" class=""flag flag-us"" alt=""English"">";
        }
    }
}
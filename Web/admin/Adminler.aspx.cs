using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhiteWorld.DAL;
using WhiteWorld.Info;

public partial class admin_Adminler : IPage
{
    int AdminKayitId
    {
        get { return ViewState["ADMINKAYITID"].ToInt32(); }
        set { ViewState["ADMINKAYITID"] = value; }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        var adminId = Session["ADMIN"].ToInt32();
        using (var db = new WhiteWorldEntities())
        {
            var admin = db.admin.FirstOrDefault(x => x.Id == adminId);
            if (!admin.Hiper)
                Response.Redirect("/admin/Anasayfa.aspx", true);
            cbKayitHiper.Enabled = false;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        var mp = this.Master as IMP_Main;
        mp.Title = "Adminler";
        mp.H1 = "Adminler";
        mp.Buradasiniz = new List<BuradasinizInfo>
        {
            new BuradasinizInfo{Title="Anasayfa",Url="/admin/Anasayfa.aspx" },
            new BuradasinizInfo{Title="Adminler"}
        };
        if (!IsPostBack)
        {
            KayitlariGetir();
        }
    }

    private void KayitlariGetir()
    {
        using (var db = new WhiteWorldEntities())
        {
            var kayitlar = (from x in db.admin
                            orderby x.Id
                            select new AdminInfo
                            {
                                Id = x.Id,
                                AdSoyad = x.AdSoyad,
                                Kod = x.Kod,
                                SonGiris = x.SonGiris,
                                Hiper = x.Hiper
                            });
            var toplam = kayitlar.Count();
            UC_Sayfalama2.Toplam = toplam;
            UC_Sayfalama2.Adet = 10;
            UC_Sayfalama2.Sayfala();
            UC_Sayfalama1.Toplam = toplam;
            UC_Sayfalama1.Adet = 10;
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
                if (Session["ADMIN"].ToInt32() == id)
                {
                    MessageBox.Show("Kendinize ait admin üyeliğini silemezsiniz!", MessageBox.MesajTipleri.Warning);
                    return;
                }
                var kayit = db.admin.FirstOrDefault(x => x.Id == id);
                db.admin.Remove(kayit);
                db.SaveChanges();
                MessageBox.Show("Admin başarıyla silindi!", MessageBox.MesajTipleri.Success, true, 1500);
                KayitlariGetir();
            }
        }
        else if (e.CommandName.Equals("Guncelle"))
        {
            using (var db = new WhiteWorldEntities())
            {
                var k = db.admin.FirstOrDefault(x => x.Id == id);
                txtKayitAdSoyad.Text = k.AdSoyad;
                txtKayitKod.Text = k.Kod;
                txtKayitSifre.Text = k.Sifre;
                pnlKayit.Style["display"] = "block";
                lblKayitBaslik.Text = "Admin Kayıt Güncelleme";
                btnKayitKaydet.Text = "Güncelle";
                AdminKayitId = id;
            }
        }
    }

    protected void btnYeni_Click(object sender, EventArgs e)
    {
        txtKayitAdSoyad.Focus();
        txtKayitAdSoyad.Text = "";
        txtKayitKod.Text = "";
        txtKayitSifre.Text = "";
        cbKayitHiper.Checked = false;
        AdminKayitId = 0;
        pnlKayit.Style["display"] = "block";
        lblKayitBaslik.Text = "Yeni Admin Ekleme";
        btnKayitKaydet.Text = "Ekle";
    }

    protected void btnKayitKapat_Click(object sender, EventArgs e)
    {
        pnlKayit.Style["display"] = "none";
    }

    protected void btnKayitKaydet_Click(object sender, EventArgs e)
    {
        var adSoyad = txtKayitAdSoyad.Text.ToTemizMetin();
        var kod = txtKayitKod.Text.ToTemizMetin();
        var sifre = txtKayitSifre.Text.ToTemizMetin();
        var hiper = cbKayitHiper.Checked;
        if (adSoyad.IsNullOrEmpty())
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                @"alert('Ad Soyad bilgisi girmelisiniz!');", true);
            txtKayitAdSoyad.Focus();
            return;
        }
        if (kod.IsNullOrEmpty())
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                @"alert('Kod bilgisi girmelisiniz!');", true);
            txtKayitKod.Focus();
            return;
        }
        if (sifre.IsNullOrEmpty())
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                @"alert('Şifre bilgisi girmelisiniz!');", true);
            txtKayitSifre.Focus();
            return;
        }
        try
        {
            using (var db = new WhiteWorldEntities())
            {
                admin admin = null;
                if (AdminKayitId == 0)
                {
                    admin = new admin
                    {
                        AdSoyad = adSoyad,
                        Hiper = hiper,
                        Kod = kod,
                        Sifre = sifre
                    };
                    db.admin.Add(admin);
                }
                else
                {
                    admin = db.admin.FirstOrDefault(x => x.Id == AdminKayitId);
                    admin.AdSoyad = adSoyad;
                    admin.Sifre = sifre;
                    admin.Kod = kod;
                    admin.Hiper = hiper;
                }
                db.SaveChanges();
                pnlKayit.Style["display"] = "none";
                KayitlariGetir();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.InnerException.InnerException.Message, MessageBox.MesajTipleri.Error);
        }
    }

    protected void btnKapatUst_Click(object sender, EventArgs e)
    {
        pnlKayit.Style["display"] = "none";
    }

    protected void gvKayitlar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
            var kayit = e.Row.DataItem as AdminInfo;
            Literal ltlHiper = e.Row.FindControl("ltlHiper") as Literal;
            ltlHiper.Text = (kayit.Hiper) ? "<i class='far fa-thumbs-up'></i>" : "<i class='far fa-thumbs-down'></i>";
        }
    }
}
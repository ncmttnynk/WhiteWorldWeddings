using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhiteWorld.DAL;
using WhiteWorld.Info;

public partial class admin_Kategoriler : IPage
{
    g_kumaslar kumas = null;
    g_renkler renk = null;
    g_siluet siluet = null;
    g_yakatipi yakatipi = null;
    g_kesim kesim = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        var mp = this.Master as IMP_Main;
        mp.H1 = "Kategoriler";
        mp.Title = "Kategoriler";
        mp.Buradasiniz = new List<BuradasinizInfo>
                {
                    new BuradasinizInfo {Title="Anasayfa", Url="../admin/Anasayfa.aspx"},
                    new BuradasinizInfo {Title="Kategoriler"}
                };
        if (!IsPostBack)
        {
            KategoriGetir(KategoriAdi);
            using (var db = new WhiteWorldEntities())
            {
                ddlKategoriler.Items.Add(new ListItem("Seçiniz...", "0"));
                ddlKategoriler.Items.Add(new ListItem("Kesim", "Kesim"));
                ddlKategoriler.Items.Add(new ListItem("Kumaşlar", "Kumas"));
                ddlKategoriler.Items.Add(new ListItem("Renkler", "Renkler"));
                ddlKategoriler.Items.Add(new ListItem("Siluet", "Siluet"));
                ddlKategoriler.Items.Add(new ListItem("Yaka Tipi", "YakaTipi"));
            }
        }
    }

    protected void ddlKategoriler_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlKategoriler.SelectedIndex < 0)
        {
            pnlEditor.Visible = false;
            return;
        }
        pnlEditor.Visible = true;
        KategoriGetir(ddlKategoriler.SelectedValue);
        KategoriAdi = ddlKategoriler.SelectedValue;
    }

    protected void gvKayitlar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var id = e.CommandArgument.ToInt32();

        if (e.CommandName.Equals("Sil"))
        {
            using (var db = new WhiteWorldEntities())
            {
                switch (KategoriAdi)
                {
                    case "Kumas":
                        kumas = db.g_kumaslar.FirstOrDefault(x => x.Id == id);
                        db.g_kumaslar.Remove(kumas);
                        break;
                    case "Renkler":
                        renk = db.g_renkler.FirstOrDefault(x => x.Id == id);
                        db.g_renkler.Remove(renk);
                        break;
                    case "Siluet":
                        siluet = db.g_siluet.FirstOrDefault(x => x.Id == id);
                        db.g_siluet.Remove(siluet);
                        break;
                    case "YakaTipi":
                        yakatipi = db.g_yakatipi.FirstOrDefault(x => x.Id == id);
                        db.g_yakatipi.Remove(yakatipi);
                        break;
                    case "Kesim":
                        kesim = db.g_kesim.FirstOrDefault(x => x.Id == id);
                        db.g_kesim.Remove(kesim);
                        break;
                    default:
                        break;
                }
                db.SaveChanges();
            }
        }
        else if (e.CommandName.Equals("Guncelle"))
        {
            using (var db = new WhiteWorldEntities())
            {
                switch (KategoriAdi)
                {
                    case "Kumas":
                        kumas = db.g_kumaslar.FirstOrDefault(x => x.Id == id);
                        txtKayitBaslik.Text = kumas.Baslik;
                        txtKayitOncelik.Text = kumas.Oncelik.ToString();
                        cbKayitGoster.Checked = kumas.Goster;
                        break;
                    case "Renkler":
                        renk = db.g_renkler.FirstOrDefault(x => x.Id == id);
                        txtKayitBaslik.Text = renk.Baslik;
                        txtKayitOncelik.Text = renk.Oncelik.ToString();
                        cbKayitGoster.Checked = renk.Goster;
                        break;
                    case "Siluet":
                        siluet = db.g_siluet.FirstOrDefault(x => x.Id == id);
                        txtKayitBaslik.Text = siluet.Baslik;
                        txtKayitOncelik.Text = siluet.Oncelik.ToString();
                        cbKayitGoster.Checked = siluet.Goster;
                        break;
                    case "YakaTipi":
                        yakatipi = db.g_yakatipi.FirstOrDefault(x => x.Id == id);
                        txtKayitBaslik.Text = yakatipi.Baslik;
                        txtKayitOncelik.Text = yakatipi.Oncelik.ToString();
                        cbKayitGoster.Checked = yakatipi.Goster;
                        break;
                    case "Kesim":
                        kesim = db.g_kesim.FirstOrDefault(x => x.Id == id);
                        txtKayitBaslik.Text = kesim.Baslik;
                        txtKayitOncelik.Text = kesim.Oncelik.ToString();
                        cbKayitGoster.Checked = kesim.Goster;
                        break;
                    default:
                        break;
                }
                pnlKayit.Style["display"] = "block";
                ltlKayitBaslik.Text = "Kategori Güncelle";
                KategoriKayitId = id;
            }
        }
        KategoriGetir(KategoriAdi);
    }

    protected void gvKayitlar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
            var kayit = e.Row.DataItem as KategoriInfo;
            Literal ltlDil = e.Row.FindControl("ltlDil") as Literal;
            Literal ltlGoster = e.Row.FindControl("ltlGoster") as Literal;
            ltlGoster.Text = (kayit.Goster) ? "<i class='far fa-thumbs-up'></i>" : "<i class='far fa-thumbs-down'></i>";
            ltlDil.Text = (kayit.DilKod == 1) ? @"<img src=""/admin/img/blank.gif"" class=""flag flag-tr"" alt=""Türkçe"">" : @"<img src=""/admin/img/blank.gif"" class=""flag flag-us"" alt=""English"">";
        }
    }

    string KategoriAdi
    {
        get
        {
            if (ViewState["KATEGORI"] == null) return "KATEGORIBOS";
            return ViewState["KATEGORI"].ToString();
        }
        set { ViewState["KATEGORI"] = value; }
    }

    int KategoriKayitId
    {
        get { return ViewState["KATEGORIKAYITID"].ToInt32(); }
        set { ViewState["KATEGORIKAYITID"] = value; }
    }

    private void KategoriGetir(string katAdi)
    {
        KategoriDB kDB = new KategoriDB();
        switch (katAdi)
        {
            case "Kumas":
                gvKayitlar.DataSource = kDB.KumaslariGetir(DilKod);
                gvKayitlar.DataBind();
                break;
            case "Renkler":
                gvKayitlar.DataSource = kDB.RenkleriGetir(DilKod);
                gvKayitlar.DataBind();
                break;
            case "Siluet":
                gvKayitlar.DataSource = kDB.SiluetleriGetir(DilKod);
                gvKayitlar.DataBind();
                break;
            case "YakaTipi":
                gvKayitlar.DataSource = kDB.YakaTipiGetir(DilKod);
                gvKayitlar.DataBind();
                break;
            case "Kesim":
                gvKayitlar.DataSource = kDB.KesimleriGetir(DilKod);
                gvKayitlar.DataBind();
                break;
            default:
                break;
        }
        divBos.Visible = (gvKayitlar.Rows.Count == 0);
    }

    protected void btnYeni_Click(object sender, EventArgs e)
    {
        txtKayitBaslik.Focus();
        txtKayitBaslik.Text = "";
        txtKayitOncelik.Text = "1000";
        cbKayitGoster.Checked = true;
        KategoriKayitId = 0;
        pnlKayit.Style["display"] = "block";
        ltlKayitBaslik.Text = "Yeni Menü Ekleme";
        btnKayitKaydet.Text = "Ekle";
    }

    protected void btnKapatUst_Click(object sender, EventArgs e)
    {
        pnlKayit.Style["display"] = "none";
    }

    protected void btnKayitKapat_Click(object sender, EventArgs e)
    {
        pnlKayit.Style["display"] = "none";
    }

    protected void btnKayitKaydet_Click(object sender, EventArgs e)
    {
        var baslik = txtKayitBaslik.Text;
        var oncelik = txtKayitOncelik.Text.ToInt32();
        var goster = cbKayitGoster.Checked;
        if (baslik.IsNullOrEmpty())
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                @"alert('Başlık bilgisi girmelisiniz!');", true);
            txtKayitBaslik.Focus();
            return;
        }
        try
        {
            if (KategoriKayitId > 0)
            {
                using (var db = new WhiteWorldEntities())
                {
                    switch (KategoriAdi)
                    {
                        case "Kumas":
                            kumas = db.g_kumaslar.FirstOrDefault(x => x.Id == KategoriKayitId);
                            kumas.Baslik = baslik;
                            kumas.Oncelik = oncelik;
                            kumas.Goster = goster;
                            kumas.DilKod = DilKod;
                            break;
                        case "Renkler":
                            renk = db.g_renkler.FirstOrDefault(x => x.Id == KategoriKayitId);
                            renk.Baslik = baslik;
                            renk.Oncelik = oncelik;
                            renk.Goster = goster;
                            renk.DilKod = DilKod;
                            break;
                        case "Siluet":
                            siluet = db.g_siluet.FirstOrDefault(x => x.Id == KategoriKayitId);
                            siluet.Baslik = baslik;
                            siluet.Oncelik = oncelik;
                            siluet.Goster = goster;
                            siluet.DilKod = DilKod;
                            break;
                        case "YakaTipi":
                            yakatipi = db.g_yakatipi.FirstOrDefault(x => x.Id == KategoriKayitId);
                            yakatipi.Baslik = baslik;
                            yakatipi.Oncelik = oncelik;
                            yakatipi.Goster = goster;
                            yakatipi.DilKod = DilKod;
                            break;
                        case "Kesim":
                            kesim = db.g_kesim.FirstOrDefault(x => x.Id == KategoriKayitId);
                            kesim.Baslik = baslik;
                            kesim.Oncelik = oncelik;
                            kesim.Goster = goster;
                            kesim.DilKod = DilKod;
                            break;
                        default:
                            break;
                    }
                    pnlKayit.Style["display"] = "none";
                    KategoriKayitId = 0;
                    db.SaveChanges();
                    KategoriGetir(KategoriAdi);
                    MessageBox.Show("Kategori başarıyla güncellendi!", MessageBox.MesajTipleri.Success);
                }
            }
            else if (!KategoriAdi.IsNullOrEmpty())
            {
                using (var db = new WhiteWorldEntities())
                {
                    switch (KategoriAdi)
                    {
                        case "Kumas":
                            kumas = new g_kumaslar();
                            kumas.Baslik = baslik;
                            kumas.Oncelik = oncelik;
                            kumas.Goster = goster;
                            kumas.DilKod = DilKod;
                            db.g_kumaslar.Add(kumas);
                            break;
                        case "Renkler":
                            renk = new g_renkler();
                            renk.Baslik = baslik;
                            renk.Oncelik = oncelik;
                            renk.Goster = goster;
                            renk.DilKod = DilKod;
                            renk.Kod = "#fff";
                            db.g_renkler.Add(renk);
                            break;
                        case "Siluet":
                            siluet = new g_siluet();
                            siluet.Baslik = baslik;
                            siluet.Oncelik = oncelik;
                            siluet.Goster = goster;
                            siluet.DilKod = DilKod;
                            db.g_siluet.Add(siluet);
                            break;
                        case "YakaTipi":
                            yakatipi = new g_yakatipi();
                            yakatipi.Baslik = baslik;
                            yakatipi.Oncelik = oncelik;
                            yakatipi.Goster = goster;
                            yakatipi.DilKod = DilKod;
                            db.g_yakatipi.Add(yakatipi);
                            break;
                        case "Kesim":
                            kesim = new g_kesim();
                            kesim.Baslik = baslik;
                            kesim.Oncelik = oncelik;
                            kesim.Goster = goster;
                            kesim.DilKod = DilKod;
                            db.g_kesim.Add(kesim);
                            break;
                        default:
                            break;
                    }
                    db.SaveChanges();
                    KategoriGetir(KategoriAdi);
                    MessageBox.Show("Kategori başarıyla eklendi!", MessageBox.MesajTipleri.Success);
                    pnlKayit.Style["display"] = "none";
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Bir sorun oluştu!", MessageBox.MesajTipleri.Warning);
        }
    }
    protected void btnHataKapat_Click(object sender, EventArgs e)
    {
        divHata.Visible = false;
    }
}
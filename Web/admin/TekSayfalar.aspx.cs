using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhiteWorld.DAL;
using WhiteWorld.Info;

public partial class TekSayfalar : IPage
{
    string Foto
    {
        get { return (string)(Session["Foto"] ?? ""); }
        set { Session["Foto"] = value; }
    }
    int KayitId
    {
        get { return ViewState["KAYITID"].ToInt32(); }
        set { ViewState["KAYITID"] = value; }
    }
    string klasor = string.Format("/upload/teksayfalar/{0}/{1}/{2}/", DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
    protected void Page_Load(object sender, EventArgs e)
    {
        var mp = this.Master as IMP_Main;
        mp.H1 = "Tek Sayfalar";
        mp.Title = "Tek Sayfalar";
        mp.Buradasiniz = new List<BuradasinizInfo>
                {
                    new BuradasinizInfo {Title="Anasayfa", Url="../admin/Anasayfa.aspx"},
                    new BuradasinizInfo {Title="Tek Sayfalar"}
                };

        ScriptManager.RegisterStartupScript(this, this.GetType(), "prepare Function2", string.Format(@"function btnClick()
         {{
         var elemt = $('#{0}');
         if (elemt != null)
            elemt.click();
            
         }}", btnFotografUpload.ClientID), true);

        Page.Form.Attributes.Add("enctype", "multipart/form-data");

        NYEditor editor = new NYEditor();
        editor.StandartAyarlar(ckKayitAyrinti);
        if (!IsPostBack)
        {
            KayitlariGetir();
        }
    }

    protected void fu_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
    {
        try
        {
            if (fu.HasFile)
            {
                DosyaDB dosyaDB = new DosyaDB(klasor, fu);
                dosyaDB.ResimYukle(2048, 1140, 300);
                Foto = klasor + dosyaDB.DosyaAdi;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Bir hata oluştu!", MessageBox.MesajTipleri.Error);
        }
    }
    private void KayitlariGetir()
    {
        using (var db = new WhiteWorldEntities())
        {
            var kayitlar = (from x in db.teksayfalar
                            where x.DilKod == DilKod
                            orderby x.Oncelik
                            select new TekSayfaInfo
                            {
                                Id = x.Id,
                                Baslik = x.Baslik,
                                Kod = x.Kod,
                                Goster = x.Goster,
                                Oncelik = x.Oncelik,
                                DilKod = x.DilKod,
                                Tarih = x.KayitTarihi
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
                var b = db.teksayfalar.FirstOrDefault(x => x.Id == id);
                DosyaDB dosyaDB = new DosyaDB();
                dosyaDB.ResimSil(b.Fotograf);
                db.teksayfalar.Remove(b);
                db.SaveChanges();
                MessageBox.Show("Sayfa başarıyla silindi!", MessageBox.MesajTipleri.Success);
                KayitlariGetir();
            }
        }
        else if (e.CommandName.Equals("Guncelle"))
        {
            using (var db = new WhiteWorldEntities())
            {
                var k = db.teksayfalar.FirstOrDefault(x => x.Id == id);
                txtKayitBaslik.Text = k.Baslik;
                ckKayitAyrinti.Text = k.Icerik;
                txtKayitKod.Text = k.Kod;
                txtOncelik.Text = k.Oncelik.ToString();
                Foto = k.Fotograf;
                imgSlider.ImageUrl = Foto;
                divFotografGoster.Visible = true;
                divFotoUpload.Visible = false;
                KayitId = id;
                pnlKayit.Style["display"] = "block";
                lblKayitBaslik.Text = "Kayıt Güncelleme";
                btnKayitKaydet.Text = "Güncelle";
            }
        }
    }
    protected void btnSil_Click(object sender, ImageClickEventArgs e)
    {
        DosyaDB dosyaDB = new DosyaDB();
        dosyaDB.ResimSil(Foto);
        divFotoUpload.Visible = true;
        divFotografGoster.Visible = false;
        MessageBox.Show("Fotoğraf silindi!", MessageBox.MesajTipleri.Success);
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
        Foto = "";
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
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        imgSlider.ImageUrl = Foto;
        imgSlider.ToolTip = Foto;
        imgSlider.AlternateText = Foto;
        divFotografGoster.Visible = true;
        divFotoUpload.Visible = false;
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
                teksayfalar sayfa = null;
                if (KayitId == 0)
                {
                    sayfa = new teksayfalar
                    {
                        AdminId = Session["ADMIN"].ToInt32(),
                        Icerik = ayrinti,
                        Baslik = baslik,
                        KayitTarihi = DateTime.Now,
                        Kod = kod,
                        OkunmaSayisi = 0,
                        Goster = cbGoster.Checked,
                        Oncelik = txtOncelik.Text.ToInt32(),
                        DilKod = DilKod,
                        Fotograf = Foto
                    };
                    db.teksayfalar.Add(sayfa);
                    MessageBox.Show("Yeni sayfa başarıyla oluşturuldu!", MessageBox.MesajTipleri.Success);
                }
                else
                {
                    sayfa = db.teksayfalar.FirstOrDefault(x => x.Id == KayitId);
                    sayfa.Icerik = ayrinti;
                    sayfa.Kod = kod;
                    sayfa.Baslik = baslik;
                    sayfa.Goster = cbGoster.Checked;
                    sayfa.Oncelik = txtOncelik.Text.ToInt32();
                    sayfa.Fotograf = Foto;
                    MessageBox.Show("Sayfa başarıyla güncellendi!", MessageBox.MesajTipleri.Success);
                }
                db.SaveChanges();
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
            var kayit = e.Row.DataItem as TekSayfaInfo;
            Literal ltlDil = e.Row.FindControl("ltlDil") as Literal;
            Literal ltlGoster = e.Row.FindControl("ltlGoster") as Literal;
            ltlGoster.Text = (kayit.Goster) ? "<i class='far fa-thumbs-up'></i>" : "<i class='far fa-thumbs-down'></i>";
            ltlDil.Text = (kayit.DilKod == 1) ? @"<img src=""/admin/img/blank.gif"" class=""flag flag-tr"" alt=""Türkçe"">" : @"<img src=""/admin/img/blank.gif"" class=""flag flag-us"" alt=""English"">";
        }
    }
}
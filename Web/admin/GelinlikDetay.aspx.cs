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

public partial class admin_GelinlikDetay : IPage
{
    int gelinlikId = 0;
    string klasor = string.Format("/upload/gelinlikler/{0}/{1}/{2}/", DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
    gelinlikler gelinlik = null;
    etiketler etiket = null;
    DosyaDB dosyaDB = null;

    string FotoKucuk
    {
        get { return (string)(Session["FotoKucuk"] ?? ""); }
        set { Session["FotoKucuk"] = value; }
    }

    string FotoBuyuk
    {
        get { return (string)(Session["FotoBuyuk"] ?? ""); }
        set { Session["FotoBuyuk"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        gelinlikId = Request.QueryString["Id"].ToInt32();
        var mp = this.Master as IMP_Main;

        NYEditor ny = new NYEditor();
        ny.StandartAyarlar(ceGelinlikDetay);
        ny.StandartAyarlar(ceGelinlikAciklama);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "prepare Function2", string.Format(@"function btnFotografClick()
         {{
         var elemt = $('#{0}');
         if (elemt != null)
            elemt.click();
            
         }}", btnFotografUpload.ClientID), true);

        Page.Form.Attributes.Add("enctype", "multipart/form-data");

        if (!IsPostBack)
        {
            KategoriGetir();
            if (gelinlikId == 0)
            {
                mp.H1 = "Yeni Gelinlik Ekle";
                mp.Title = "Yeni Gelinlik Ekle";
                mp.Buradasiniz = new List<WhiteWorld.Info.BuradasinizInfo>
                {
                    new WhiteWorld.Info.BuradasinizInfo {Title="Anasayfa", Url="../admin/Anasayfa.aspx"},
                    new WhiteWorld.Info.BuradasinizInfo {Title="Gelinlikler",Url="../admin/Gelinlikler.aspx"},
                    new WhiteWorld.Info.BuradasinizInfo {Title="Yeni Gelinlik Ekle"}
                };
                ltlFormBaslik.Text = "Yeni Gelinlik Ekle";
            }
            else
            {
                using (var db = new WhiteWorldEntities())
                {
                    gelinlik = db.gelinlikler.FirstOrDefault(x => x.Id == gelinlikId);
                    mp.H1 = gelinlik.Baslik;
                    mp.Title = gelinlik.Baslik;
                    ltlFormBaslik.Text = gelinlik.Baslik;
                    txtGelinlikBaslik.Text = gelinlik.Baslik;
                    txtGelinlikOncelik.Text = gelinlik.Oncelik.ToString();
                    ceGelinlikAciklama.Text = gelinlik.Aciklama;
                    ceGelinlikDetay.Text = gelinlik.Detay;
                    cbGelinlikGoster.Checked = gelinlik.Goster;
                    cbEnCokSatan.Checked = gelinlik.EnCokSatan;
                    cbOzelUrun.Checked = gelinlik.OzelUrun;
                    cbYeni.Checked = gelinlik.Yeni;
                    cbYeniSezon.Checked = gelinlik.YeniSezon;
                    cbAnasayfa.Checked = gelinlik.Anasayfa;

                    var tag = (from x in db.etiketler
                               where x.GelinlikId == gelinlikId
                               select x.Baslik).ToList();
                    foreach (var t in tag)
                    {
                        txtGelinlikTag.Text += t + ", ";
                    }


                    //ddlKumas.SelectedValue = gelinlik.KumasId.ToString();
                    //ddlRenk.SelectedValue = gelinlik.RenkId.ToString();
                    //ddlSiluet.SelectedValue = gelinlik.SiluetId.ToString();
                    //ddlYakaTipi.SelectedValue = gelinlik.YakaTipiId.ToString();
                    //ddlBeden.SelectedValue = gelinlik.Beden.ToString();
                    ddlKesim.SelectedValue = gelinlik.KesimId.ToString();
                    ddlSezon.SelectedValue = gelinlik.Sezon.ToString();

                    FotograflariGetir();
                    mp.Buradasiniz = new List<WhiteWorld.Info.BuradasinizInfo>
                    {
                    new WhiteWorld.Info.BuradasinizInfo {Title="Anasayfa", Url="../admin/Anasayfa.aspx"},
                    new WhiteWorld.Info.BuradasinizInfo {Title="Gelinlikler",Url="../admin/Gelinlikler.aspx"},
                    new WhiteWorld.Info.BuradasinizInfo {Title= gelinlik.Baslik}
                    };
                }
            }
        }
    }

    private void FotograflariGetir()
    {
        using (var db = new WhiteWorldEntities())
        {
            var fotograflar = (from x in db.gelinlikfotograflari
                               where x.GelinlikId == gelinlikId
                               select new FotografInfo
                               {
                                   Id = x.Id,
                                   FotoKucuk = x.FotoKucuk,
                                   ArkaFoto = x.ArkaFoto,
                                   OnFoto = x.OnFoto,
                                   Anasayfa = x.Anasayfa
                               }).ToList();
            if (fotograflar.Count > 0)
            {
                pnlFotograflar.Visible = true;
                rptMakineFotograflar.DataSource = fotograflar;
                rptMakineFotograflar.DataBind();
            }
        }
    }

    protected void btnFotografUpload_Click(object sender, EventArgs e)
    {
        var etiket = txtFotografEtiket.Text;
        try
        {
            using (var db = new WhiteWorldEntities())
            {
                gelinlikfotograflari yeniFoto = new gelinlikfotograflari
                {
                    FotoBuyuk = FotoBuyuk,
                    FotoKucuk = FotoKucuk,
                    GelinlikId = gelinlikId,
                    ArkaFoto = false,
                    OnFoto = false,
                    Etiket = etiket
                };
                db.gelinlikfotograflari.Add(yeniFoto);
                db.SaveChanges();
                FotograflariGetir();
                FotoKucuk = "";
                FotoBuyuk = "";
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show("Bir hata oluştu!", MessageBox.MesajTipleri.Error);
        }

    }
    private void KategoriGetir()
    {
        KategoriDB kDB = new KategoriDB();
        //ddlKumas.DataSource = kDB.KumaslariGetir(DilKod);
        //ddlKumas.DataTextField = "Baslik";
        //ddlKumas.DataValueField = "Id";
        //ddlKumas.DataBind();
        //ddlKumas.Items.Insert(0, new ListItem("Seçiniz...", "0"));

        //ddlRenk.DataSource = kDB.RenkleriGetir(DilKod);
        //ddlRenk.DataTextField = "Baslik";
        //ddlRenk.DataValueField = "Id";
        //ddlRenk.DataBind();
        //ddlRenk.Items.Insert(0, new ListItem("Seçiniz...", "0"));

        //ddlSiluet.DataSource = kDB.SiluetleriGetir(DilKod);
        //ddlSiluet.DataTextField = "Baslik";
        //ddlSiluet.DataValueField = "Id";
        //ddlSiluet.DataBind();
        //ddlSiluet.Items.Insert(0, new ListItem("Seçiniz...", "0"));

        //ddlYakaTipi.DataSource = kDB.YakaTipiGetir(DilKod);
        //ddlYakaTipi.DataTextField = "Baslik";
        //ddlYakaTipi.DataValueField = "Id";
        //ddlYakaTipi.DataBind();
        //ddlYakaTipi.Items.Insert(0, new ListItem("Seçiniz...", "0"));

        ddlKesim.DataSource = kDB.KesimleriGetir(DilKod);
        ddlKesim.DataTextField = "Baslik";
        ddlKesim.DataValueField = "Id";
        ddlKesim.DataBind();
        ddlKesim.Items.Insert(0, new ListItem("Seçiniz...", "0"));
    }
    protected void fuMakineFotograf_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
    {
        try
        {
            if (fuMakineFotograf.HasFile)
            {
                dosyaDB = new DosyaDB(klasor);
                dosyaDB.ResimYukle(fuMakineFotograf, 4096, 1000, 1000, 850, 850);
                FotoKucuk = klasor + dosyaDB.ThumbnailDosyaAdi;
                FotoBuyuk = klasor + dosyaDB.DosyaAdi;
                FotograflariGetir();
            }
            MessageBox.Show("Fotoğraf başarıyla yüklendi!", MessageBox.MesajTipleri.Success);
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnKaydet_Click(object sender, EventArgs e)
    {
        var baslik = txtGelinlikBaslik.Text;
        var oncelik = txtGelinlikOncelik.Text.ToInt32();
        var tag = txtGelinlikTag.Text.Split(',');
        var aciklama = ceGelinlikAciklama.Text;
        var detaylar = ceGelinlikDetay.Text;
        var goster = cbGelinlikGoster.Checked;
        var yeniSezon = cbYeniSezon.Checked;
        var enCokSatan = cbEnCokSatan.Checked;
        var yeni = cbYeni.Checked;
        var ozelUrun = cbOzelUrun.Checked;
        var anasayfa = cbAnasayfa.Checked;
        //var beden = ddlBeden.SelectedValue.ToInt32();
        //var renkId = ddlRenk.SelectedValue.ToInt32();
        //var siluetId = ddlSiluet.SelectedValue.ToInt32();
        //var yakaTipiId = ddlYakaTipi.SelectedValue.ToInt32();
        //var kumasId = ddlKumas.SelectedValue.ToInt32();
        var kesimId = ddlKesim.SelectedValue.ToInt32();
        var sezon = ddlSezon.SelectedValue.ToInt32();

        try
        {
            if (gelinlikId == 0)
            {
                using (var db = new WhiteWorldEntities())
                {
                    gelinlik = new gelinlikler
                    {
                        Aciklama = aciklama,
                        Baslik = baslik,
                        Detay = detaylar,
                        DilKod = DilKod,
                        Goster = goster,
                        Oncelik = oncelik,
                        YeniSezon = yeniSezon,
                        Yeni = yeni,
                        EnCokSatan = enCokSatan,
                        OzelUrun = ozelUrun,
                        KesimId = kesimId,
                        Sezon = sezon,
                        //Beden = beden,
                        //KumasId = kumasId,
                        //RenkId = renkId,
                        //SiluetId = siluetId,
                        //YakaTipiId = yakaTipiId,
                        Anasayfa = anasayfa
                    };

                    db.gelinlikler.Add(gelinlik);
                    db.SaveChanges();

                    foreach (var t in tag)
                    {
                        etiket = new etiketler
                        {
                            Baslik = t.ToURL().Replace(",", ""),
                            GelinlikId = gelinlik.Id
                        };
                        db.etiketler.Add(etiket);
                        db.SaveChanges();

                    }
                    var fotograflar = db.gelinlikfotograflari.Where(x => x.GelinlikId == 0).ToList();
                    foreach (var f in fotograflar)
                    {
                        f.GelinlikId = gelinlik.Id;
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                using (var db = new WhiteWorldEntities())
                {
                    gelinlik = db.gelinlikler.FirstOrDefault(x => x.Id == gelinlikId);
                    gelinlik.Baslik = baslik;
                    gelinlik.Aciklama = aciklama;
                    gelinlik.Detay = detaylar;
                    gelinlik.Goster = goster;
                    gelinlik.Oncelik = oncelik;
                    gelinlik.Yeni = yeni;
                    gelinlik.YeniSezon = yeniSezon;
                    gelinlik.OzelUrun = ozelUrun;
                    gelinlik.EnCokSatan = enCokSatan;
                    gelinlik.KesimId = kesimId;
                    gelinlik.Sezon = sezon;
                    //gelinlik.RenkId = renkId;
                    //gelinlik.KumasId = kumasId;
                    //gelinlik.SiluetId = siluetId;
                    //gelinlik.YakaTipiId = yakaTipiId;
                    //gelinlik.Beden = beden;
                    gelinlik.Anasayfa = anasayfa;

                    var tagler = db.etiketler.Where(x => x.GelinlikId == gelinlik.Id).ToList();
                    foreach (var a in tagler)
                    {
                        db.etiketler.Remove(a);
                        db.SaveChanges();
                    }
                    foreach (var t in tag)
                    {
                        etiket = new etiketler
                        {
                            GelinlikId = gelinlik.Id,
                            Baslik = t.ToURL().Replace(",", "")
                        };
                        db.etiketler.Add(etiket);
                        db.SaveChanges();
                    }
                    db.SaveChanges();
                }
            }

            MessageBox.Show("Gelinlik başarıyla kayıt edildi!", MessageBox.MesajTipleri.Success);
            Response.Redirect("/admin/Gelinlikler.aspx", true);
        }
        catch (Exception)
        {
        }
    }

    protected void rptMakineFotograflar_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int id = e.CommandArgument.ToInt32();
        if (id > 0)
        {
            if (e.CommandName.Equals("Sil"))
            {
                using (var db = new WhiteWorldEntities())
                {
                    var fotograf = db.gelinlikfotograflari.FirstOrDefault(x => x.Id == id);
                    dosyaDB = new DosyaDB();
                    dosyaDB.ResimSil(fotograf.FotoKucuk);
                    dosyaDB.ResimSil(fotograf.FotoBuyuk);
                    db.gelinlikfotograflari.Remove(fotograf);
                    db.SaveChanges();
                    FotograflariGetir();
                    MessageBox.Show("Fotoğraf silindi!", MessageBox.MesajTipleri.Success);
                }
            }
            else if (e.CommandName.Equals("OnFoto"))
            {
                using (var db = new WhiteWorldEntities())
                {
                    var fotograf = db.gelinlikfotograflari.FirstOrDefault(x => x.Id == id);
                    var onFoto = db.gelinlikfotograflari.FirstOrDefault(x => x.GelinlikId == gelinlikId && x.OnFoto);
                    if (fotograf.ArkaFoto)
                        MessageBox.Show("Bir fotoğraf arkada kullanılırken önde kullanamazsınız!", MessageBox.MesajTipleri.Warning);
                    else
                    {
                        if (onFoto != null)
                            onFoto.OnFoto = false;
                        fotograf.OnFoto = true;
                        db.SaveChanges();
                        MessageBox.Show("Fotoğraf ön fotoğraf olarak ayarlandı!", MessageBox.MesajTipleri.Success);
                        FotograflariGetir();
                    }
                }
            }
            else if (e.CommandName.Equals("ArkaFoto"))
            {
                using (var db = new WhiteWorldEntities())
                {
                    var fotograf = db.gelinlikfotograflari.FirstOrDefault(x => x.Id == id);
                    var arkaFoto = db.gelinlikfotograflari.FirstOrDefault(x => x.GelinlikId == gelinlikId && x.ArkaFoto);
                    if (fotograf.OnFoto)
                        MessageBox.Show("Bir fotoğraf önde kullanılırken arkada kullanamazsınız!", MessageBox.MesajTipleri.Warning);
                    else
                    {
                        if (arkaFoto != null)
                            arkaFoto.ArkaFoto = false;
                        fotograf.ArkaFoto = true;
                        db.SaveChanges();
                        MessageBox.Show("Fotoğraf arka fotoğraf olarak ayarlandı!", MessageBox.MesajTipleri.Success);
                        FotograflariGetir();
                    }
                }
            }
            else if (e.CommandName.Equals("Anasayfa"))
            {
                using (var db = new WhiteWorldEntities())
                {
                    var fotograf = db.gelinlikfotograflari.FirstOrDefault(x => x.Id == id);
                    var anasayfaFoto = db.gelinlikfotograflari.FirstOrDefault(x => x.GelinlikId == gelinlikId && x.Anasayfa);
                    if (anasayfaFoto != null)
                        anasayfaFoto.Anasayfa = false;
                    fotograf.Anasayfa = true;
                    db.SaveChanges();
                    MessageBox.Show("Fotoğraf anasayfa fotoğrafı olarak ayarlandı!", MessageBox.MesajTipleri.Success);
                    FotograflariGetir();
                }
            }
        }
    }

    protected void rptMakineFotograflar_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            var kayit = e.Item.DataItem as FotografInfo;
            LinkButton lbOnFoto = e.Item.FindControl("lbOnFoto") as LinkButton;
            LinkButton lbArkaFoto = e.Item.FindControl("lbArkaFoto") as LinkButton;
            LinkButton lbAnasayfa = e.Item.FindControl("lbAnasayfa") as LinkButton;
            if (kayit.ArkaFoto)
                lbArkaFoto.Visible = false;
            if (kayit.OnFoto)
                lbOnFoto.Visible = false;
            if (kayit.Anasayfa)
                lbAnasayfa.Visible = false;
        }
    }
}

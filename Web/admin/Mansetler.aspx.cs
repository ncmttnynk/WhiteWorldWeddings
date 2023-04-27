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

public partial class admin_Slider : IPage
{
    string FileName
    {
        get { return (string)(Session["FileName"] ?? ""); }
        set { Session["FileName"] = value; }
    }

    string Path
    {
        get { return (string)(Session["Path"] ?? ""); }
        set { Session["Path"] = value; }
    }

    int KayitId
    {
        get { return ViewState["KAYITID"].ToInt32(); }
        set { ViewState["KAYITID"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        var mp = this.Master as IMP_Main;
        mp.H1 = "Manşetler";
        mp.Title = "Manşetler";
        mp.Buradasiniz = new List<BuradasinizInfo>
                {
                    new BuradasinizInfo {Title="Anasayfa", Url="../admin/Anasayfa.aspx"},
                    new BuradasinizInfo {Title="Manşetler"}
                };

        ScriptManager.RegisterStartupScript(this, this.GetType(), "prepare Function", string.Format(@"function btnClick()
         {{
         var elem = $('#{0}');
         if (elem != null)
            elem.click();
            
         }}", btnUpload.ClientID), true);

        Page.Form.Attributes.Add("enctype", "multipart/form-data");

        if (!IsPostBack)
        {
            KayitlariGetir();
        }
    }

    private void KayitlariGetir()
    {
        using (var db = new WhiteWorldEntities())
        {
            var kayitlar = from x in db.mansetler
                           where x.DilKod == DilKod
                           orderby x.Oncelik
                           select new MansetlerInfo
                           {
                               Id = x.Id,
                               Oncelik = x.Oncelik,
                               Fotograf = x.Fotograf,
                               Baslik = x.Baslik,
                               Url = x.Url,
                               Goster = x.Goster,
                               DilKod = x.DilKod
                           };
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
    protected void btnKapatUst_Click(object sender, EventArgs e)
    {
        pnlKayit.Visible = false;
        KayitId = 0;
    }

    protected void btnKapat_Click(object sender, EventArgs e)
    {
        pnlKayit.Visible = false;
        KayitId = 0;
    }

    protected void btnYeni_Click(object sender, EventArgs e)
    {
        pnlKayit.Visible = true;
        pnlFotografEkleme.Visible = true;
        divFotografGoster.Visible = false;
        divFotoUpload.Visible = true;
        txtMansetOncelik.Text = "1000";
        txtMansetBaslik.Focus();
        txtMansetBaslik.Text = "";
        txtMansetUrl.Text = "";
        pnlKayit.Style["display"] = "block";
        imgSlider.ImageUrl = "";
        lblKayitBaslik.Text = "Yeni Manşet Ekle";
    }

    protected void gvKayitlar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
            var kayit = e.Row.DataItem as MansetlerInfo;
            Literal ltlDil = e.Row.FindControl("ltlDil") as Literal;
            Literal ltlGoster = e.Row.FindControl("ltlGoster") as Literal;
            ltlGoster.Text = (kayit.Goster) ? "<i class='far fa-thumbs-up'></i>" : "<i class='far fa-thumbs-down'></i>";
            ltlDil.Text = (kayit.DilKod == 1) ? @"<img src=""/admin/img/blank.gif"" class=""flag flag-tr"" alt=""Türkçe"">" : @"<img src=""/admin/img/blank.gif"" class=""flag flag-us"" alt=""English"">";
        }
    }

    protected void fu_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
    {
        try
        {
            if (fu.HasFile)
            {
                // Belki dosyanın adına özel bir şeyler düşünülebilir...
                FileName = fu.FileName;
                Path = string.Format("~/upload/mansetler/{0}/{1}/{2}/", DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
                if (!Directory.Exists(Server.MapPath(Path)))
                    Directory.CreateDirectory(Server.MapPath(Path));
                fu.SaveAs(Server.MapPath(Path) + FileName);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Bir hata oluştu!", MessageBox.MesajTipleri.Error);
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        imgSlider.ImageUrl = Path + FileName;
        imgSlider.ToolTip = Path + FileName;
        imgSlider.AlternateText = Path + FileName;
        divFotografGoster.Visible = true;
        divFotoUpload.Visible = false;
    }

    protected void gvKayitlar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var id = e.CommandArgument.ToInt32();
        if (e.CommandName.Equals("Sil"))
        {
            using (var db = new WhiteWorldEntities())
            {
                var k = db.mansetler.FirstOrDefault(x => x.Id == id);
                db.mansetler.Remove(k);
                db.SaveChanges();
                DosyaDB dosyaDB = new DosyaDB();
                dosyaDB.ResimSil(k.Fotograf);
                MessageBox.Show("Manşet başarıyla silindi!", MessageBox.MesajTipleri.Success);
                KayitlariGetir();
            }
        }
    }

    protected void btnSil_Click(object sender, ImageClickEventArgs e)
    {
        DosyaDB dosyaDB = new DosyaDB();
        dosyaDB.ResimSil(Path + FileName);
        divFotoUpload.Visible = true;
        divFotografGoster.Visible = false;
        MessageBox.Show("Fotoğraf silindi!", MessageBox.MesajTipleri.Success);
    }

    protected void btnKaydet_Click(object sender, EventArgs e)
    {
        var baslik = txtMansetBaslik.Text;
        var url = (!txtMansetUrl.Text.IsNullOrEmpty() ? txtMansetUrl.Text : "javascript:void(0)");
        var oncelik = txtMansetOncelik.Text.ToInt32();
        var goster = cbSliderGoster.Checked;
        var yeni = cbSliderYeni.Checked;
        using (var db = new WhiteWorldEntities())
        {
            mansetler yeniManset = new mansetler
            {
                Baslik = baslik,
                DilKod = DilKod,
                Fotograf = (Path + FileName).Substring(1),
                Goster = goster,
                Oncelik = oncelik,
                Url = url,
                Yeni = yeni
            };
            db.mansetler.Add(yeniManset);
            db.SaveChanges();
            MessageBox.Show("Manşet başarıyla eklendi!", MessageBox.MesajTipleri.Success, true, 2000);
            pnlKayit.Visible = false;
            KayitlariGetir();
            up2.Update();
        }
    }
}
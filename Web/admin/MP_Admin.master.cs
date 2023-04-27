using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhiteWorld.DAL;
using WhiteWorld.Info;

public partial class admin_MP_Admin : IMP_Main
{
    public admin AktifAdmin
    {
        set
        {
            if (value == null)
                return;
            else
                AktifAdmin = value;
        }
        get
        {
            using (var db = new WhiteWorldEntities())
            {
                var admin = db.admin.FirstOrDefault(x => x.Id == AdminId);
                return admin;
            }
        }
    }
    public string MyTitle
    {
        get
        {
            if (ViewState["TITLE"] == null) return "";
            return ViewState["TITLE"].ToString();
        }
        set { ViewState["TITLE"] = value; }
    }
    public string MyH1
    {
        get
        {
            if (ViewState["H1"] == null) return "";
            return ViewState["H1"].ToString();
        }
        set { ViewState["H1"] = value; }
    }
    public override List<BuradasinizInfo> Buradasiniz
    {
        set
        {
            UC_Buradasiniz1.Buradasiniz = value;
        }
    }
    public override string Title
    {
        set
        {
            MyTitle = value;
        }
    }
    public override string H1
    {
        set
        {
            MyH1 = value;
        }
    }
    protected string Dil
    {
        get
        {
            if (Session["Dil"] == null)
                Session["Dil"] = "tr";
            return Session["Dil"].ToString();
        }
    }
    protected int DilKod
    {
        get
        {
            if (Session["Dil"] == null)
                Session["Dil"] = "tr";
            if (Session["Dil"].ToString() == "tr") return 1;
            else if (Session["Dil"].ToString() == "ar") return 2;
            return 1;
        }
    }
    string AdminAdSoyad
    {
        get { return Session["ADMINADSOYAD"].ToString(); }
        set { Session["ADMINADSOYAD"] = value; }
    }
    int AdminId
    {
        get { return Session["ADMIN"].ToInt32(); }
        set { Session["ADMIN"] = value; }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!MyTitle.IsNullOrEmpty())
        {
            Page.Title = MyTitle + " | " + Config.Title;
        }
        else
            Page.Title = Config.Title;

        if (!MyH1.IsNullOrEmpty())
        {
            lblH1.Text = MyH1;
            divPageHeader.Visible = true;
        }
        ltlAdmin.Text = AdminAdSoyad;
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        if (AdminId == 0) Response.Redirect("Sifre.aspx", true);
        using (var db = new WhiteWorldEntities())
        {
            var admin = db.admin.FirstOrDefault(x => x.Id == AdminId);
            if (admin.Hiper)
            {
                hlAdminler.Visible = true;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ltlSeciliDil.Text = (DilKod == 1) ? @"<img src=""/admin/img/blank.gif"" class=""flag flag-tr"" alt=""Türkçe""/>
                        Türkçe
                                                    <i class=""fas fa-angle-down""></i>" : @"<img src=""/admin/img/blank.gif"" class=""flag flag-ae"" alt=""Arabia""/>
                        Arabia
                                                    <i class=""fas fa-angle-down""></i>";
            ltlMenuDil.Text = (DilKod == 1) ? @"<a class=""dropdown-item"" href=""/Language.aspx?Dil=ar&Url=%2fadmin%2fAnasayfa.aspx"">
                            <img src= ""/admin/img/blank.gif"" class=""flag flag-ae"" alt=""Arabia"" />
                            Arabia</a>"
                         : @"<a class=""dropdown-item"" href=""/Language.aspx?Dil=tr&Url=%2fadmin%2fAnasayfa.aspx"">
                              <img src=""/admin/img/blank.gif"" class=""flag flag-tr"" alt=""Türkçe"" />
                            Türkçe</a>";
        }
    }

    protected void btnCikisYap_Click(object sender, EventArgs e)
    {
        HttpCookie c = new HttpCookie("ADMINKOD");
        c.Expires = DateTime.Today.AddDays(-14.0);
        Response.Cookies.Add(c);

        c = new HttpCookie("ADMINSIFRE");
        c.Expires = DateTime.Today.AddDays(-14.0);
        Response.Cookies.Add(c);

        Session.Abandon();
        Response.Redirect("/", true);
    }

    protected void btnCacheSifirla_Click(object sender, EventArgs e)
    {
        Config.Sifirla();

        foreach (DictionaryEntry x in Cache)
            Cache.Remove(x.Key.ToString());
        MessageBox.Show("Cache başarı ile sıfırlandı!", MessageBox.MesajTipleri.Success);
    }

    protected void lblAdminSifre_Click(object sender, EventArgs e)
    {
        pnlKayit.Style["display"] = "block";
        txtAdminSifre.Text = AktifAdmin.Sifre;
        txtAdminKod.Text = AktifAdmin.Kod;
        txtAdminAdSoyad.Text = AktifAdmin.AdSoyad;
        cbAdminHiper.Checked = AktifAdmin.Hiper;
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
        var adSoyad = txtAdminAdSoyad.Text.ToTemizMetin();
        var kod = txtAdminKod.Text.ToTemizMetin();
        var sifre = txtAdminSifre.Text.ToTemizMetin();
        using (var db = new WhiteWorldEntities())
        {
            var admin = db.admin.FirstOrDefault(x => x.Id == AdminId);
            admin.AdSoyad = adSoyad;
            admin.Kod = kod;
            admin.Sifre = sifre;
            db.SaveChanges();
            pnlKayit.Style["display"] = "none";
            AdminAdSoyad = admin.AdSoyad;
        }
    }
}

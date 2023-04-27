using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhiteWorld.DAL;

public partial class admin_Sifre : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = "Şifre | " + Config.Title;
        Response.Expires = 0;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.AddHeader("pragma", "no-cache");
        Response.AddHeader("cache-control", "private");
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            Session["CaptchaMetin"] = DevrimAltinkurt.DogrulamaKodu.RastgeleKodUretici();
            txtKod.Focus();
            try
            {
                var kod = Request.Cookies["ADMINKOD"].Value.SifreCoz();
                var sifre = Request.Cookies["ADMINSIFRE"].Value.SifreCoz();

                var id = KontrolEt(kod, sifre);
                if (id > 0)
                {
                    Session["ADMIN"] = id.ToString();
                    Response.Redirect("/admin/Anasayfa.aspx", true);
                }
            }
            catch (Exception)
            {
            }
        }
    }

    protected void btnGiris_Click(object sender, EventArgs e)
    {
        divCaptcha.Visible = false;
        if (!Session["CaptchaMetin"].ToString().Equals(txtCaptcha.Text))
        {
            txtCaptcha.Text = "";
            Session["CaptchaMetin"] = DevrimAltinkurt.DogrulamaKodu.RastgeleKodUretici();
            divCaptcha.Visible = true;
            txtCaptcha.Focus();
            return;
        }
        Session["CaptchaMetin"] = DevrimAltinkurt.DogrulamaKodu.RastgeleKodUretici();

        var kod = txtKod.Text.ToTemizMetin();
        var sifre = txtSifre.Text.ToTemizMetin();

        var id = KontrolEt(kod, sifre);
        if (id > 0)
        {
            Session["ADMIN"] = id.ToString();
            if (cbBeniHatirla.Checked)
            {
                var c1 = new HttpCookie("ADMINKOD", kod.Sifrele());
                var c2 = new HttpCookie("ADMINSIFRE", sifre.Sifrele());

                Response.Cookies.Add(c1);
                Response.Cookies.Add(c2);
            }
            else
            {
                try
                {
                    HttpCookie c1 = Request.Cookies["ADMINKOD"];
                    c1.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(c1);

                    HttpCookie c2 = Request.Cookies["ADMINSIFRE"];
                    c2.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(c2);
                }
                catch (Exception)
                {
                }
            }
            Response.Redirect("Anasayfa.aspx", true);
        }
        divHata.Visible = true;
    }

    int KontrolEt(string kod, string sifre)
    {
        using (var db = new WhiteWorldEntities())
        {
            var admin = db.admin.FirstOrDefault(x => x.Kod == kod);
            if (admin == null) return 0;
            if (admin.Sifre == sifre)
            {
                admin.SonGiris = DateTime.Now.Date;
                db.SaveChanges();
                Session["ADMINADSOYAD"] = admin.AdSoyad;
                return admin.Id;
            }
            return 0;
        }
    }
}
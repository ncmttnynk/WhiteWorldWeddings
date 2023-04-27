using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhiteWorld.DAL;
using DA.MetaTagGenerator;

public partial class Iletisim : IPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            base.MP_Main.Title = (DilKod == 1) ? "İletişim" : "Contact Us";
            SabitlerDB sDB = new SabitlerDB();
            ltlAlt.Text = (DilKod == 1) ? sDB.TurkceIcerikGetir(Enums.Sabitler.IletisimAlt) : sDB.IngilizceIcerikGetir(Enums.Sabitler.IletisimAlt);
            ltlUst.Text = (DilKod == 1) ? sDB.TurkceIcerikGetir(Enums.Sabitler.IletisimUst) : sDB.IngilizceIcerikGetir(Enums.Sabitler.IletisimUst);
            ltlIletisimFormBaslik.Text = (DilKod == 1) ? sDB.TurkceIcerikGetir(Enums.Sabitler.IletisimFormBaslik) : sDB.IngilizceIcerikGetir(Enums.Sabitler.IletisimFormBaslik);
            ltlIletisimAciklama.Text = (DilKod == 1) ? sDB.TurkceIcerikGetir(Enums.Sabitler.IletisimAciklama) : sDB.IngilizceIcerikGetir(Enums.Sabitler.IletisimAciklama);

            txtIletisimAdSoyad.Attributes["placeholder"] = (Dil.Equals("tr") ? "Ad Soyad" : "Name Surname");
            txtIletisimEposta.Attributes["placeholder"] = (Dil.Equals("tr") ? "E-Posta" : "E-Mail");
            txtIletisimAciklama.Attributes["placeholder"] = (Dil.Equals("tr") ? "Mesaj" : "Message");
            btnYorumKaydet.Text = (Dil.Equals("tr") ? "Kaydet" : "Submit");

            MetaTagGenerator gen = MetaTagGenerator.CreateInstance(this);
            gen.MetaTags = new Tags.MetaTagsInfo
            {
                Description = "White World iletişim bilgileri",
                Keywords = "iletişim, adres, white, world, osmanbey, gelinlik, özel",
                Language = Dil,
                Title = "İletişim | White World",
                Topic = "Özel Dikim Gelinlik",
            };
            gen.GenerateTags();

            //GoogleMapsHazirla("41.0553827,28.986985");
        }
    }

    //private void GoogleMapsHazirla(string koord)
    //    {
    //        GoogleMapForASPNet1.GoogleMapObject.APIKey = Config.GoogleMapApiKey;

    //        GoogleMapForASPNet1.GoogleMapObject.Width = "100%"; // You can also specify percentage(e.g. 80%) here
    //        GoogleMapForASPNet1.GoogleMapObject.Height = "400px";

    //        double lat = 38.68551;
    //        double lon = 34.23339;
    //        int zum = 6;
    //        if (!string.IsNullOrEmpty(koord))
    //        {
    //            try
    //            {
    //                var split = koord.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
    //                lat = split[0].Replace(".", ",").ToDouble();
    //                lon = split[1].Replace(".", ",").ToDouble();
    //                zum = 16;
    //            }
    //            catch (Exception)
    //            {
    //                lat = 38.68551;
    //                lon = 34.23339;
    //                zum = 6;
    //            }
    //        }
    //        GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = zum;
    //        GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", lat, lon);

    //        GooglePoint GP2 = new GooglePoint();
    //        GP2.ID = "SimplePushpin";
    //        GP2.Latitude = lat;
    //        GP2.Longitude = lon;
    //        GP2.InfoHTML = string.Format(
    //    @"<p class=""text-dark""><b>{0}</b></p>
    //<a href=""{1}"" class=""text-primary"" target=""_blank"">{2}</a>",
    //    Config.Title,
    //    Config.SiteAdresi, Config.SiteAdresi);
    //        GP2.Draggable = false;
    //        GP2.ToolTip = Config.Title;
    //        GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP2);
    //    }
    protected void btnYorumKaydet_Click(object sender, EventArgs e)
    {
        var adSoyad = txtIletisimAdSoyad.Text.ToTemizMetin();
        var ePosta = txtIletisimEposta.Text.ToTemizMetin();
        var aciklama = txtIletisimAciklama.Text.ToTemizMetin();
        var ip = Config.Statistics.IP;

        try
        {
            using (var db = new WhiteWorldEntities())
            {
                iletisim yeniKayit = new iletisim
                {
                    Aciklama = aciklama,
                    AdSoyad = adSoyad,
                    Cevaplandi = false,
                    DilKod = DilKod,
                    Eposta = ePosta,
                    Ip = ip,
                    Tarih = DateTime.Now
                };
                db.iletisim.Add(yeniKayit);
                db.SaveChanges();
                pnlIletisim.Enabled = false;
                string mesaj = (DilKod == 1) ? "İletişim kaydınız başarıyla oluşturuldu! </br> En kısa sürede dönüş sağlanacaktır!" : "";
                MessageBox.Show(mesaj, MessageBox.MesajTipleri.Success);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
}
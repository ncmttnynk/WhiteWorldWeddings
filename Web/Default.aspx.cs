using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhiteWorld.DAL;
using DA.MetaTagGenerator;

public partial class _Default : IPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MetaTagGenerator gen = MetaTagGenerator.CreateInstance(this);
            gen.MetaTags = new Tags.MetaTagsInfo
            {
                Description = "Osmanbey'de bulunan mağazamızın gelinliklerini sergilediğimiz sayfamız!",
                Keywords = "gelinlik, osmanbey, özel dikim, abiye, düğün, osmanbeyde gelinlik, işlemeli, dantelli",
                Language = Dil,
                Title = "White World | 2018'in En Moda Gelinlik Dünyası!",
                Topic = "Gelinlik, abiye, tasarım, özel, dikim, özel dikim, kişiye özel",
            };
            gen.GenerateTags();
            base.MP_Main.Title = (DilKod == 1) ? "Anasayfa" : "Home";
            using (var db = new WhiteWorldEntities())
            {

                //var anasayfaGelinlik = (from x in db.gelinlikler
                //                        where x.DilKod == DilKod && x.Goster && x.Anasayfa
                //                        orderby MyDbFunction.Rastgele()
                //                        select x).FirstOrDefault();
                //if (anasayfaGelinlik != null)
                //{
                //    pnlTekUrun.Visible = true;
                //    GelinlikDB gDB = new GelinlikDB();


                //    ltlRenk.Text = gDB.Renk(anasayfaGelinlik.Id);
                //    ltlSiluet.Text = gDB.Siluet(anasayfaGelinlik.Id);
                //    ltlYakaTipi.Text = gDB.YakaTipi(anasayfaGelinlik.Id);
                //    ltlKumas.Text = gDB.Kumas(anasayfaGelinlik.Id);
                //    //ltlBeden.Text = anasayfaGelinlik.Beden.ToString();

                //    ltlGelinlikAciklama.Text = anasayfaGelinlik.Aciklama;
                //    hlGelinlik.Text = anasayfaGelinlik.Baslik;
                //    hlGelinlik.NavigateUrl = hlGelinlikFotograf.NavigateUrl = string.Format("/{0}/GelinlikDetay/{1}/{2},product", Dil, anasayfaGelinlik.Id.ToString(), anasayfaGelinlik.Baslik.ToString().ToURL());

                //    var fotograf = db.gelinlikfotograflari.FirstOrDefault(x => x.GelinlikId == anasayfaGelinlik.Id && x.Anasayfa);
                //    if (fotograf != null)
                //    {
                //        imgGelinlik.ImageUrl = fotograf.FotoBuyuk;
                //        imgGelinlik.AlternateText = fotograf.Etiket;
                //    }
                //}
            }
        }
    }

    protected void lbMail_Click(object sender, EventArgs e)
    {
        string mail = txtMail.Text;
        if (!string.IsNullOrEmpty(mail))
        {
            using (var db = new WhiteWorldEntities())
            {
                var mailIletisim = new iletisim();
                mailIletisim.AdSoyad = mail;
                mailIletisim.Aciklama = mail;
                mailIletisim.Eposta = mail;
                mailIletisim.DilKod = this.DilKod;
                mailIletisim.Tarih = DateTime.Now;
                mailIletisim.Ip = Config.Statistics.IP;
                mailIletisim.Cevaplandi = false;
                db.iletisim.Add(mailIletisim);
                db.SaveChanges();
                lbMail.Enabled = false;
                txtMail.Enabled = false;
                ltlMessage.Text = "Kadınız başarıyla alındı!";
                ltlMessage.Visible = true;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhiteWorld.DAL;
using WhiteWorld.Info;

public partial class admin_Gelinlikler : IPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var mp = this.Master as IMP_Main;
        mp.H1 = "Gelinlikler";
        mp.Title = "Gelinlikler";
        mp.Buradasiniz = new List<BuradasinizInfo>
                {
                    new BuradasinizInfo {Title="Anasayfa", Url="../admin/Anasayfa.aspx"},
                    new BuradasinizInfo {Title="Gelinlikler"}
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
            var kayitlar = (from x in db.gelinlikler
                            //join k in db.kategoriler on x.KategoriId equals k.Id
                            where x.DilKod == DilKod
                            orderby x.Oncelik
                            select new GelinlikInfo
                            {
                                Id = x.Id,
                                Baslik = x.Baslik,
                                //Kategori = k.Baslik,
                                YeniSezon = x.YeniSezon,
                                OzelUrun = x.OzelUrun,
                                EnCokSatan = x.EnCokSatan,
                                Yeni = x.Yeni,
                                Oncelik = x.Oncelik,
                                Goster = x.Goster,
                                DilKod = x.DilKod
                            });
            var toplam = kayitlar.Count();
            UC_Sayfalama1.Toplam = toplam;
            UC_Sayfalama1.Adet = 5;
            UC_Sayfalama1.Sayfala();
            UC_Sayfalama2.Toplam = toplam;
            UC_Sayfalama2.Adet = 5;
            UC_Sayfalama2.Sayfala();

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
            DosyaDB dosyaDB = new DosyaDB();
            using (var db = new WhiteWorldEntities())
            {
                var kayit = db.gelinlikler.FirstOrDefault(x => x.Id == id);
                var kayitFotograflari = db.gelinlikfotograflari.Where(x => x.GelinlikId == id);
                foreach (var f in kayitFotograflari)
                {
                    db.gelinlikfotograflari.Remove(f);
                    dosyaDB.ResimSil(f.FotoBuyuk);
                    dosyaDB.ResimSil(f.FotoKucuk);
                }
                db.gelinlikler.Remove(kayit);
                db.SaveChanges();
                MessageBox.Show("Gelinlik başarıyla silindi!", MessageBox.MesajTipleri.Success, true, 1500);
                KayitlariGetir();
            }
        }
    }

    protected void gvKayitlar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
            var kayit = e.Row.DataItem as GelinlikInfo;
            Literal ltlDil = e.Row.FindControl("ltlDil") as Literal;
            Literal ltlGoster = e.Row.FindControl("ltlGoster") as Literal;
            Literal ltlYeniSezon = e.Row.FindControl("ltlYeniSezon") as Literal;
            Literal ltlOzelUrun= e.Row.FindControl("ltlOzelUrun") as Literal;
            Literal ltlEnCokSatan= e.Row.FindControl("ltlEnCokSatan") as Literal;
            Literal ltlYeni = e.Row.FindControl("ltlYeni") as Literal;

            ltlYeniSezon.Text = (kayit.YeniSezon) ? "<i class='far fa-thumbs-up'></i>" : "<i class='far fa-thumbs-down'></i>";
            ltlOzelUrun.Text = (kayit.OzelUrun) ? "<i class='far fa-thumbs-up'></i>" : "<i class='far fa-thumbs-down'></i>";
            ltlEnCokSatan.Text = (kayit.EnCokSatan) ? "<i class='far fa-thumbs-up'></i>" : "<i class='far fa-thumbs-down'></i>";
            ltlYeni.Text = (kayit.Yeni) ? "<i class='far fa-thumbs-up'></i>" : "<i class='far fa-thumbs-down'></i>";
            ltlGoster.Text = (kayit.Goster) ? "<i class='far fa-thumbs-up'></i>" : "<i class='far fa-thumbs-down'></i>";
            ltlDil.Text = (kayit.DilKod == 1) ? @"<img src=""/admin/img/blank.gif"" class=""flag flag-tr"" alt=""Türkçe"">" : @"<img src=""/admin/img/blank.gif"" class=""flag flag-us"" alt=""English"">";
        }
    }

    protected void btnYeni_Click(object sender, EventArgs e)
    {
        Session["Path"] = Session["FileName"] = "";
        Response.Redirect("/admin/GelinlikDetay.aspx?Id=0", true);
    }
}
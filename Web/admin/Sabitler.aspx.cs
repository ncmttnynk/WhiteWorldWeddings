using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhiteWorld.DAL;
using WhiteWorld.Info;

public partial class admin_Sabitler : IPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var mp = this.Master as IMP_Main;
        mp.H1 = "Sabitler";
        mp.Title = "Sabitler";
        mp.Buradasiniz = new List<BuradasinizInfo>
                {
                    new BuradasinizInfo {Title="Anasayfa", Url="../admin/Anasayfa.aspx"},
                    new BuradasinizInfo {Title="Sabitler"}
                };
        if (!IsPostBack)
        {
            using (var db = new WhiteWorldEntities())
            {
                ddlSabitler.DataSource = (from x in db.sabitler
                                          where x.DilKod == DilKod || x.DilKod == 0
                                          orderby x.Id
                                          select x).ToList();
                ddlSabitler.DataTextField = "Baslik";
                ddlSabitler.DataValueField = "Id";
                ddlSabitler.DataBind();
                ddlSabitler.Items.Insert(0, new ListItem("Seçiniz...", "0"));
            }
        }
    }

    protected void ddlSabitler_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSabitler.SelectedIndex < 0)
        {
            pnlEditor.Visible = false;
            return;
        }
        pnlEditor.Visible = true;
        using (var db = new WhiteWorldEntities())
        {
            string kod = ddlSabitler.SelectedItem.Text;
            var s = db.sabitler.FirstOrDefault(x => x.Baslik == kod && (x.DilKod == DilKod || x.DilKod == 0));
            ckIcerik.Text = s.Icerik;
        }
    }

    protected void btnKaydet_Click(object sender, EventArgs e)
    {
        if (ddlSabitler.SelectedIndex < 0)
        {
            pnlEditor.Visible = false;
            return;
        }
        pnlEditor.Visible = true;
        using (var db = new WhiteWorldEntities())
        {
            string kod = ddlSabitler.SelectedItem.Text;
            db.sabitler.FirstOrDefault(x => x.Baslik == kod && (x.DilKod == DilKod || x.DilKod == 0)).Icerik = ckIcerik.Text;
            db.SaveChanges();
        }
        MessageBox.Show("Sabit başarıyla güncellendi! </br> Cache'i sıfırlamayı unutmayınız.", MessageBox.MesajTipleri.Success);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhiteWorld.Info;

public partial class usercontrols_UC_UrunListesi_Ozel : IUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GelinlikDB gDB = new GelinlikDB();
            var ozelUrun = gDB.OzelUrunGetir(DilKod);
            if (ozelUrun.Count > 0)
            {
                rptOzelUrunler.DataSource = gDB.OzelUrunGetir(DilKod);
                rptOzelUrunler.DataBind();

                divGelinlikler.Visible = true;
            }
        }
    }

    protected void rptOzelUrunler_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            var kayit = e.Item.DataItem as GelinlikInfo;
            Literal ltlOzelUrunYeni = e.Item.FindControl("ltlOzelUrunYeni") as Literal;
            if (kayit.Yeni && DilKod == 1)
            {
                ltlOzelUrunYeni.Text = @"<span class=""new"">YENİ</span>";
            }
            else if (kayit.Yeni && DilKod == 2)
            {
                ltlOzelUrunYeni.Text = @"<span class=""new"">NEW</span>";
            }
        }
    }
}
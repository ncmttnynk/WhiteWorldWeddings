using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhiteWorld.Info;

public partial class usercontrols_UC_UrunListesi_EnCokSatan : IUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GelinlikDB gDB = new GelinlikDB();
            var enCokSatan = gDB.EnCokSatanGetir(DilKod);
            if (enCokSatan.Count > 0)
            {
                rptEnCokSatan.DataSource = gDB.EnCokSatanGetir(DilKod);
                rptEnCokSatan.DataBind();

                divGelinlikler.Visible = true;
            }
        }
    }

    protected void rptEnCokSatan_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            var kayit = e.Item.DataItem as GelinlikInfo;
            Literal ltlEnCokSatan = e.Item.FindControl("ltlEnCokSatan") as Literal;
            if (kayit.Yeni && DilKod == 1)
            {
                ltlEnCokSatan.Text = @"<span class=""new"">YENİ</span>";
            }
            else if (kayit.Yeni && DilKod == 2)
            {
                ltlEnCokSatan.Text = @"<span class=""new"">NEW</span>";
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhiteWorld.Info;

public partial class usercontrols_UC_UrunListesi_YeniSezon : IUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GelinlikDB gDB = new GelinlikDB();
            var yeniSezon = gDB.YeniSezonGetir(DilKod);
            if (yeniSezon.Count > 0 )
            {
                rptYeniSezon.DataSource = gDB.YeniSezonGetir(DilKod);
                rptYeniSezon.DataBind();

                divGelinlikler.Visible = true;
            }
        }
    }

    protected void rptYeniSezon_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            var kayit = e.Item.DataItem as GelinlikInfo;
            Literal ltlYeniSezonYeni = e.Item.FindControl("ltlYeniSezonYeni") as Literal;
            if (kayit.Yeni && DilKod == 1)
            {
                ltlYeniSezonYeni.Text = @"<span class=""new"">YENİ</span>";
            }
            else if (kayit.Yeni && DilKod == 2)
            {
                ltlYeniSezonYeni.Text = @"<span class=""new"">NEW</span>";
            }
        }
    }
}
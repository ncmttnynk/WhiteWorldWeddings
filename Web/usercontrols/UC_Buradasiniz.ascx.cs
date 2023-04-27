using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhiteWorld.Info;

public partial class usercontrols_UC_Buradasiniz : System.Web.UI.UserControl
{
    public List<BuradasinizInfo> Buradasiniz
    {
        set
        {
            rptUl.DataSource = value;
            rptUl.DataBind();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void rptUl_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            var bilgi = e.Item.DataItem as BuradasinizInfo;
            var lbl = e.Item.FindControl("lbl") as Literal;
            var hl = e.Item.FindControl("hl") as HyperLink;
            if (bilgi.Url.IsNullOrEmpty())
            {
                lbl.Text = bilgi.Title;
                lbl.Visible = true;
            }
            else
            {
                hl.Text = bilgi.Title;
                hl.NavigateUrl = bilgi.Url;
                hl.Visible = true;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Language : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["dil"] == null)
        {
            Response.End();
            return;
        }
        string qs = Request.QueryString[0].ToString().ToTemizMetin();

        string lang = "tr";
        try
        {
            if (Request.QueryString["dil"].ToString() == "Sil")
            {
                Session.Abandon();
            }
            else
            {
                if (qs.Equals("tr"))
                    Session["Dil"] = "tr";
                else if (qs.Equals("ar"))
                    Session["Dil"] = "ar";
                else
                    Session["Dil"] = "tr";
            }
            lang = Session["Dil"].ToString();
            if (Request.QueryString["Url"] != null)
            {
                string url = Request.QueryString["Url"].ToString().ToTemizMetin();
                Response.Redirect(url, true);
            }
            else
                Response.Redirect("/" );
        }
        catch (Exception)
        {


        }
    }
}
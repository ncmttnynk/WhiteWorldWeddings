using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ckeditor_plugins_simogeo_Browser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Klasor"] != null)

                litScript.Text =
                    string.Format(@"<script type=""text/javascript"" src=""scripts/filemanager-config.ashx?Klasor={0}""></script>",
                    Request.QueryString["Klasor"].ToString());
            else
                litScript.Text =
                  @"<script type=""text/javascript"" src=""scripts/filemanager-config.ashx?Klasor=/upload/""></script>";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MP_TekSutun : IMP_Child
{
    public override string Baslik
    {
        set
        {
            //lblH2.Text = value;
            //h2.Visible = true;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}

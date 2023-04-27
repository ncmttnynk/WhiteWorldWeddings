using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhiteWorld.Info;

public partial class admin_Anasayfa : IPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var mp = this.Master as IMP_Main;
        mp.Title = "Anasayfa";
        mp.H1 = "Anasayfa";
        mp.Buradasiniz = new List<BuradasinizInfo>
        {
            new BuradasinizInfo{Title="Anasayfa"}
        };
    }
}
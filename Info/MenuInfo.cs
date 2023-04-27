using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteWorld.Info
{
    public class MenuInfo
    {
        public int Id { get; set; }
        public int UstMenuId { get; set; }
        public string Baslik { get; set; }
        public string Url { get; set; }
        public int Oncelik { get; set; }
        public bool Goster { get; set; }
        public int DilKod { get; set; }
    }
}

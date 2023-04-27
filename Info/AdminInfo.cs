using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteWorld.Info
{
    public class AdminInfo
    {
        public int Id { get; set; }
        public string Kod { get; set; }
        public string AdSoyad { get; set; }
        public string Sifre { get; set; }
        public DateTime SonGiris { get; set; }
        public bool Hiper { get; set; }
    }
}

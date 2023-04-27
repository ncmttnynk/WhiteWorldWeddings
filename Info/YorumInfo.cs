using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteWorld.Info
{
    public class YorumInfo
    {
        public int Id { get; set; }
        public int UstYorumId { get; set; }
        public int GelinlikId { get; set; }
        public string GelinlikBaslik { get; set; }
        public string AdSoyad { get; set; }
        public string Eposta { get; set; }
        public string Aciklama { get; set; }
        public DateTime Tarih { get; set; }
        public string Ip { get; set; }
        public bool Goster { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteWorld.Info
{
    public class CMSInfo
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Kod { get; set; }
        public int BaslikId { get; set; }
        public int AnaBaslikId { get; set; }
        public string Ayrinti { get; set; }
        public int OkunmaSayisi { get; set; }
        public int AdminId { get; set; }
        public DateTime KayitTarihi { get; set; }
        public int Oncelik { get; set; }
        public bool Goster { get; set; }
        public int DilKod { get; set; }
    }
}

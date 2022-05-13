using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeBerlin.Data
{
    public class SiparisDetay
    {
        public string UrunAd { get; set; }

        public decimal BirimFiyat { get; set; }

        public int Adet { get; set; }

        public string TutarTL => $"{Tutar():c2}";

        public decimal Tutar()
        {
            return BirimFiyat * Adet;
        }
    }
}

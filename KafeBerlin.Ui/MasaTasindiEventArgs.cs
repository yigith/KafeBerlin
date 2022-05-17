﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeBerlin.Ui
{
    public class MasaTasindiEventArgs : EventArgs
    {
        public int EskiMasaNo { get; }
        public int YeniMasaNo { get; }

        public MasaTasindiEventArgs(int eskiMasaNo, int yeniMasaNo)
        {
            EskiMasaNo = eskiMasaNo;
            YeniMasaNo = yeniMasaNo;
        }
    }
}

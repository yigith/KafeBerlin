﻿using KafeBerlin.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KafeBerlin.Ui
{
    public partial class GecmisSiparislerForm : Form
    {
        private readonly KafeVeri _db;

        public GecmisSiparislerForm(KafeVeri db)
        {
            _db = db;
            InitializeComponent();
            dgvSiparisler.DataSource = _db.GecmisSiparisler;
        }

        private void dgvSiparisler_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSiparisler.SelectedRows.Count == 0)
            {
                dgvDetaylar.DataSource = null;
            }
            else
            {
                var siparis = (Siparis)dgvSiparisler.SelectedRows[0].DataBoundItem;
                dgvDetaylar.DataSource = siparis.SiparisDetaylar;
            }
        }
    }
}

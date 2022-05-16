using KafeBerlin.Data;
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
    public partial class SiparisForm : Form
    {
        private readonly KafeVeri _db;
        private readonly Siparis _siparis;
        BindingList<SiparisDetay> _blSiparisDetaylar;

        public SiparisForm(KafeVeri db, Siparis siparis)
        {
            _db = db;
            _siparis = siparis;
            InitializeComponent();
            dgvDetaylar.AutoGenerateColumns = false;
            MasaNoGuncelle();
            OdemeTutariGuncelle();
            UrunleriListele();
            DetaylariListele();
        }

        private void DetaylariListele()
        {
            _blSiparisDetaylar = new BindingList<SiparisDetay>(_siparis.SiparisDetaylar);
            _blSiparisDetaylar.ListChanged += _blSiparisDetaylar_ListChanged;
            dgvDetaylar.DataSource = _blSiparisDetaylar;
        }

        private void _blSiparisDetaylar_ListChanged(object sender, ListChangedEventArgs e)
        {
            OdemeTutariGuncelle();
        }

        private void OdemeTutariGuncelle()
        {
            lblOdemeTutari.Text = _siparis.ToplamTutarTL;
        }

        private void UrunleriListele()
        {
            cboUrun.DataSource = _db.Urunler;
        }

        private void MasaNoGuncelle()
        {
            Text = $"Masa {_siparis.MasaNo}";
            lblMasaNo.Text = _siparis.MasaNo.ToString("00");
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (cboUrun.SelectedItem == null)
            {
                MessageBox.Show("Önce bir ürün seçiniz.");
                return;
            }

            Urun urun = (Urun)cboUrun.SelectedItem;

            _blSiparisDetaylar.Add(new SiparisDetay()
            {
                Adet = (int)nudAdet.Value,
                UrunAd = urun.UrunAd,
                BirimFiyat = urun.BirimFiyat
            });

            nudAdet.Value = 1;
        }

        private void btnAnasayfayaDon_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSiparisIptal_Click(object sender, EventArgs e)
        {
            SiparisKapat(0, SiparisDurum.Iptal);
        }

        private void btnOdemeAl_Click(object sender, EventArgs e)
        {
            SiparisKapat(_siparis.ToplamTutar(), SiparisDurum.Odendi);
        }

        void SiparisKapat(decimal odenenTutar, SiparisDurum durum)
        {
            string eylem = durum == SiparisDurum.Iptal ? "iptal edilecektir" : "kapatılacaktır";
            string baslik = durum == SiparisDurum.Iptal ? "İptal" : "Kapatma";

            DialogResult dr = MessageBox.Show(
                $"{_siparis.MasaNo} nolu masanın siparişi {eylem}. Emin misiniz?", 
                $"Sipariş {baslik} Onayı", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (dr == DialogResult.Yes)
            {
                _siparis.OdenenTutar = odenenTutar;
                _siparis.Durum = durum;
                _siparis.KapanisZamani = DateTime.Now;
                _db.AktifSiparisler.Remove(_siparis);
                _db.GecmisSiparisler.Add(_siparis);
                DialogResult = DialogResult.OK;
            }

        }
    }
}

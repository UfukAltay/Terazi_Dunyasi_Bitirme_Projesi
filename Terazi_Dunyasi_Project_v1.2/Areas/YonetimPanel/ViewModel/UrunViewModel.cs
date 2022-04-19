using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Terazi_Dunyasi_Project_v12.Areas.YonetimPanel.ViewModel
{
    public class UrunViewModel
    {
        public UrunViewModel()
        {
            this.AltKategoriListesi = new List<SelectListItem>();
            AltKategoriListesi.Insert(0, new SelectListItem { Selected = true, Text = "Alt Kategori Seçiniz", Value = "" });
        }

        public int KategoriID { get; set; }

        public int AltKategoriID { get; set; }

        public string UrunAdi { get; set; }

        public int UrunFiyati { get; set; }

        public int StokDurumu { get; set; }

        public string UrunAciklama { get; set; }

        public string FotoYol { get; set; }

        public List<SelectListItem> KategoriListesi { get; set; }

        public List<SelectListItem> AltKategoriListesi { get; set; }
    }
}
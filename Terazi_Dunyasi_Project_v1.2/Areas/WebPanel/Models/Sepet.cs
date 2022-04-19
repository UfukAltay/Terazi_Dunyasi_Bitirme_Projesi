using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Terazi_Dunyasi_Project_v12.Areas.WebPanel.Models
{
    public class Sepet
    {
        public int UrunID { get; set; }
        public int Adet { get; set; }

        public int Ekle()
        {

            List<Sepet> sepetList = (List<Sepet>)HttpContext.Current.Session["Sepet"];

            // sepet varmı?
            if (sepetList == null)
            {
                // yoksa sepet oluştur
                sepetList = new List<Sepet>();
            }


            //  gelen ürünıd sepette varmı?


            Sepet sep = sepetList.FirstOrDefault(c => c.UrunID == this.UrunID);

            // eğer sepet'te ürünId varsa 1 arttır
            if (sep != null)
            {
                sep.Adet++;
            }
            else
            {
                sep = new Sepet();
                sep.UrunID = this.UrunID;
                sep.Adet = 1;
                sepetList.Add(sep);
            }


            // session'a sepeti ekle
            HttpContext.Current.Session["Sepet"] = sepetList;

            int Count = sepetList.Sum(c => c.Adet);

            return Count;
        }

        public int Guncelle()
        {
            List<Sepet> sepetList = (List<Sepet>)HttpContext.Current.Session["Sepet"];

            Sepet sep = sepetList.FirstOrDefault(c => c.UrunID == this.UrunID);
            sep.Adet = this.Adet;

            int Count = sepetList.Sum(c => c.Adet);
            return Count;

        }

        public int Sil()
        {
            List<Sepet> sepetList = (List<Sepet>)HttpContext.Current.Session["Sepet"];

            Sepet sep = sepetList.FirstOrDefault(c => c.UrunID == this.UrunID);
            sepetList.Remove(sep);

            HttpContext.Current.Session["Sepet"] = sepetList;
            return 0;

        }
        public List<Sepet> sepetList()
        {
            List<Sepet> sepetList = (List<Sepet>)HttpContext.Current.Session["Sepet"];

            // sepet varmı?
            if (sepetList == null)
            {
                // yoksa sepet oluştur
                sepetList = new List<Sepet>();
            }

            return sepetList;
        }

    }
}
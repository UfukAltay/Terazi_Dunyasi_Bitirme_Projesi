//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Terazi_Dunyasi_Project_v12
{
    using System;
    using System.Collections.Generic;
    
    public partial class Urunler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Urunler()
        {
            this.SiparisDetays = new HashSet<SiparisDetay>();
        }
    
        public int UrunID { get; set; }
        public int KategoriID { get; set; }
        public int AltKategoriID { get; set; }
        public string UrunAdi { get; set; }
        public Nullable<int> UrunFiyati { get; set; }
        public Nullable<int> StokDurumu { get; set; }
        public string UrunAciklama { get; set; }
        public string FotoYol { get; set; }
    
        public virtual AltKategoriler AltKategoriler { get; set; }
        public virtual Kategoriler Kategoriler { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SiparisDetay> SiparisDetays { get; set; }
    }
}
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
    
    public partial class AltKategoriler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AltKategoriler()
        {
            this.Urunlers = new HashSet<Urunler>();
        }
    
        public int AltKategoriID { get; set; }
        public int KategoriID { get; set; }
        public string AltKategoriAdi { get; set; }
        public string AltKategoriAciklama { get; set; }
        public string FotoYol { get; set; }
    
        public virtual Kategoriler Kategoriler { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Urunler> Urunlers { get; set; }
    }
}

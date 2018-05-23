namespace MalzemeTakipMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ENV_MALZEME
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ENV_MALZEME()
        {
            ENV_MALZEMEHAREKET = new HashSet<ENV_MALZEMEHAREKET>();
            ENV_MALZEMEPERSONEL = new HashSet<ENV_MALZEMEPERSONEL>();
        }


        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Tur { get; set; }

        [StringLength(30)]
        public string AltTur { get; set; }

        [Required]
        [StringLength(100)]
        public string Adi { get; set; }

        [Required]
        [StringLength(50)]
        public string SeriNo { get; set; }

        [StringLength(50)]
        public string Ozellik1 { get; set; }

        [StringLength(50)]
        public string Ozellik2 { get; set; }

        [StringLength(50)]
        public string Ozellik3 { get; set; }

        [StringLength(50)]
        public string Ozellik4 { get; set; }

        [StringLength(50)]
        public string Ozellik5 { get; set; }

        public DateTime IslemTarihi { get; set; }

        public bool AktifPasif { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ENV_MALZEMEHAREKET> ENV_MALZEMEHAREKET { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ENV_MALZEMEPERSONEL> ENV_MALZEMEPERSONEL { get; set; }
    }

  
}

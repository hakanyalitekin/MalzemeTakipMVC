namespace MalzemeTakipMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ENV_PERSONEL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ENV_PERSONEL()
        {
            ENV_MALZEMEHAREKET = new HashSet<ENV_MALZEMEHAREKET>();
            ENV_MALZEMEHAREKET1 = new HashSet<ENV_MALZEMEHAREKET>();
            ENV_MALZEMEPERSONEL = new HashSet<ENV_MALZEMEPERSONEL>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string AdiSoyadi { get; set; }

        public int DepartmanId { get; set; }

        public bool AktifPasif { get; set; }

        public DateTime IslemTarihi { get; set; }

        public virtual ENV_DEPARTMAN ENV_DEPARTMAN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ENV_MALZEMEHAREKET> ENV_MALZEMEHAREKET { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ENV_MALZEMEHAREKET> ENV_MALZEMEHAREKET1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ENV_MALZEMEPERSONEL> ENV_MALZEMEPERSONEL { get; set; }
    }
}

namespace MalzemeTakipMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WEB_PORTALPARAMETRE
    {
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ParametreAdi { get; set; }

        [Required]
        [StringLength(50)]
        public string ParametreKodu { get; set; }

        [Required]
        [StringLength(50)]
        public string BirinciDegeri { get; set; }

        [StringLength(50)]
        public string IkinciDeger { get; set; }

        [StringLength(50)]
        public string UcuncuDeger { get; set; }

        [StringLength(150)]
        public string Aciklama { get; set; }

        public DateTime GecerlilikBaslangicTarihi { get; set; }

        public DateTime GecerlilikBitisTarihi { get; set; }

        [Column(TypeName = "numeric")]
        public decimal IslemKullaniciId { get; set; }

        public DateTime IslemTarihi { get; set; }
    }
}

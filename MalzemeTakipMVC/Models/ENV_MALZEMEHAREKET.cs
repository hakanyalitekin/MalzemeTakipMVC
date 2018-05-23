namespace MalzemeTakipMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ENV_MALZEMEHAREKET
    {
        public int Id { get; set; }

        public int MalzemeId { get; set; }

        public int KaynakPersonelId { get; set; }

        public int HedefPersonalId { get; set; }

        public int Adet { get; set; }

        [StringLength(250)]
        public string Acıklama { get; set; }

        public DateTime IslemTarihi { get; set; }

        public virtual ENV_MALZEME ENV_MALZEME { get; set; }

        public virtual ENV_PERSONEL ENV_PERSONEL { get; set; }

        public virtual ENV_PERSONEL ENV_PERSONEL1 { get; set; }
    }
}

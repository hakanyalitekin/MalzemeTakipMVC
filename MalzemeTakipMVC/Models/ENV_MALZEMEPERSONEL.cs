namespace MalzemeTakipMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ENV_MALZEMEPERSONEL
    {
        public int Id { get; set; }

        public int MalzemeId { get; set; }

        public int PersonelId { get; set; }

        public int Adet { get; set; }

        public DateTime IslemTarihi { get; set; }

        public virtual ENV_MALZEME ENV_MALZEME { get; set; }

        public virtual ENV_PERSONEL ENV_PERSONEL { get; set; }
    }
}

namespace MalzemeTakipMVC.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MalzemeTakipDB : DbContext
    {
        public MalzemeTakipDB()
            : base("name=MalzemeTakipDB")
        {
        }

        public virtual DbSet<ENV_DEPARTMAN> ENV_DEPARTMAN { get; set; }
        public virtual DbSet<ENV_MALZEME> ENV_MALZEME { get; set; }
        public virtual DbSet<ENV_MALZEMEHAREKET> ENV_MALZEMEHAREKET { get; set; }
        public virtual DbSet<ENV_MALZEMEPERSONEL> ENV_MALZEMEPERSONEL { get; set; }
        public virtual DbSet<ENV_PERSONEL> ENV_PERSONEL { get; set; }
        public virtual DbSet<WEB_PORTALPARAMETRE> WEB_PORTALPARAMETRE { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ENV_DEPARTMAN>()
                .HasMany(e => e.ENV_PERSONEL)
                .WithRequired(e => e.ENV_DEPARTMAN)
                .HasForeignKey(e => e.DepartmanId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ENV_MALZEME>()
                .Property(e => e.Tur)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ENV_MALZEME>()
                .HasMany(e => e.ENV_MALZEMEHAREKET)
                .WithRequired(e => e.ENV_MALZEME)
                .HasForeignKey(e => e.MalzemeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ENV_MALZEME>()
                .HasMany(e => e.ENV_MALZEMEPERSONEL)
                .WithRequired(e => e.ENV_MALZEME)
                .HasForeignKey(e => e.MalzemeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ENV_PERSONEL>()
                .HasMany(e => e.ENV_MALZEMEHAREKET)
                .WithRequired(e => e.ENV_PERSONEL)
                .HasForeignKey(e => e.HedefPersonalId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ENV_PERSONEL>()
                .HasMany(e => e.ENV_MALZEMEHAREKET1)
                .WithRequired(e => e.ENV_PERSONEL1)
                .HasForeignKey(e => e.KaynakPersonelId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ENV_PERSONEL>()
                .HasMany(e => e.ENV_MALZEMEPERSONEL)
                .WithRequired(e => e.ENV_PERSONEL)
                .HasForeignKey(e => e.PersonelId)
                .WillCascadeOnDelete(false);
        }
    }
}

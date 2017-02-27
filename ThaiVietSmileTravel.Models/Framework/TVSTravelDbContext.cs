namespace ThaiVietSmileTravel.Models.Framework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TVSTravelDbContext : DbContext
    {
        public TVSTravelDbContext()
            : base("name=TVSTravelDbContext")
        {
        }

        public virtual DbSet<tbl_About> tbl_About { get; set; }
        public virtual DbSet<tbl_Administrator> tbl_Administrator { get; set; }
        public virtual DbSet<tbl_Categories> tbl_Categories { get; set; }
        public virtual DbSet<tbl_Contact> tbl_Contact { get; set; }
        public virtual DbSet<tbl_Customers> tbl_Customers { get; set; }
        public virtual DbSet<tbl_Language> tbl_Language { get; set; }
        public virtual DbSet<tbl_Menu> tbl_Menu { get; set; }
        public virtual DbSet<tbl_OrderDetail> tbl_OrderDetail { get; set; }
        public virtual DbSet<tbl_Orders> tbl_Orders { get; set; }
        public virtual DbSet<tbl_Tour> tbl_Tour { get; set; }
        public virtual DbSet<tbl_Banner> tbl_Banner { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_Categories>()
                .Property(e => e.LanguageID)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Language>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Menu>()
                .Property(e => e.LanguageID)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_OrderDetail>()
                .Property(e => e.DonGia)
                .HasPrecision(18, 0);
        }
    }
}

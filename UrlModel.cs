namespace ShortenUrlWebApplication
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public class UrlModel : DbContext
    {
        public UrlModel() : base("name=UrlModel")
        {
        }
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
       

        public virtual DbSet<UrlInfo> UrlInfo { get; set; }
    }

    public class UrlInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string ShortName { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }
    }

}
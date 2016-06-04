using AlkusStore.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AlkusStore.Models.Configuration
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            Property(x => x.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasMaxLength(100);
            Property(x => x.Category).HasMaxLength(100);
            Property(x => x.Description).HasMaxLength(500).IsOptional();
            Property(x => x.Price).IsRequired();
            Property(x => x.Stock).IsRequired();
        }
    }
}

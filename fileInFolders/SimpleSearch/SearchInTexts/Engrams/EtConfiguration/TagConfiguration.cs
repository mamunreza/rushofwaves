namespace Engrams.EtConfiguration
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using Engrams.Models;

    public class TagConfiguration : EntityTypeConfiguration<Tag>
    {
        public TagConfiguration()
        {
            this.Property(a => a.Name).HasMaxLength(50).IsRequired();
            this.Property(a => a.Description).HasMaxLength(255);
        }
    }
}
namespace Engrams.EtConfiguration
{
    using System.Data.Entity.ModelConfiguration;

    using Engrams.Models;

    public class LinkConfiguration : EntityTypeConfiguration<Link>
    {
        public LinkConfiguration()
        {
            this.Property(a => a.LinkText).HasMaxLength(500).IsRequired();
            this.HasRequired(c => c.Memory).WithMany().HasForeignKey(c => c.MemoryId).WillCascadeOnDelete(false);
        }
    }
}
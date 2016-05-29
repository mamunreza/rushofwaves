namespace Engrams.EtConfiguration
{
    using System.Data.Entity.ModelConfiguration;

    using Engrams.Models;

    public class MemoryConfiguration : EntityTypeConfiguration<Memory>
    {
        public MemoryConfiguration()
        {
            this.Property(a => a.Title).HasMaxLength(255);
            this.Property(a => a.Note).HasMaxLength(255).IsRequired();
            this.HasRequired(c => c.Tag).WithMany().HasForeignKey(c => c.TagId).WillCascadeOnDelete(false);

            this.HasOptional(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .WillCascadeOnDelete(false);
        }
    }
}
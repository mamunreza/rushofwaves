namespace Engrams.Models
{
    using System.Collections.Generic;

    public class Memory
    {
        public int MemoryId { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int? ParentId { get; set; }
        public virtual Memory Parent { get; set; }
        public virtual ICollection<Memory> Children { get; set; }
    }
}
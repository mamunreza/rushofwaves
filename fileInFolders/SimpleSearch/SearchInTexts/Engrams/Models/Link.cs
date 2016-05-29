namespace Engrams.Models
{
    public class Link
    {
        public int LinkId { get; set; }
        public string LinkText { get; set; }
        public int MemoryId { get; set; }
        public Memory Memory { get; set; }
    }
}
namespace LogTailer.Domain.Models
{
    using System.Drawing;
    using Base;

    public class LineHighlight : IEntity
    {
        public int Id { get; set; }

        public string SearchString { get; set; }

        public Color ForegroundColor { get; set; }

        public Color BackgroundColor { get; set; }

        public bool IsRegex { get; set; }

        public bool IsActive { get; set; }
    }

}

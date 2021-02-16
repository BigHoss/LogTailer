namespace LogTailer.Domain.Models
{
    using Base;

    public class OpenFile : IEntity
    {
        public int Id { get; set; }

        public string FilePath { get; set; }
    }
}

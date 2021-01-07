

namespace LogTailer.Ui.Models
{
    using System;
    public class FileLine
    {
        public FileLine(DateTime timestamp,
                        string text)
        {
            Timestamp = timestamp;
            Text = text;
        }

        public DateTime Timestamp { get; set; }
        public string Text { get; set; }
    }
}

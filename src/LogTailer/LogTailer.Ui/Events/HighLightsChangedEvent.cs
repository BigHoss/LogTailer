namespace LogTailer.Ui.Events
{
    using System.Collections.Generic;
    using Domain.Models;

    public class HighLightsChangedEvent
    {
        public List<LineHighlight> Highlights { get; set; } = new List<LineHighlight>();
    }
}

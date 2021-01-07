
// ReSharper disable ClassNeverInstantiated.Global
namespace LogTailer.Domain.Models
{
    using System;
    using System.Collections.Generic;
    using Base;

    public class Session : IEntity
    {
        public int Id { get; set; }

        public bool RibbonCollapsed { get; set; }

        public int ThemeIndex{ get; set; }

        public List<OpenFile> OpenFiles { get; set; } = new List<OpenFile>();
    }
}

namespace LogTailer.Ui.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Base;
    using Domain.Models;

    public interface ISessionStateService : IService, IDisposable
    {
        IEnumerable<OpenFile> ReadOpenFiles();
        Task UpdateOpenFiles(IEnumerable<OpenFile> files);

        Task<OpenFile> CreateOpenFile(string filePath);
    }
}

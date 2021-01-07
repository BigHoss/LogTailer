namespace LogTailer.Services
{
    using System;
    using System.Threading.Tasks;
    using Base;
    using Domain.Models;

    public interface ISessionStateService : IService, IDisposable
    {
        Session ReadSession();
        Task UpdateSession(Session session, bool writeToDb);
    }
}

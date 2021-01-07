namespace LogTailer.Data.Services
{
    using System.Threading.Tasks;
    using Base;
    using Domain.Models;

    public interface ISessionService : IDataService
    {
        Session ReadSession();

        Task UpdateSession(Session session);
    }
}

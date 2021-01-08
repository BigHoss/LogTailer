
namespace LogTailer.Data.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Domain.Models;

    public class SessionService : ISessionService
    {
        private readonly LogTailerContext _context;
        public SessionService(LogTailerContext context) => _context = context;


        public Session ReadSession()
        {
            _context.Database.EnsureCreated();

            return _context.Sessions.FirstOrDefault();
        }

        public async Task UpdateSession(Session session)
        {
            _ = _context.Sessions.Update(session);
            _ = await _context.SaveChangesAsync();
        }
    }
}

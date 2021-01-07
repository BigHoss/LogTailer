namespace LogTailer.Services
{
    using System.Threading.Tasks;
    using Data.Services;
    using Domain.Models;

    public class SessionStateService : ISessionStateService
    {
        private readonly ISessionService _sessionService;
        public Session Session { get; private set; }

        public SessionStateService(ISessionService sessionService)
        {
            _sessionService = sessionService;

            Session = _sessionService.ReadSession();
        }



        public Session ReadSession()
        {
            Session = _sessionService.ReadSession();

            if (Session is not null)
            {
                return Session;
            }

            Session = new Session();

            _sessionService.UpdateSession(Session);

            return Session;
        }

        public Task UpdateSession(Session session, bool writeToDb)
        {
            Session = session;

            return writeToDb ? _sessionService.UpdateSession(session) : Task.CompletedTask;
        }

        public void Dispose() => _sessionService.UpdateSession(Session);
    }
}

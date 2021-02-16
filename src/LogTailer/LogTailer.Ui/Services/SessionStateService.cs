namespace LogTailer.Ui.Services
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Services;
    using Domain.Models;

    public class SessionStateService : ISessionStateService
    {
        private readonly ISessionService _sessionService;
        private Session? session;

        private Session Session
        {
            get
            {
                if (session is not null)
                {
                    return session;
                }

                session = new Session();

                _sessionService.UpdateSession(session);
                return session;
            }
            set => session = value;
        }

        public SessionStateService(ISessionService sessionService)
        {
            _sessionService = sessionService;

            Session = _sessionService.ReadSession();
        }


        private Task UpdateSession() => _sessionService.UpdateSession(Session);

        public void Dispose() => _sessionService.UpdateSession(Session);
        public IEnumerable<OpenFile> ReadOpenFiles() => Session.OpenFiles;

        public async Task UpdateOpenFiles(IEnumerable<OpenFile> files)
        {
            Session.OpenFiles = files.ToList();
            await UpdateSession();
        }

        public async Task<OpenFile> CreateOpenFile(string filePath)
        {
            var file = new OpenFile {FilePath = filePath};
            Session.OpenFiles.Add(file);
            await UpdateSession();

            return await Task.FromResult(file);
        }

        public List<LineHighlight> ReadHighLights() => Session.LineHighlights;

        public async Task<LineHighlight> CreateHighLight(string searchString,
                                                         Color foreground,
                                                         Color background,
                                                         bool isRegex)
        {
            var highlight = new LineHighlight
            {
                SearchString = searchString,
                ForegroundColor = foreground,
                BackgroundColor = background,
                IsRegex = isRegex,
                IsActive = true
            };
            Session.LineHighlights.Add(highlight);
            await UpdateSession();

            return await Task.FromResult(highlight);
        }

        public async Task UpdateHighLights(List<LineHighlight> highlights)
        {
            Session.LineHighlights = highlights;
            await UpdateSession();
        }
    }
}

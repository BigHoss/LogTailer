namespace LogTailer.Ui.Pages.Shell
{
    using Domain.Models;
    using File;
    using Microsoft.Win32;
    using Services;
    using Stylet;
    using Syncfusion.SfSkinManager;

    public class RibbonViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISessionStateService _stateService;

        private Session session;

        public Session Session
        {
            get => session;
            set => SetAndNotify(ref session, value);
        }

        private VisualStyles selectedTheme;

        public VisualStyles SelectedTheme
        {
            get
            {
                selectedTheme = (VisualStyles)Session.ThemeIndex;
                return selectedTheme;
            }
            set
            {
                SetAndNotify(ref selectedTheme, value);
                Session.ThemeIndex = (int)selectedTheme;
            }
        }

        public RibbonViewModel(IEventAggregator eventAggregator,
                               ISessionStateService stateService)
        {
            _eventAggregator = eventAggregator;
            _stateService = stateService;

            Session =_stateService.ReadSession();
        }

        public void OpenFile()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var fileName = openFileDialog.FileName;

                _eventAggregator.PublishOnUIThread(new OpenFileEvent(fileName));
            }
        }
    }
}

namespace LogTailer.Ui.Pages.Shell
{
    using File;
    using Stylet;

    public class FileTabsViewModel : Conductor<FileDisplayViewModel>.Collection.AllActive, IHandle<OpenFileEvent>
    {
        private readonly IEventAggregator _eventAggregator;
        public FileTabsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _eventAggregator.Subscribe(this);
        }

        public void Handle(OpenFileEvent message) => Items.Add(new FileDisplayViewModel(message.FilePath));
    }
}

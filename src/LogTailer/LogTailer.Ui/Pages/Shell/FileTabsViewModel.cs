namespace LogTailer.Ui.Pages.Shell
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain.Models;
    using Events;
    using File;
    using Services;
    using Stylet;
    using Syncfusion.Windows.Tools.Controls;

    public class FileTabsViewModel : Conductor<FileDisplayViewModel>.Collection.AllActive, IHandle<OpenFileEvent>
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly IEventAggregator _eventAggregator;
        private readonly ISessionStateService _stateService;
        private List<OpenFile> openFiles = new List<OpenFile>();

        public FileTabsViewModel(IEventAggregator eventAggregator,
                                 ISessionStateService stateService)
        {
            _eventAggregator = eventAggregator;
            _stateService = stateService;

            UpdateOpenFiles(_stateService.ReadOpenFiles().ToList());

            _eventAggregator.Subscribe(this);
        }

        private void UpdateOpenFiles(List<OpenFile> files)
        {
            openFiles = files;

            Items.AddRange(files.Select(x => new FileDisplayViewModel(x.FilePath)));
        }

        private async Task AddOpenFile(string filePath)
        {
            if (openFiles.All(x => x.FilePath != filePath))
            {
                var file = await _stateService.CreateOpenFile(filePath);
                openFiles.Add(file);

                Items.Add(new FileDisplayViewModel(file.FilePath));
            }
        }

        public void CloseTab(object sender,
                              CloseTabEventArgs e)
        {
            var viewModel = e.TargetTabItem.DataContext as FileDisplayViewModel;

            var file = openFiles.Single(x => viewModel != null && x.FilePath == viewModel.FilePath);

            openFiles.Remove(file);
        }

        protected override void OnClose() => _stateService.UpdateOpenFiles(openFiles);

        public void Handle(OpenFileEvent message) => Task.Run(() => AddOpenFile(message.FilePath));
    }
}

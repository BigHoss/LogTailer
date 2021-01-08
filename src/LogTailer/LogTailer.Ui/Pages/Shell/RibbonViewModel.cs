// ReSharper disable ClassNeverInstantiated.Global
namespace LogTailer.Ui.Pages.Shell
{
    using Events;
    using Microsoft.Win32;
    using Stylet;

    public class RibbonViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;

        public RibbonViewModel(IEventAggregator eventAggregator) => _eventAggregator = eventAggregator;

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

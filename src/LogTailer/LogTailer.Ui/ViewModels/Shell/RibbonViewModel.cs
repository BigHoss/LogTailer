// ReSharper disable ClassNeverInstantiated.Global
namespace LogTailer.Ui.ViewModels.Shell
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Threading.Tasks;
    using Domain.Models;
    using Events;
    using Microsoft.Win32;
    using Services;
    using Stylet;
    using Syncfusion.UI.Xaml.Grid;

    public class RibbonViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISessionStateService _stateService;

        public RibbonViewModel(IEventAggregator eventAggregator,
                               ISessionStateService stateService)
        {
            _eventAggregator = eventAggregator;
            _stateService = stateService;

            Highlights = stateService.ReadHighLights();
        }

        public List<LineHighlight> Highlights { get; set; } = new();

        public LineHighlight SelectedHighlight { get; set; } = new();

        public void HighLightsGridDoubleTapped(GridCellDoubleTappedEventArgs args)
        {
            // ReSharper disable once UnusedVariable
            var asdf = (LineHighlight)args.Record;
        }

        public async Task AddNewHighlightInitiating()
        {
            var highlight = await _stateService.CreateHighLight("*", Color.Black, Color.Transparent, false);
            Highlights.Insert(0, highlight);
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

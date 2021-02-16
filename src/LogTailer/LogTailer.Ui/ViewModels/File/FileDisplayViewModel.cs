namespace LogTailer.Ui.ViewModels.File
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models;
    using Stylet;
    using Syncfusion.UI.Xaml.Grid;
    using Syncfusion.UI.Xaml.ScrollAxis;

    public class FileDisplayViewModel : Screen
    {
        private long lastPosition;
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private FileSystemWatcher? fileSystemWatcher;
        private string? title;
        private IObservableCollection<FileLine> textLines = new BindableCollection<FileLine>();
        private FileLine? selectedLine;

        public string? Title
        {
            get => title;
            set => SetAndNotify(ref title, value);
        }

        public IObservableCollection<FileLine> TextLines
        {
            get => textLines;
            set => SetAndNotify(ref textLines, value);
        }

        public FileLine? SelectedLine
        {
            get => selectedLine;
            set => SetAndNotify(ref selectedLine, value);
        }



        public FileDisplayViewModel(string filePath)
        {
            FilePath = filePath;
            Title = Path.GetFileNameWithoutExtension(filePath);


            _ = Task.Run(async () =>
            {
                await foreach (var line in InitializeFileTailing(filePath))
                {
                    TextLines.Add(line);
                }
            });
        }

        public string FilePath { get; set; }

        private async IAsyncEnumerable<FileLine> InitializeFileTailing(string filePath)
        {
            await using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var streamReader = new StreamReader(fileStream, Encoding.Default);
            string? line;
            while ((line = await streamReader.ReadLineAsync()) != null)
            {
                yield return new FileLine(DateTime.Now, line);
            }

            await RefreshSelectedToLast();

            lastPosition = fileStream.Position;

            var path = Path.GetDirectoryName(filePath);

            var fileName = Path.GetFileName(filePath);

            if (path != null)
            {
                fileSystemWatcher = new FileSystemWatcher(path, fileName);
                fileSystemWatcher.Changed += FileSystemWatcherOnChanged;
                fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;
                fileSystemWatcher.EnableRaisingEvents = true;
            }
        }

        public void GridSelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            var grid = (SfDataGrid)sender;
            if (grid.SelectedItem == null)
            {
                return;
            }

            var selectedIndex = grid.ResolveToRowIndex(grid.SelectedItem);
            if (selectedIndex < 0)
            {
                selectedIndex = 0;
            }

            grid.ScrollInView(new RowColumnIndex(selectedIndex,
                grid.ResolveToStartColumnIndex()));
        }

        private Task RefreshSelectedToLast()
        {
            SelectedLine = TextLines.Last();
            return Task.CompletedTask;
        }

        private void FileSystemWatcherOnChanged(object sender,
                                                FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }

            var filePath = e.FullPath;
            using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var streamReader = new StreamReader(fileStream, Encoding.Default);

            if (lastPosition > fileStream.Length)
            {
                lastPosition = 0;
                TextLines = new BindableCollection<FileLine>();
            }

            fileStream.Seek(lastPosition, SeekOrigin.Begin);

            string? line;
            while ((line = streamReader.ReadLine()) != null)
            {
                TextLines.Add(new FileLine(DateTime.Now, line));
            }

            RefreshSelectedToLast();

            lastPosition = fileStream.Position;
        }
    }
}

namespace LogTailer.Ui.Pages.File
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models;
    using Stylet;

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

            _ = Task.Run(() => InitializeFileTailing(filePath));
        }

        public string FilePath { get; set; }

        private async Task InitializeFileTailing(string filePath)
        {
            await using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var streamReader = new StreamReader(fileStream, Encoding.Default);
            string? line;
            while ((line = await streamReader.ReadLineAsync()) != null)
            {
                TextLines.Add(new FileLine(DateTime.Now, line));
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
            using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
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

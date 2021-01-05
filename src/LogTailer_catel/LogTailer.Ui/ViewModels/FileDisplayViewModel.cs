namespace LogTailer.Ui.ViewModels
{
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Catel.Data;
    using Catel.MVVM;
    using Extensions;

    public class FileDisplayViewModel : ViewModelBase
    {
        private readonly string inhputFilePath;

        private long lastPosition;
        private FileSystemWatcher? fileSystemWatcher;

        public FileDisplayViewModel(string inhputFileName)
        {
            inhputFilePath = inhputFileName;

            Task.Run(async () =>
            {
                await using var fs = new FileStream(inhputFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var sr = new StreamReader(fs, Encoding.Default);
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    TextLines.Add(line);
                }

                lastPosition = sr.GetPosition();

                var path = Path.GetFullPath(inhputFilePath);

                var fileName = Path.GetFileName(inhputFilePath);

                fileSystemWatcher = new FileSystemWatcher(path, fileName);
                fileSystemWatcher.Changed += FileSystemWatcherOnChanged;
            });
        }

        public override string Title => $"File Display - {inhputFilePath}";

        public ObservableCollection<string> TextLines
        {
            get => GetValue<ObservableCollection<string>>(TextLinesProperty);
            set => SetValue(TextLinesProperty, value);
        }

        public static readonly PropertyData TextLinesProperty = RegisterProperty(nameof(TextLines), typeof(ObservableCollection<string>));

    

        protected override async Task InitializeAsync() => await base.InitializeAsync();


        protected override async Task CloseAsync()
        {
            fileSystemWatcher?.Dispose();
            await base.CloseAsync();
        }

        private void FileSystemWatcherOnChanged(object sender,
                                                FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                var filePath = e.FullPath;
                using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var sr = new StreamReader(fs, Encoding.Default);

                sr.SetPosition(lastPosition);

                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    TextLines.Add(line);
                }
            }
        }
    }
}

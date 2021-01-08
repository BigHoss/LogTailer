namespace LogTailer.Ui.Events
{
    public class OpenFileEvent
    {
        public OpenFileEvent(string filePath) => FilePath = filePath;

        public string FilePath { get; private set; }
    }
}

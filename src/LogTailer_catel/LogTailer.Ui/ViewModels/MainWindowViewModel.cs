namespace LogTailer.Ui.ViewModels
{
    using Catel.MVVM;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Services;

    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IMainTabService _tabService;
        public override string Title => "LogTrailer";
        
        public MainWindowViewModel(IMainTabService tabService)
        {
            _tabService = tabService;
            OpenFileCommand = new TaskCommand(OnOpenFileCommandExecute);
        }

        public TaskCommand OpenFileCommand { get; private set; }


        private async Task OnOpenFileCommandExecute()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var fileName = openFileDialog.FileName;

                await _tabService.AddAndActivateAsync<FileDisplayViewModel>(new FileDisplayViewModel(fileName));
            }
        }

        protected override async Task InitializeAsync() => await base.InitializeAsync();

        protected override async Task CloseAsync() => await base.CloseAsync();
    }
}

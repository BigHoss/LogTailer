
namespace LogTailer.Ui.Views
{
    using Services;

    public partial class MainWindow
    {
        public MainWindow(IMainTabService tabService)
        {
            InitializeComponent();

            tabService.SetTabControl(TabControl);
        }
    }
}

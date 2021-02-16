// ReSharper disable ClassNeverInstantiated.Global
namespace LogTailer.Ui.ViewModels
{
    using Shell;
    using Stylet;

    public class ShellViewModel : Screen
    {

        public RibbonViewModel RibbonViewModel { get; set; }
        public FileTabsViewModel FileTabsViewModel { get; set; }

        public string Title { get; set; } = "LogTailer";

        public ShellViewModel(RibbonViewModel ribbonViewModel,
                              FileTabsViewModel fileTabsViewModel)
        {
            RibbonViewModel = ribbonViewModel;
            FileTabsViewModel = fileTabsViewModel;
        }

    }
}

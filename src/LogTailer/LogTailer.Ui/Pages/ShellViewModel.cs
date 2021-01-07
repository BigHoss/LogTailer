// ReSharper disable ClassNeverInstantiated.Global
namespace LogTailer.Ui.Pages
{
    using Services;
    using Shell;
    using Stylet;
    using Syncfusion.SfSkinManager;

    public class ShellViewModel : Screen
    {
        private VisualStyles selectedTheme;
        public RibbonViewModel RibbonViewModel { get; set; }
        public FileTabsViewModel FileTabsViewModel { get; set; }

        public string Title { get; set; } = "LogTailer";

        public ShellViewModel(RibbonViewModel ribbonViewModel,
                              FileTabsViewModel fileTabsViewModel,
                              ISessionStateService sessionStateService)
        {
            RibbonViewModel = ribbonViewModel;
            FileTabsViewModel = fileTabsViewModel;

            SelectedTheme = (VisualStyles)sessionStateService.ReadSession().ThemeIndex;
        }

        public VisualStyles SelectedTheme
        {
            get => selectedTheme;
            set => SetAndNotify(ref selectedTheme, value);
        }
    }
}

namespace LogTailer.Ui
{
    using Catel.Logging;
    using System.Windows;
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        protected override void OnStartup(StartupEventArgs e)
        {
#if DEBUG
            LogManager.AddDebugListener();
#endif

            Log.Info("Starting application");

            // Want to improve performance? Uncomment the lines below. Note though that this will disable
            // some features. 
            //
            // For more information, see http://docs.catelproject.com/vnext/faq/performance-considerations/

            // Log.Info("Improving performance");
            // Catel.Windows.Controls.UserControl.DefaultCreateWarningAndErrorValidatorForViewModelValue = false;
            // Catel.Windows.Controls.UserControl.DefaultSkipSearchingForInfoBarMessageControlValue = true;

            // TODO: Register custom types in the ServiceLocator
            //Log.Info("Registering custom types");
            //var serviceLocator = ServiceLocator.Default;
            //serviceLocator.RegisterType<IMyInterface, IMyClass>();

            // To auto-forward styles, check out Orchestra (see https://github.com/wildgums/orchestra)
            // StyleHelper.CreateStyleForwardersForDefaultStyles();

            Log.Info("Calling base.OnStartup");

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mzc3NjY3QDMxMzgyZTM0MmUzMEFYUXJmL25vSWY3UFRuY2tHemFTK2Z1L3FHTVhkazJOMlRjOEp2YllsNG89");

            base.OnStartup(e);
        }
    }
}

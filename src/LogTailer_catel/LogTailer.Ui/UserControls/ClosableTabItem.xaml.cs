namespace LogTailer.Ui.UserControls
{
    using System.Windows;
    using System.Windows.Data;
    using Catel.MVVM;
    using Catel.Windows;
    using Syncfusion.Windows.Tools.Controls;

    public partial class ClosableTabItem
    {
        public ClosableTabItem()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string),
            typeof(ClosableTabItem), new PropertyMetadata(string.Empty));


        public bool CanClose
        {
            get => (bool)GetValue(CanCloseProperty);
            set => SetValue(CanCloseProperty, value);
        }

        public static readonly DependencyProperty CanCloseProperty = DependencyProperty.Register(nameof(CanClose), typeof(bool),
            typeof(ClosableTabItem), new PropertyMetadata(true));

        private void OnLoaded(object sender,
                              RoutedEventArgs e)
        {
            if (!(Content is IViewModelContainer container))
            {
                return;
            }

            var viewModel = container.ViewModel;

            if (viewModel == null)
            {
                if (container is FrameworkElement frameworkElement)
                {
                    viewModel = frameworkElement.DataContext as IViewModel;
                }

                if (viewModel == null)
                {
                    return;
                }
            }

            SetBinding(TitleProperty, new Binding {Source = viewModel, Path = new PropertyPath(nameof(Title))});
        }

        private void OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            if (!CanClose)
            {
                return;
            }

            var tabControl = this.FindVisualAncestorByType<TabControlExt>();
            tabControl?.Items.Remove(DataContext);
        }
    }
}

namespace LogTailer.Ui.Models
{
    using System;
    using System.Threading.Tasks;
    using Catel;
    using Catel.MVVM;

    public class MainTabItem
    {
        public event EventHandler<EventArgs>? Closed;

        public MainTabItem(IViewModel viewModel)
        {
            Argument.IsNotNull(() => viewModel);

            ViewModel = viewModel;
            CanClose = true;

            if (!viewModel.IsClosed)
            {
                viewModel.ClosedAsync += OnViewModelClosed;
            }
        }

        private Task OnViewModelClosed(object sender,
                                       ViewModelClosedEventArgs e)
        {
            ViewModel.ClosedAsync -= OnViewModelClosed;

            Closed?.Invoke(this, e);

            return Task.CompletedTask;
        }

        public bool CanClose { get; set; }

        public IViewModel ViewModel { get; set; }
    }
}

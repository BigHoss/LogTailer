namespace LogTailer.Ui.Services
{
    using System.Threading.Tasks;
    using Catel;
    using Catel.IoC;
    using Catel.MVVM;
    using Models;

    public static class ITabServiceExtensions
    {
        public static MainTabItem Add<TViewModel>(this IMainTabService tabService,
                                                  object? dataContext = null,
                                                  bool canClose = false)
        where TViewModel : IViewModel
        {
            Argument.IsNotNull(() => tabService);

            var tabItem = CreateTabItem<TViewModel>(tabService, dataContext);
            tabItem.CanClose = canClose;

            tabService.Add(tabItem);

            return tabItem;
        }

        public static MainTabItem AddAndActivate<TViewModel>(this IMainTabService tabService,
                                                             object? dataContext = null,
                                                             bool canClose = false)
            where TViewModel : IViewModel
        {
            Argument.IsNotNull(() => tabService);

            var tabItem = Add<TViewModel>(tabService, dataContext, canClose);
            tabService.Activate(tabItem);

            return tabItem;
        }

        private static MainTabItem CreateTabItem<TViewModel>(this IMainTabService tabService,
                                                        object? dataContext = null)
            where TViewModel : IViewModel
        {
            Argument.IsNotNull(() => tabService);

            var dependencyResolver = tabService.GetDependencyResolver();
            var viewModelFactory = dependencyResolver.Resolve<IViewModelFactory>();

            var viewModel = viewModelFactory.CreateViewModel<TViewModel>(dataContext);
            return new MainTabItem(viewModel);
        }

        public static void AddAndActivate(this IMainTabService tabService,
                                          MainTabItem tabItem)
        {
            Argument.IsNotNull(() => tabItem);
            Argument.IsNotNull(() => tabService);

            tabService.Add(tabItem);
            tabService.Activate(tabItem);
        }

        public static async Task<MainTabItem?> AddAndActivateAsync<TViewModel>(this IMainTabService tabService,
                                                                               object? dataContext = null,
                                                                               bool canClose = false)
            where TViewModel : IViewModel
        {
            await Task.Run(() =>
            {
                Argument.IsNotNull(() => tabService);

                var tabItem = Add<TViewModel>(tabService, dataContext, canClose);
                tabService.Activate(tabItem);

                return tabItem;
            });

            return null;
        }
    }
}

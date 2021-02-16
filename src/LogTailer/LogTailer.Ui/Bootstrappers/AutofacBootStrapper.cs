// ReSharper disable UnusedType.Global
namespace LogTailer.Ui.Bootstrappers
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Autofac;
    using Stylet;

    public class AutofacBootstrapper<TRootViewModel> : BootstrapperBase where TRootViewModel : class
    {
        private IContainer? container;

        private object? rootViewModel;
        protected virtual object RootViewModel => rootViewModel ??= GetInstance(typeof(TRootViewModel));

        protected override void ConfigureBootstrapper()
        {
            var builder = new ContainerBuilder();
            DefaultConfigureIoC(builder);
            ConfigureIoC(builder);
            container = builder.Build();
        }

        /// <summary>
        /// Carries out default configuration of the IoC container. Override if you don't want to do this
        /// </summary>
        protected virtual void DefaultConfigureIoC(ContainerBuilder builder)
        {
            var viewManagerConfig = new ViewManagerConfig()
            {
                ViewFactory = GetInstance,
                ViewAssemblies = new List<Assembly>() { GetType().Assembly }
            };
            builder.RegisterInstance<IViewManager>(new ViewManager(viewManagerConfig));
            builder.RegisterType<MessageBoxView>();

            builder.RegisterInstance<IWindowManagerConfig>(this).ExternallyOwned();
            builder.RegisterType<WindowManager>().As<IWindowManager>().SingleInstance();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<MessageBoxViewModel>().As<IMessageBoxViewModel>().ExternallyOwned(); // Not singleton!
            builder.RegisterAssemblyTypes(GetType().Assembly).Where(x=>!x.Name.Contains("ProcessedByFody")).ExternallyOwned();
        }

        /// <summary>
        /// Override to add your own types to the IoC container.
        /// </summary>
        protected virtual void ConfigureIoC(ContainerBuilder builder) { }

        public override object GetInstance(Type type) => (container ?? throw new InvalidOperationException()).Resolve(type);

        protected override void Launch() => base.DisplayRootView(RootViewModel);

        public override void Dispose()
        {
            ScreenExtensions.TryDispose(rootViewModel);
            container?.Dispose();

            base.Dispose();
        }
    }
}

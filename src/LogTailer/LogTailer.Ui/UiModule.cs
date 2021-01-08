namespace LogTailer.Ui
{
    using Autofac;
    using Data;
    using Services.Base;

    public class UiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            var serviceType = typeof(IService);
            builder.RegisterAssemblyTypes(typeof(UiModule).Assembly)
                   .Where(x => serviceType.IsAssignableFrom(x))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();


            builder.RegisterModule<DataModule>();
        }
    }
}

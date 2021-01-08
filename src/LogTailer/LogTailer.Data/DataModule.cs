namespace LogTailer.Data
{
    using Autofac;
    using Services.Base;

    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new LogTailerContext())
                   .AsSelf()
                   .InstancePerLifetimeScope();

            var serviceType = typeof(IDataService);
            builder.RegisterAssemblyTypes(serviceType.Assembly)
                   .Where(x => serviceType.IsAssignableFrom(x))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}

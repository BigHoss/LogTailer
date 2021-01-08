namespace LogTailer.Ui
{
    using System;
    using Autofac;
    using Bootstrappers;
    using Microsoft.Extensions.Configuration;
    using Pages;
    using Syncfusion.SfSkinManager;

    public class Bootstrapper : AutofacBootstrapper<ShellViewModel>
    {
        protected override void ConfigureIoC(ContainerBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                       .SetBasePath(Environment.CurrentDirectory)
                       .AddJsonFile("appsettings.json");

            builder.Register(_ => configuration)
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.RegisterModule<UiModule>();
            base.ConfigureIoC(builder);
        }

        protected override void Configure()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
                "Mzc3NjY3QDMxMzgyZTM0MmUzMEFYUXJmL25vSWY3UFRuY2tHemFTK2Z1L3FHTVhkazJOMlRjOEp2YllsNG89");

            SfSkinManager.ApplyStylesOnApplication = true;
        }
    }
}

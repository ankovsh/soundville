using Castle.MicroKernel.Registration;
using System.Web.Mvc;
using Castle.Windsor;
using Soundville.Domain.Installers;
using Soundville.Infrastructure.WindsorCastle;
using Soundville.Presentation.Installers;
using Sounville.Configuration.WindsorCastle.Installers;
using Sounville.Configuration.WindsorCastle.Plumbing;

namespace Sounville.Configuration.WindsorCastle
{
    public class WindsorCastleConfig
    {
        public static void RegisterInstallers()
        {
            var container = IoC.ContainerInstance;
            container.Register(Component.For<IWindsorContainer>().Instance(container));

            container.Install(
                new ControllerInstaller(),
                new PresentationServiceInstaller(),
                new DomainServiceInstaller(),
                new DbContextInstaller());

            container.Register(Component.For<IControllerFactory>().ImplementedBy<WindsorControllerFactory>().LifeStyle.Singleton);
            var controllerFactory = container.Resolve<IControllerFactory>();
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}

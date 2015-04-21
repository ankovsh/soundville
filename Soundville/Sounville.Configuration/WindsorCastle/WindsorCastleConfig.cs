using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Soundville.Configuration.WindsorCastle.Installers;
using Soundville.Configuration.WindsorCastle.Plumbing;
using Soundville.Domain.Installers;
using Soundville.Infrastructure.WindsorCastle;
using Soundville.Presentation.Installers;

namespace Soundville.Configuration.WindsorCastle
{
    public class WindsorCastleConfig
    {
        public static void RegisterInstallers()
        {
            var container = IoC.ContainerInstance;
            container.Register(Component.For<IWindsorContainer>().Instance(container));

            container.Install(
                new ControllerInstaller(),
                new ApiControllerInstaller(),
                new PresentationServiceInstaller(),
                new DomainServiceInstaller(),
                new DbContextInstaller());

            container.Register(Component.For<IControllerFactory>().ImplementedBy<WindsorControllerFactory>().LifeStyle.Singleton);
            var controllerFactory = container.Resolve<IControllerFactory>();
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}

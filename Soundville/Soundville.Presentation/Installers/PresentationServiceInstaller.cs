using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Soundville.Presentation.Services.Interfaces;

namespace Soundville.Presentation.Installers
{
    public class PresentationServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly()
                    .BasedOn<IPresentationService>()
                    .LifestylePerWebRequest()
                    .WithServiceDefaultInterfaces());
        }
    }
}

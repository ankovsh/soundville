using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Soundville.Domain.EntityFramework;

namespace Soundville.Domain.Installers
{
    public class DbContextInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ISoundvilleContext>()
                .ImplementedBy<SoundvilleContext>()
                .LifestylePerWebRequest());
        }
    }
}

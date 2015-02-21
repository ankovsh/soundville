using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Soundville.Domain.Services.Interfaces;

namespace Soundville.Domain.Installers
{
    public class DomainServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly()
                .BasedOn(typeof(IDomainService<>))
                .LifestylePerWebRequest()
                .WithServiceDefaultInterfaces());
        }
    }
}

using Castle.Windsor;

namespace Soundville.Infrastructure.WindsorCastle
{
    public class IoC
    {
        private static readonly IWindsorContainer Container = new WindsorContainer();

        public static IWindsorContainer ContainerInstance
        {
            get
            {
                return Container;
            }
        }
    }
}

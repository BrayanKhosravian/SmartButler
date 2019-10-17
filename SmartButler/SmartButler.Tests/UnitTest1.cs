using Autofac;
using NUnit.Framework;
using SmartButler.Bootstrapper;
using SmartButler.Bootstrapper.Modules;
using SmartButler.Services.Registrable;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<PageComponentsModule>();
            builder.RegisterModule<ServiceModule>();
            var container = builder.Build();

            while (true)
                container.Resolve<IResourceManager>();

        }
    }
}
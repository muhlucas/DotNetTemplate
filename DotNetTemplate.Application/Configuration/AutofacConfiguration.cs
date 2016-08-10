using Autofac;
using DotNetTemplate.Domain.Repositories.Interfaces;
using DotNetTemplate.Domain.Services;
using DotNetTemplate.Domain.Services.Interfaces;
using DotNetTemplate.Infrastructure.Database.Repositories;

namespace DotNetTemplate.Application.Configuration
{
    public class AutofacConfiguration
    {
        public static IContainer Builder(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ServiceBase<>)).As(typeof(IServiceBase<>)).InstancePerDependency();
            builder.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IRepositoryBase<>)).InstancePerDependency();
            //builder.RegisterType<Example>().As<IExample>().InstancePerRequest();

            IContainer container = builder.Build();
            return container;
        }
    }
}

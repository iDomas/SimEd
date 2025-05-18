using SimEd.Common.Interfaces;
using SimEd.IoC.Interfaces;

namespace SimEd.IoC.Tools;

internal static class ServicesTools
{
    public static IInjector Initialize()
    {
        ServiceProvider serviceProvider = new ServiceProvider();
        IInjectorHost injector = (IInjectorHost)serviceProvider.GetService<IInjector>();
        injector.Provider = serviceProvider;
        return injector;
    }
}
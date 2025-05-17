using SimEd.Common.Interfaces;
using SimEd.IoC.Interfaces;

namespace SimEd.IoC.Tools;

internal static class ServicesTools
{
    public static IGetService Initialize()
    {
        ServiceProvider serviceProvider = new ServiceProvider();
        IGetServiceHost getService = (IGetServiceHost)serviceProvider.GetService<IGetService>();
        getService.Provider = serviceProvider;
        return getService;
    }
}
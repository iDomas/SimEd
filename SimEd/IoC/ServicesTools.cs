namespace SimEd.IoC;

internal static class ServicesTools
{
    public static IGetService Initialize()
    {
        ServiceProvider serviceProvider = new ServiceProvider();
        IGetService getService = serviceProvider.GetService<IGetService>();
        getService.Provider = serviceProvider;
        return getService;
    }
}
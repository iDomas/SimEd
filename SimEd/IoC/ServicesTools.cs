namespace SimEd.IoC;

public static class ServicesTools
{
    public static ServiceProvider Initialize()
    {
        ServiceProvider serviceProvider = new ServiceProvider();
        IGetService getService = serviceProvider.GetService<IGetService>();
        getService.Provider = serviceProvider;
        return serviceProvider;
    }
}
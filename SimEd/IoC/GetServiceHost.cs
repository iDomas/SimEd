namespace SimEd.IoC;

public class GetServiceHost : IGetService
{
    public ServiceProvider Provider { get; set; }

    public T GetService<T>() => Provider.GetService<T>();
}
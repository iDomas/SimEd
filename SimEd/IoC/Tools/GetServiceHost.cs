using SimEd.IoC.Interfaces;

namespace SimEd.IoC.Tools;

public class GetServiceHost : IGetServiceHost
{
    public ServiceProvider Provider { get; set; }

    public T GetService<T>() => Provider.GetService<T>();
}
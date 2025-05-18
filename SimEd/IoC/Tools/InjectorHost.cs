using SimEd.IoC.Interfaces;

namespace SimEd.IoC.Tools;

public class InjectorHost : IInjectorHost
{
    public ServiceProvider Provider { get; set; } = null!;

    public T GetService<T>() => Provider.GetService<T>();
}
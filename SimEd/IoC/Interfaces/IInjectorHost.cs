using SimEd.Common.Interfaces;

namespace SimEd.IoC.Interfaces;

public interface IInjectorHost : IInjector
{
    public ServiceProvider Provider { get; set; }
}
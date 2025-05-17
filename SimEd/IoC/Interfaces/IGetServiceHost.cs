using SimEd.Common.Interfaces;

namespace SimEd.IoC.Interfaces;

public interface IGetServiceHost : IGetService
{
    public ServiceProvider Provider { get; set; }
}
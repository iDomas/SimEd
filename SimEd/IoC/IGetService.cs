namespace SimEd.IoC;

public interface IGetService
{
    public ServiceProvider Provider { get; set; }
    public T GetService<T>();
}
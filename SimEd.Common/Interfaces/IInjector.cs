namespace SimEd.Common.Interfaces;

public interface IInjector
{
    public T GetService<T>();
}
namespace CsvConverter.Repositories;

public interface IRepository<T>
{
    void Add(T writer);
    T Get(string type);
}
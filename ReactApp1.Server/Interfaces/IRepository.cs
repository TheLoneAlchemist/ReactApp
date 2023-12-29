namespace ReactApp1.Server.Interfaces
{
    public interface IRepository<T> where T:class
    {
       Task AddAsync(T entity);
    }
}

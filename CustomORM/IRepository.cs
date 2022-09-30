namespace CustomORM
{
    public interface IRepository<T>
    {
        void Insert(T entity);
        void Delete(int id);
        void Update(int id, T entity);
        T Get(int id);
        List<T> GetAll();
    }
}

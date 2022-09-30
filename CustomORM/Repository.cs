namespace CustomORM
{
    public class Repository<T> : IRepository<T>
    {
        private QueryCreator<T> queryCreator;
        private DBConverter<T> dBConverter;
        
        public Repository()
        {
            queryCreator = new QueryCreator<T>();
            dBConverter = new DBConverter<T>();
        }
        public void Insert(T entity)
        {
            ListOfQuery.Queries.Add(queryCreator.Insert(entity));
        }
        public void Delete(int id)
        {
            ListOfQuery.Queries.Add(queryCreator.Delete(id));
        }
        public void Update(int id, T entity)
        {
            ListOfQuery.Queries.Add(queryCreator.Update(id, entity));
        }
        public T Get(int id)
        {
            var dataSet = QueryExecuter.ExecuteSelectQuery(queryCreator.Get(id));
            return dBConverter.Convert(dataSet);
        }
        public List<T> GetAll()
        {
            var dataSet = QueryExecuter.ExecuteSelectQuery(queryCreator.GetAll());
            return dBConverter.ConvertToList(dataSet);
        }
    }
}

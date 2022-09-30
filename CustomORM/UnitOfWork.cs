namespace CustomORM
{
    public class UnitOfWork
    {
        public void SaveChanges()
        {
            QueryExecuter.ExecuteSaveChanges();
            ListOfQuery.Queries.Clear();
        }
    }
}

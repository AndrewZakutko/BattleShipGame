namespace CustomORM
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class RepositoryAttribute : Attribute
    {
        public Type Type { get; set; }
        
        public RepositoryAttribute(Type type)
        {
            Type = type;
        }
    }
}

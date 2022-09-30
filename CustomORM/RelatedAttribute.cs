namespace CustomORM
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class RelatedAttribute : Attribute
    {
        public Type Type { get; set; } 

        public RelatedAttribute(Type type)
        {
            Type = type;
        }
    }
}

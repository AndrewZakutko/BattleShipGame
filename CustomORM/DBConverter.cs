using System.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomORM
{
    internal class DBConverter<T>
    {
        private object FindHelper(DataSet dataSet, string NameOfColumn, int indexOfRow)
        {
            var list = dataSet.Tables[0].Rows;
            return list[indexOfRow][NameOfColumn];
        }
        private object ConvertHelper(DataSet dataSet, object entity, int indexOfRow)
        {
            var propertyIndex = default(int);
            var listProperties = entity.GetType().GetProperties();
            foreach (var property in listProperties)
            {
                foreach (var attribute in property.GetCustomAttributes(false))
                {
                    if (attribute.GetType() == typeof(ForeignKeyAttribute))
                    {
                        var props = attribute.GetType().GetProperties();
                        if (property.PropertyType.IsEnum || property.PropertyType == typeof(string))
                        {
                            listProperties[propertyIndex].SetValue(entity, FindHelper(dataSet, props[0].GetValue(attribute).ToString(), indexOfRow));
                            propertyIndex++;
                        }
                        if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                        {
                            listProperties[propertyIndex].SetValue(entity, ConvertHelper(dataSet, property.GetValue(entity), indexOfRow));
                            propertyIndex++;
                        }
                    }
                    if (attribute.GetType() == typeof(ColumnAttribute))
                    {
                        var props = attribute.GetType().GetProperties();
                        listProperties[propertyIndex].SetValue(entity, FindHelper(dataSet, props[0].GetValue(attribute).ToString(), indexOfRow));
                        propertyIndex++;
                    }
                }
            }
            return entity;
        }
        public T Convert(DataSet dataSet)
        {
            var obj = Activator.CreateInstance<T>();
            var propertyIndex = default(int);
            var listProperties = typeof(T).GetProperties();
            var indexOfRow = default(int);
            foreach(var property in listProperties)
            {
                foreach(var attribute in property.GetCustomAttributes(false))
                {
                    if(attribute.GetType() == typeof(ForeignKeyAttribute))
                    {
                        var props = attribute.GetType().GetProperties();
                        if (property.PropertyType.IsEnum || property.PropertyType == typeof(string))
                        {
                            listProperties[propertyIndex].SetValue(obj, FindHelper(dataSet, props[0].GetValue(attribute).ToString(), indexOfRow));
                            propertyIndex++;
                        }
                        if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                        {
                            listProperties[propertyIndex].SetValue(obj, ConvertHelper(dataSet, property.GetValue(obj), indexOfRow));
                            propertyIndex++;
                        }
                    }
                    if(attribute.GetType() == typeof(ColumnAttribute))
                    {
                        var props = attribute.GetType().GetProperties();
                        listProperties[propertyIndex].SetValue(obj, FindHelper(dataSet, props[0].GetValue(attribute).ToString(), indexOfRow));
                        propertyIndex++;
                    }
                }
            }
            return obj;
        }
        public List<T> ConvertToList(DataSet dataSet)
        {
            var list = new List<T>();
            var indexOfRow = default(int);
            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                var obj = Activator.CreateInstance<T>();
                list.Add((T)ConvertHelper(dataSet, obj, indexOfRow));
                indexOfRow++;
            }
            return list;
        } 
    }
}

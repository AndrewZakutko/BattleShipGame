using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace CustomORM
{
    internal class QueryCreator<T>
    {
        public string Insert(T entity)
        {
            var listHelper = new List<string>();
            var listColumns = new List<string>();
            foreach (var property in entity.GetType().GetProperties())
            {
                foreach (var item in property.GetCustomAttributes(false))
                {
                    if (item.GetType() == typeof(ForeignKeyAttribute))
                    {
                        listColumns.Add($"{property.Name}Id");
                        if(property.PropertyType.IsEnum || property.PropertyType == typeof(string))
                        {
                            listHelper.Add($"(SELECT TOP 1 Id FROM {property.Name}s WHERE {property.Name}s.Name = '{property.GetValue(entity)}')");
                        }
                        if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                        {
                            var obj = property.GetValue(entity);
                            listHelper.Add(InsertAndUpdateHelper(obj));
                        }
                    }
                    if (item.GetType() == typeof(ColumnAttribute))
                    {
                        listColumns.Add($"{property.Name}");
                        if(property.GetValue(entity).GetType() == typeof(string))
                        {
                            listHelper.Add($"'{property.GetValue(entity)}'");
                        }
                        else
                        {
                            listHelper.Add($"{property.GetValue(entity)}");
                        }
                    }
                }
            }
            var strResult = String.Join(", ", listHelper);
            var stringColumns = String.Join(", ", listColumns);
            if(string.IsNullOrEmpty(stringColumns) || string.IsNullOrEmpty(strResult))
            {
                throw new Exception("Insert exception!");
            }
            return $"INSERT {typeof(T).Name}s ({stringColumns}) VALUES ({strResult})";
        }
        public string Delete(int id)
        {
            var listHelper = new List<string>();
            foreach (var attribute in typeof(T).GetCustomAttributes(false))
            {
                if (attribute.GetType() == typeof(RelatedAttribute))
                {
                    var property = attribute.GetType().GetProperty("Type");
                    if(property == null)
                    {
                        throw new Exception("There is no such property in attribute!");
                    }
                    var objType = (Type)property.GetValue(attribute);
                    listHelper.Add(DeleteHelper(id.ToString(), objType, typeof(T)));
                }
            }
            listHelper.Add($"DELETE FROM {typeof(T).Name}s WHERE Id IN ({id});");
            return string.Join(null, listHelper);
        }
        public string Update(int id, T entity)
        {
            var listHelper = new List<String>();
            foreach(var property in typeof(T).GetProperties())
            {
                foreach (var item in property.GetCustomAttributes(false))
                {
                    if (item.GetType() == typeof(ForeignKeyAttribute))
                    {
                        if (property.PropertyType.IsEnum || property.PropertyType == typeof(string))
                        {
                            listHelper.Add($"{property.Name}Id = (SELECT TOP 1 Id FROM {property.Name}s WHERE {property.Name}s.Name = '{property.GetValue(entity)}')");
                        }
                        if(property.PropertyType.IsClass && property.PropertyType != typeof(string))
                        {
                            var obj = property.GetValue(entity);
                            listHelper.Add($"{property.Name}Id = {InsertAndUpdateHelper(obj)}");
                        }
                    }
                    if (item.GetType() == typeof(ColumnAttribute))
                    {
                        listHelper.Add($"{property.Name} = {property.GetValue(entity)}");
                    }
                }
            }
            var stringSet = String.Join(", ", listHelper);
            return $"UPDATE {typeof(T).Name}s SET {stringSet} WHERE Id = {id}";
        }
        public string Get(int id)
        {
            var command = $"SELECT * FROM {typeof(T).Name}s";
            var listHelper = new List<String>();
            foreach (var property in typeof(T).GetProperties())
            {
                foreach (var attribute in property.GetCustomAttributes(false))
                {
                    if (attribute.GetType() == typeof(ForeignKeyAttribute))
                    {
                        listHelper.Add(GetHelper(typeof(T), property.PropertyType));
                    }
                }
            }
            command += string.Join(null, listHelper);
            command += $" WHERE {typeof(T).Name}s.Id = {id}";
            return command;
        }
        public string GetAll()
        {
            var command = $"SELECT * FROM {typeof(T).Name}s";
            var listHelper = new List<String>();
            foreach (var property in typeof(T).GetProperties())
            {
                foreach(var attribute in property.GetCustomAttributes(false))
                {
                    if (attribute.GetType() == typeof(ForeignKeyAttribute))
                    {
                        listHelper.Add(GetHelper(typeof(T), property.PropertyType));
                    }
                }
            }
            command += String.Join(null, listHelper);
            return command;
        }
        private string GetHelper(Type objType ,Type type)   
        {
            var listResult = new List<String>();
            listResult.Add($" INNER JOIN {type.Name}s ON {objType.Name}s.{type.Name}Id = {type.Name}s.Id");
            foreach (var property in type.GetProperties())
            {
                foreach (var attribute in property.GetCustomAttributes(false))
                {
                    if (attribute.GetType() == typeof(ForeignKeyAttribute))
                    {
                        listResult.Add(GetHelper(type, property.PropertyType));
                    }
                }
            }
            return String.Join(null, listResult);
        }
        private string DeleteHelper(string ids, Type objType, Type type)
        {
            var listHelper = new List<string>();
            var listIds = new List<string>();
            var newIds = String.Empty;
            foreach (var attribute in objType.GetCustomAttributes(false))
            {
                if (attribute.GetType() == typeof(RelatedAttribute))
                {
                    var property = attribute.GetType().GetProperty("Type");
                    if (property == null)
                    {
                        throw new Exception("There is no such property!");
                    }
                    var dataSet = QueryExecuter.ExecuteSelectQuery($"SELECT Id FROM {objType.Name}s WHERE {type.Name}Id IN ({ids})");
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        var cells = dr.ItemArray;
                        listIds.Add(cells[0].ToString());
                    }
                    newIds = String.Join(", ", listIds);
                    if(newIds.Length == 0)
                    {
                        return String.Join(null, listHelper);
                    }
                    var typeProperty = (Type)property.GetValue(attribute);
                    listHelper.Add(DeleteHelper(newIds, typeProperty, objType));
                }
            }
            listHelper.Add($"DELETE FROM {objType.Name}s WHERE {type.Name}Id IN ({ids});");
            return String.Join(null, listHelper);
        }
        private string InsertAndUpdateHelper(object entity)
        {
            var stringValues = String.Empty;
            var listResult = new List<String>();
            var listAllValues = new List<String>();
            var listOfColumnValues = new List<String>();
            var typeEntity = entity.GetType();
            var typeBase = typeEntity.BaseType;
            var hasTableAttribute = typeBase.GetCustomAttributes(typeof(TableAttribute), false).Any();
            foreach (var property in typeEntity.GetProperties())
            {
                foreach (var propertyattribute in property.GetCustomAttributes(false))
                {
                    if (propertyattribute.GetType() == typeof(ForeignKeyAttribute))
                    {
                        if (property.PropertyType.IsEnum || property.PropertyType == typeof(string))
                        {
                            var stringSubquery = $"s.{property.Name}Id = (SELECT TOP 1 Id FROM {property.Name}s WHERE {property.Name}s.Name = '{property.GetValue(entity)}')";
                            if (hasTableAttribute)
                            {
                                listAllValues.Add(String.Format("{0}{1}", typeEntity.BaseType.Name, stringSubquery));
                            }
                            else
                            {
                                listAllValues.Add(String.Format("{0}{1}", typeEntity.Name, stringSubquery));
                            }
                        }
                        if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                        {
                            var obj = property.GetValue(entity);
                            var str = InsertAndUpdateHelper(obj);
                            var stringSubquery = $"s.{property.Name}Id = {str})";
                            if (hasTableAttribute)
                            {
                                listAllValues.Add(String.Format("{0}{1}", typeEntity.BaseType.Name, stringSubquery));
                            }
                            else
                            {
                                listAllValues.Add(String.Format("{0}{1}", typeEntity.Name, stringSubquery));
                            }
                        }
                    }
                    if (propertyattribute.GetType() == typeof(ColumnAttribute))
                    {
                        var stringSubquery = $"s.{property.Name} = {property.GetValue(entity)}";
                        if (hasTableAttribute)
                        {
                            listOfColumnValues.Add(String.Format("{0}{1}", typeEntity.BaseType.Name, stringSubquery));
                        }
                        else
                        {
                            listOfColumnValues.Add(String.Format("{0}{1}", typeEntity.Name, stringSubquery));
                        }
                    }
                }
            }
            if (listOfColumnValues.Count > 0)
            {
                stringValues = String.Join(" AND ", listOfColumnValues);
                listAllValues.Add($"{stringValues})");
                stringValues = String.Empty;
            }
            if (listAllValues.Count > 0)
            {
                stringValues = String.Join(" AND ", listAllValues);
                if (hasTableAttribute)
                {
                    listResult.Add($"(SELECT TOP 1 Id FROM {typeEntity.BaseType.Name}s WHERE ({stringValues})");
                }
                else
                {
                    listResult.Add($"(SELECT TOP 1 Id FROM {typeEntity.Name}s WHERE ({stringValues})");
                }
                stringValues = String.Empty;
            }
            return $"{listResult[0]}";
        }
    }
}

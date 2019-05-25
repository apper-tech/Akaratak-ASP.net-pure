using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.DynamicData;
using System.Linq.Dynamic;
using System.Web.UI;
using System.Collections.Specialized;
using DynamicDataLibrary;
using DynamicDataLibrary.Attributes;
using NotAClue.Web.DynamicData;
using System.Collections;
using System.Linq.Expressions;
using System.ComponentModel;


namespace DynamicDataLibrary
{
    public static class ExtentionMethods
    {
        public static string DetectFormat(this MetaColumn column, bool withoutNum)
        {
            string format = null;
            if (!String.IsNullOrEmpty(column.DataFormatString))
            {
                format = column.DataFormatString;
            }
            else if (column.DataTypeAttribute != null)
            {
                switch (column.DataTypeAttribute.DataType)
                {
                    case DataType.Date:
                        format = "yyyy/MM/dd";
                        break;
                    case DataType.DateTime:
                        format = "yyyy/MM/dd hh:mm tt";
                        break;
                    case DataType.Time:
                        format = "hh:mm tt";
                        break;

                }
            }
            else if (column.ColumnType.Equals(typeof(DateTime)))
            {
                format = "yyyy/MM/dd hh:mm tt";
            }
            else if (column.ColumnType.Equals(typeof(TimeSpan)))
            {
                format = "hh:mm tt";
            }
            if (!withoutNum)
                return format;
            else
                return format.Trim('{', '}').Replace("0:", "").Replace("1:", "").Replace("2:", "").Replace("3:", "").Replace("4:", "")
                    .Replace("5:", "").Replace("6:", "").Replace("7:", "").Replace("8:", "").Replace("9:", "");
        }
        //public static bool Contains(this Nullable<long> str, int number)
        //{
        //    if (str.HasValue)
        //        return str.ToString().Contains(Convert.ToString(number));
        //    else
        //        return false;
        //}

        public static bool Contains(Nullable<long> str, int number)
        {
            if (str.HasValue)
                return str.ToString().Contains(Convert.ToString(number));
            else
                return false;
        }

        public static TEntity GetItemObject<TEntity>(object dataItem)
            where TEntity : class
        {
            var entity = dataItem as TEntity;
            if (entity != null)
            {
                return entity;
            }
            var td = dataItem as System.ComponentModel.ICustomTypeDescriptor;
            if (td != null)
            {
                return (TEntity)td.GetPropertyOwner(null);
            }
            return null;
        }
        public static object GetItemObject(Type type, object dataItem)
        {
            if (type.IsInstanceOfType(dataItem))
            {
                return dataItem;
            }
            var td = dataItem as System.ComponentModel.ICustomTypeDescriptor;
            if (td != null)
            {
                return td.GetPropertyOwner(null);
            }
            return null;
        }

        //public static void FillKeysAndValuesFromBackup(this MetaTable table, 
        //    System.Collections.Specialized.IOrderedDictionary keys,
        //    System.Collections.Specialized.IOrderedDictionary values,
        //    System.Collections.Specialized.IOrderedDictionary backUpKeys,
        //    System.Collections.Specialized.IOrderedDictionary backUpValues)
        //{
        //    if (backUpValues.Count != 0)
        //    {
        //        values.Clear();
        //        foreach (System.Collections.DictionaryEntry item in backUpValues)
        //        {
        //            values.Add(item.Key, item.Value);
        //        }
        //        keys.Clear();
        //        foreach (System.Collections.DictionaryEntry item in backUpKeys)
        //        {
        //            keys.Add(item.Key, item.Value);
        //        }
        //    }
        //    else
        //    {
        //        IQueryable query = QueryAccordingToKeysAndValues(table, keys, backUpKeys);

        //        foreach (System.Collections.DictionaryEntry item in keys)
        //        {
        //            backUpKeys.Add(item.Key, item.Value);
        //        }
        //        foreach (var item in query)
        //        {
        //            foreach (var pro in item.GetType().GetProperties())
        //            {
        //                foreach (var itemValue in query.Select(pro.Name, null))
        //                {
        //                    values.Add(pro.Name, itemValue);
        //                    backUpValues.Add(pro.Name, itemValue);
        //                }
        //            }

        //        }
        //    }
        //}

        public static void FillValues(this MetaTable table,
            System.Collections.Specialized.IOrderedDictionary keys,
            System.Collections.Specialized.IOrderedDictionary values)
        {
            IQueryable query = table.QueryAccordingToKeysAndValues(keys);
            values.Clear();
            foreach (var item in query)
            {
                foreach (var pro in item.GetType().GetProperties())
                {
                    try
                    {
                        if (table.Columns.Count(c => c.Name == pro.Name && c.GetType() == typeof(MetaColumn)) == 0)
                        {
                            continue;
                        }
                        foreach (var itemValue in query.Select(pro.Name, null))
                        {
                            values.Add(pro.Name, itemValue);
                        }
                    }
                    catch (Exception)
                    {
                        //Exception occur when the class has a custom property!!!
                        //So, just skip this property.
                    }
                }
            }
        }

        public static OrderedDictionary FillValues(this MetaTable table, object context)
        {
            OrderedDictionary values = new OrderedDictionary();
            IQueryable query = table.GetQuery(context);
            foreach (var item in query)
            {
                foreach (var pro in item.GetType().GetProperties())
                {
                    foreach (var itemValue in query.Select(pro.Name, null))
                    {
                        values.Add(pro.Name, itemValue);
                    }
                }
            }
            return values;
        }


        public static void Copy(this MetaTable table,
            System.Collections.Specialized.IOrderedDictionary source,
            System.Collections.Specialized.IOrderedDictionary dist)
        {
            dist.Clear();
            foreach (System.Collections.DictionaryEntry item in source)
            {
                dist.Add(item.Key, item.Value);
            }
        }

        public static IQueryable QueryAccordingToKeysAndValues(this MetaTable table,
            System.Collections.Specialized.IOrderedDictionary keys)
        {
            StringBuilder whereKeys = new StringBuilder();
            foreach (System.Collections.DictionaryEntry item in keys)
            {
                whereKeys.AppendFormat("{0} == {1}", item.Key, item.Value);
                whereKeys.Append(" && ");
            }
            string whereClause;
            if (whereKeys.ToString().Contains(" && "))
                whereClause = whereKeys.ToString().Substring(0, whereKeys.ToString().LastIndexOf(" && "));
            else
                whereClause = whereKeys.ToString();
            IQueryable query;
            if (!String.IsNullOrEmpty(whereClause))
                query = table.GetQuery().Where(whereClause).Select("it", null);
            else
                query = table.GetQuery();

            return query;
        }

        /// <summary>
        /// When using GridView Field Template there are fields from child tables added to the parent values!
        /// So, we just remove them.
        /// </summary>
        /// <param name="values"></param>
        public static void RemoveWrongEntries(this MetaTable table, IOrderedDictionary values)
        {
            DictionaryEntry[] oldValues = new DictionaryEntry[values.Count];
            int counter = 0;
            foreach (DictionaryEntry item in values)
            {
                oldValues[counter++] = item;
            }
            int removalCount = 0;
            for (int i = 0; i < oldValues.Length; i++)
            {
                if (table.Columns.Count(c => c.Name == oldValues[i].Key.ToString() && c.GetType() == typeof(MetaColumn)) == 0
                    //|| oldValues.Take(i).Count(ov => ov.Key == oldValues[i].Key) > 0
                    )
                {
                    values.RemoveAt(i - removalCount);
                    removalCount++;
                }
            }
        }

        public static T CustomCast<T>(this object o)
        {
            return (T)o;
        }



        #region IQueryable methods from: http://csharpbits.notaclue.net/2009/04/cascading-filters-and-fields-dynamic.html, check also http://csharpbits.notaclue.net/2010/05/part-1-cascading-hierarchical-field.html
        /// <summary>
        /// Gets a list of entities from the source IQueryable 
        /// filtered by the MetaForeignKeyColumn's selected value
        /// </summary>
        /// <param name="sourceQuery">The query to filter</param>
        /// <param name="fkColumn">The column to filter the query on</param>
        /// <param name="fkSelectedValue">The value to filter the query by</param>
        /// <returns>
        /// An IQueryable of the based on the source query 
        /// filtered but the FK column and value passed in.
        /// </returns>
        public static IQueryable GetQueryFilteredByParent(this IQueryable sourceQuery, MetaForeignKeyColumn fkColumn, String fkSelectedValue)
        {
            // if no filter value return the query
            if (String.IsNullOrEmpty(fkSelectedValue))
                return sourceQuery;

            // {RequiredPlots}
            var parameterExpression = Expression.Parameter(sourceQuery.ElementType, fkColumn.Table.Name);

            // {(RequiredPlots.Builders.Id = 1)}
            var body = BuildWhereClause(fkColumn, parameterExpression, fkSelectedValue);

            // {RequiredPlots => (RequiredPlots.Builders.Id = 1)}
            var lambda = Expression.Lambda(body, parameterExpression);

            // Developers.Where(RequiredPlots => (RequiredPlots.Builders.Id = 1))}
            MethodCallExpression whereCall = Expression.Call(typeof(Queryable),
                "Where", new Type[] { sourceQuery.ElementType },
                sourceQuery.Expression,
                Expression.Quote(lambda));

            // create and return query
            return sourceQuery.Provider.CreateQuery(whereCall);
        }

        /// <summary>
        /// This builds the and where clause taking
        /// into account composite keys
        /// </summary>
        /// <param name="fkColumn">The column to filter the query on</param>
        /// <param name="fkSelectedValue">The value to filter the query by</param>
        /// <param name="parameterExpression">Parameter expression</param>
        /// <returns>
        /// Returns the expression for the where clause 
        /// i.e. ((x = 1) && (Y = 2)) or (x = 1) etc.
        /// </returns>
        private static Expression BuildWhereClause(
            MetaForeignKeyColumn fkColumn,
            ParameterExpression parameterExpression,
            string fkSelectedValue)
        {
            // get the FK's and value into dictionary
            IDictionary dict = new OrderedDictionary();
            fkColumn.ExtractForeignKey(dict, fkSelectedValue);

            // setup index into dictionary
            int i = 0;

            // setup array list to hold each AND fragment
            ArrayList andFragments = new ArrayList();
            foreach (DictionaryEntry entry in dict)
            {
                // get fk name 'Builders.Id'
                string keyName = fkColumn.Name
                    + "." + fkColumn.ParentTable.PrimaryKeyColumns[i++].Name;

                // Build property expression 
                // i.e. {RequiredPlots.Builders.Id}
                Expression propertyExpression
                    = BuildPropertyExpression(parameterExpression, keyName);

                // sets the type based on the propertyExpression's type
                // i.e. all the values returned from the DDL are of type string
                // so the type on the expression needs setting to the correct type
                object value = ChangeType(entry.Value, propertyExpression.Type);

                // join the property expression and value in an
                // equals expression i.e. (RequiredPlots.Builders.Id = 1)
                Expression equalsExpression
                    = Expression.Equal(propertyExpression,
                    Expression.Constant(value, propertyExpression.Type));

                // add a fragment to array list
                andFragments.Add(equalsExpression);
            }

            // initialise result
            Expression result = null;
            // join add fragments of composite keys 
            // together together
            foreach (Expression e in andFragments)
            {
                if (result == null)
                    result = e;
                else
                    result = Expression.AndAlso(result, e);
            }
            // joined fragments look something like:
            // (RequiredPlots.Developer.Id = 1) && (RequiredPlots.HouseType.Id = 1)
            return result;
        }

        /// <summary>
        /// Builds a property expression from the parts it joins
        /// the parameterExpression and the propertyName together.
        /// i.e. {RequiredPlots}  and "Builders.Id"
        /// becomes: {RequiredPlots.Developers.Id}
        /// </summary>
        /// <param name="parameterExpression">
        /// The parameter expression.
        /// </param>
        /// <param name="propertyName">
        /// Name of the property.
        /// </param>
        /// <returns>
        /// A property expression
        /// </returns>
        public static Expression BuildPropertyExpression(
            Expression parameterExpression,
            string propertyName)
        {
            Expression expression = null;
            // split the propertyName into each part to 
            // be build into a property expression
            string[] strArray = propertyName.Split(new char[] { '.' });
            foreach (string str in strArray)
            {
                if (expression == null)
                    expression
                        = Expression.PropertyOrField(parameterExpression, str);
                else
                    expression
                        = Expression.PropertyOrField(expression, str);
            }
            // {RequiredPlots.Developer.Id}
            return expression;
        }

        /// <summary>
        /// Changes the type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="type">The type to convert to.</param>
        /// <returns>The value converted to the type.</returns>
        public static object ChangeType(object value, Type type)
        {
            // if type is null throw exception can't
            // carry on nothing to convert to.
            if (type == null)
                throw new ArgumentNullException("type");

            if (value == null)
            {
                // test for nullable type 
                // (i.e. if Nullable.GetUnderlyingType(type)
                // is not null then it is a nullable type 
                // OR if it is a reference type
                if ((Nullable.GetUnderlyingType(type) != null)
                    || !type.IsValueType)
                    return null;
                else // for 'not nullable value types' return the default value.
                    return Convert.ChangeType(value, type);
            }

            // ==== Here we are guaranteed to have a type and value ====

            // get the type either the underlying type or 
            // the type if there is no underlying type.
            type = Nullable.GetUnderlyingType(type) ?? type;

            // Convert using the type
            TypeConverter converter
                = TypeDescriptor.GetConverter(type);
            if (converter.CanConvertFrom(value.GetType()))
            {
                // return the converted value
                return converter.ConvertFrom(value);
            }

            // Convert using the values type
            TypeConverter converter2
                = TypeDescriptor.GetConverter(value.GetType());
            if (!converter2.CanConvertTo(type))
            {
                // if the type cannot be converted throw an error
                throw new InvalidOperationException(
                    String.Format("Unable to convert type '{0}' to '{1}'",
                    new object[] { value.GetType(), type }));
            }
            // return the converted value
            return converter2.ConvertTo(value, type);
        }
        #endregion



        /// <summary>
        /// Get the attribute or a default instance of the attribute
        /// if the Column attribute do not contain the attribute
        /// </summary>
        /// <typeparam name="T">
        /// Attribute type
        /// </typeparam>
        /// <param name="table">
        /// Column to search for the attribute on.
        /// </param>
        /// <returns>
        /// The found attribute or a default 
        /// instance of the attribute of type T
        /// </returns>
        public static T GetAttributeOrDefault<T>(this MetaColumn column) where T : Attribute, new()
        {
            return column.Attributes.OfType<T>().DefaultIfEmpty(new T()).FirstOrDefault();
        }
    }
}
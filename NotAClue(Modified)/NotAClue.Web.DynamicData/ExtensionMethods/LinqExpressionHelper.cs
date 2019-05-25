using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.DynamicData;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;

namespace NotAClue.Web.DynamicData
{
    public static class LinqExpressionHelper
    {
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
        internal static IQueryable GetQueryFilteredByParent(this IQueryable sourceQuery, MetaForeignKeyColumn fkColumn, String fkSelectedValue)
        {
            // if no filter value return the query
            if (String.IsNullOrEmpty(fkSelectedValue))
                return sourceQuery;

            // {RequiredPlots}
            var parameterExpression = Expression.Parameter(sourceQuery.ElementType, fkColumn.ParentTable.Name);

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
        internal static Expression BuildWhereClause(MetaForeignKeyColumn fkColumn, ParameterExpression parameterExpression, string fkSelectedValue)
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
                //string keyName = fkColumn.ParentTable.Name + "." + fkColumn.ParentTable.PrimaryKeyColumns[i++].Name;
                string keyName = fkColumn.Name + "." + fkColumn.ParentTable.PrimaryKeyColumns[i++].Name;

                // Build property expression 
                // i.e. {RequiredPlots.Builders.Id}
                Expression propertyExpression = BuildPropertyExpression(parameterExpression, keyName);

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
        internal static Expression BuildPropertyExpression(Expression parameterExpression, string propertyName)
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
            // {RequiredPlots.Developers.Id}
            return expression;
        }

        public static MethodCallExpression BuildSingleItemQuery(IQueryable query, MetaTable metaTable, string[] primaryKeyValues)
        {
            // Items.Where(row => row.ID == 1)
            var whereCall = BuildItemsQuery(query, metaTable, metaTable.PrimaryKeyColumns, primaryKeyValues);
            // Items.Where(row => row.ID == 1).Single()
            var singleCall = Expression.Call(typeof(Queryable), "Single", new Type[] { metaTable.EntityType }, whereCall);

            return singleCall;
        }

        public static MethodCallExpression BuildItemsQuery(IQueryable query, MetaTable metaTable, IList<MetaColumn> columns, string[] values)
        {
            // row
            var rowParam = Expression.Parameter(metaTable.EntityType, "row");
            // row.ID == 1
            var whereBody = BuildWhereBody(rowParam, columns, values);
            // row => row.ID == 1
            var whereLambda = Expression.Lambda(whereBody, rowParam);
            // Items.Where(row => row.ID == 1)
            var whereCall = Expression.Call(typeof(Queryable), "Where", new Type[] { metaTable.EntityType }, query.Expression, whereLambda);

            return whereCall;
        }

        public static MethodCallExpression BuildWhereQuery(IQueryable query, MetaTable metaTable, MetaColumn column, string value)
        {
            // row
            var rowParam = Expression.Parameter(metaTable.EntityType, column.Name);
            // row.ID == 1
            var whereBody = BuildWhereBodyFragment(rowParam, column, value);
            // row => row.ID == 1
            var whereLambda = Expression.Lambda(whereBody, rowParam);
            // Items.Where(row => row.ID == 1)
            var whereCall = Expression.Call(typeof(Queryable), "Where", new Type[] { metaTable.EntityType }, query.Expression, whereLambda);

            return whereCall;
        }

        public static MethodCallExpression BuildCustomQuery(IQueryable query, MetaTable metaTable, MetaColumn column, string value, QueryType queryType)
        {
            // row
            var rowParam = Expression.Parameter(metaTable.EntityType, metaTable.Name);

            // row.DisplayName
            var property = Expression.Property(rowParam, column.Name);

            // "prefix"
            var constant = Expression.Constant(value);

            // row.DisplayName.StartsWith("prefix")
            var startsWithCall = Expression.Call(property, typeof(string).GetMethod(queryType.ToString(), new Type[] { typeof(string) }), constant);

            // row => row.DisplayName.StartsWith("prefix")
            var whereLambda = Expression.Lambda(startsWithCall, rowParam);

            // Customers.Where(row => row.DisplayName.StartsWith("prefix"))
            var whereCall = Expression.Call(typeof(Queryable), "Where", new Type[] { metaTable.EntityType }, query.Expression, whereLambda);

            return whereCall;
        }

        public static BinaryExpression BuildWhereBody(ParameterExpression parameter, IList<MetaColumn> columns, string[] values)
        {
            // row.ID == 1
            var whereBody = BuildWhereBodyFragment(parameter, columns[0], values[0]);
            for (int i = 1; i < values.Length; i++)
            {
                // row.ID == 1 && row.ID2 == 2
                whereBody = Expression.AndAlso(whereBody, BuildWhereBodyFragment(parameter, columns[i], values[i]));
            }

            return whereBody;
        }

        private static BinaryExpression BuildWhereBodyFragment(ParameterExpression parameter, MetaColumn column, string value)
        {
            // row.ID
            var property = Expression.Property(parameter, column.Name);
            // row.ID == 1
            return Expression.Equal(property, Expression.Constant(ChangeValueType(column, value)));
        }

        private static object ChangeValueType(MetaColumn column, string value)
        {
            if (column.ColumnType == typeof(Guid))
                return new Guid(value);
            else
                return Convert.ChangeType(value, column.TypeCode, CultureInfo.InvariantCulture);
        }

        public static Expression GetValue(Expression exp)
        {
            Type realType = GetUnderlyingType(exp.Type);
            if (realType == exp.Type)
                return exp;

            return Expression.Convert(exp, realType);
        }

        private static Type RemoveNullableFromType(Type type)
        {
            return Nullable.GetUnderlyingType(type) ?? type;
        }

        public static Type GetUnderlyingType(Type type)
        {
            return RemoveNullableFromType(type);
        }

        public static Expression Join(IEnumerable<Expression> expressions, Func<Expression, Expression, Expression> joinFunction)
        {
            Expression result = null;
            foreach (Expression e in expressions)
            {
                if (e == null)
                    continue;

                if (result == null)
                    result = e;
                else
                    result = joinFunction(result, e);
            }
            return result;
        }

        public static Expression CreatePropertyExpression(Expression parameterExpression, string propertyName)
        {
            Expression propExpression = null;
            string[] props = propertyName.Split('.');
            foreach (var p in props)
            {
                if (propExpression == null)
                    propExpression = Expression.PropertyOrField(parameterExpression, p);
                else
                    propExpression = Expression.PropertyOrField(propExpression, p);
            }
            return propExpression;
        }

        private static bool TypeAllowsNull(Type type)
        {
            return Nullable.GetUnderlyingType(type) != null || !type.IsValueType;
        }

        /// <summary>
        /// Changes the type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="type">The type to convert to.</param>
        /// <returns>The value converted to the type.</returns>
        internal static object ChangeType(object value, Type type)
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
    }
}

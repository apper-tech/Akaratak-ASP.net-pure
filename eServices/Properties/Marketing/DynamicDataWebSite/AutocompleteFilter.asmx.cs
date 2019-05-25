using System;
using System.Diagnostics;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Web.DynamicData;
using System.Web.Services;
using AjaxControlToolkit;
using System.Collections.Generic;
using DynamicDataLibrary;
using DynamicDataLibrary.Attributes;

namespace DynamicDataWebSite
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    public class AutocompleteFilterService : System.Web.Services.WebService
    {
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            MetaTable table = GetTable(contextKey);

            List<IQueryable> ListOfQueryable = BuildFilterQuery(table, prefixText, count);

            var values = new List<String>();
            foreach (IQueryable queryable in ListOfQueryable)
            {
                foreach (var row in queryable)
                {
                    values.Add(CreateAutoCompleteItem(table, row));
                }
            }

            return values.ToArray();
        }

        private static List<IQueryable> BuildFilterQuery(MetaTable table, string prefixText, int maxCount)
        {
            IQueryable query = table.GetQuery();

            var entityParam = Expression.Parameter(table.EntityType, "row");

            string[] columnNames;
            //int idPart;

            ColumnsToSearchWithAutoCompleteAttribute columns = table.Attributes.OfType<ColumnsToSearchWithAutoCompleteAttribute>().FirstOrDefault();
            if (columns != null && columns.ColumnsToSearchAt != null && columns.ColumnsToSearchAt.Length > 0)

            //table.Name == "Employees" && 
            //int.TryParse(prefixText, out idPart))
            {
                columnNames = columns.ColumnsToSearchAt;
            }
            else
            {
                // row.DisplayName
                columnNames = new string[] { table.DisplayColumn.Name };
            }

            List<IQueryable> res = new List<IQueryable>();
            foreach (string columnName in columnNames)
            {
                // row.DisplayName
                MemberExpression property = Expression.Property(entityParam, columnName);
                // "prefix"
                ConstantExpression constant = Expression.Constant(prefixText);
                // row.DisplayName.StartsWith("prefix")
                MethodCallExpression containsCall = Expression.Call(property, typeof(string).GetMethod("Contains", new Type[] { typeof(string) }), constant);

                // row => row.DisplayName.StartsWith("prefix")
                LambdaExpression whereLambda = Expression.Lambda(containsCall, entityParam);

                // Customers.Where(row => row.DisplayName.StartsWith("prefix"))
                MethodCallExpression whereCall = Expression.Call(
                    typeof(Queryable),
                    "Where",
                    new Type[] { table.EntityType },
                    query.Expression,
                    whereLambda);
                // Customers.Where(row => row.DisplayName.StartsWith("prefix")).Take(20)
                MethodCallExpression takeCall = Expression.Call(typeof(Queryable), "Take", new Type[] { table.EntityType }, whereCall, Expression.Constant(maxCount));

                res.Add(query.Provider.CreateQuery(takeCall));
            }

            return res;
        }

        public static string GetContextKey(MetaTable parentTable)
        {
            return String.Format("{0}#{1}", parentTable.DataContextType.FullName, parentTable.Name);
        }

        public static MetaTable GetTable(string contextKey)
        {
            string[] param = contextKey.Split('#');
            Debug.Assert(param.Length == 2, String.Format("The context key '{0}' is invalid", contextKey));
            if (param[0] == "System.Data.Objects.ObjectContext")
                param[0] = "System.Data.Objects.ObjectContext, System.Data.Entity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
            else if (param[0] == "System.Data.Entity.Core.Objects.ObjectContext")
            {
                param[0] = "System.Data.Entity.Core.Objects.ObjectContext";
                throw new NotImplementedException("You should check for \"System.Data.Entity.Core.Objects.ObjectContext\" full name!");
            }
            
            Type type = Type.GetType(param[0]);
            return MetaModel.GetModel(type).GetTable(param[1], type);
        }

        private static string CreateAutoCompleteItem(MetaTable table, object row)
        {
            return AutoCompleteExtender.CreateAutoCompleteItem(table.GetDisplayString(row), table.GetPrimaryKeyString(row));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Reflection;

namespace DynamicDataWebSite
{
    public static class BLUtilities
    {
        public static String DomainName
        {
            get
            {
                return String.IsNullOrEmpty(ConfigurationManager.AppSettings["DomainName"]) ? String.Empty : ConfigurationManager.AppSettings["DomainName"];
            }
        }

        //public static String DomainAccountUserName
        //{
        //    get
        //    {
        //        return String.IsNullOrEmpty(ConfigurationManager.AppSettings["DomainAccountUserName"]) ? String.Empty : ConfigurationManager.AppSettings["DomainAccountUserName"];
        //    }
        //}

        //public static String DomainAccountPassword
        //{
        //    get
        //    {
        //        return String.IsNullOrEmpty(ConfigurationManager.AppSettings["DomainAccountPassword"]) ? String.Empty : ConfigurationManager.AppSettings["DomainAccountPassword"];
        //    }
        //}

        public static DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable result = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null)
            {
                return result;
            }

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow

                if (oProps == null)
                {
                    oProps = ((System.Type)rec.GetType()).GetProperties();

                    foreach (PropertyInfo pi in oProps)
                    {
                        System.Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        result.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                if (oProps != null)
                {
                    DataRow dr = result.NewRow();

                    foreach (PropertyInfo pi in oProps)
                    {
                        dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue(rec, null);
                    }

                    result.Rows.Add(dr);
                }
            
            }

            return result;
        }
    }
}

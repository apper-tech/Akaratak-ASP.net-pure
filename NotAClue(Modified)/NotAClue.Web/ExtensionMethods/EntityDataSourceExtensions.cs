using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.ComponentModel;

namespace NotAClue.Web
{
    public static class EntityDataSourceExtensions
    {
        public static TEntity GetEntityAs<TEntity>(this object dataItem)
            where TEntity : class
        {
            var entity = dataItem as TEntity;

            if (entity != null)
                return entity;

            var td = dataItem as ICustomTypeDescriptor;

            if (td != null)
                return (TEntity)td.GetPropertyOwner(null);

            return null;
        }

        public static Object GetEntity(this object dataItem)
        {
            var td = dataItem as ICustomTypeDescriptor;

            if (td != null)
                return td.GetPropertyOwner(null);

            return null;
        }
    }
}
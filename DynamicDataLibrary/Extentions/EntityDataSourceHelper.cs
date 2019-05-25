using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDataLibrary
{
    public class EntityDataSourceHelper
    {
        public static object GetItemObject(object dataItem)
        {
            var td = dataItem as ICustomTypeDescriptor;
            if (td != null)
            {
                return td.GetPropertyOwner(null);
            }
            //return null;
            return dataItem;
        }

        public static TEntity GetItemObject<TEntity>(object dataItem)
            where TEntity : class
        {
            var entity = dataItem as TEntity;
            if (entity != null)
            {
                return entity;
            }
            var td = dataItem as ICustomTypeDescriptor;
            if (td != null)
            {
                return (TEntity)td.GetPropertyOwner(null);
            }
            return null;
        }
    }
}

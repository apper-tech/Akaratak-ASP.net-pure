using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDataLibrary.Attributes
{
   public class LocalizedForeignKeyAttribute : Attribute
    {
        string resourceFileName;
        bool dataLoadAccess;
        string tableName;
        public string ResourceFileName
        {
            get
            {
                return resourceFileName;
            }

            set
            {
                resourceFileName = value;
            }
        }

        public bool DataLoadAccess
        {
            get
            {
                return dataLoadAccess;
            }

            set
            {
                dataLoadAccess = value;
            }
        }

        public string TableName
        {
            get
            {
                return tableName;
            }

            set
            {
                tableName = value;
            }
        }
    }
}

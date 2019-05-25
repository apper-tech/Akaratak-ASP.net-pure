using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DynamicDataLibrary.Attributes
{
    public class ContributionLimitedToCreatorAttribute : Attribute
    {
        private string creatorUserNameFiled = null;
        public string CreatorUserNameFiled
        {
            get { return creatorUserNameFiled; }
        }

        public ContributionLimitedToCreatorAttribute(string createdByUserNameFiled)
        {
            this.creatorUserNameFiled = createdByUserNameFiled;
        }

        public bool CanContributeOnDataItem(object dataItem)
        {
            if (this.CreatorUserNameFiled == null)
                return true;
            object item = EntityDataSourceHelper.GetItemObject(dataItem);
            if(item==null)
            {
                return false;
            }
            string fieldName = this.CreatorUserNameFiled;
            string[] fieldsNames = fieldName.Split('.');
            object val = item;
            foreach (string part in fieldsNames)
            {
                System.Reflection.PropertyInfo pi = val.GetType().GetProperty(part);
                val = pi.GetValue(val);
            }
            if (Convert.ToString(HttpContext.Current.Session["CurrentUserID"]) == Convert.ToString(val))
                return true;
            else
                return false;
        }
    }
}

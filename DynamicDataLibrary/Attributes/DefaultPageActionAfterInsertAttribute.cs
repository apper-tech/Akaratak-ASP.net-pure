using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.DynamicData;

namespace DynamicDataLibrary.Attributes
{
    public enum PageActionAfterInsert { Details, List, Edit, ListDetails,Default }
    public class DefaultPageActionAfterInsertAttribute : Attribute
    {
        public PageActionAfterInsert PageAction { set; get; }

        public DefaultPageActionAfterInsertAttribute(PageActionAfterInsert pageAction)
        {
            this.PageAction = pageAction;
        }
    }
}
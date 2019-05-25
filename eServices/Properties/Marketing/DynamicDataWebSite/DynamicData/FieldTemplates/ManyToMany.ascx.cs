using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite
{
    public partial class ManyToManyField : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            object entity;
            ICustomTypeDescriptor rowDescriptor = Row as ICustomTypeDescriptor;
            if (rowDescriptor != null)
            {
                // Get the real entity from the wrapper
                entity = rowDescriptor.GetPropertyOwner(null);
            }
            else
            {
                entity = Row;
            }

            // Get the collection and make sure it's loaded
            var entityCollection = Column.EntityTypeProperty.GetValue(entity, null);
            var realEntityCollection = entityCollection as RelatedEnd;
            if (realEntityCollection != null && !realEntityCollection.IsLoaded)
            {
                realEntityCollection.Load();
            }


            // Bind the repeater to the list of children entities
            Repeater1.DataSource = entityCollection;
            Repeater1.DataBind();
        }

        public override Control DataControl
        {
            get
            {
                return Repeater1;
            }
        }


    }
}

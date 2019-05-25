using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using DynamicDataLibrary;

namespace DynamicDataWebSite
{
    public partial class Default_InsertEntityTemplate : System.Web.DynamicData.EntityTemplateUserControl
    {
        private MetaColumn currentColumn;

        protected override void OnLoad(EventArgs e)
        {
            //To prevent possible cycling in FormView & GridView filed templates
            if (this.TableAlreadyAddedAtParent())
                return;
        
            if(direct!=null)
            {
                if(System.Threading.Thread.CurrentThread.CurrentCulture.Name.Contains("ar"))
                {
                    direct.Attributes.Add("dir", "rtl");
                }
            }
            foreach (MetaColumn column in Table.GetScaffoldColumns(Mode, ContainerType))
            {
                if (column.IsColumnOfParent(this))
                    continue;

                currentColumn = column;
                Control item = new DefaultEntityTemplate._NamingContainer();
                EntityTemplate1.ItemTemplate.InstantiateIn(item);
                EntityTemplate1.Controls.Add(item);
            }
        }

        protected void Label_Init(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.Text = currentColumn.DisplayName;
        }

        protected void Label_PreRender(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            DynamicControl dynamicControl = (DynamicControl)label.FindControl("DynamicControl");
            FieldTemplateUserControl ftuc = dynamicControl.FieldTemplate as FieldTemplateUserControl;
            if (ftuc != null && ftuc.DataControl != null)
            {
                label.AssociatedControlID = ftuc.DataControl.GetUniqueIDRelativeTo(label);
            }
        }

        protected void DynamicControl_Init(object sender, EventArgs e)
        {
            DynamicControl dynamicControl = (DynamicControl)sender;
            dynamicControl.DataField = currentColumn.Name;
        }

        protected void DynamicControl_PreRender(object sender, EventArgs e)
        {
            DynamicControl dynamicControl = (DynamicControl)sender;
            dynamicControl.ValidationGroup = this.ValidationGroup;
        }
    }
}

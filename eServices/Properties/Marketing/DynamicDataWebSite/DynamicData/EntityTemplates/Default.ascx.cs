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
    public partial class DefaultEntityTemplate : System.Web.DynamicData.EntityTemplateUserControl
    {
        private MetaColumn currentColumn;

        protected override void OnLoad(EventArgs e)
        {
            //To prevent possible cycling in FormView & GridView filed templates
            if (this.TableAlreadyAddedAtParent())
                return;

            foreach (MetaColumn column in Table.GetScaffoldColumns(Mode, ContainerType))
            {
                if (column.IsColumnOfParent(this))
                    continue;

                currentColumn = column;
                Control item = new _NamingContainer();
                EntityTemplate1.ItemTemplate.InstantiateIn(item);
                EntityTemplate1.Controls.Add(item);
            }
        }

        protected void Label_Init(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.Text = currentColumn.DisplayName;
        }

        protected void DynamicControl_Init(object sender, EventArgs e)
        {
            DynamicControl dynamicControl = (DynamicControl)sender;
            dynamicControl.DataField = currentColumn.Name;
        }

        public class _NamingContainer : Control, INamingContainer { }

    }
}

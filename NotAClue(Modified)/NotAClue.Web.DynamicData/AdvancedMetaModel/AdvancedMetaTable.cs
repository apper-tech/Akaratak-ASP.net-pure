using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.DynamicData;
using System.Web.DynamicData.ModelProviders;
using System.Web.UI.WebControls;
using NotAClue.ComponentModel.DataAnnotations;
using System.Web.UI;
using System.Web;

namespace NotAClue.Web.DynamicData
{
    public class AdvancedMetaTable : MetaTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedMetaTable"/> class.
        /// </summary>
        /// <param name="metaModel">The meta model.</param>
        /// <param name="tableProvider">The table provider.</param>
        public AdvancedMetaTable(MetaModel metaModel, TableProvider tableProvider) :
            base(metaModel, tableProvider) { }

        /// <summary>
        /// Initializes data that may not be available when the constructor is called.
        /// </summary>
        protected override void Initialize() { base.Initialize(); }

        //protected override MetaColumn CreateColumn(ColumnProvider columnProvider)
        //{
        //    return new AdvancedMetaColumn(this, columnProvider);
        //}

        //protected override MetaChildrenColumn CreateChildrenColumn(ColumnProvider columnProvider)
        //{
        //    return new AdvancedMetaChildrenColumn(this, columnProvider);
        //}

        //protected override MetaForeignKeyColumn CreateForeignKeyColumn(ColumnProvider columnProvider)
        //{
        //    return new AdvancedMetaForeignKeyColumn(this, columnProvider);
        //}

        /// <summary>
        /// Gets the scaffold columns.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="containerType">Type of the container.</param>
        /// <returns></returns>
        public override IEnumerable<MetaColumn> GetScaffoldColumns(DataBoundControlMode mode, ContainerType containerType)
        {
            var pageTemplate = FieldTemplateExtensionMethods.GetPageTemplate();
            var visibleColumns = from c in base.GetScaffoldColumns(mode, containerType)
                                 where !c.IsHidden(pageTemplate)
                                 select c;

            return visibleColumns;
        }

        public override IEnumerable<MetaColumn> GetFilteredColumns()
        {
            var filteredColumns = base.GetFilteredColumns();
            var filteredColumnsSorted = from f in filteredColumns
                                        orderby f.GetAttributeOrDefault<FilterAttribute>(), f.Name
                                        select f;

            return filteredColumnsSorted;
        }
    }
}
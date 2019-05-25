using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.DynamicData;
using NotAClue.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace NotAClue.Web.DynamicData
{
    public static class TemplateGeneratorExtensionMethods
    {
        /// <summary>
        /// Gets the entity column.
        /// </summary>
        /// <param name="fkColumn">The fk column.</param>
        /// <returns></returns>
        public static MetaColumn GetEntityColumn(this MetaColumn fkColumn)
        {
            foreach (var column in fkColumn.Table.Columns.OfType<MetaForeignKeyColumn>())
            {
                if (column.Provider.Association != null 
                    && column.Provider.Association.ForeignKeyNames.Contains(fkColumn.Name))
                    return column;
            }

            return null;
        }
    }
}

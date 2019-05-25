using System.Web.DynamicData;
using System.Web.DynamicData.ModelProviders;

namespace NotAClue.Web.DynamicData
{
    public class AdvancedMetaModel : MetaModel
    {
        /// <summary>
        /// Creates the table.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        protected override MetaTable CreateTable(TableProvider provider)
        {
            return new AdvancedMetaTable(this, provider);
        }

    }
}

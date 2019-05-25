using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace DynamicDataModel.Model
{
    using DynamicDataLibrary.Attributes;
    using App_LocalResources;
    using NotAClue.ComponentModel.DataAnnotations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Security;

    [MetadataType(typeof(Meta_Country))]
    [Serializable]
    public partial class Country
    {
        public override string ToString()
        {
            return
                String.Format("{0} ({1})",
                this.Country_Name, this.Country_Native_Name);

        }
    }
    public class Meta_Country
    {

    }
}
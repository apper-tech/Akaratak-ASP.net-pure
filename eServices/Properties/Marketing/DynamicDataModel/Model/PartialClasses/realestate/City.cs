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

    [MetadataType(typeof(Meta_City))]
    [Serializable]
    public partial class City
    {
        public override string ToString()
        {
            return
                this.City_Name!=this.City_Native_Name?
                String.Format("{0} ({1})",
                this.City_Name, this.City_Native_Name):this.City_Name;

        }
    }
    public class Meta_City
    {

    }
}
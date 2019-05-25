using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicDataModel.Model
{
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(Meta_Property_Type))]
    [Serializable]
    public partial class Property_Type
    {
    }
    public class Meta_Property_Type { }
}
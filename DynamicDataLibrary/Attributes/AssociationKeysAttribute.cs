using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicDataLibrary.Attributes
{
    public class AssociationKeysAttribute : System.ComponentModel.DataAnnotations.Schema.ColumnAttribute
    {
        //
        // Summary:
        //     Gets or sets members of this entity class to represent the key values on
        //     this side of the association.
        //
        public string ThisKey { get; set; }
        //
        // Summary:
        //     Gets or sets one or more members of the target entity class as key values
        //     on the other side of the association.
        //
        public string OtherKey { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicDataLibrary.ModelRelated
{
    public interface IAuditable
    {
        string CreatedBy { get; set; }
        Nullable<System.DateTime> CreatedDateTime { get; set; }
        string UpdatedBy { get; set; }
        Nullable<System.DateTime> UpdatedDateTime { get; set; }
    }
}

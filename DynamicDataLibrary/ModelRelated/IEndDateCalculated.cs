using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicDataLibrary.ModelRelated
{
    public interface IEndDateCalculated
    {
        int Period { set; get; }
        System.DateTime StartDate { set; get; }
        System.DateTime EndDate { set; get; }
    }
}
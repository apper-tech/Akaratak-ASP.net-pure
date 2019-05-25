using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace DynamicDataLibrary.ModelRelated
{
    public static class CalculateEndDate
    {
        public static void SetEndDate(IEnumerable<Object> list)
        {
            foreach (ObjectStateEntry item in list)
            {
                var entity = item.Entity as IEndDateCalculated;
                if (entity != null)
                {
                    entity.EndDate = entity.StartDate.AddDays(entity.Period -1);
                }
            }
        }
    }
}
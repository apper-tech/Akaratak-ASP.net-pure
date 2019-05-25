using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDataLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
  public  class EnroleUserAttribute:Attribute
    {
        string defaultRole;

        public string DefaultRole
        {
            get
            {
                return defaultRole;
            }

            set
            {
                defaultRole = value;
            }
        }
    }
}

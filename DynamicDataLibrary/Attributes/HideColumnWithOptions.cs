using NotAClue.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDataLibrary.Attributes
{
    public class HideColumnWithOptions: Attribute
    {
        bool ifEmpty;
        PageTemplate template;

        public bool IfEmpty
        {
            get
            {
                return ifEmpty;
            }

            set
            {
                ifEmpty = value;
            }
        }

        public PageTemplate Template
        {
            get
            {
                return template;
            }

            set
            {
                template = value;
            }
        }
    }
}

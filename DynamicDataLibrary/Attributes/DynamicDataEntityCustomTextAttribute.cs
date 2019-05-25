using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDataLibrary
{
    public class DynamicDataEntityCustomTextAttribute : Attribute
    {
        public string NewItemCommandText { get; set; }
        public string NewItemLinkText { get; set; }
        public string DeleteItemCommandText { get; set; }
        public string DeleteConfirmationMesssage { get; set; }
        public string ItemDetailsCommandText { get; set; }
        public string EditItemCommandTextAtList { get; set; }
        public string EditItemCommandTextAtEdit { get; set; }

        public string NewItemTitleFormat { get; set; }
        public string EditItemTitle { get; set; }
        public string ItemDetailsTitleFormat { get; set; }
        
        public string NoRecordsAtList { get; set; }
    }
}

using System;

namespace NotAClue.ComponentModel.DataAnnotations
{
    [Flags]
    public enum PageTemplate
    {
        // standard page templates
        Details     = 0x01,
        Edit        = 0x02,
        Insert      = 0x04,
        List        = 0x08,
        ListDetails = 0x10,
        // custom page templates
        DetailsList = 0x20,
        EditList    = 0x40,
        Unknown     = 0x80,
    }

    public static class CombinedPageTemplates
    {
        /// <summary>
        /// Read Only access (AllowActions.Details | AllowActions.List)
        /// </summary>
        public const PageTemplate All =
            PageTemplate.Details |
            PageTemplate.Edit |
            PageTemplate.Insert |
            PageTemplate.List |
            PageTemplate.ListDetails |
            PageTemplate.DetailsList |
            PageTemplate.EditList;
    }
}

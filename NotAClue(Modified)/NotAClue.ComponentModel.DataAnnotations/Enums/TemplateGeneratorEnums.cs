using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotAClue.ComponentModel.DataAnnotations
{
    public enum ListViewTemplateMode
    {
        Normal,
        Alternate,
        Edit,
        Insert,
        Selected,
    }

    [Flags]
    public enum CommandButtons // = 0x7
    {
        NoButtons   = 0x00,
        Edit        = 0x01,
        Delete      = 0x02,
        Details     = 0x04,
        Insert      = 0x08,
        Refresh     = 0x10
    }

    /// <summary>
    /// Template type for the ListViewPageColumnGenerator
    /// </summary>
    public enum ListViewTemplateType
    {
        AlternatingItemTemplate,
        EditItemTemplate,
        EmptyDataTemplate,
        EmptyItemTemplate,
        GroupSeparatorTemplate,
        GroupTemplate,
        InsertItemTemplate,
        ItemSeparatorTemplate,
        ItemTemplate,
        LayoutTemplate,
        SelectedItemTemplate,
    }

    /// <summary>
    /// Template type for the ListViewPageColumnGenerator
    /// </summary>
    public enum FormViewTemplateType
    {
        ItemTemplate,
        EditItemTemplate,
        InsertItemTemplate,
        EmptyDataTemplate,
        HeaderTemplate,
        FooterTemplate,
        PagerTemplate,
    }
}

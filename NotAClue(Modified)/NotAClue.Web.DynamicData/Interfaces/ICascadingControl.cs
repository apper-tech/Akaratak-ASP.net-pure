using System;
namespace NotAClue.Web.DynamicData
{
    public interface ICascadingControl
    {
        System.Web.DynamicData.MetaForeignKeyColumn ChildColumn { get; }
        System.Web.DynamicData.MetaForeignKeyColumn ParentColumn { get; }
        ICascadingControl ParentControl { get; set; }
        String SelectedValue { get; }

        void RaiseSelectedIndexChanged(String value, String text);
        event SelectionChangedEventHandler SelectionChanged;
    }
}

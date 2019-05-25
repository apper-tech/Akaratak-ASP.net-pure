using System;
using System.Web.UI.WebControls;

namespace NotAClue.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class GroupItemAttribute : Attribute
    {
        public int Position { get; set; }

        internal static FilterAttribute Default = new FilterAttribute();
        public GroupItemAttribute() : this(0) { }

        public GroupItemAttribute(int position)
        {
            Position = position;
        }

        #region IComparable<FilterAttribute> Members
        public int CompareTo(GroupItemAttribute other)
        {
            return Position - other.Position;
        }
        #endregion
    }
}
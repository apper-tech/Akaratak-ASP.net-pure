using System;

namespace NotAClue.ComponentModel.DataAnnotations
{
    /// <summary>
    /// An attribute used to specify the filtering behavior for a column.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class FilterAttribute : Attribute, IComparable<FilterAttribute>
    {
        internal static FilterAttribute Default = new FilterAttribute();

        public FilterAttribute()
        {
            Order = 0;
            Hidden = false;
        }

        /// <summary>
        /// The ordering of a filter. Negative values are allowed.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FilterAttribute"/> is hidden.
        /// </summary>
        /// <value><c>true</c> if hidden; otherwise, <c>false</c>.</value>
        public Boolean Hidden { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        /// <remarks></remarks>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the hidden for roles.
        /// </summary>
        /// <value>The hidden for roles.</value>
        /// <remarks>
        /// If set only user in these roles will have the filter hidden, 
        /// otherwise if not set then filter will just be hidden.
        /// </remarks>
        public String[] HiddenForRoles { get; set; }

        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        /// <value>The default value.</value>
        /// <remarks></remarks>
        public String DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the group that this filter will be in.
        /// </summary>
        /// <value>The group.</value>
        public String Group { get; set; }

        #region IComparable<FilterAttribute> Members

        public int CompareTo(FilterAttribute other)
        {
            return Order - ((FilterAttribute)other).Order;
        }
        /// <summary>
        /// if the filter is hidden
        /// </summary>
        /// <param name="h">is hidden</param>
        public FilterAttribute(bool h)
        {
            Hidden = h;
        }
       
        #endregion
    }
}

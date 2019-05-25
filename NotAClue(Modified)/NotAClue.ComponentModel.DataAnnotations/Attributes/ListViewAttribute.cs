using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace NotAClue.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ListViewAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets a value indicating whether [enable details].
        /// </summary>
        /// <value><c>true</c> if [enable details]; otherwise, <c>false</c>.</value>
        /// <remarks></remarks>
        public Boolean EnableDetails { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enable insert].
        /// </summary>
        /// <value><c>true</c> if [enable insert]; otherwise, <c>false</c>.</value>
        /// <remarks></remarks>
        public Boolean EnableInsert { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enable delete].
        /// </summary>
        /// <value><c>true</c> if [enable delete]; otherwise, <c>false</c>.</value>
        /// <remarks></remarks>
        public Boolean EnableDelete { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enable update].
        /// </summary>
        /// <value><c>true</c> if [enable update]; otherwise, <c>false</c>.</value>
        /// <remarks></remarks>
        public Boolean EnableUpdate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enable refresh].
        /// </summary>
        /// <value><c>true</c> if [enable refresh]; otherwise, <c>false</c>.</value>
        /// <remarks></remarks>
        public Boolean EnableRefresh { get; set; }

        /// <summary>
        /// Gets or sets the insert position.
        /// </summary>
        /// <value>The insert position.</value>
        /// <remarks></remarks>
        public InsertItemPosition InsertPosition { get; set; }
        
        public ListViewAttribute() : this(false) { }

        public ListViewAttribute(Boolean enableInsert)
        {
            EnableInsert = enableInsert;
        }
    }
}
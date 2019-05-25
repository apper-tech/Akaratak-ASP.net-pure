using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace NotAClue.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class GroupsAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        /// <remarks></remarks>
        public String Title { get; set; }

        /// <summary>
        /// Gets or sets the steps.
        /// </summary>
        /// <value>The steps.</value>
        /// <remarks></remarks>
        public SortedList<int, String> Groups { get; private set; }

        public GroupsAttribute(String title, params String[] steps)
        {
            Title = title;

            Groups = new SortedList<int, String>();
            int i = 0;
            foreach (var item in steps)
            {
                Groups.Add(i++, item);
            }
        }
    }
}
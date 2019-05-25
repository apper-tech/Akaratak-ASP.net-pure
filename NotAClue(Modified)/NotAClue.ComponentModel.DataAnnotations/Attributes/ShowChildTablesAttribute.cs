/// <copyright file="ShowChildTablesAttribute.cs" company="NotAClue? Studios">
/// Copyright (c) 2011 All Right Reserved, http://csharpbits.notaclue.net/
///
/// This source is subject to the per project license unless otherwise agreed.
/// All other rights reserved.
///
/// </copyright>
/// <author>Stephen J Naughton</author>
/// <email>steve@notaclue.net</email>
/// <project>NotAClue.ComponentModel.DataAnnotations</project>
/// <date>24/07/2011</date>
using System;

namespace NotAClue.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ShowChildTablesAttribute : Attribute
    {
        ///// <summary>
        ///// Default constructor.
        ///// </summary>
        //public ShowChildTablesAttribute() 
        //{
        //    EnableInLineEdit = true;
        //}
        /// <summary>
        /// If true the children column is enabled.
        /// </summary>
        public Boolean Enabled { get; set; }

        /// <summary>
        /// If true enables Delete on control. 
        /// </summary>
        public Boolean EnableDelete { get; set; }

        /// <summary>
        /// If true enables Insert on control.
        /// </summary>
        public Boolean EnableInsert { get; set; }

        /// <summary>
        /// If true enables Update on control.
        /// </summary>
        public Boolean EnableUpdate { get; set; }

        /// <summary>
        /// If true enables Details on control.
        /// </summary>
        public Boolean EnableDetails { get; set; }

        /// <summary>
        /// If true enables Insert on control.
        /// </summary>
        public Boolean EnableRefresh { get; set; }

        /// <summary>
        /// Indicates the sort of the column.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Sets the tab name.
        /// </summary>
        public String TabName { get; set; }

        /// <summary>
        /// Add an insert link with parent keys
        /// </summary>
        public Boolean InsertLink { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [disable in line edit].
        /// </summary>
        /// <value><c>true</c> if [disable in line edit]; otherwise, <c>false</c>.</value>
        /// <remarks></remarks>
        public Boolean DisableInLineEdit { get; set; }

        /// <summary>
        /// Gets or sets the UIHint.
        /// </summary>
        /// <value>The UIHint.</value>
        public String UIHint { get; set; }

        #region IComparable Members
        public int CompareTo(object obj)
        {
            return Order - ((ShowChildTablesAttribute)obj).Order;
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotAClue.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MultiColumnSearchAttribute : Attribute
    {
        public String[] Columns { get; private set; }

        public int Width { get; private set; }

        public MultiColumnSearchAttribute() { }

        public MultiColumnSearchAttribute(params String[] columns)
        {
            Columns = columns;
        }

        public MultiColumnSearchAttribute(int width, params String[] columns)
        {
            Width = width;
            Columns = columns;
        }
    }
}
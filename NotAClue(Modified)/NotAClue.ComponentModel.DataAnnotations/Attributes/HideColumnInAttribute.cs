﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace NotAClue.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class HideColumnInAttribute : Attribute
    {
        public PageTemplate PageTemplate { get; private set; }

        public HideColumnInAttribute() { }

        public HideColumnInAttribute(PageTemplate pageTemplate)
        {
            PageTemplate = pageTemplate;
        }
    }
}
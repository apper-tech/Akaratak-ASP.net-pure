using NotAClue.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace NotAClue.Web.DynamicData
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ConditionalUIHintAttribute : Attribute
    {
        public String UIHint { get; private set; }
        public PageTemplate PageTemplates { get; private set; }

        public ConditionalUIHintAttribute(String uiHint, PageTemplate pageTemplates)
        {
            UIHint = uiHint;
            PageTemplates = pageTemplates;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Web.DynamicData;
using AjaxControlToolkit.HTMLEditor;
using AjaxControlToolkit;

namespace DynamicDataLibrary
{
    /// <summary>
    /// Attribute to identify which column to use as a 
    /// parent column for the child column to depend upon
    /// </summary>
    /// <remarks>
    /// Original Idea from: http://csharpbits.notaclue.net/2009/06/html-editor-fieldtemplate-for-dynamic.html
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public class HtmlEditorAttribute : Attribute
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public HtmlEditorAttribute()
        {
        }

        public string textBoxCssClass;
        /// <summary>
        /// A css class override used to define a custom look 
        /// and feel for the HTMLEditor. See the HTMLEditor 
        /// Theming section for more details
        /// </summary>
        public String TextBoxCssClass
        {
            get { return this.textBoxCssClass; }
            set { this.textBoxCssClass = value; }
        }


        public string textBoxHeight = "180px";
        /// <summary>
        /// Sets the height of the body of the HTMLEditor 
        /// </summary>
        public string TextBoxHeight
        {
            get { return this.textBoxHeight; }
            set { this.textBoxHeight = value; }
        }

        public string textBoxWidth = "100%";
        /// <summary>
        /// Sets the width of the body of the HTMLEditor 
        /// </summary>
        public string TextBoxWidth
        {
            get { return this.textBoxWidth; }
            set { this.textBoxWidth = value; }
        }

        private bool displaySourceTab = true;
        public bool DisplaySourceTab
        {
            get { return this.displaySourceTab; }
            set { this.displaySourceTab = value; }
        }
    }
}
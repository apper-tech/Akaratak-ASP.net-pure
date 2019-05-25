using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.Design;

namespace NotAClue.Web.UI.WebControls
{
    class PopupClientDesigner : System.Web.UI.Design.ControlDesigner
    {
        //Component is the instance of the component or control that
        //this designer object is associated with. This property is 
        //inherited from System.ComponentModel.ComponentDesigner.
        private const String TAG = "<img src=\"{0}\" alt=\"{1}\" />";
        public override string GetDesignTimeHtml()
        {
            PopupClient popupClient = (PopupClient)Component;
            StringWriter sw = new StringWriter();
            HtmlTextWriter output = new HtmlTextWriter(sw);


            // Get a ClientScriptManager reference from the Page class.
            ClientScriptManager csm = popupClient.Page.ClientScript;

            // get the type for calendar
            Type type = this.GetType();
            String popupClientImage = csm.GetWebResourceUrl(type, "NotAClue.Web.UI.WebControls.ResourceFiles.NAC_PopupClient.png");

            output.Write(String.Format(TAG, popupClientImage, popupClient.ID));
            return sw.ToString();
        }
    }
}

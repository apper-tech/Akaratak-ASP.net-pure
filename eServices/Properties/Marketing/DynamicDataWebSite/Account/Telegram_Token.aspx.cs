using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace DynamicDataWebSite.Account
{
    public partial class Telegram_Token : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            byte[] key = new byte[8] { 15, 5, 22, 6, 4, 2, 0, 53 };
            byte[] iv = new byte[8] { 64, 28, 17, 63, 87, 53, 14, 4 };
            Crypto_String.Key = key;
            Crypto_String.Iv = iv;
            Options_Handler telegram_User = new Options_Handler();
            if (Request.QueryString.Count > 0)
            {
                string crypt = Request.QueryString[0];
                string decrypt = Crypto_String.Decrypt(crypt);
                telegram_User = Options_Handler.Deserialize_User(decrypt);
            }
        }
    }
}


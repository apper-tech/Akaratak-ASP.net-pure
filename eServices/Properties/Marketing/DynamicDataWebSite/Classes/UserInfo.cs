using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicDataWebSite
{
    public class UserInfo
    {
        private String userNeme = String.Empty;

        private String displayName = String.Empty;

        private Dictionary<String, String> permissions = new Dictionary<String, String>();

        public UserInfo(String userNeme, String displayName, Dictionary<String, String> permissions)
        {
            UserName = userNeme;

            DisplayName = displayName;

            Permissions = permissions;
        }

        public String UserName
        {
            set
            {
                userNeme = value;
            }
            get
            {
                return userNeme;
            }
        }

        public String DisplayName
        {
            set
            {
                displayName = value;
            }
            get
            {
                return displayName;
            }
        }

        public Dictionary<String, String> Permissions
        {
            set
            {
                permissions = value;
            }
            get
            {
                return permissions;
            }
        }
        public String EnglishPermissionsAsString
        {
            get
            {
                String data = String.Empty;

                try
                {
                    foreach (KeyValuePair<String, String> permissions in Permissions)
                    {
                        data = permissions.Key + CommonStrings.HTMLLineBreak + data;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return data;
            }
        }

        public String ArabicPermissionsAsString
        {
            get
            {
                String data = String.Empty;

                try
                {
                    foreach (KeyValuePair<String, String> permissions in Permissions)
                    {
                        data = permissions.Value + CommonStrings.HTMLLineBreak + data;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return data;
            }
        }
    }
}

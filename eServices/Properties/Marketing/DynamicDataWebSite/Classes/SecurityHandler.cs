using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.DirectoryServices;
using System.Reflection;
using System.Web.SessionState;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Host.SystemWeb;
using System.Net.Http;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Data;
using DynamicDataWebSite.DynamicData.FieldTemplates;
using System.Net.Mail;

namespace DynamicDataWebSite
{
    public static class SecurityHandler
    {
        public static string ErrorPath { get { return "~/Error?User_ID=" + HttpContext.Current.User.Identity.GetUserId(); } }
        public static string Data_Ext_User_ID = "1e98087e-72ed-4e5e-b2ae-df3952599935";
        private static string MailServer { get { return WebConfigurationManager.AppSettings["Host_MailServer"]; } }
        private static string InfoUserName { get { return WebConfigurationManager.AppSettings["Host_Mail_Info_Username"]; } }

        private static string InfoPassword { get { return WebConfigurationManager.AppSettings["Host_Mail_Info_Password"]; } }
        private static string SupportUsername { get { return WebConfigurationManager.AppSettings["Host_Mail_Support_Username"]; } }
        private static string SupportPassword { get { return WebConfigurationManager.AppSettings["Host_Mail_Support_Password"]; } }

        public static void SendConfirmMail(string txtTo, string subj, string body)
        {
            SendMail(InfoUserName, InfoPassword, MailServer, subj, body, txtTo);
        }
        public static void SendResetMail(string txtTo, string subj, string body)
        {
            SendMail(SupportUsername, SupportPassword, MailServer, subj, body, txtTo);
        }
        static void SendMail(string username, string password, string host, string title, string body, string recive)
        {
            MailMessage m = new MailMessage();
            SmtpClient sc = new SmtpClient();
            m.From = new MailAddress(username);
            m.To.Add(recive);
            m.Subject = title;
            m.Body += body;
            m.IsBodyHtml = true;
            sc.Host = host;
            string str1 = "gmail.com";
            string str2 = username.ToLower();
            if (str2.Contains(str1))
            {
                try
                {
                    sc.Port = 587;
                    sc.Credentials = new System.Net.NetworkCredential(username, password);
                    sc.EnableSsl = true;
                    sc.Send(m);
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                try
                {
                    sc.Port = 8889;
                    sc.Credentials = new System.Net.NetworkCredential(username, password);
                    sc.EnableSsl = false;
                    sc.Send(m);
                }
                catch (Exception ex)
                {
                }
            }
        }
        public static Boolean HasThePermission(String permission)
        {
            Boolean hasThePermission = false;

            try
            {
                hasThePermission = HasThePermission(HttpContext.Current.User.Identity.Name, permission);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return hasThePermission;
        }

        public static Boolean HasThePermission(String loginid, String permission)
        {
            Boolean hasThePermission = false;

            try
            {
                hasThePermission = Roles.IsUserInRole(loginid, permission);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return hasThePermission;
        }

        public static List<UserInfo> GetUsersInfo(String filterUserName)
        {
            List<UserInfo> usersInfo = new List<UserInfo>();

            try
            {
                using (DirectoryEntry entry = new DirectoryEntry(CommonStrings.LDAPProtocol + BLUtilities.DomainName))
                // , BLUtilities.DomainAccountUserName, BLUtilities.DomainAccountPassword))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(entry))
                    {
                        //searcher.Filter = "(&(objectCategory=person)(objectClass=user))";

                        searcher.Filter = "(&(&(objectCategory=person)(objectClass=user))((!userAccountControl:1.2.840.113556.1.4.803:=2)))";

                        searcher.PageSize = 1000;

                        Dictionary<String, String> systemPermissions = GetSystemPermissions();

                        String logInName = String.Empty;

                        Dictionary<String, String> permissions = new Dictionary<String, String>();

                        foreach (SearchResult searchResult in searcher.FindAll())
                        {
                            logInName = BLUtilities.DomainName + CommonStrings.BackSlash + searchResult.Properties["sAMAccountName"][0].ToString();

                            permissions = (from role in Roles.GetRolesForUser(logInName)
                                           select role).ToDictionary(d => d, d => systemPermissions[d]);

                            if (!string.IsNullOrEmpty(filterUserName))
                            {
                                if (searchResult.Properties["name"][0].ToString().ToLower().Contains(filterUserName.ToLower()))
                                    usersInfo.Add(new UserInfo(logInName.Split(new Char[] { CommonStrings.BackSlashAsChar }, StringSplitOptions.RemoveEmptyEntries)[1],
                                        searchResult.Properties["name"][0].ToString(), permissions));
                            }
                            else
                            {
                                usersInfo.Add(new UserInfo(logInName.Split(new Char[] { CommonStrings.BackSlashAsChar }, StringSplitOptions.RemoveEmptyEntries)[1],
                                        searchResult.Properties["name"][0].ToString(), permissions));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return usersInfo;
        }

        public static Dictionary<String, String> GetSystemPermissions()
        {
            Dictionary<String, String> permissions = new Dictionary<String, String>();

            try
            {
                //HttpContext.GetOwinContext().Get<RoleManager<IdentityRole>>();
                string[] roles = Roles.GetAllRoles();
                permissions = roles.ToDictionary<string, string>(x => x);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return permissions;
        }

        public static UpdateUsersPermessionsOperationResult UpdateUsersPermessions(String[] loginNames, String permession)
        {
            UpdateUsersPermessionsOperationResult result = UpdateUsersPermessionsOperationResult.NotDefined;

            try
            {
                if (!String.IsNullOrEmpty(permession) && GetSystemPermissions().Keys.Contains(permession))
                {
                    if (loginNames.Count() > 0)
                    {
                        if (permession == Permissions.AccessToSystem)
                        {
                            String[] usersInRole = Roles.GetUsersInRole(permession);

                            foreach (String user in usersInRole)
                            {
                                Boolean isDeleted = true;
                                foreach (String loginName in loginNames)
                                {
                                    String logInName = BLUtilities.DomainName + CommonStrings.BackSlash + loginName;
                                    if (user == logInName)
                                    {
                                        isDeleted = false;
                                        break;
                                    }
                                }
                                if (isDeleted)
                                    Roles.RemoveUserFromRoles(user, Roles.GetRolesForUser(user));
                            }
                        }

                        List<String> allUsersLogInNames = GetUsersInfo(string.Empty).Select(userInfo => BLUtilities.DomainName + CommonStrings.BackSlash + userInfo.UserName).ToList();

                        loginNames = loginNames.Select(loginName => BLUtilities.DomainName + CommonStrings.BackSlash + loginName).ToArray();

                        Boolean allLogInNamesFoundedInAD = true;

                        foreach (String loginName in loginNames)
                        {
                            if (!allUsersLogInNames.Contains(loginName))
                            {
                                allLogInNamesFoundedInAD = false;

                                break;
                            }
                        }

                        if (allLogInNamesFoundedInAD)
                        {
                            String[] usersInRole = Roles.GetUsersInRole(permession);

                            if (usersInRole.Length > 0)
                            {
                                Roles.RemoveUsersFromRole(usersInRole, permession);
                            }

                            Roles.AddUsersToRole(loginNames, permession);
                            string[] newUsers = loginNames.Where(u => !Roles.IsUserInRole(u, Permissions.AccessToSystem)).ToArray();
                            if (newUsers.Length > 0)
                                Roles.AddUsersToRole(newUsers, Permissions.AccessToSystem);

                            result = UpdateUsersPermessionsOperationResult.Succeed;

                            String objectId = CommonStrings.LeftBraket +
                                GetSystemPermissions().Where(p => p.Key == permession).First().Value +
                                CommonStrings.Column +
                                permession +
                                CommonStrings.RightBraket;

                            String oldObjectValue = String.Join(", ", usersInRole.Select(user => user.Split(new Char[] { CommonStrings.BackSlashAsChar }, StringSplitOptions.RemoveEmptyEntries)[1]).ToArray());

                            String newObjectValue = String.Join(", ", Roles.GetUsersInRole(permession).Select(user => user.Split(new Char[] { CommonStrings.BackSlashAsChar }, StringSplitOptions.RemoveEmptyEntries)[1]).ToArray());
                        }
                        else
                        {
                            result = UpdateUsersPermessionsOperationResult.NotAllUsersExists;
                        }
                    }
                    else
                    {
                        String[] usersInRole = Roles.GetUsersInRole(permession);

                        if (usersInRole.Length > 0)
                        {
                            Roles.RemoveUsersFromRole(usersInRole, permession);
                        }

                        result = UpdateUsersPermessionsOperationResult.Succeed;
                    }
                }
                else
                {
                    result = UpdateUsersPermessionsOperationResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public static bool UserInRole(ICollection<IdentityUserRole> roles, string rolename)
        {
            try
            {
                var v = GetQuery("Select id from AspNetRoles where Name='" + rolename + "'", "RoleProviderConnectionString");
                if (v != null)
                {
                    foreach (var item in roles)
                    {
                        if (item.RoleId == v.ToString())
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }
        private static object GetQuery(string sql)
        {
            SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DynamicDataConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, sqlconn);
            try
            {
                sqlconn.Open();
                object o = cmd.ExecuteScalar();
                sqlconn.Close();
                return o;
            }
            catch
            {
            }
            return null;
        }

        public static DataTable GetQuery(string sql, string connName, object args)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connName].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                System.Data.DataSet ds = new System.Data.DataSet();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
                if (args != null && args.GetType() == typeof(int))
                {
                    int cnt = ((int)args);
                    cnt -= dt.Rows.Count;
                    for (int i = cnt; i < 0; i++)
                    {
                        dt.Rows.Remove(dt.Rows[dt.Rows.Count - 1]);
                    }
                    return dt;
                }
                else
                    return dt;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }
        public static SqlDataReader GetQuery(string sql, int row)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DynamicDataConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                return dr;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }
        private static object GetQuery(string sql, string connname)
        {
            SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings[connname].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, sqlconn);
            try
            {
                sqlconn.Open();
                object o = cmd.ExecuteScalar();
                sqlconn.Close();
                return o;
            }
            catch
            {
            }
            return null;
        }
        private static List<int> GetUnaryQuery(string sql)
        {
            SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DynamicDataConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, sqlconn);
            try
            {
                sqlconn.Open();
                SqlDataReader o = cmd.ExecuteReader();
                List<int> l = new List<int>();
                while (o.Read())
                {
                    l.Add(o.GetInt32(0));
                }
                sqlconn.Close();
                return l;
            }
            catch (Exception e)
            {
                return null;
            }
            return null;
        }
        private static void SetQuery(string sql)
        {
            SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DynamicDataConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, sqlconn);
            try
            {
                sqlconn.Open();
                cmd.ExecuteNonQuery();
                sqlconn.Close();
            }
            catch
            {
            }
        }
        //public static String GetCurrentLoggedInUserDisplayName(HttpSessionState session)
        //{
        //    String displayName = String.Empty;

        //    try
        //    {
        //        if (session["displayName"] != null)
        //        {
        //            displayName = session["displayName"].ToString();
        //        }
        //        else
        //        {
        //            using (DirectoryEntry entry = new DirectoryEntry(CommonStrings.LDAPProtocol + BLUtilities.DomainName, BLUtilities.DomainAccountUserName, BLUtilities.DomainAccountPassword))
        //            {
        //                DirectorySearcher searcher = new DirectorySearcher(entry);

        //                searcher.Filter = "(&(objectClass=user)(sAMAccountName=" + HttpContext.Current.User.Identity.Name.Split(new Char[] { CommonStrings.BackSlashAsChar }, StringSplitOptions.RemoveEmptyEntries)[1] + "))";

        //                Boolean atLeastOnTime = false;

        //                foreach (SearchResult searchResult in searcher.FindAll())
        //                {
        //                    session["displayName"] = searchResult.Properties["name"][0].ToString();

        //                    displayName = session["displayName"].ToString();

        //                    atLeastOnTime = true;

        //                    break;
        //                }

        //                if (!atLeastOnTime)
        //                {
        //                    session["displayName"] = String.Empty;

        //                    displayName = session["displayName"].ToString();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return displayName;
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace DynamicDataLibrary.ModelRelated
{
    public static class AuditingHelperUtility
    {
        public static void ProcessAuditFields(IEnumerable<Object> list, bool InsertMode = true)
        {
            foreach (ObjectStateEntry item in list)
            {
                var appUserID = GetUserId();
                // deal with insert and update entities
                var auditEntity = item.Entity as IAuditable;
                if (auditEntity != null)
                {

                    if (InsertMode)
                    {
                        auditEntity.CreatedBy = appUserID;
                        auditEntity.CreatedDateTime = DateTime.Now;
                    }

                    auditEntity.UpdatedBy = appUserID;
                    auditEntity.UpdatedDateTime = DateTime.Now;
                }

            }
        }
        public static void ProcessAuditFields(Object entity, bool InsertMode = true)
        {
            var appUserID = GetUserId();
            // deal with insert and update entities
            var auditEntity = entity as IAuditable;
            if (auditEntity != null)
            {

                if (InsertMode)
                {
                    auditEntity.CreatedBy = appUserID;
                    auditEntity.CreatedDateTime = DateTime.Now;
                }

                auditEntity.UpdatedBy = appUserID;
                auditEntity.UpdatedDateTime = DateTime.Now;
            }
        }

        public static void ProcessAuditFields(IOrderedDictionary values, bool InsertMode = true)
        {
            var appUserID = GetUserId();
            // deal with insert and update entities
            if (values.Contains("CreatedBy") && values.Contains("CreatedDateTime")
                && values.Contains("UpdatedBy") && values.Contains("UpdatedDateTime"))
            {
                if (InsertMode)
                {
                    values["CreatedBy"] = appUserID;
                    values["CreatedDateTime"] = DateTime.Now;
                }

                values["UpdatedBy"] = appUserID;
                values["UpdatedDateTime"] = DateTime.Now;
            }
        }
        public static String GetUserId()
        {
            string userName = (System.Web.HttpContext.Current.User != null && System.Web.HttpContext.Current.User.Identity != null)
                ? System.Web.HttpContext.Current.User.Identity.Name.ToLower()
                : null;
            //string displayName = General.GetCurrentUserNameExUtil.GetUserFullName();
            return userName;
        }

        //public static string GetUserFullName(string domain, string userName)
        //{
        //    DirectoryEntry userEntry = new DirectoryEntry("WinNT://" + domain + "/" + userName + ",User");
        //    return (string)userEntry.Properties["fullname"].Value;

        //}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDataLibrary.ModelRelated
{
    public class GeneralActionsMethods
    {
        public static bool CheckAndProcessBeforeInserting(System.Collections.Specialized.IOrderedDictionary values, bool force, out string message, out MessageActionType messageActionType)
        {
            message = String.Empty;
            messageActionType = MessageActionType.NoMessage;
            return true;
        }

        public static bool CheckAndProcessBeforeEditing(System.Collections.Specialized.IOrderedDictionary oldValues, System.Collections.Specialized.IOrderedDictionary newValues, bool force, out string message, out MessageActionType messageActionType)
        {
            string messageOfDelete = null;
            string messageOfInsert = null;
            bool checkBeforeDelete = true;
            if (oldValues.Values.Count != 0)
                checkBeforeDelete = CheckAndProcessBeforeDeleting(oldValues, force, out messageOfDelete, out messageActionType);
            else
            {
                messageActionType = MessageActionType.NoMessage;
            }
            bool checkBeforeInsert = true;
            if (checkBeforeDelete != false)
                checkBeforeInsert = CheckAndProcessBeforeInserting(newValues, force, out messageOfInsert, out messageActionType);

            message = !String.IsNullOrEmpty(messageOfDelete) && !String.IsNullOrEmpty(messageOfInsert) ?
                String.Join("<br />", messageOfDelete, messageOfInsert)
                : !String.IsNullOrEmpty(messageOfDelete) ? messageOfDelete
                : !String.IsNullOrEmpty(messageOfInsert) ? messageOfInsert
                : null;

            return checkBeforeDelete
                && checkBeforeInsert;
        }

        public static bool CheckAndProcessBeforeDeleting(System.Collections.Specialized.IOrderedDictionary values, bool force, out string message, out MessageActionType messageActionType)
        {
            message = String.Empty;
            messageActionType = MessageActionType.NoMessage;
            return true;
        }

        public static void ProcessAfterInserting(System.Collections.Specialized.IOrderedDictionary values, int affectedRows, out string message, out MessageAfterActionType messageAfterActionType)
        {
            message = String.Empty;
            messageAfterActionType = MessageAfterActionType.NoMessage;
        }

        public static void ProcessAfterEditing(System.Collections.Specialized.IOrderedDictionary oldValues, System.Collections.Specialized.IOrderedDictionary newValues, int affectedRows, out string message, out MessageAfterActionType messageAfterActionType)
        {
            string messageOfDelete = null;
            string messageOfInsert = null;
            MessageAfterActionType messageAfterDeleteActionType = MessageAfterActionType.NoMessage;
            MessageAfterActionType messageAfterInsertActionType = MessageAfterActionType.NoMessage;
            ProcessAfterDeleting(oldValues, affectedRows, out messageOfDelete, out messageAfterDeleteActionType);
            ProcessAfterInserting(newValues, affectedRows, out messageOfInsert, out messageAfterInsertActionType);

            message = !String.IsNullOrEmpty(messageOfDelete) && !String.IsNullOrEmpty(messageOfInsert) ?
                String.Join("<br />", messageOfDelete, messageOfInsert)
                : !String.IsNullOrEmpty(messageOfDelete) ? messageOfDelete
                : !String.IsNullOrEmpty(messageOfInsert) ? messageOfInsert
                : null;
            messageAfterActionType = (MessageAfterActionType)Math.Max((int)messageAfterDeleteActionType, (int)messageAfterInsertActionType);
        }

        public static void ProcessAfterDeleting(System.Collections.Specialized.IOrderedDictionary values, int affectedRows, out string message, out MessageAfterActionType messageAfterActionType)
        {
            message = String.Empty;
            messageAfterActionType = MessageAfterActionType.NoMessage;
        }

        public static IQueryable CustomQuery(IQueryable query)
        {
            return query;
        }
    }
}

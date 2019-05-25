using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web.DynamicData;

namespace DynamicDataLibrary
{
    public enum DDAction { Inserting, Editing, Deleting }
    public enum MessageActionType { Information = 1, NeedConfirmation = 2, Error = 3, NoMessage = 0 }
    public enum MessageAfterActionType : int { Information = 1, Error = 2, NoMessage = 0 }

    public delegate bool CheckAndProcessOnInsert(IOrderedDictionary values, bool force, out string message, out MessageActionType messageActionType);

    public delegate void ProcessAfterInsert(IOrderedDictionary values, int affectedRows, out string message, out MessageAfterActionType messageAfterActionType);

    public delegate bool CheckAndProcessOnEdit(IOrderedDictionary oldValues, IOrderedDictionary newValues, bool force, out string message, out MessageActionType messageActionType);

    public delegate void ProcessAfterEdit(IOrderedDictionary oldValues, IOrderedDictionary newValues, int affectedRows, out string message, out MessageAfterActionType messageAfterActionType);

    public delegate bool CheckAndProcessOnDelete(IOrderedDictionary values, bool force, out string message, out MessageActionType messageActionType);

    public delegate void ProcessAfterDelete(IOrderedDictionary values, int affectedRows, out string message, out MessageAfterActionType messageAfterActionType);

    public delegate IQueryable ProcessQuery(IQueryable query);

    //public interface ICustomCheckOnAction
    //{
    //    bool CheckOnInsert(IOrderedDictionary values, bool force, out string message);
    //    bool CheckOnEdit(IOrderedDictionary values, bool force, out string message);
    //    bool CheckOnDelete(IOrderedDictionary values, bool force, out string message);
    //}
}

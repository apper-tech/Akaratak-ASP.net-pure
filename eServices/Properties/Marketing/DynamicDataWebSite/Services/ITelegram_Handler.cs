using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DynamicDataWebSite.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITelegram_Handler" in both code and config file together.
    [ServiceContract]
     interface ITelegram_Handler
    {
        [OperationContract]
         string _Add_User(Options_Handler handler, string token);
        [OperationContract]
        string Test();
        [OperationContract]
        bool _User_Exist(Options_Handler handler, string token);
        [OperationContract]
        string _Update_User(Options_Handler handler);
        [OperationContract]
        string _Add_Property(Options_Handler property, string token);
        [OperationContract]
        List<Options_Handler> _Get_Property_List(Options_Handler handler);
        [OperationContract]
        string _Update_Property(Options_Handler property, string token);
        [OperationContract]
        List<City_Handler> _Get_City_List(int cid);
        [OperationContract]
        List<Country_Handler> _Get_Country_List();
        [OperationContract]
        Options_Handler _Get_Property_Value(Options_Handler handler, string address, string date);
    }
}

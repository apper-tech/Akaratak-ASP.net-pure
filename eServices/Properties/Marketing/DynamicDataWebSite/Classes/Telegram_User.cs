using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Xml.Serialization;

namespace DynamicDataWebSite
{
    public class Options_Handler
    {
        public string Cat { get => cat; set => cat = value; }
        public string Type { get => type; set => type = value; }
        public bool Garage { get => garage; set => garage = value; }
        public bool Garden { get => garden; set => garden = value; }

        public long Id { get => id; set => id = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Phone { get => phone; set => phone = value; }
        string cat;
        string type;
        bool garage;
        bool garden;
        long id;
        string expectedEntry;
        System.IO.FileStream photostream;
        string lang;
        string email;
        string password;
        string oldpassword;
        string phone;
        string firstName;
        string lastName;
        public int PropertyID { get; set; }
        public int Property_Type_ID { get; set; }
        public int Property_Size { get; set; }
        public System.DateTime Date_Added { get; set; }
        public int Floor { get; set; }
        public Nullable<bool> Has_Garage { get; set; }
        public Nullable<bool> Has_Garden { get; set; }
        public int Num_Bedrooms { get; set; }
        public int Num_Bathrooms { get; set; }
        public System.DateTime Expire_Date { get; set; }
        public Nullable<int> Contract_Type { get; set; }
        public Nullable<int> City_ID { get; set; }
        public Nullable<int> Country_ID { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string Zip_Code { get; set; }
        public string Other_Details { get; set; }
        public Nullable<int> Sale_Price { get; set; }
        public Nullable<int> Rent_Price { get; set; }
        public Nullable<int> Num_Floors { get; set; }
        public string User_ID { get; set; }
        public string Property_Photo { get; set; }
        public int Property_Category_ID { get; set; }
        public string Property_Id_ext { get; set; }
        public string Url_ext { get; set; }
        public static string Serialize_User(Options_Handler deserializedData)
        {
            string serializedData = string.Empty;

            XmlSerializer serializer = new XmlSerializer(deserializedData.GetType());
            using (StringWriter sw = new StringWriter())
            {
                serializer.Serialize(sw, deserializedData);
                serializedData = sw.ToString();
                return serializedData;
            }
        }
        public static Options_Handler Deserialize_User(string serializedData)
        {

            XmlSerializer deserializer = new XmlSerializer(typeof(Options_Handler));
            using (TextReader tr = new StringReader(serializedData))
            {
                Options_Handler deserializedUser = (Options_Handler)deserializer.Deserialize(tr);
                return deserializedUser;
            }
        }
        public static void Store_Settings(Options_Handler user)
        {
            SqlConnection sqlConnection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["SettingsRemote"].ConnectionString);
            SqlCommand command = new SqlCommand("SELECT * FROM Settings WHERE Id=" + user.Id, sqlConnection);
            sqlConnection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (!reader.HasRows)
                command.CommandText = "INSERT INTO Settings (Id,Lang) VALUES (" + user. Id + ",'" + user. Lang + "')";
            else
                command.CommandText = "UPDATE Settings Set Lang='" + user.Lang + "' WHERE Id=" + user. Id;
            reader.Close();
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public static void Read_Settings(Options_Handler user)
        {
            SqlConnection sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["SettingsRemote"].ConnectionString);
            SqlCommand command = new SqlCommand("SELECT [Id],[Lang] FROM [User_Settings].[dbo].[Settings] WHERE Id=" + user.Id, sqlConnection);
            sqlConnection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                user.id = reader.GetInt32(0);
                user.lang = reader.GetString(1);
            }
            reader.Close();

            sqlConnection.Close();
        }
        public string Lang { get => lang; set => lang = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Oldpassword { get => oldpassword; set => oldpassword = value; }
        public DateTime Date { get => date; set => date = value; }
        public System.IO.FileStream Photostream { get => photostream; set => photostream = value; }
        public string ExpectedEntry { get => expectedEntry; set => expectedEntry = value; }

        DateTime date;
        public Options_Handler() { }
      
    }
    public class Country_Handler
    {
        public int Country_ID { get; set; }
        public string Country_Code { get; set; }
        public string Country_Name { get; set; }
        public string Country_Native_Name { get; set; }
    }
    public class City_Handler
    {
        public int City_ID { get; set; }
        public string City_Name { get; set; }
        public int Country_ID { get; set; }
        public string City_Native_Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string City_Latin_Name { get; set; }
    }
    public static class Crypto_String
    {
        private static byte[] key;
        private static byte[] iv;

        public static byte[] Key { get => key; set => key = value; }
        public static byte[] Iv { get => iv; set => iv = value; }

        public static string Crypt(this string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateEncryptor(Key, Iv);
            byte[] inputbuffer = Encoding.Unicode.GetBytes(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Convert.ToBase64String(outputBuffer);
        }

        public static string Decrypt(this string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateDecryptor(Key, Iv);
            byte[] inputbuffer = Convert.FromBase64String(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Encoding.Unicode.GetString(outputBuffer);
        }
    }
}
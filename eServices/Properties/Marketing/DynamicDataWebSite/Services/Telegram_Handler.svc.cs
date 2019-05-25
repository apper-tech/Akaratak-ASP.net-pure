using System.Collections.Generic;
using System.Linq;
using DynamicDataModel.Model;
using DynamicDataWebSite.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System;
using System.Globalization;
using System.IO;

namespace DynamicDataWebSite.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Telegram_Handler" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Telegram_Handler.svc or Telegram_Handler.svc.cs at the Solution Explorer and start debugging.
    public class Telegram_Handler : ITelegram_Handler
    {
        private static readonly string Token_GET = "K0rdMAbcZRePqaTMzG8h";
        public string _Add_User(Options_Handler handler, string token)
        {
            var Context = HttpContext.Current;
            if (token == Token_GET)
            {
                //Options_Handler handler = new Options_Handler { FirstName = "mh", LastName = "mazen", Phone = "00963968286504", Email = "mh@ma.com", Address = "somewhere", Id = 1455858, Password = "PA1Swor$" };
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
                Entities dbContext = new Entities();
                var user = new ApplicationUser()
                {
                    UserName = handler.Email,
                    Email = handler.Email,
                    FirstName = handler.FirstName,
                    LastName = handler.LastName,
                    Address = handler.Address,
                    HasOffice = false,
                    PhoneNumber = handler.Phone,
                    PhoneNumberConfirmed = false,
                    EmailConfirmed = true
                };
                IdentityResult result = manager.Create(user, handler.Password);
                if (result.Succeeded)
                {

                    DynamicDataModel.Model.User dbuser = new DynamicDataModel.Model.User
                    {
                        User_ID = manager.Find(handler.Email, handler.Password).Id,
                        First_Name = handler.FirstName,
                        Last_Name = handler.LastName,
                        Address = handler.Address,
                        Has_Office = false,
                        Allow_Prom = false,
                        Sub_NewsLetter = false,
                        Email = handler.Email,
                        Phone_Num = handler.Phone,
                        Properties = new List<Property>(),
                        Telegram_ID = handler.Id
                    };
                    dbContext.Users.Add(dbuser);
                    dbContext.SaveChanges();
                }
                else
                {
                    ApplicationUser dbuser = manager.Find(handler.Email, handler.Password);
                    if (dbuser != null)
                    {
                        DynamicDataModel.Model.User dbduser = new DynamicDataModel.Model.User
                        {
                            User_ID = manager.Find(handler.Email, handler.Password).Id,
                            First_Name = handler.FirstName,
                            Last_Name = handler.LastName,
                            Address = handler.Address,
                            Has_Office = false,
                            Allow_Prom = false,
                            Sub_NewsLetter = false,
                            Email = handler.Email,
                            Phone_Num = handler.Phone,
                            Properties = new List<Property>(),
                            Telegram_ID = handler.Id
                        };

                        var dbsuser = dbContext.Users.Where(x => x.Email == handler.Email && x.Telegram_ID == handler.Id).Count() > 0 ? dbContext.Users.Where(x => x.Email == handler.Email && x.Telegram_ID == handler.Id).First() : dbContext.Users.Where(x => x.Email == handler.Email).First();

                        if (dbsuser == null)
                        {
                            dbContext.Users.Add(dbduser);
                            dbContext.SaveChanges();
                        }
                    }
                    else
                    {
                        return result.Errors.First();
                    }
                }
                //return dbContext.Users.Where
                //    (x => x.Email == handler.Email && x.Telegram_ID == handler.Id).Count() > 0 ?
                //    dbContext.Users.Where(x => x.Email == handler.Email && x.Telegram_ID == handler.Id).First().User_ID
                //    : dbContext.Users.Where(x => x.Email == handler.Email).First().User_ID;
            }
            return "";
        }
        public string Test()
        {
            return "test";
        }
        public bool _User_Exist(Options_Handler handler, string token)
        {
            if (token == Token_GET)
            {
                Entities dbContext = new Entities();
                bool e = dbContext.Users.Where
               (x => x.Telegram_ID == handler.Id).Count() > 0 ? true : false;
                return e;
            }
            return false;
        }    
        public string _Update_User(Options_Handler handler)
        {
            var Context = HttpContext.Current;
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            Entities dbContext = new Entities();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser();
            var dbuser = dbContext.Users.Where
                (x => x.Telegram_ID == handler.Id).Count() > 0 ? dbContext.Users.Where
                (x => x.Telegram_ID == handler.Id).First() : null;
            if (dbuser != null)
            {
                user = manager.FindById(dbuser.User_ID);
            }
            else
                if ((handler.Email != null && handler.Password != null))
                user = manager.Find(handler.Email, handler.Password);
            else
            {
                if (handler.Email == null)
                    return "Wrong Email";
                if (handler.Password == null)
                    return "Wrong Passwrd";
            }
            if (user != null && !string.IsNullOrEmpty(user.Id))
            {
                IdentityResult result = null;
                user.Email = !string.IsNullOrEmpty(handler.Email) ? handler.Email : user.Email;
                user.FirstName = !string.IsNullOrEmpty(handler.FirstName) ? handler.FirstName : user.FirstName;
                user.LastName = !string.IsNullOrEmpty(handler.LastName) ? handler.LastName : user.LastName;
                user.Address = !string.IsNullOrEmpty(handler.Address) ? handler.Address : user.Address;
                if (!string.IsNullOrEmpty(handler.Oldpassword) && !string.IsNullOrEmpty(handler.Password))
                {
                    result = manager.ChangePassword(user.Id, handler.Oldpassword, handler.Password);
                }
                if (result == null)
                    result = manager.Update(user);
                if (result.Succeeded)
                {
                    dbuser = dbContext.Users.Where(x => x.User_ID == dbuser.User_ID).First();
                    dbuser.User_ID = user.Id;
                    dbuser.Telegram_ID = handler.Id;
                    dbuser.Phone_Num = handler.Phone;
                    dbuser.Email = !string.IsNullOrEmpty(handler.Email) ? handler.Email : dbuser.Email;
                    dbuser.First_Name = !string.IsNullOrEmpty(handler.FirstName) ? handler.FirstName : dbuser.First_Name;
                    dbuser.Last_Name = !string.IsNullOrEmpty(handler.LastName) ? handler.LastName : dbuser.Last_Name;
                    dbuser.Address = !string.IsNullOrEmpty(handler.Address) ? handler.Address : dbuser.Address;
                    dbContext.SaveChanges();
                    return "";
                }
                else
                    return result.Errors.First();
            }
            return "Unknown Error";
        }     
        public string _Add_Property(Options_Handler property, string token)
        {
            if (token == Token_GET)
            {
                Entities dbContext = new Entities();
                int catN = dbContext.Property_Category.Where(x => x.Cat_Name == property.Cat).FirstOrDefault().Cat_ID;
                int typN = dbContext.Property_Type.Where(x => x.Property_Type_Name == property.Type).FirstOrDefault().Property_Type_ID;
                var properties = dbContext.Properties.ToList();
                var uid = dbContext.Users.Where(x => x.Telegram_ID == property.Id).FirstOrDefault();
                Property p = null;
                if (uid != null)
                    foreach (var item in properties)
                        if (item.User_ID == uid.User_ID)
                        {
                            p = dbContext.Properties.Where(x => x.PropertyID == item.PropertyID).FirstOrDefault();
                            break;
                        }
                p.Address = string.IsNullOrEmpty(property.Address) ? property.Address != p.Address ? property.Address : p.Address : p.Address;
                p.Country_ID = property.Country_ID;
                p.City_ID = property.City_ID;
                p.Date_Added = property.Date_Added;
                p.Expire_Date = property.Expire_Date;
                p.Location = string.IsNullOrEmpty(property.Location) ? property.Location != p.Location ? property.Location : p.Location : p.Location;
                p.Num_Bathrooms = string.IsNullOrEmpty(property.Num_Bathrooms + "") ? property.Num_Bathrooms != p.Num_Bathrooms ? property.Num_Bathrooms : p.Num_Bathrooms : p.Num_Bathrooms;
                p.Num_Bedrooms = string.IsNullOrEmpty(property.Num_Bedrooms + "") ? property.Num_Bedrooms != p.Num_Bedrooms ? property.Num_Bedrooms : p.Num_Bedrooms : p.Num_Bedrooms;
                p.Num_Floors = string.IsNullOrEmpty(property.Num_Floors + "") ? property.Num_Floors != p.Num_Floors ? property.Num_Floors : p.Num_Floors : p.Num_Floors;
                p.Other_Details = string.IsNullOrEmpty(property.Other_Details) ? property.Other_Details != p.Other_Details ? property.Other_Details : p.Other_Details : p.Other_Details;
                p.Zip_Code = string.IsNullOrEmpty(property.Zip_Code) ? property.Zip_Code != p.Zip_Code ? property.Zip_Code : p.Zip_Code : p.Zip_Code;
                p.Floor = string.IsNullOrEmpty(property.Floor + "") ? property.Floor != p.Floor ? property.Floor : p.Floor : p.Floor;
                p.Has_Garage = property.Has_Garage;
                p.Has_Garden = property.Has_Garden;
                p.Property_Photo = string.IsNullOrEmpty(property.Property_Photo) ? property.Property_Photo != p.Property_Photo ? property.Property_Photo : p.Property_Photo : p.Property_Photo;
                p.PropertyID = property.PropertyID;
                p.Sale_Price = property.Sale_Price;
                p.Rent_Price = property.Rent_Price;
                p.Property_Type_ID = typN;
                p.Property_Category_ID = catN;
                p.Property_Size = string.IsNullOrEmpty(property.Property_Size + "") ? property.Property_Size != p.Property_Size ? property.Property_Size : p.Property_Size : p.Property_Size;
                try
                {
                    dbContext.SaveChanges();
                }
                catch (Exception e)
                {

                    return e.Message;
                }

            }
            _Move_Images();
            return "";
        }   
        public string _Update_Property(Options_Handler property, string token)
        {
            if (token == Token_GET)
            {
                Entities dbContext = new Entities();
                int catN = dbContext.Property_Category.Where(x => x.Cat_Name == property.Cat).FirstOrDefault().Cat_ID;
                int typN = dbContext.Property_Type.Where(x => x.Property_Type_Name == property.Type).FirstOrDefault().Property_Type_ID;
                dbContext.Properties.Add(new Property
                {
                    Address = property.Address,
                    Country_ID = property.Country_ID,
                    City_ID = property.City_ID,
                    Date_Added = property.Date_Added,
                    Expire_Date = property.Expire_Date,
                    Location = property.Location,
                    Num_Bathrooms = property.Num_Bathrooms,
                    Num_Bedrooms = property.Num_Bedrooms,
                    Num_Floors = property.Num_Floors,
                    Other_Details = property.Other_Details,
                    Zip_Code = property.Zip_Code,
                    Floor = property.Floor,
                    Has_Garage = property.Has_Garage,
                    Has_Garden = property.Has_Garden,
                    Property_Photo = property.Property_Photo,
                    PropertyID = property.PropertyID,
                    Sale_Price = property.Sale_Price,
                    Rent_Price = property.Rent_Price,
                    Property_Type_ID = typN,
                    Property_Category_ID = catN,
                    Property_Size = property.Property_Size,
                });
                try
                {
                    dbContext.SaveChanges();
                }
                catch (Exception e)
                {

                    return e.Message;
                }

            }
            _Move_Images();
            return "";
        }
       
        public string _Test()
        {
            return "Test - " + new Random().Next(0, 1500);
        }
       
        public List<Country_Handler> _Get_Country_List()
        {
            Entities dbContext = new Entities();
            List<Country_Handler> country_Handlers = new List<Country_Handler>();
            var countries = dbContext.Countries.ToList();
            foreach (var item in countries)
                country_Handlers.Add(new Country_Handler { Country_Code = item.Country_Code, Country_ID = item.Country_ID, Country_Name = item.Country_Name, Country_Native_Name = item.Country_Native_Name });
            return country_Handlers;
        }
       
        public List<City_Handler> _Get_City_List(int cid)
        {
            Entities dbContext = new Entities();
            List<City_Handler> city_Handlers = new List<City_Handler>();
            var cities = dbContext.Cities.ToList();
            foreach (var item in cities)
                if (item.Country_ID == cid)
                    city_Handlers.Add(new City_Handler { City_ID = item.City_ID, Country_ID = item.Country_ID, City_Name = item.City_Name, City_Latin_Name = item.City_Latin_Name, City_Native_Name = item.City_Native_Name, Latitude = item.Latitude, Longitude = item.Longitude });
            return city_Handlers;
        }
       
        public List<Options_Handler> _Get_Property_List(Options_Handler handler)
        {
            Entities dbContext = new Entities();
            List<Options_Handler> property_Handlers = new List<Options_Handler>();
            var properties = dbContext.Properties.ToList();
            var uid = dbContext.Users.Where(x => x.Telegram_ID == handler.Id).FirstOrDefault();
            if (uid != null)
                foreach (var item in properties)
                    if (item.User_ID == uid.User_ID)
                    {
                        property_Handlers.Add(new Options_Handler
                        {
                            Address = item.Address,
                            Country_ID = item.Country_ID,
                            City_ID = item.City_ID,
                            Cat = dbContext.Property_Category.Where(x => x.Cat_ID == item.Property_Category_ID).FirstOrDefault().ToString(),
                            Type = dbContext.Property_Type.Where(x => x.Property_Type_ID == item.Property_Type_ID).FirstOrDefault().ToString(),
                            Date_Added = item.Date_Added,
                            Expire_Date = item.Expire_Date,
                            Location = item.Location,
                            Num_Bathrooms = item.Num_Bathrooms,
                            Num_Bedrooms = item.Num_Bedrooms,
                            Num_Floors = item.Num_Floors,
                            Other_Details = item.Other_Details,
                            Zip_Code = item.Zip_Code,
                            Floor = item.Floor,
                            Has_Garage = item.Has_Garage,
                            Has_Garden = item.Has_Garden,
                            Property_Photo = item.Property_Photo,
                            PropertyID = item.PropertyID,
                            Sale_Price = item.Sale_Price,
                            Rent_Price = item.Rent_Price,
                            Property_Type_ID = item.Property_Type_ID,
                            Property_Size = item.Property_Size,
                        });
                    }

            return property_Handlers;
        }
       
        public Options_Handler _Get_Property_Value(Options_Handler handler, string address, string date)
        {
            Entities dbContext = new Entities();
            Options_Handler property = new Options_Handler();
            var properties = dbContext.Properties.ToList();
            var uid = dbContext.Users.Where(x => x.Telegram_ID == handler.Id).FirstOrDefault();
            if (uid != null)
                foreach (var item in properties)
                    if (item.User_ID == uid.User_ID)
                        if (item.Address == address)
                        {
                            DateTime added_date = DateTime.ParseExact(date, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                            if (item.Date_Added.ToString("M/d/yyyy h:mm") == added_date.ToString("M/d/yyyy h:mm"))
                            {
                                property = new Options_Handler
                                {
                                    Address = item.Address,
                                    Country_ID = item.Country_ID,
                                    City_ID = item.City_ID,
                                    Cat = dbContext.Property_Category.Where(x => x.Cat_ID == item.Property_Category_ID).FirstOrDefault().Cat_Name,
                                    Type = dbContext.Property_Type.Where(x => x.Property_Type_ID == item.Property_Type_ID).FirstOrDefault().Property_Type_Name,
                                    Date_Added = item.Date_Added,
                                    Expire_Date = item.Expire_Date,
                                    Location = item.Location,
                                    Num_Bathrooms = item.Num_Bathrooms,
                                    Num_Bedrooms = item.Num_Bedrooms,
                                    Num_Floors = item.Num_Floors,
                                    Other_Details = item.Other_Details,
                                    Zip_Code = item.Zip_Code,
                                    Floor = item.Floor,
                                    Has_Garage = item.Has_Garage,
                                    Has_Garden = item.Has_Garden,
                                    Property_Photo = item.Property_Photo,
                                    PropertyID = item.PropertyID,
                                    Sale_Price = item.Sale_Price,
                                    Rent_Price = item.Rent_Price,
                                    Property_Type_ID = item.Property_Type_ID,
                                    Property_Size = item.Property_Size,
                                    Id = handler.Id,
                                    Lang = handler.Lang
                                };
                                break;
                            }

                        }

            return property;
        }
        private static string _Generate_Random_String(int length)
        {
            Random random = new Random();
            return new string((from s in Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length)
                               select s[random.Next(s.Length)]).ToArray());
        }
        public void _Move_Images()
        {
            DirectoryInfo telegram_temp = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + ("\\TelegramBot\\Temp"));
            DirectoryInfo Property_Images = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + ("\\RealEstate\\PropertyImage"));
            foreach (var src in telegram_temp.GetFiles())
            {
                var des = Property_Images.FullName + "\\" + src.Name;
                while (File.Exists(des))
                    des = Property_Images.FullName + "\\" + _Generate_Random_String(15) + ".jpg";
                if (new[] { ".jpg", ".png", ".bmp" }.Any(c => src.Extension.Contains(c)))
                {
                    File.Move(src.FullName, des);
                }
            }
        }
    }
}

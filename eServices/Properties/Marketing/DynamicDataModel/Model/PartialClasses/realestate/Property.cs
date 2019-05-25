using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace DynamicDataModel.Model
{
    using DynamicDataLibrary.Attributes;
    using App_LocalResources;
    using NotAClue.ComponentModel.DataAnnotations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Security;

    [MetadataType(typeof(Meta_Property))]
    [Serializable]
    public partial class Property
    {

    }
    [System.ComponentModel.DisplayName("Akaratak")]
    [AddIconToHomePage(1, "View_Property", "/List", @"\CustomDesign\images\vih.png", "View_Property_Desc", Locale_Resource_Name = "RealEstate")]
    [AddIconToHomePage(2, "View_Added_Property", "/Added", @"\CustomDesign\images\vah.png", "View_Added_Property_Desc", Locale_Resource_Name = "RealEstate", UrlParameters = "User_ID")]
    [AddIconToHomePage(3, "Add_Property", "/Insert", @"\CustomDesign\images\adh.png", "Add_Property_Desc", Locale_Resource_Name = "RealEstate", ShowEvenIfUrlNotAccessable = true)]
    [CustomView(HideColumnIfEmpty = true, HideColumnInTemplate = PageTemplate.List | PageTemplate.Details, Coulmn_Names_File_Name = "RealEstate")]
    public class Meta_Property
    {
        //-----------------------Property Category---------------------------//
        [Display(Order = 5)]
        [LocalizedDisplayName("Column_Property_Category", typeof(TableCols))]
        [LocalizedForeignKey(ResourceFileName = "Property_Category")]
        [UIHint("CascadingForeignKey")]
        [FilterUIHint("CascadingForeignKey")]
        [Filter(Order = 5)]
        public virtual Property_Category Property_Category { get; set; }
        //------------------------Property Type-------------------------------//
        [LocalizedForeignKey(ResourceFileName = "Property_Type")]
        [UIHint("CascadingForeignKey")]
        [FilterUIHint("CascadingForeignKey")]
        [LocalizedDisplayName("Column_Property_Type", typeof(TableCols))]
        [Cascade(ParentColumn = "Property_Category", ConnectedColumn = "Property_Category_ID",Child_Select_First = "Property_Type_Select_First")]
        [Filter(Order = 10)]
        [Display(Order = 10)]
        public virtual Property_Type Property_Type { get; set; }
        //------------------------Property Size-------------------------------//
        [LocalizedDisplayName("Column_Property_Size", typeof(TableCols))]
        [LocalizedDescription("Column_Property_Size", typeof(TableColsDesc))]
        [HideColumnIn(PageTemplate.List)]
        [UIHint("Integer")]
        [Filter(Order = 20)]
        [Display(Order = 20)]
        public int Property_Size { get; set; }
        //------------------------Contract Type-------------------------------//
        //[LocalizedDisplayName("Column_Contract_Type", typeof(TableCols))]
        //[LocalizedForeignKey(ResourceFileName = "Contract_Type")]
        //[UIHint("Localized_Foreign_Key")]
        //[Filter(Hidden =true)]
        //[Display(Order = 40)]
        [ScaffoldColumn(false)]
        public virtual Contract_Type Contract_Type1 { get; set; }
        //------------------------Property Photo-------------------------------//
        [UIHint("ImagePathUpload")]
        [LocalizedDisplayName("Column_Photos", typeof(TableCols))]
        [HideColumnIn(PageTemplate.List)]
        [CustomView(CustomText = "/RealEstate/PropertyImage/")]
        [Display(Order = 30)]
        public string Property_Photo { get; set; }
        //------------------------Sale Price-------------------------------//
        [LocalizedDisplayName("Column_Sale_Price", typeof(TableCols))]
        [UIHint("Integer")]
        //[FilterUIHint("Range")]
        [LocalizedDescription("Column_Sale_Price", typeof(TableColsDesc))]
        [HideColumnIn(PageTemplate.List)]
      //  [Integer(MinValue = 0, EmptyString = true)]
        [HideBasedOnDependeeValue("Contract_Type1",
            new string[] { "4" })] //Write Comment for the colums' names, please.
        [Filter(Order = 50)]
        [Display(Order = 50)]
        public int Sale_Price { get; set; }
        //------------------------Rent Price-------------------------------//
        [LocalizedDisplayName("Column_Rent_Price", typeof(TableCols))]
        [LocalizedDescription("Column_Rent_Price", typeof(TableColsDesc))]
        [HideBasedOnDependeeValue("Contract_Type1",
            new string[] { "1", "2", "3" })] //Write Comment for the colums' names, please.
        [UIHint("Integer")]
        [HideColumnIn(PageTemplate.List)]
       // [Integer(MinValue = 0, EmptyString = true)]
        [Filter(Order = 60)]
        [Display(Order = 60)]
        public int Rent_Price { get; set; }
        //------------------------Country -------------------------------//
        [LocalizedDisplayName("Column_Country", typeof(TableCols))]
        [HideColumnIn(PageTemplate.List)]
        [LocalizedForeignKey(DataLoadAccess = true, TableName = "COUNTRIES")]
        [UIHint("CascadingForeignKey")]
        [FilterUIHint("CascadingForeignKey")]
        [Filter(Order = 70)]
        [Display(Order = 70)]
        [CascadeAttribute(AllowNavigation = false)]
        public virtual Country Country { get; set; }
        //------------------------City -------------------------------//
        [LocalizedDisplayName("Column_City", typeof(TableCols))]
        [LocalizedForeignKey(DataLoadAccess = true, TableName = "CITIES")]
        [UIHint("CascadingForeignKey")]
        [FilterUIHint("CascadingForeignKey")]
        [Cascade(ParentColumn = "Country",AllowNavigation =false, ConnectedColumn = "Country_ID",Child_Select_First = "Column_City_Select_First",CustomFeildName ="Tooltip",CustomFeildValue ="Longitude;Latitude")]
        [Filter(Order = 80)]
        [Display(Order = 80)]
        [CustomView(Length =13)]
        public virtual City City { get; set; }
        //------------------------Address-------------------------------//
        [LocalizedDisplayName("Column_Address", typeof(TableCols))]
        [LocalizedDescription("Column_Address", typeof(TableColsDesc))]
        [Display(Order = 90)]
        public string Address { get; set; }
        //------------------------Zip Code-------------------------------//
        [LocalizedDisplayName("Column_Zip", typeof(TableCols))]
        [LocalizedDescription("Column_Zip", typeof(TableColsDesc))]
        [HideColumnIn(PageTemplate.List | PageTemplate.ListDetails)]
        [Filter(Order = 110)]
        [Display(Order = 110)]
        public string Zip_Code { get; set; }
        //------------------------Location-------------------------------//
        [UIHint("MapLocation")]
        [HideColumnIn(PageTemplate.List | PageTemplate.ListDetails)]
        [LocalizedDisplayName("Column_Location", typeof(TableCols))]
        [Filter(Order = 120)]
        [Display(Order = 120)]
        public string Location { get; set; }
        //------------------------Floor-------------------------------//
        [LocalizedDisplayName("Column_Floor", typeof(TableCols))]
        [LocalizedDescription("Column_Floor", typeof(TableColsDesc))]
        [HideBasedOnDependeeValue("Property_Type",
            new string[] { "3", "4", "6", "8" })] //Write Comment for the colums' names, please.
        [UIHint("Integer")]
        [Integer(MaxValue = 250, MinValue = -25)]
        [CustomView(CustomRes = "RealEstate", Floor = true)]
        [Filter(Order = 130)]
        [Display(Order = 130)]
        public int Floor { get; set; }
        //------------------------Has Garden-------------------------------//
        [LocalizedDisplayName("Column_Garden", typeof(TableCols))]
        [LocalizedDescription("Column_Garden", typeof(TableColsDesc))]
        [HideColumnIn(PageTemplate.List | PageTemplate.ListDetails)]
        [FilterUIHint("Boolean")]
        [HideBasedOnDependeeValue("Property_Type",
            new string[] { "2", "3", "4", "5", "7", "8" })] //Write Comment for the colums' names, please.
        [Filter(Order = 140)]
        [Display(Order = 140)]
        public Nullable<bool> Has_Garden { get; set; }
        //------------------------Num Bedrooms-------------------------------//
        [LocalizedDisplayName("Column_Num_Bedroom", typeof(TableCols))]
        [LocalizedDescription("Column_Num_Bedroom", typeof(TableColsDesc))]
        [HideColumnIn(PageTemplate.List | PageTemplate.ListDetails)]
        [HideBasedOnDependeeValue("Property_Type",
            new string[] { "3", "4", "5", "7", "8" })] //Write Comment for the colums' names, please.
        [Filter(Order = 150)]
        [Display(Order = 150)]
        public int Num_Bedrooms { get; set; }
        //------------------------Num Bathrooms-------------------------------//
        [LocalizedDisplayName("Column_Num_Bathroom", typeof(TableCols))]
        [LocalizedDescription("Column_Num_Bathroom", typeof(TableColsDesc))]
        [HideColumnIn(PageTemplate.List | PageTemplate.ListDetails)]
        [HideBasedOnDependeeValue("Property_Type",
            new string[] { "3", "4", "5", "7", "8" })] //Write Comment for the colums' names, please.
        [Filter(Order = 160)]
        [Display(Order = 160)]
        public int Num_Bathrooms { get; set; }
        //------------------------Has Carage-------------------------------//
        [LocalizedDisplayName("Column_Garage", typeof(TableCols))]
        [HideBasedOnDependeeValue("Property_Type",
            new string[] { "2", "3", "4", "5", "7", "8" })] //Write Comment for the colums' names, please.
        [FilterUIHint("Boolean")]
        [LocalizedDescription("Column_Garage", typeof(TableColsDesc))]
        [HideColumnIn(PageTemplate.List | PageTemplate.ListDetails)]
        [Filter(Order = 170)]
        [Display(Order = 170)]
        public object Has_Garage { get; set; }
        //------------------------Num Floors-------------------------------//
        [LocalizedDisplayName("Column_Num_Floors", typeof(TableCols))]
        [HideBasedOnDependeeValue("Property_Type",
            new string[] { "2", "4", "5", "7" })] //Write Comment for the colums' names, please.
        [LocalizedDescription("Column_Num_Floors", typeof(TableColsDesc))]
        [HideColumnIn(PageTemplate.List | PageTemplate.ListDetails)]
        [Filter(Order = 180)]
        [Display(Order = 180)]
        public int Num_Floors { get; set; }
        //------------------------Date Added-------------------------------//
        [UIHint("DatePicker")]
        [ShowColumnOnlyIn(PageTemplate.Details)]
        [LocalizedDisplayName("Column_Date_Add", typeof(TableCols))]
        [LocalizedDescription("Column_Date_Add", typeof(TableColsDesc))]
        [HideColumnIn(PageTemplate.ListDetails)]
        [Calender(DefaultValue = "today", Disable = true)]
        [Filter(Order = 190)]
        [Display(Order = 190)]
        public string Date_Added { get; set; }
        //    //------------------------Expire Date-------------------------------//
        [UIHint("DatePicker")]
        [LocalizedDisplayName("Column_Date_Expire", typeof(TableCols))]
        [LocalizedDescription("Column_Date_Expire", typeof(TableColsDesc))]
        [HideColumnIn(PageTemplate.ListDetails)]
        [Filter(Order = 200)]
        [Display(Order = 200)]
        public string Expire_Date { get; set; }
        //------------------------User -------------------------------//
        [DisplayName("")]
        [HideColumnIn(PageTemplate.List | PageTemplate.ListDetails)]
        [UIHint("User")]
        [Filter(Hidden = true)]
        [Display(Order = 210)]
        public User User { get; set; }
        //------------------------Other Details-------------------------------//
        [UIHint("HTMLEditor")]
        [LocalizedDisplayName("Column_Other", typeof(TableCols))]
        [LocalizedDescription("Column_Other", typeof(TableColsDesc))]
        [HideColumnIn(PageTemplate.List | PageTemplate.ListDetails)]
        [Filter(Hidden = true)]
        [Display(Order = 220)]
        public string Other_Details { get; set; }
        //-------------------------Property Extract ID ----------------------//
        [UIHint("Nestoria_Logo")]
        [DisplayName("")]
        [HideColumnIn(PageTemplate.List | PageTemplate.ListDetails| PageTemplate.Insert)]
        [Filter(Hidden =true)]
        [Display(Order =230)]
        public Nullable<int> Property_Id_ext { get; set; }
        //-------------------------Url to Host Site -------------------------//
        [UIHint("Url")]
        [DisplayName("")]
        [HideColumnIn(PageTemplate.List | PageTemplate.ListDetails|PageTemplate.Insert)]
        [Filter(Hidden = true)]
        [Display(Order = 230)]
        public string Url_ext { get; set; }
    }
}
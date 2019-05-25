using DynamicDataLibrary;
using DynamicDataLibrary.Attributes;
using DynamicDataModel.App_LocalResources;
using NotAClue.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace DynamicDataModel.Model
{
    [MetadataType(typeof(Meta_User))]
    [Serializable]
    public partial class User
    {
    }
    [System.ComponentModel.DisplayName("Users")]
    [ContributionLimitedToCreator("User_ID")]
    [CustomView(HideFilters =true)]
    [EnroleUser(DefaultRole ="Stackholder")]
    [ViewLimitedToCurrentUser(true,"User_ID")]
    [DefaultPageActionAfterInsert(PageActionAfterInsert.Default)]
    [AddIconToHomePage(4, "Edit_User_Details", "/Account/Edit", @"\CustomDesign\images\ueh.png", "Edit_User_Details_Desc", Locale_Resource_Name = "Account",UrlParameters = "User_ID",CheckContribution =true)]
    [AddIconToHomePage(5, "Feed_Back", "/Feed", @"\CustomDesign\images\fbh.png", "Feed_Back_Desc", Locale_Resource_Name = "RealEstate")]
    public class Meta_User
    {
        [UIHint("User")]
        [DisplayName("")]
        public string User_ID { get; set; }
        [LocalizedDisplayName("Column_First_Name", typeof(TableCols))]
        [Display(Order = 10)]
        public string First_Name { get; set; }
        [LocalizedDisplayName("Column_Last_Name", typeof(TableCols))]
        [Display(Order = 20)]
        public string Last_Name { get; set; }
        [LocalizedDisplayName("Column_Address", typeof(TableCols))]
        [Display(Order = 30)]
        [UIHint("MultilineText")]
        [MaxLength(250)]
        [HtmlEditorAttribute(DisplaySourceTab =false)]
        public string Address { get; set; }
        [LocalizedDisplayName("Column_Has_Office", typeof(TableCols))]
        [HideColumnIn(PageTemplate.List)]
        [Display(Order = 50)]
        [CustomView(CustomText = "Page_User_Details_Office_Desc", CustomRes ="RealEstate")]
        [UIHint("Boolean")]
        public bool Has_Office { get; set; }
        [LocalizedDisplayName("Column_Allow_Prom", typeof(TableCols))]
        [Display(Order = 60)]
        [HideColumnIn(PageTemplate.List)]
        [UIHint("Boolean")]
        public bool Allow_Prom { get; set; }
        [LocalizedDisplayName("Column_Sub_NewsLetter", typeof(TableCols))]
        [HideColumnIn(PageTemplate.List)]
        [Display(Order = 65)]
        [UIHint("Boolean")]
        public bool Sub_NewsLetter { get; set; }
        [LocalizedDisplayName("Column_Email", typeof(TableCols))]
        [UIHint("EmailAddress")]
        [Display(Order = 45)]
        [EmbedField(true,true)]
        public string Email { get; set; }
        [LocalizedDisplayName("Column_Phone", typeof(TableCols))]
        [Display(Order = 40)]
        [UIHint("Phone")]
        [MaxLength(250)]
        public string Phone_Num { get; set; }
        [LocalizedDisplayName("Column_Properties", typeof(TableCols))]
        [Display(Order =50)]
        [HideColumnIn(PageTemplate.Edit|PageTemplate.List|PageTemplate.Insert)]
        public virtual ICollection<Property> Properties { get; set; }

        [HideColumnIn(PageTemplate.Edit | PageTemplate.List | PageTemplate.Insert |PageTemplate.Details|PageTemplate.List)]
        public Nullable<long> Telegram_ID { get; set; }

    }
}
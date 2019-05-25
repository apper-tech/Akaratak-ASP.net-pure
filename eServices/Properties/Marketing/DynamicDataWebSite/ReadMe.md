
----------------------------------------------
General notes about what is the customization done to dynamic data template + some handy references.
----------------------------------------------

Dynamic Data:
	+ Cusotmization at DateTime Field Template.
	+ If user is limited (has no access to edit and insert) and he accessed the Details Page, 
		the page will be redirected to the item related to this user at Details.aspx?key=value.
	+ If user is limited (has no access to edit and insert) and he accessed the List Page and there was only one item related to him, 
		the page will be redirected to the item related to this user at Details.aspx?key=value.
	+ Adding GridView field template.
	+ Adding CompairNumber filter.

 
 - Install-Package NotAClue.DynamicData.CustomFilters
 15 Filters for Dynamic Data; GreaterThan, GreaterThanOrEqual, LessThan, LessThanOrEqual, DateRange, Range, Autocomplete, Contains, MultiForeignKey, StartsWith, EndsWith, DateFrom, DataTo.

 - Install-Package NotAClue.DynamicData.CustomFieldTemplates
	NotAClue Dynamic Data Custom FieldTemplates for Uploading Files, Images, Selecting from a set of images and Ajax Date Picker.

 - Install-Package NotAClue.DynamicData.Extensions
 - Install-Package DynamicData.Extensions
	NotAClue Dynamic Data Extensions, Includes: HideColumnIn, ShowOnlyIn and Filter Order attributes with extended MetaModel.

- Install-Package NotAClue.DynamicData.AutocompleteForeignKey
	NotAClue Dynamic Data Autocomplete ForeignKey Field Templates for foreign key fields with many rows.

- Install-Package System.Linq.Dynamic
	This is the Microsoft assembly for the .Net 4.0 Dynamic language functionality.



At file: AutocompleteFilter.asmx.cs
	Change GetTable(string contextKey) to have this body:
	public static MetaTable GetTable(string contextKey)
        {
            string[] param = contextKey.Split('#');
            Debug.Assert(param.Length == 2, String.Format("The context key '{0}' is invalid", contextKey));
            if (param[0] == "System.Data.Objects.ObjectContext")
                param[0] = "System.Data.Objects.ObjectContext, System.Data.Entity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
            Type type = Type.GetType(param[0]);
            return MetaModel.GetModel(type).GetTable(param[1], type);
        }

Check: sjnaughton's Profile
	https://www.nuget.org/profiles/sjnaughton/

For AjaxControlToolKit:
	Check Securit Issue:
		http://www.asp.net/ajaxlibrary/ajaxcontroltoolkitsamplesite/htmleditorextender/htmleditorextender.aspx

Try: 
 - Install-Package NotAClueDynamicDataValidation
	Adds popup validation and field highlighting to Web Forms and Dynamic Data

 - Install-Package NotAClue.Web.UI.BootstrapWebControls
	This is a set of Web Controls to enable easy integration with Bootstrap

 - Install-Package NotAClue.Bootstrap.v3.Datetimepicker
	A date/time picker component based on the work of Stefan Petre.
	CSS is available from github. Less file is included

- Install-Package NotAClue.DynamicData.Cascade
	NotAClue Dynamic Data Cascade Filters and Field Templates



For Localization and Globalization check:
 - ASP.NET Dynamic Data 4 – UI Field Localization:
	http://carstent.com/2009/12/22/aspnet-dynamic-data-4-ui-field-localization/
	[Display(ResourceType = typeof(DynamicData), Name = "Entity_Brand_Name", ShortName = "Entity_Brand_Name_Short")]

UniqueValidationAttribute:
 - http://stackoverflow.com/questions/2691444/how-can-i-create-a-generic-uniquevalidationattribute-in-c-sharp-and-dataannotati

General Purpose Data Annotations Validation Attribute:
 - http://weblogs.asp.net/ricardoperes/archive/2012/02/20/general-purpose-data-annotations-validation-attribute.aspx
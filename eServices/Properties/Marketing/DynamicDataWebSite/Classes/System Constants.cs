using System;
namespace DynamicDataWebSite
{
    public static class CommonStrings
    {
        public const String HTMLLineBreak = "<br />";

        public const String HTMLDoubleLineBreak = "<br /><br />";

        public const String LineBreak = "\n";

        public const String DoubleLineBreak = "\n\n";

        public const String LongDashes = "-----------------------------------------------------------";

        public const String SpaceColumnSpace = " : ";

        public const String Space = " ";

        public const String LoginName = "Login Name";

        public const String MinusOne = "-1";

        public const String Null = "null";

        public const String HijriChar = "هـ";

        public const String LDAPProtocol = "LDAP://";

        public const String LeftBraket = "(";

        public const String RightBraket = ")";

        public const String BackSlash = @"\";

        public const Char BackSlashAsChar = '\\';

        public const String Column = ":";

        public const String CustomSeparator = "#$12-5-B.M.H.BOBO.FOR.EVER-2011$#";

        public const String Ar = "Ar";

        public const String En = "En";

        public const String Empty = "";

        public const String Dashe = "-";

        public const String NotApplicaple = "N/A";

        public const String PdfExtention = ".pdf";
    }

    public static class ArabicPermissions
    {
        public const String AccessToSystem = "الوصول الى النظام";

        public const String EmployeesDataInquiry = "استعلام عن الأفراد";
        public const String ViewNotes = "استعراض الملاحظات على الأفراد";
        public const String AddEditNotes = "إضافة واستعراض الملاحظات على الأفراد";

        public const String AdministrativeInfoes = "الشؤون الإدارية";
        public const String VacationApprovals = "موافقة الإجازات";
        public const String PermissionLeaveApprovals = "موافقة الرخص";
        public const String AuthorizationPermitApprovals = "موافقة الوكالات";

        public const String SickLeavesManagement = "إدارة الإجازات المرضية";
        public const String AbsencesManagement = "إدارة الغيابات";
        public const String CoursesManagement = "إدارة معلومات الدورات";
        public const String DeputationsManagement = "إدارة الانتدابات";
        public const String PunishmentsManagement = "إدارة العقوبات";
        public const String ArrestsManagement = "إدارة التوقيفات";
        public const String VisitorsManagement = "إدارة الزوار";

        public const String AddDailyReports = "إضافة التقارير اليومية";
        public const String EditDailyReports = "تعديل التقارير اليومية";
        public const String ViewDailyReports = "استعراض التقارير اليومية";

        public const String ShiftsManagement = "إدارة الورديات";
        public const String ComplementShiftsManagement = "إدارة تكميل الورديات";

        public const String ViewReports = "عرض التقارير";
        public const String Administration = "إدارة النظام";
    }

    public static class Permissions
    {
        public const String AccessToSystem = "AccessToSystem";

        public const String EmployeesDataInquiry = "EmployeesDataInquiry";
        public const String ViewNotes = "ViewNotes";
        public const String AddEditNotes = "AddEditNotes";

        public const String AdministrativeInfoes = "AdministrativeInfoes";
        public const String VacationApprovals = "VacationApprovals";
        public const String PermissionLeaveApprovals = "PermissionLeaveApprovals";
        public const String AuthorizationPermitApprovals = "AuthorizationPermitApprovals";

        public const String SickLeavesManagement = "SickLeavesManagement";
        public const String AbsencesManagement = "AbsencesManagement";
        public const String CoursesManagement = "CoursesManagement";
        public const String DeputationsManagement = "DeputationsManagement";
        public const String PunishmentsManagement = "PunishmentsManagement";
        public const String ArrestsManagement = "ArrestsManagement";
        public const String VisitorsManagement = "VisitorsManagement";

        public const String AddDailyReports = "AddDailyReports";
        public const String EditDailyReports = "EditDailyReports";
        public const String ViewDailyReports = "ViewDailyReports";

        public const String ShiftsManagement = "ShiftsManagement";
        public const String ComplementShiftsManagement = "ComplementShiftsManagement";

        public const String ViewReports = "ViewReports";
        public const String Administration = "Administration";
    }

    public enum UpdateUsersPermessionsOperationResult
    {
        NotDefined,
        NotAllUsersExists,
        Failed,
        Succeed,
    }

}

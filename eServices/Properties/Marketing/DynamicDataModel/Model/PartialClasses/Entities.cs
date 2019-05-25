using DynamicDataLibrary.ModelRelated;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using DynamicDataLibrary;
using System.Web.DynamicData;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

namespace DynamicDataModel.Model
{
    public partial class Entities
    {
        #region Custom Actions Dictionaries

        private static bool isDictionalyFiled = false;

        private static Dictionary<System.Type, CheckAndProcessOnInsert> customCheckBeforeInsert 
            = new Dictionary<System.Type, CheckAndProcessOnInsert>();
        public static Dictionary<System.Type, CheckAndProcessOnInsert> CustomCheckBeforeInsert
        {
            get { return customCheckBeforeInsert; }
        }

        private static Dictionary<System.Type, ProcessAfterInsert> customProcessAfterInsert 
            = new Dictionary<System.Type, ProcessAfterInsert>();
        public static Dictionary<System.Type, ProcessAfterInsert> CustomProcessAfterInsert
        {
            get { return customProcessAfterInsert; }
        }

        private static Dictionary<System.Type, CheckAndProcessOnEdit> customCheckBeforeEdit
            = new Dictionary<System.Type, CheckAndProcessOnEdit>();
        public static Dictionary<System.Type, CheckAndProcessOnEdit> CustomCheckBeforeEdit
        {
            get { return customCheckBeforeEdit; }
        }

        private static Dictionary<System.Type, ProcessAfterEdit> customProcessAfterEdit
            = new Dictionary<System.Type, ProcessAfterEdit>();
        public static Dictionary<System.Type, ProcessAfterEdit> CustomProcessAfterEdit
        {
            get { return customProcessAfterEdit; }
        }

        private static Dictionary<System.Type, CheckAndProcessOnDelete> customCheckBeforeDelete
            = new Dictionary<System.Type, CheckAndProcessOnDelete>();
        public static Dictionary<System.Type, CheckAndProcessOnDelete> CustomCheckBeforeDelete
        {
            get { return customCheckBeforeDelete; }
        }

        private static Dictionary<System.Type, ProcessAfterDelete> customProcessAfterDelete
            = new Dictionary<System.Type, ProcessAfterDelete>();
        public static Dictionary<System.Type, ProcessAfterDelete> CustomProcessAfterDelete
        {
            get { return customProcessAfterDelete; }
        }

        private static Dictionary<System.Type, ProcessQuery> customQuery
            = new Dictionary<System.Type, ProcessQuery>();
        public static Dictionary<System.Type, ProcessQuery> CustomQuery
        {
            get { return customQuery; }
        }
        #endregion

        public Entities(bool addSavingEvent, MetaModel metaModel)
            : base("name=Entities")
        {
            if (addSavingEvent)
                (this as IObjectContextAdapter).ObjectContext.SavingChanges += objectContext_SavingChanges;

            #region Register Custom Actions

            List<MetaTable> visibleTables = metaModel.VisibleTables;
            foreach (MetaTable table in visibleTables)
            {
                System.Type[] interfaces = table.GetType().GetInterfaces();
                //The folowing return false in all cases!!!
                //So, I manually added the actions...
                {
                    bool contains1 = interfaces.Contains(typeof(IAuditable));
                    bool contains2 = table.GetType().IsAssignableFrom(typeof(IAuditable));
                }
            }

            if (!isDictionalyFiled)
            {
                //Entities.CustomCheckBeforeInsert.Add(typeof(Absence), Absence.CheckAndProcessBeforeInserting);
                //Entities.CustomCheckBeforeEdit.Add(typeof(Absence), Absence.CheckAndProcessBeforeEditing);
                //Entities.CustomCheckBeforeDelete.Add(typeof(Absence), Absence.CheckAndProcessBeforeDeleting);

                //Entities.CustomProcessAfterInsert.Add(typeof(Absence), Absence.ProcessAfterInserting);
                //Entities.CustomProcessAfterEdit.Add(typeof(Absence), Absence.ProcessAfterEditing);
                //Entities.CustomProcessAfterDelete.Add(typeof(Absence), Absence.ProcessAfterDeleting);
                
                //Entities.CustomCheckBeforeInsert.Add(typeof(SickLeave), SickLeave.CheckAndProcessBeforeInserting);
                //Entities.CustomCheckBeforeEdit.Add(typeof(SickLeave), SickLeave.CheckAndProcessBeforeEditing);
                //Entities.CustomCheckBeforeDelete.Add(typeof(SickLeave), SickLeave.CheckAndProcessBeforeDeleting);

                //Entities.CustomQuery.Add(typeof(View_AbsenceCount), DynamicDataModel.Model.View_AbsenceCount.CustomQuery);

                //Entities.CustomCheckBeforeInsert.Add(typeof(Vacation), Vacation.CheckAndProcessBeforeInserting);
                //Entities.CustomProcessAfterInsert.Add(typeof(Vacation), Vacation.ProcessAfterInserting);
                //Entities.CustomCheckBeforeDelete.Add(typeof(Vacation), Vacation.CheckAndProcessBeforeDeleting);

                //Entities.CustomProcessAfterEdit.Add(typeof(VacationAdministrativeInfo), VacationAdministrativeInfo.ProcessAfterEditing);
                //Entities.CustomQuery.Add(typeof(VacationAdministrativeInfo), DynamicDataModel.Model.VacationAdministrativeInfo.CustomQuery);

                //Entities.CustomProcessAfterEdit.Add(typeof(VacationApproval), VacationApproval.ProcessAfterEditing);
                //Entities.CustomQuery.Add(typeof(VacationApproval), DynamicDataModel.Model.VacationApproval.CustomQuery);

                //Entities.CustomProcessAfterInsert.Add(typeof(PermissionLeave), PermissionLeave.ProcessAfterInserting);
                //Entities.CustomCheckBeforeDelete.Add(typeof(PermissionLeave), PermissionLeave.CheckAndProcessBeforeDeleting);

                //Entities.CustomProcessAfterEdit.Add(typeof(PermissionLeaveAdministrativeInfo), PermissionLeaveAdministrativeInfo.ProcessAfterEditing);
                //Entities.CustomQuery.Add(typeof(PermissionLeaveAdministrativeInfo), DynamicDataModel.Model.PermissionLeaveAdministrativeInfo.CustomQuery);

                //Entities.CustomProcessAfterEdit.Add(typeof(PermissionLeaveApproval), PermissionLeaveApproval.ProcessAfterEditing);
                //Entities.CustomQuery.Add(typeof(PermissionLeaveApproval), DynamicDataModel.Model.PermissionLeaveApproval.CustomQuery);

                //Entities.CustomProcessAfterInsert.Add(typeof(AuthorizationPermit), AuthorizationPermit.ProcessAfterInserting);
                //Entities.CustomCheckBeforeDelete.Add(typeof(AuthorizationPermit), AuthorizationPermit.CheckAndProcessBeforeDeleting);

                //Entities.CustomProcessAfterEdit.Add(typeof(AuthorizationPermitAdministrativeInfo), AuthorizationPermitAdministrativeInfo.ProcessAfterEditing);
                //Entities.CustomQuery.Add(typeof(AuthorizationPermitAdministrativeInfo), DynamicDataModel.Model.AuthorizationPermitAdministrativeInfo.CustomQuery);

                //Entities.CustomProcessAfterEdit.Add(typeof(AuthorizationPermitApproval), AuthorizationPermitApproval.ProcessAfterEditing);
                //Entities.CustomQuery.Add(typeof(AuthorizationPermitApproval), DynamicDataModel.Model.AuthorizationPermitApproval.CustomQuery);

                //Entities.CustomProcessAfterEdit.Add(typeof(AuthorizationPermitAuthorizedApproval), AuthorizationPermitAuthorizedApproval.ProcessAfterEditing);
                //Entities.CustomQuery.Add(typeof(AuthorizationPermitAuthorizedApproval), DynamicDataModel.Model.AuthorizationPermitAuthorizedApproval.CustomQuery);

                //Entities.CustomProcessAfterInsert.Add(typeof(Directorate_Shift), DynamicDataModel.Model.Directorate_Shift.ProcessAfterInserting);

                //Entities.CustomProcessAfterInsert.Add(typeof(TechnicalAffairsLocation), DynamicDataModel.Model.TechnicalAffairsLocation.ProcessAfterInserting);

                //Entities.CustomProcessAfterInsert.Add(typeof(JanadriahStoreLocation), DynamicDataModel.Model.JanadriahStoreLocation.ProcessAfterInserting);

                //Entities.CustomProcessAfterInsert.Add(typeof(MonthlyShift_Distribution), DynamicDataModel.Model.MonthlyShift_Distribution.ProcessAfterInserting);

                //Entities.CustomProcessAfterInsert.Add(typeof(ComplementShift), DynamicDataModel.Model.ComplementShift.ProcessAfterInserting);

                isDictionalyFiled = true;
            }
            #endregion
        }
        public override int SaveChanges()
        {
            //throw new Exception("Congratulations!!!! SaveChanges is called!");
            var erros = this.GetValidationErrors();
            return base.SaveChanges();
        }

        static void objectContext_SavingChanges(object sender, EventArgs e)
        {
            ObjectContext objectContext = sender as ObjectContext;

            var objects = objectContext.ObjectStateManager;

            //Handle auditing:
            AuditingHelperUtility.ProcessAuditFields(objects.GetObjectStateEntries(EntityState.Added));
            AuditingHelperUtility.ProcessAuditFields(objects.GetObjectStateEntries(EntityState.Modified), InsertMode: false);

            //Set EndTime based on  StartTime and Period:
            CalculateEndDate.SetEndDate(objects.GetObjectStateEntries(EntityState.Added));
            CalculateEndDate.SetEndDate(objects.GetObjectStateEntries(EntityState.Modified));
        }

    }

}
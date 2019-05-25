using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DynamicDataModel.Model;

namespace DynamicDataModel.Classes
{
    public static class BL
    {
        //public static List<String> getAllLocations()
        //{
        //    List<String> result = new List<String>();
        //    using (Entities db = new Entities())
        //    {
        //        List<Directorate_Location> locations = db.Directorate_Location.Where(l => l.Id != null).ToList();

        //        foreach (Directorate_Location item in locations)
        //        {
        //            result.Add(item.Value);
        //        }
        //    }

        //    return result;
        //}

        //public static List<String> getAllShiftTeamNames()
        //{
        //    List<String> result = new List<String>();
        //    using (Entities db = new Entities())
        //    {
        //        List<Directorate_ShiftTeamName> shiftTeamNames = db.Directorate_ShiftTeamName.Where(s => s.Id != null).ToList();

        //        foreach (Directorate_ShiftTeamName item in shiftTeamNames)
        //        {
        //            result.Add(item.Name);
        //        }
        //    }

        //    return result;
        //}

        //public static DataTable getShiftsDistributionDetails(DateTime date)
        //{
        //    DataTable result = new DataTable();
        //    result.Columns.Add("LocationId");
        //    result.Columns.Add("ShiftId");
        //    result.Columns.Add("Value");

        //    using (Entities db = new Entities())
        //    {
        //        List<Directorate_ShiftDistribution> shiftsDistributionDetails = new List<Directorate_ShiftDistribution>();

        //        if (date != DateTime.MinValue)
        //        {
        //            shiftsDistributionDetails = db.Directorate_ShiftDistribution.Where(s => s.Directorate_Shift.Date != date).ToList();
        //        }
        //        else
        //        {
        //            shiftsDistributionDetails = db.Directorate_ShiftDistribution.Where(s => s.Id != null).ToList();
        //        }

        //        foreach (Directorate_ShiftDistribution item in shiftsDistributionDetails)
        //        {
        //            DataRow newRow = result.NewRow();

        //            newRow["LocationId"] = item.LocationId;

        //            Diectorate_ShiftDistributionShiftTeamName shiftTeam = db.Diectorate_ShiftDistributionShiftTeamName.Where(s => s.Directorate_ShiftDistributionId == item.Id).FirstOrDefault();
        //            newRow["ShiftId"] = shiftTeam.ShiftTeamNameId;

        //            List<Directorate_ShiftDistributionEmployee> employees = db.Directorate_ShiftDistributionEmployee.Where(e => e.Diectorate_ShiftDistributionShiftTeamNameId == shiftTeam.Id).ToList();
        //            String allEmployees = String.Empty;
        //            foreach (Directorate_ShiftDistributionEmployee employee in employees)
        //            {
        //                allEmployees += employee.Employee.GetRankAndName() + "<br>";
        //            }
        //            newRow["Value"] = allEmployees;

        //            result.Rows.Add(newRow);
        //        }
        //    }

        //    return result;
        //}
    }
}
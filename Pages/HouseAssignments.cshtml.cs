using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HousePointsApp.Models;
using HousePointsApp.DataServices;
using HousePointsApp.Interfaces;

namespace HousePointsApp
{
    public class HouseAssignmentsModel : PageModel
    {
        public List<(String campus_id, String house)> HouseAssignments;

        public string DisplayMessage;
        public void OnGet()
        {
            StudentDataService sds = new StudentDataService();
            HouseAssignments = sds.GetHouseAssignments();

            if (HouseAssignments == null)
            {
                DisplayMessage = "Error Obtaining Records from the Database.";
            }
            else
            {
                DisplayMessage = "Student House Assignments";
            }
        }
    }
}
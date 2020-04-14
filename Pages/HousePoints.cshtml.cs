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
    public class HousePointsModel : PageModel
    {
        public List<House> HousePoints;
        public int houses_filled;

        public string DisplayMessage;
        public void OnGet()
        {
            StudentDataService sds = new StudentDataService();
            (HousePoints, houses_filled) = sds.GetHousePoints();

            if (HousePoints == null)
            {
                DisplayMessage = "Error Obtaining Student Records from the Database.";
            }
            else
            {
                DisplayMessage = "Current Point Totals for Each House";
            }
        }
    }
}
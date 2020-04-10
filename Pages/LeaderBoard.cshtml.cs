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
    public class LeaderBoardModel : PageModel
    {
        public List<Student> TopFiveStudent;
        
        public string DisplayMessage;
        public void OnGet()
        {
            StudentDataService IfaceStudent = new StudentDataService();
            TopFiveStudent = IfaceStudent.GetTopFiveScoringStudent();
            if (TopFiveStudent == null)
            {
                DisplayMessage = "Error Obtaining Student Records from the Database.";
            }
            else
            {
                DisplayMessage = "Top Five Scoring Student at the moment";
            }
        }
    }
}
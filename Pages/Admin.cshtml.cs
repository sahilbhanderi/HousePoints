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
    public class AdminModel : PageModel
    {   
        
        [TempData]
        public string Action { get; set; } = "";
        [TempData]
        public string DisplayMessage { get; set; } = "";
        [TempData]
        public string UID { get; set; } = "";
        [TempData]
        public int ChangeValue { get; set; } 
        public string DropDownMessage = "Actions";
        public string ChangeValueLabel = "Set Point to";
        public string CurrentPoint = "No User Selected";
        
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            // Default, Not Used ATM
            return Page();
        }
        // Activates when Submit button on admin page is clicked.
        public IActionResult OnPostSubmit()
        {
            Student student = new Student();
            StudentDataService IfaceStudent = new StudentDataService();
            if (UID == "" || UID == null || Action == "" || Action == null)
            {
                DisplayMessage = $"Error Occured due to Action or ID number transition.";
            }
            if ((student = IfaceStudent.GetStudent(UID)) != null)
            {

                DisplayMessage = $"Action done with {Action} and student {student.first_name} {student.last_name}";
                switch (Action)
                {
                    case "CheckBalance":
                        CurrentPoint = student.total_points.ToString();
                        break;
                    case "IncreasePoint":
                        AdminDataService IfaceAdmin = new AdminDataService();
                        IfaceAdmin.IncrementPoints(UID, ChangeValue);
                        CurrentPoint = IfaceAdmin.CheckPoints(UID).ToString();
                        break;
                    case "DecreasePoint":
                        break;
                    case "SetPoint":
                        break;
                    case "DeleteAccount":
                        break;
                    default:
                        DisplayMessage = $"Error Occured due to Action variable.";
                        break;
                }
                return Page();
            }
            else
            {
                DisplayMessage = $"Error Occured due to Student not found.";
                return Page();
            }

        }
        public IActionResult OnPostCheckBalance()
        {
            DropDownMessage = "Check Point Balances";
            
            ChangeValueLabel = "No Input Needed";

            CurrentPoint = "99";

            Action = "CheckBalance";

            return Page();
        }
        public IActionResult OnPostIncreasePoint()
        {
            DropDownMessage = "Increase/Award Points";

            ChangeValueLabel = "Increase Point By";

            Action = "IncreasePoint";

            return Page();
        }
        public IActionResult OnPostDecreasePoint()
        {
            DropDownMessage = "Decrease/Redeem Points";

            ChangeValueLabel = "Decrease Point by";

            Action = "DecreasePoint";

            return Page();
        }
        public IActionResult OnPostSetPoint()
        {
            DropDownMessage = "Set Point Balances";

            ChangeValueLabel = "Set Point to";

            Action = "SetPoint";

            return Page();
        }
        public IActionResult OnPostDeleteAccount()
        {
            DropDownMessage = "Delete Account";

            ChangeValueLabel = "No Input Needed";

            Action = "DeleteAccount";

            return Page();
        }
    }
    
}
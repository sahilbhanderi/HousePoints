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

        [BindProperty]
        public string UID { get; set; } = "";

        [BindProperty]
        public int ChangeValue { get; set; } = 0;
        public string DropDownMessage { get; set; } = "Student Actions";
        public string PrizeMessage { get; set; } = "Prize Actions";
        public string ChangeValueLabel { get; set; } = "Set Point to";
        public string PointMessage { get; set; } = "No Selection Made";
        [TempData]
        public string PrizeList { get; set; } = null;
        [TempData]
        public string Actiontype { get; set; } = "User";

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


            AdminDataService IfaceAdmin = new AdminDataService();
            bool success = true;
            bool defaultCase = false;
            // Basic error checking
            if (UID == "" || UID == null)
            {
                DisplayMessage = $"Error Occured due to ID/Prize Field Empty.";
                return Page();
            }
            if (Action == "" || Action == null)
            {
                DisplayMessage = $"Error Occured due to Action not specified.";
                return Page();
            }
            if (ChangeValue < 0)
            {
                DisplayMessage = $"Error Occured due Negative Change Value. Please only enter positive number";
                return Page();
            }
            if (Actiontype == "User")
            {
                Student student = new Student();
                StudentDataService IfaceStudent = new StudentDataService();
                PrizeList = null;
                if ((student = IfaceStudent.GetStudent(UID)) != null)
                {
                    switch (Action)
                    {
                        case "CheckBalance":
                            PointMessage = student.total_points.ToString();
                            break;
                        case "IncreasePoint":
                            success = IfaceAdmin.IncrementPoints(UID, ChangeValue);
                            PointMessage = $" {student.first_name} {student.last_name} has {IfaceAdmin.CheckPoints(UID).ToString()} Points";
                            break;
                        case "DecreasePoint":
                            success = IfaceAdmin.DecrementPoints(UID, ChangeValue);
                            break;
                        case "SetPoint":
                            success = IfaceAdmin.SetPoints(UID, ChangeValue);
                            break;
                        case "DeleteAccount":
                            break;
                        default:
                            DisplayMessage = $"Error Occured due to Action variable.";
                            PointMessage = $"Could not Complete Action";
                            defaultCase = true;
                            success = false;
                            break;
                    }
                    if (success)
                    {
                        PointMessage = $" {student.first_name} {student.last_name} has {IfaceAdmin.CheckPoints(UID).ToString()} Points";
                        DisplayMessage = $"Action done with {Action} and student {student.first_name} {student.last_name}";
                    }
                    else if (defaultCase)
                    {
                        return Page();
                    }
                    else
                    {
                        DisplayMessage = $"Action Failed due to database operation issue";
                        PointMessage = $"Could not Complete Action";
                    }
                    return Page();
                }
                else
                {
                    DisplayMessage = $"Error Occured due to Student not found.";
                    return Page();
                }
            }
            else if (Actiontype == "Prize")
            {  
                switch (Action)
                {
                    case "SetPrize":
                        success = IfaceAdmin.UpdatePrizePoints(UID, ChangeValue);
                        break;
                    case "AddPrize":
                        success = IfaceAdmin.AddPrize(UID, ChangeValue);
                        break;
                    case "DeletePrize":
                        success = IfaceAdmin.DeletePrize(UID);
                        break;
                    default:
                        DisplayMessage = $"Error Occured due to Action variable.";
                        defaultCase = true;
                        success = false;
                        break;
                }
                PrizeList = IfaceAdmin.GetAllPrizes();
                if (success)
                {
                    PointMessage = $"Action Success with Prize {UID}";
                }
                else if (defaultCase)
                {
                    return Page();
                }
                else
                {
                    PointMessage = $"Action Failed due to database operation issue";
                }
                return Page();
            }
            else
            {
                DisplayMessage = $"Action Type Undefinied!";
                return Page();
            }

        }
        public IActionResult OnPostCheckBalance()
        {
            PrizeList = null;

            DropDownMessage = "Check Point Balances";
            
            ChangeValueLabel = "No Input Needed";

            Action = "CheckBalance";

            Actiontype = "User";

            ChangeValue = 0;

            return Page();
        }
        public IActionResult OnPostIncreasePoint()
        {
            PrizeList = null;

            DropDownMessage = "Increase/Award Points";

            ChangeValueLabel = "Increase Point By";

            Action = "IncreasePoint";

            Actiontype = "User";

            return Page();
        }
        public IActionResult OnPostDecreasePoint()
        {
            PrizeList = null;

            DropDownMessage = "Decrease/Redeem Points";

            ChangeValueLabel = "Decrease Point by";

            Action = "DecreasePoint";

            Actiontype = "User";

            return Page();
        }
        public IActionResult OnPostSetPoint()
        {
            PrizeList = null;

            DropDownMessage = "Set Point Balances";

            ChangeValueLabel = "Set Point to";

            Action = "SetPoint";

            Actiontype = "User";

            return Page();
        }
        public IActionResult OnPostDeleteAccount()
        {
            PrizeList = null;

            DropDownMessage = "Delete Account";

            ChangeValueLabel = "No Input Needed";

            Action = "DeleteAccount";

            Actiontype = "User";

            return Page();
        }

        public IActionResult OnPostSetPrize()
        {
            AdminDataService GetAllPrizes = new AdminDataService();
            PrizeList = $"{GetAllPrizes.GetAllPrizes()}";

            ChangeValueLabel = "Set Prize Value to";

            PrizeMessage = "Set Value of Existing Prize";

            Action = "SetPrize";

            Actiontype = "Prize";

            return Page();
        }
        public IActionResult OnPostAddPrize()
        {
        
            AdminDataService GetAllPrizes = new AdminDataService();
            PrizeList = $"{GetAllPrizes.GetAllPrizes()}";

            PrizeMessage = "Add New Prize";

            ChangeValueLabel = "Set New Prize Value to";

            Action = "AddPrize";

            Actiontype = "Prize";

            return Page();
        }
        public IActionResult OnPostDeletePrize()
        {
            AdminDataService GetAllPrizes = new AdminDataService();
            PrizeList = $"{GetAllPrizes.GetAllPrizes()}";

            PrizeMessage = "Delete Existing Prize";

            ChangeValueLabel = "No Input Needed";

            Action = "DeletePrize";

            Actiontype = "Prize";

            return Page();
        }
    }
    
}
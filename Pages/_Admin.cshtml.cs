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
        public string Actiontype { get; set; } = "";

        public string DisplayMessage { get; set; } = "";

        [BindProperty]
        public string UID { get; set; } = "";

        [BindProperty]
        public int ChangeValue { get; set; } = 0;
        
        public string DropDownMessage { get; set; } = "Student Actions";
        
        public string PrizeMessage { get; set; } = "Prize Actions";
 
        public string ChangeValueLabel { get; set; } = "Set Point to";

        public string PointMessage { get; set; } = "No Selection Made";
        
        public List<String> PrizeNameList { get; set; } = null;
        
        public List<String> PrizeValueList { get; set; } = null;


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
            //String Action = HttpContext.Session.GetString(SessionAction);
            //String Actiontype = HttpContext.Session.GetString(SessionActyionType);
            AdminDataService IfaceAdmin = new AdminDataService();
            bool success = true;
            bool defaultCase = false;
            // Basic error checking, Need to reset TempData as well
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
                            success = IfaceStudent.DeleteStudent(UID);
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
                        // do Nothing
                    }
                    else
                    {
                        DisplayMessage = $"Action Failed due to database operation issue";
                        PointMessage = $"Could not Complete Action";
                    }
                   
                }
                else
                {
                    DisplayMessage = $"Error Occured due to Student not found.";
                    
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
                PrizeNameList = IfaceAdmin.GetAllPrizesName();
                PrizeValueList = IfaceAdmin.GetAllPrizesValue();
                if (success)
                {
                    PointMessage = $"Action Success with Prize {UID}";
                }
                else if (defaultCase)
                {
                    //Do Nothing
                }
                else
                {
                    PointMessage = $"Action Failed due to database operation issue";
                }
                
            }
            else
            {
                DisplayMessage = $"Action Type Undefinied!";
                
            }
            TempData.Keep("Action");
            TempData.Keep("Actiontype");
            return Page();
        }
        public IActionResult OnPostCheckBalance()
        {
            TempData.Remove("Action");
            TempData.Remove("Actiontype");
            DropDownMessage = "Check Point Balances";
            
            ChangeValueLabel = "No Input Needed";
            
            TempData["Action"]= "CheckBalance";
            TempData["Actiontype"] = "User";

            ChangeValue = 0;

            return Page();
        }
        public IActionResult OnPostIncreasePoint()
        {
            TempData.Remove("Action");
            TempData.Remove("Actiontype");
            DropDownMessage = "Increase/Award Points";

            ChangeValueLabel = "Increase Point By";

            TempData["Action"] = "IncreasePoint";
            TempData["Actiontype"] = "User";
            return Page();
        }
        public IActionResult OnPostDecreasePoint()
        {
            TempData.Remove("Action");
            TempData.Remove("Actiontype");
            DropDownMessage = "Decrease/Redeem Points";

            ChangeValueLabel = "Decrease Point by";

            TempData["Action"] = "DecreasePoint";
            TempData["Actiontype"] = "User";
            return Page();
        }
        public IActionResult OnPostSetPoint()
        {
            TempData.Remove("Action");
            TempData.Remove("Actiontype");
            DropDownMessage = "Set Point Balances";

            ChangeValueLabel = "Set Point to";
            TempData["Action"] = "SetPoint";
            TempData["Actiontype"] = "User";
            return Page();
        }
        public IActionResult OnPostDeleteAccount()
        {
            TempData.Remove("Action");
            TempData.Remove("Actiontype");
            DropDownMessage = "Delete Account";

            ChangeValueLabel = "No Input Needed";

            TempData["Action"] = "DeleteAccount";
            TempData["Actiontype"] = "User";

            return Page();
        }

        public IActionResult OnPostSetPrize()
        {
            TempData.Remove("Action");
            TempData.Remove("Actiontype");
            AdminDataService IfaceAdmin = new AdminDataService();
            PrizeNameList = IfaceAdmin.GetAllPrizesName();
            PrizeValueList = IfaceAdmin.GetAllPrizesValue();
            if (PrizeNameList == null || PrizeValueList == null)
            {
                DisplayMessage = "Error Retrieving Prize List from Database!";
            }
            ChangeValueLabel = "Set Prize Value to";

            PrizeMessage = "Set Value of Existing Prize";
            
            TempData["Action"] = "SetPrize";
            TempData["Actiontype"] = "Prize";
            return Page();
        }
        public IActionResult OnPostAddPrize()
        {
            TempData.Remove("Action");
            TempData.Remove("Actiontype");
            AdminDataService IfaceAdmin = new AdminDataService();
            PrizeNameList = IfaceAdmin.GetAllPrizesName();
            PrizeValueList = IfaceAdmin.GetAllPrizesValue();
            if (PrizeNameList == null || PrizeValueList == null)
            {
                DisplayMessage = "Error Retrieving Prize List from Database!";
            }
            PrizeMessage = "Add New Prize";

            ChangeValueLabel = "Set New Prize Value to";
            TempData["Action"] = "AddPrize";
            TempData["Actiontype"] = "Prize";
            return Page();
        }
        public IActionResult OnPostDeletePrize()
        {
            TempData.Remove("Action");
            TempData.Remove("Actiontype");
            AdminDataService IfaceAdmin = new AdminDataService();
            PrizeNameList = IfaceAdmin.GetAllPrizesName();
            PrizeValueList = IfaceAdmin.GetAllPrizesValue();
            if (PrizeNameList == null || PrizeValueList == null)
            {
                DisplayMessage = "Error Retrieving Prize List from Database!";
            }
            PrizeMessage = "Delete Existing Prize";

            ChangeValueLabel = "No Input Needed";

            TempData["Action"] = "DeletePrize";
            TempData["Actiontype"] = "Prize";
            return Page();
        }

    }
    
}
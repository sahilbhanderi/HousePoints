using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HousePointsApp
{
    public class AdminModel : PageModel
    {   
        
        [TempData]
        public string Action { get; set; } = "";
        [TempData]
        public string DisplayMessage { get; set; } = "";
        public string DropDownMessage = "Actions";
        public string ChangeValueLabel = "Set Point to";
        public string CurrentPoint = "No User Selected";
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            // Call ProcessSwipe here with student id from card reader
            return Page();
        }
        public IActionResult OnPostSubmit()
        {
            // Call ProcessSwipe here with student id from card reader

            DisplayMessage = $"Action done with {Action}";
            return Page();
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

         

            return Page();
        }
        public IActionResult OnPostDecreasePoint()
        {
            DropDownMessage = "Decrease/Redeem Points";

            ChangeValueLabel = "Decrease Point by";

            

            return Page();
        }
        public IActionResult OnPostSetPoint()
        {
            DropDownMessage = "Set Point Balances";

            ChangeValueLabel = "Set Point to";


            return Page();
        }
        public IActionResult OnPostDeleteAccount()
        {
            DropDownMessage = "Delete Account";

            ChangeValueLabel = "No Input Needed";

         

            return Page();
        }
    }
    
}
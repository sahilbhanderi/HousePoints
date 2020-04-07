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
    public class AdminLoginModel : PageModel
    {
        [BindProperty]
        public string UID { get; set; } = "";

        [BindProperty]
        public string password { get; set; } ="";
        [TempData]
        public string DisplayMessage { get; set; } = "";
        public void OnGet()
        {

        }
        public IActionResult OnPostSubmit()
        {
            bool isAdmin = false;
            AdminDataService IAdminService = new AdminDataService();

            if (UID == "" || UID == null)
            {
                DisplayMessage = $"Error Occured due to ID/Prize Field Empty.";
                return Page();
            }
            if (password == "" || password == null)
            {
                DisplayMessage = $"Error Occured due to Action not specified.";
                return Page();
            }

            if (IAdminService.CheckIsAdmin(UID, password))
            {
                isAdmin = true;
            }
            if (isAdmin)
            {
                return RedirectToPage("./_Admin");
            }
            else
            {
                DisplayMessage = "ID/Password Invlaid";
                return Page();
            }
            
        }
    }
}
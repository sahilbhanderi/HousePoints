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
        public string DisplayMessage { get; set; } = "";
        public void OnGet()
        {

        }
        public IActionResult OnPostSubmit()
        {
            bool isAdmin = false;
            AdminDataService IAdminService = new AdminDataService();
            TempData.Remove("IsLoggedIn");
            if (UID == "" || UID == null)
            {
                DisplayMessage = $"Error Occured due to ID Field Empty.";
                return Page();
            }
            if (password == "" || password == null)
            {
                DisplayMessage = $"Error Occured due to Password Field Empty.";
                return Page();
            }

            if (IAdminService.CheckIsAdmin(UID, password))
            {
                isAdmin = true;
            }
            if (isAdmin)
            {
                TempData.TryAdd("IsLoggedIn", "True");
                return RedirectToPage("./_Admin");
            }
            else
            {   
                DisplayMessage = "ID/Password Invalid";
                return Page();
            }
            
        }
    }
}
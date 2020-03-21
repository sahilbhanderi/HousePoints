using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HousePointsApp.Models;

namespace HousePointsApp
{
    public class AddUserModel : PageModel
    {
        [ViewData]
        public string UserName { get; set; } = "Default";
        public string Text { get; set; }

        [BindProperty]
        [ViewData]
        public UserModel UserAccount { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        { 
            if (ModelState.IsValid == false)
            { return Page(); }

            ViewData["UserName"] = "Average Joe";

            return RedirectToPage("./Response/_LoginSuccess");
        }

    }
}
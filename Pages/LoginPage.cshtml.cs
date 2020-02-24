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
        [BindProperty]
        public UserModel UserAccount { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        { 
            if (ModelState.IsValid == false)
            { return Page(); }
            return RedirectToPage("./Response/_LoginSuccess");
        }
    }
}
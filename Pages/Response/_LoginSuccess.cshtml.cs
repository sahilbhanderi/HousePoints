﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HousePointsApp.Models;

namespace HousePointsApp
{
    public class LoginSucessModel : PageModel
    {
        public string UID = "Unknown";
        public string CurrentUserName = "";
        public void OnGet()
        {
            
        }
    }
}
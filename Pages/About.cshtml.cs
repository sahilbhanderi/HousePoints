using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HousePointsApp.DataServices;
using HousePointsApp.Interfaces;

namespace HousePointsApp.Pages
{
    public class AboutModel : PageModel
    {

        public List<String> PrizeNameList { get; set; } = null;

        public List<String> PrizeValueList { get; set; } = null;
        public void OnGet()
        {
            AdminDataService IfaceAdmin = new AdminDataService();
            PrizeNameList = IfaceAdmin.GetAllPrizesName();
            PrizeValueList = IfaceAdmin.GetAllPrizesValue();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HousePointsApp.Models;
using HousePointsApp.DataServices;

namespace HousePointsApp
{
    public class AddUserModel : PageModel
    {
        [BindProperty]
        public Student student { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            // Call ProcessSwipe here with student id from card reader

            if (ModelState.IsValid == false)
            { return Page(); }
            return RedirectToPage("./Response/_LoginSuccess");
        }

        public String ProcessSwipe(String studentId)
        {
            String message;

            StudentDataService sds = new StudentDataService();
            AttendanceDataService ads = new AttendanceDataService();

            Student student = sds.GetStudent(studentId);

            if (student.student_id == null)
            {
                sds.CreateStudent(studentId);
                ads.CreateAttendance(studentId);

                message = "Welcome to the Learning Factory " + student.first_name + "!" +
                    "This is your first time signing in. We have created a new profile for you. " +
                    "Have fun building!";
            } else
            {
                Attendance attendance = ads.GetLatestAttendance(studentId);

                if (attendance.check_out == null)
                {
                    ads.UpdateSession(attendance.session_id.ToString());

                    message = "You have been signed out. Thanks for coming!";
                } else
                {
                    ads.CreateAttendance(studentId);

                    message = "You have signed into the Learning Factory. Have fun building!";
                }
            }


            return message;
        }
    }
}
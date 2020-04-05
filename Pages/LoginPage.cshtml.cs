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
        [ViewData]
        public string UserName { get; set; } = "Default";
        public string Text { get; set; }

        [BindProperty]
        public Student student { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            // Call ProcessSwipe with student id from card swipe
            ProcessSwipe("385720839");

            if (ModelState.IsValid == false)
            { return Page(); }

            ViewData["UserName"] = "Average Joe";

            return RedirectToPage("./Response/_LoginSuccess");
        }

        // ProcessSwipe handles the workflow for a student swiping their id
        // card at the Learning Factory.

        public String ProcessSwipe(String studentId)
        {
            String message = "";

            StudentDataService sds = new StudentDataService();
            AttendanceDataService ads = new AttendanceDataService();

            Student student = sds.GetStudent(studentId);

            if (student.student_id == null) // new student swipes id card
            {
                sds.CreateStudent(studentId);
                ads.CreateAttendance(studentId);

                message = "Welcome to the Learning Factory " + student.first_name + "!" +
                    "This is your first time signing in. We have created a new profile for you. " +
                    "Have fun building!";
            }

            else // existing student swipes id card
            {
                Attendance attendance = ads.GetLatestAttendance(studentId);

                if (attendance.check_out == null) // student has not yet signed out
                {
                    ads.UpdateSession(attendance.session_id.ToString());

                    message = "You have been signed out. Thanks for coming!";
                }

                else // student has no open attendance sessions
                {
                    ads.CreateAttendance(studentId);

                    message = "You have signed into the Learning Factory. Have fun building!";
                }
            }


            return message;
        }
    }
}


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
        [BindProperty(Name = "username", SupportsGet = true)]
        public string UserName { get; set; }
        [BindProperty(Name = "text", SupportsGet = true)]
        public string Text { get; set; }

        [BindProperty]
        public Student student { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            // Call ProcessSwipe with student id from card swipe
            var studentID = Request.Form["studentID"];
            Text = ProcessSwipe(studentID);

            if (ModelState.IsValid == false)
            { return Page(); }


            return RedirectToPage("./Response/_LoginSuccess", "Display", new { text = Text });
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
                
                message = "Welcome to the Learning Factory " + student.first_name + "! " +
                    "This is your first time signing in. We have created a new profile for you. " +
                    "Have fun building!";
            }

            else // existing student swipes id card
            {
                Attendance attendance = ads.GetLatestAttendance(studentId);

                if (attendance.check_out == null) // student has not yet signed out
                {
                    ads.UpdateSession(attendance.session_id.ToString());

                    message = "You have been signed out. Thanks for coming " +
                        student.first_name+"!";
                }

                else // student has no open attendance sessions
                {
                    ads.CreateAttendance(studentId);

                    message = "Hi " + student.first_name + ", you have signed " +
                        "into the Learning Factory. Have fun building!";
                }
            }


            return message;
        }
    }
}


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
        public string status = "";

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            // Call ProcessSwipe with student id from card swipe
            var studentID = Request.Form["studentID"];

            // Check if studentID is valid int
            bool result = int.TryParse(studentID, out _);
            if (studentID == "" || result == false || studentID.ToString().Length != 9)
            {
                status = "Please enter valid 9 digit PSU ID.";
                return Page();
            }

            Text = ProcessSwipe(studentID);

            if (ModelState.IsValid == false)
            { return Page(); }


            return RedirectToPage("./Response/_LoginSuccess", "Display", new { text = Text });
        }

        // Function that will randomly assign new students to a house. The four
        // house options are: Nittany House, Atherton House, Schreyer House, Beaver House

        public String HouseAssignment()
        {
            // Create a random generator
            Random ran = new Random();
            int num = ran.Next(4);

            if (num == 0)
                return "Nittany House";
            else if (num == 1)
                return "Atherton House";
            else if (num == 2)
                return "Schreyer House";
            else
                return "Beaver House";
        }

        // ProcessSwipe handles the workflow for a student swiping their id
        // card at the Learning Factory.

        public String ProcessSwipe(String studentId)
        {
            String message = "";

            StudentDataService sds = new StudentDataService();
            AttendanceDataService ads = new AttendanceDataService();

            Student student = sds.GetStudent(studentId);

            if (student == null) // new student swipes id card
            {
                String house_assignment = HouseAssignment();

                if (sds.CreateStudent(studentId, house_assignment) && ads.CreateAttendance(studentId))
                {
                    message = "Welcome to the Learning Factory! " +
                        "Your house assignment is " + house_assignment +
                        ". Please see our FAQ page if you have questions about this house " +
                        "assignment or the point tracking process.";
                }
                else
                    message = "An error occurred when processing your card. Please try again or " +
                        "find a staff member if error continues to occur.";
            }

            else // existing student swipes id card
            {
                Attendance attendance = ads.GetLatestAttendance(studentId);

                if ((attendance.check_in != null) && (attendance.check_out != null))
                {
                    if (ads.CreateAttendance(studentId))
                        message = "Hi " + student.first_name + ", you have signed " +
                            "into the Learning Factory. You currently have "
                            + student.total_points + " points. Have fun building!";
                    else
                        message = "An error occurred when processing your card. Please try again or " +
                            "find a staff member if error continues to occur.";
                }

                else
                {
                    if (ads.UpdateSession(attendance.session_id.ToString()))
                        message = "You have been signed out. You received " +
                            ads.GetLatestAttendance(studentId).session_points + " points for this visit, " +
                            " and you have " + sds.GetStudent(studentId).total_points + " points " +
                            "total. Thanks for coming " + student.first_name + "!";
                    else
                        message = "An error occurred when processing your card. Please try again or " +
                            "find a staff member if error continues to occur.";
                }
            }
            return message;
        }
    }
}

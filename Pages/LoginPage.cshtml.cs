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
            StudentDataService sds = new StudentDataService();
            AttendanceDataService ads = new AttendanceDataService();

            // Test that we can get a student
            Student student = sds.GetStudent("123456789");
            Console.WriteLine(student.student_id);
            Console.WriteLine(student.first_name);
            Console.WriteLine(student.last_name);
            Console.WriteLine(student.total_points);

            // Test that we can insert a student
            //Boolean success = sds.CreateStudent("487623409");
            Boolean attendance = ads.CreateAttendance("487623409");
            //Console.WriteLine(success);
            Console.WriteLine(attendance);

            Attendance maxAttendance = ads.GetLatestAttendance("123456789");
            Console.WriteLine(maxAttendance.sessionid);
            Console.WriteLine(maxAttendance.student_id);
            Console.WriteLine(maxAttendance.check_in);
            Console.WriteLine(maxAttendance.check_out);
            Console.WriteLine(maxAttendance.session_points);

            /* Test that we can delete a student
            Boolean deleted = sds.DeleteStudent("12345678");
            Console.WriteLine(deleted);*/

            // Test that we can update a check-out time
            Boolean update_checkout = ads.UpdateSession("1010");
            Console.WriteLine(update_checkout);

            if (ModelState.IsValid == false)
            { return Page(); }
            return RedirectToPage("./Response/_LoginSuccess");
        }
    }
}
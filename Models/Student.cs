using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousePointsApp.Models
{
    public class Student
    {
        public string student_id { get; set; }
        public string campus_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int total_points { get; set; }
    }
}

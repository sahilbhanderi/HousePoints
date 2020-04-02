﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousePointsApp.Models
{
    public class Attendance
    {
        public int session_id { get; set; }
        public string student_id { get; set; }
        public DateTime check_in { get; set; }
        public Nullable<DateTime> check_out { get; set; }
        public Nullable<int> session_points { get; set; }
        public string sessionid { get; set; }
        public string studentid { get; set; }
    }
}
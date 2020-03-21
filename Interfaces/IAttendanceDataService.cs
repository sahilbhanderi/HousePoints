using System;
using HousePointsApp.Models;

namespace HousePointsApp.Interfaces
{
    public interface IAttendanceDataService
    {
        public Boolean CreateAttendance(String studentId);
        public Attendance GetLatestAttendance(String studentId);
        public Boolean UpdateSession(String sessionId);
    }
}

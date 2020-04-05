using System;
using HousePointsApp.Models;

namespace HousePointsApp.Interfaces
{
    public interface IStudentDataService
    {
        public String GetFirstName(String studentId);
        public String GetLastName(String studentId);
        public String GetCampusId(String studentId);
        public Student GetStudent(String studentId);
        public Boolean CreateStudent(String studentId);
        public Boolean DeleteStudent(String campusId);
    }
}

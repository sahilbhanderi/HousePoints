using System;
using System.Collections.Generic;
using HousePointsApp.Models;

namespace HousePointsApp.Interfaces
{
    public interface IStudentDataService
    {
        public String GetFirstName(String studentId);
        public String GetLastName(String studentId);
        public String GetCampusId(String studentId);
        public Student GetStudent(String studentId);
        public Boolean CreateStudent(String studentId, String house);
        public Boolean DeleteStudent(String campusId);

        public List<Student> GetTopFiveScoringStudent();
        public (List<House>, int) GetHousePoints();
        public List<(String campus_id, String house)> GetHouseAssignments();
    }
}

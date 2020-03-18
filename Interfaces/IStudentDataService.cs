using System;
using HousePointsApp.Models;

namespace HousePointsApp.Interfaces
{
    public interface IStudentDataService
    {
        public Student GetStudent(String studentId);

        public Boolean CreateStudent(String studentId);

        public Boolean DeleteStudent(String studentId);
    }
}

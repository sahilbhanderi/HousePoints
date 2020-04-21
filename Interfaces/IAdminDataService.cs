using System;
using System.Collections.Generic;
using HousePointsApp.Models;

namespace HousePointsApp.Interfaces
{
    interface IAdminDataService
    {
        public String GetStudentId(String campusId);

        public String GetFirstName(String campusId);

        public String GetLastName(String campusId);

        public int CheckPoints(String campusId);

        public Boolean IncrementPoints(String campusId, int point);

        public Boolean DecrementPoints(String campusId, int point);

        public Boolean SetPoints(String campusId, int points);

        public Boolean AddAccount(String campusID);

        public Boolean AddPrize(String prize, int pointValue);

        public Boolean DeletePrize(String prize);

        public Boolean UpdatePrizePoints(String prize, int prizePoints);

        public List<int> GetPrizesId();
        public List<String> GetAllPrizesName();
        public List<String> GetAllPrizesValue();

        public List<Student> GetAllStudents();

        public Boolean CheckIsAdmin(String AdminID, String password);
    }
}

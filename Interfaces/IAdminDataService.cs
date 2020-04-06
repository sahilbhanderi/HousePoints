﻿using System;
using HousePointsApp.Models;

namespace HousePointsApp.Interfaces
{
    interface IAdminDataService
    {
        public int CheckPoints(String studentId);

        public Boolean IncrementPoints(String studentId, int point);

        public Boolean DecrementPoints(String studentId, int point);

        public Boolean SetPoints(String studentId, int points);

        public Boolean AddAccount(String studentID, String firstName, String lastName);

        public Boolean AddPrize(String prize, int pointValue);

        public Boolean DeletePrize(String prize);

        public Boolean UpdatePrizePoints(String prize, int prizePoints);


    }
}
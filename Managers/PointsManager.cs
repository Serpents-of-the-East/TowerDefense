using System;
using CrowEngineBase;

namespace TowerDefense
{
    public static class PointsManager
    {
        private static int playerPoints = 0;


        public static int GetPlayerPoints()
        {
            return playerPoints;
        }

        public static void AddPlayerPoints(int points)
        {
            playerPoints += points;
        }


        public static void SubtractPlayerPoints(int points)
        {
            playerPoints -= points;
        }

        public static void ResetPoints()
        {
            playerPoints = 1000;
        }
    }
}

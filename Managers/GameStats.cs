using System;
using CrowEngineBase;

namespace TowerDefense
{
    public static class GameStats
    {
        public static int creepsDestroyed = 0;
        public static int totalValueOfTowers = 0;
        public static int numberWaves = 0;
        public static int numberLevels = 0;
        public static int currentWave = 0;
        public static int lives = 10;


        public static void DestroyedCreep()
        {
            creepsDestroyed++;
        }

        public static int GetCreepsDestroyed()
        {
            return creepsDestroyed;
        }

        public static void AddTowerValue(int towerValue)
        {
            totalValueOfTowers += towerValue;
        }

        public static void RemoveTowerValue(int towerValue)
        {
            totalValueOfTowers -= towerValue;
        }

        public static int GetTowerValue()
        {
            return totalValueOfTowers;
        }

        public static void AddWave()
        {
            numberWaves++;
        }

        public static void AddLevel()
        {
            numberLevels++;
        }

        public static void ResetStats()
        {
            creepsDestroyed = 0;
            totalValueOfTowers = 0;
            numberWaves = 0;
            numberLevels = 0;
        }

        public static int GetCurrentWave()
        {
            return currentWave;
        }

        public static void ResetCurrentWave()
        {
            currentWave = 0;
        }

        public static void IncrementCurrentWave()
        {
            currentWave++;
        }

        public static void LoseLife()
        {
            lives--;
        }

        public static void ResetLives()
        {
            lives = 10;
        }


    }
}

using System;
using CrowEngineBase;

namespace TowerDefense
{
    public class TowerComponent
    {
        // The Level the tower is on
        public int upgradeLevel;
        // The speed at which the tower can turn to target the enemy
        public float turnSpeed;
        // The type of enemy the tower can target
        public EnemyType enemyType;
        // The cooldown before the tower can shoot again
        public TimeSpan cooldown;



        public TowerComponent()
        {
            upgradeLevel = 0;
            turnSpeed = 1.0f;
            enemyType = EnemyType.GROUND;
            cooldown = TimeSpan.FromSeconds(5);
        }
    }
}

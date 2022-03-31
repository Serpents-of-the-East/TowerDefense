using System;
using System.Collections.Generic;
using CrowEngineBase;

namespace TowerDefense
{
    public class TowerComponent : Component
    {
        // The Level the tower is on
        public int upgradeLevel;
        // The speed at which the tower can turn to target the enemy
        public float turnSpeed;
        // The type of enemy the tower can target
        public EnemyType enemyType;
        // The cooldown before the tower can shoot again
        public List<TimeSpan> cooldown;
        // Damage for each level
        public List<float> damage;


        public TowerComponent()
        {
            upgradeLevel = 0;
            turnSpeed = 1.0f;
            enemyType = EnemyType.GROUND;
            damage = new List<float>() { 1.0f, 2.0f, 3.0f };
            cooldown = new List<TimeSpan>(){ TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(1) };
        }
    }
}

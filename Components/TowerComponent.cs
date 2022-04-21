using System;
using System.Collections.Generic;
using CrowEngineBase;

using Microsoft.Xna.Framework.Graphics;

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
        // Texture for each level
        public List<Texture2D> towerTextureByLevel;

        public Texture2D towerTexture { get
            {
                if (upgradeLevel > towerTextureByLevel.Count)
                {
                    return towerTextureByLevel[^1];
                }
                return towerTextureByLevel[upgradeLevel];

            } }

        public TowerComponent()
        {
            upgradeLevel = 0;
            turnSpeed = 1.0f;
            enemyType = EnemyType.GROUND;
            damage = new List<float>() { 10.0f, 20.0f, 30.0f };
            cooldown = new List<TimeSpan>(){ TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(1) };
            towerTextureByLevel = new List<Texture2D>();
        }
    }
}

using System;
using System.Collections.Generic;
using CrowEngineBase;

namespace TowerDefense
{
    public class EnemyHealth : Component
    {
        public float health;
        public float maxHealth;
        public List<GameObject> instantiateOnDeathObject;

        public EnemyHealth()
        {
            health = 100f;
            maxHealth = health;
        }
    }
}

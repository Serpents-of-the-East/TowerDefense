
using System;
using CrowEngineBase;

namespace TowerDefense
{
    public abstract class Weapon : Component
    {
        public TimeSpan maxLifetime;
        public float damage;
        public bool hasDoneDamage = false;
    }
}

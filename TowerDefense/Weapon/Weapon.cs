using System;
using CrowEngineBase;

namespace TowerDefense
{
    public abstract class Weapon : Component
    {
        public abstract float cooldownTimer { get; set; }
    }
}

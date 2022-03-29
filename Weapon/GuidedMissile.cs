using System;
using CrowEngineBase;

namespace TowerDefense
{
    public class GuidedMissile : Weapon
    {
        public float speed { get; set; }
        public float radius { get; set; }

        public GuidedMissile()
        {
            speed = 2.0f;
            maxLifetime = TimeSpan.FromSeconds(5);
            radius = 1.5f;
        }

    }
}

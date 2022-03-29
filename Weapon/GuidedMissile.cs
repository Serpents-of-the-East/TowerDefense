using System;
using CrowEngineBase;

namespace TowerDefense
{
    public class GuidedMissile : Weapon
    {
        public override float cooldownTimer { get; set; }
        public float speed { get; set; }
        public float maxLifetime { get; set; }
        public float radius { get; set; }

        public GuidedMissile()
        {
            cooldownTimer = 3.0f;
            speed = 2.0f;
            maxLifetime = 5.0f;
            radius = 1.5f;
        }

    }
}

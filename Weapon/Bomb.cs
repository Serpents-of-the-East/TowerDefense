using System;
using CrowEngineBase;

namespace TowerDefense
{
    public class Bomb : Weapon
    {
        public override float cooldownTimer { get; set; }
        public float speed { get; set; }
        public float maxLifetime { get; set; }
        public float radius { get; set; }


        public Bomb()
        {
            cooldownTimer = 5.0f;
            speed = 1.0f;
            maxLifetime = 5.0f;
            radius = 3.0f;
        }

    }
}

using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public class Bullet : Weapon
    {
        public override float cooldownTimer { get; set; }
        public float speed { get; set; }
        public float maxLifetime { get; set; }

        public Bullet()
        {
            cooldownTimer = 1.0f;
            speed = 1.0f;
            maxLifetime = 5.0f;
        }

    }
}

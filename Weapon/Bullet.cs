using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public class Bullet : Weapon
    {
        public float speed { get; set; }

        public Bullet()
        {
            speed = 1.0f;
            maxLifetime = new TimeSpan(0, 0, 5);

        }

    }
}

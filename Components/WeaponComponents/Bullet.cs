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
            maxLifetime = TimeSpan.FromMilliseconds(5000);
            damage = 0.0f;

            
        }

    }
}

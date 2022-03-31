using System;
using CrowEngineBase;

namespace TowerDefense
{
    public class Bomb : Weapon
    {
        public float speed { get; set; }
        public float radius { get; set; }


        public Bomb()
        {
            speed = 1.0f;
            maxLifetime = new TimeSpan(0, 0, 8);
            radius = 3.0f;
            damage = 1.0f;
        }

    }
}

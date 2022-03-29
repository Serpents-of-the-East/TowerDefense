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
            maxLifetime = new TimeSpan(0, 0, 5);
            radius = 3.0f;
        }

    }
}

using System;
using CrowEngineBase;

namespace TowerDefense
{
    public class FreezeBurst : Weapon
    {
        public float maxLength { get; set; }
        public float speed { get; set; }
        public float slowSpeed { get; set; }

        public FreezeBurst()
        {
            maxLifetime = TimeSpan.FromSeconds(5);
            maxLength = 5.0f;
            speed = 1.0f;
            slowSpeed = 1.0f;
        }
    }
}

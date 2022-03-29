using System;
using CrowEngineBase;

namespace TowerDefense
{
    public class FreezeBurst : Weapon
    {
        public override float cooldownTimer { get; set; }
        public float maxLength { get; set; }
        public float speed { get; set; }
        public float slowSpeed { get; set; }

        public FreezeBurst()
        {
            cooldownTimer = 10.0f;
            maxLength = 5.0f;
            speed = 1.0f;
            slowSpeed = 1.0f;
        }
    }
}

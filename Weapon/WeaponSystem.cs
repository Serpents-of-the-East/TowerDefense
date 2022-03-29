using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public class WeaponSystem : CrowEngineBase.System
    {
        public WeaponSystem(SystemManager systemManager) : base(systemManager, typeof(Weapon), typeof(Rigidbody), typeof(Transform), typeof(Collider))
        {
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (uint id in m_gameObjects.Keys)
            {
                Weapon weapon = m_gameObjects[id].GetComponent<Weapon>();

                weapon.maxLifetime -= gameTime.ElapsedGameTime;

                if (weapon.maxLifetime <= TimeSpan.Zero)
                {
                    systemManager.Remove(id);
                }

                if (weapon.GetType() == typeof(Bullet))
                {
                    
                }
                else if (weapon.GetType() == typeof(Bomb))
                {
                    
                }
                else if (weapon.GetType() == typeof(GuidedMissile))
                {
                    
                }
                else if (weapon.GetType() == typeof(FreezeBurst))
                {

                }
            }
        }
    }
}

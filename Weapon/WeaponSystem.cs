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
                Rigidbody weaponRigidbody = m_gameObjects[id].GetComponent<Rigidbody>();
                Transform weaponTransform = m_gameObjects[id].GetComponent<Transform>();


                weapon.maxLifetime -= gameTime.ElapsedGameTime;

                if (weapon.maxLifetime <= TimeSpan.Zero)
                {
                    systemManager.Remove(id);
                }

                else if (weapon.GetType() == typeof(Bullet))
                {
                    
                }
                else if (weapon.GetType() == typeof(Bomb))
                {
                    
                }
                else if (weapon.GetType() == typeof(GuidedMissile))
                {
                    if (((GuidedMissile)weapon).target != null)
                    {
                        weaponRigidbody.velocity = (((GuidedMissile)weapon).target.target.position - weaponTransform.position);
                        weaponRigidbody.velocity.Normalize();
                        weaponRigidbody.velocity *= ((GuidedMissile)weapon).speed;
                    }
                }
                else if (weapon.GetType() == typeof(FreezeBurst))
                {

                }
            }
        }
    }
}

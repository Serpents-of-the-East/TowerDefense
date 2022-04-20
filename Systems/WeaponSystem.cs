using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public class WeaponSystem : CrowEngineBase.System
    {
        public WeaponSystem(SystemManager systemManager) : base(systemManager, typeof(Bullet), typeof(Rigidbody), typeof(Transform), typeof(Collider))
        {
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (uint id in m_gameObjects.Keys)
            {
                GameObject weapon = m_gameObjects[id];
                Rigidbody weaponRigidbody = m_gameObjects[id].GetComponent<Rigidbody>();
                weapon.GetComponent<Bullet>().maxLifetime -= gameTime.ElapsedGameTime;

                if (weapon.GetComponent<Bullet>().maxLifetime <= TimeSpan.Zero)
                {
                    systemManager.Remove(id);
                }

                if (weapon.ContainsComponent<GuidedMissile>() && weapon.GetComponent<GuidedMissile>().target.position != null)
                {
                    Vector2 targetPosition = weapon.GetComponent<GuidedMissile>().target.position;
                    Vector2 direction = (targetPosition - weapon.GetComponent<Transform>().position);
                    direction.Normalize();

                    float rotation = MathF.Atan2(direction.Y, direction.X);

                    weapon.GetComponent<Transform>().rotation = rotation;
                    weaponRigidbody.velocity = targetPosition - weapon.GetComponent<Transform>().position;
                    weaponRigidbody.velocity.Normalize();
                    weaponRigidbody.velocity *= 4;

                }
            }
        }
    }
}

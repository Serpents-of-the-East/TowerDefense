using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public class WeaponSystem : CrowEngineBase.System
    {
        public WeaponSystem(SystemManager systemManager) : base(systemManager, typeof(Weapon))
        {
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (uint id in m_gameObjects.Keys)
            {
                Weapon weapon = m_gameObjects[id].GetComponent<Weapon>();

                if (weapon.cooldownTimer <= 0f)
                {

                }
                else
                {
                    weapon.cooldownTimer -= gameTime.ElapsedGameTime.Milliseconds / 1000f;
                }


            }
        }
    }
}

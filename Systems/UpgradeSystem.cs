using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CrowEngineBase;

namespace TowerDefense
{
    public class UpgradeSystem : CrowEngineBase.System
    {

        public UpgradeSystem(SystemManager systemManager) : base(systemManager, typeof(TowerComponent), typeof(Sprite))
        {
        }

        protected override void Update(GameTime gameTime)
        {
            foreach(uint id in m_gameObjects.Keys)
            {
                Sprite sprite = m_gameObjects[id].GetComponent<Sprite>();
                TowerComponent towerComponent = m_gameObjects[id].GetComponent<TowerComponent>();

                if (sprite.sprite != towerComponent.towerTexture)
                {
                    sprite.sprite = towerComponent.towerTexture;
                }
            }
        }
    }
}

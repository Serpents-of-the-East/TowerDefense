using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CrowEngineBase;

namespace TowerDefense
{
    public class UpgradeSystem : CrowEngineBase.System
    {

        public UpgradeSystem(SystemManager systemManager) : base(systemManager, typeof(TowerComponent), typeof(RenderedComponent))
        {
        }

        protected override void Update(GameTime gameTime)
        {
            foreach(uint id in m_gameObjects.Keys)
            {

                Sprite sprite = m_gameObjects[id].GetComponent<Sprite>();
                AnimatedSprite animatedSprite = m_gameObjects[id].GetComponent<AnimatedSprite>();
                TowerComponent towerComponent = m_gameObjects[id].GetComponent<TowerComponent>();

                if (towerComponent.changeAnimatedTexture && animatedSprite.spritesheet != towerComponent.towerTexture)
                {
                    animatedSprite.spritesheet = towerComponent.towerTexture;
                }
                else if (!towerComponent.changeAnimatedTexture && sprite.sprite != towerComponent.towerTexture)
                {
                    sprite.sprite = towerComponent.towerTexture;
                }
            }
        }
    }
}

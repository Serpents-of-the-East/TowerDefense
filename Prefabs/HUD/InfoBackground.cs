using System;
using System.Collections.Generic;
using System.Text;

using CrowEngineBase;

using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class InfoBackground
    {
        static InfoBackground()
        {
            ResourceManager.RegisterTexture("Textures/info-background", "info-background");
        }

        public static GameObject Create(Vector2 position)
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new Transform(position, 0, Vector2.One * 2));
            gameObject.Add(new Sprite(ResourceManager.GetTexture("info-background"), Color.White, HUDelement: true));

            return gameObject;
        }
    }
}

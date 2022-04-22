using System;
using System.Collections.Generic;
using System.Text;

using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class LargeAltScreenBackground
    {
        static LargeAltScreenBackground()
        {
            ResourceManager.RegisterTexture("Textures/other-backgrounds-large", "other-backgrounds-large");
        }

        public static GameObject Create()
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new Transform(Vector2.One * 500, 0, Vector2.One * 8));

            gameObject.Add(new Sprite(ResourceManager.GetTexture("other-backgrounds-large"), Color.White, HUDelement: false));

            return gameObject;
        }
    }
}

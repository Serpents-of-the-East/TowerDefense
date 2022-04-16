using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

using CrowEngineBase;

namespace TowerDefense
{
    public static class TitleCard
    {
        static TitleCard()
        {
            ResourceManager.RegisterTexture("Textures/magic-knights", "magic-knights");
        }

        public static GameObject Create()
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new Transform(new Vector2(500, 300), 0, Vector2.One * 1.5f));
            gameObject.Add(new AnimatedSprite(ResourceManager.GetTexture("magic-knights"), new int[] { 100, 100, 100, 100, 100, 100, 100, 100 }, Vector2.One * ResourceManager.GetTexture("magic-knights").Height, HUDelement: true));

            return gameObject;
        }
    }
}

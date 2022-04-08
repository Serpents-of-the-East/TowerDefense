using System;
using System.Collections.Generic;
using System.Text;

using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class BackgroundPrefab
    {
        static BackgroundPrefab()
        {
            ResourceManager.RegisterTexture("Textures/tower-map", "tower-map");
        }
        public static GameObject Create()
        {

            GameObject gameObject = new GameObject();
            gameObject.Add(new Sprite(ResourceManager.GetTexture("tower-map"), Color.White, 0));
            gameObject.Add(new Transform(Vector2.Zero, 0, Vector2.One * 5f));

            return gameObject;
        } 
    }
}

using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class LivesPrefab
    {
        public static GameObject Create()
        {
            GameObject gameObject = new GameObject();
            gameObject.Add(new Transform(new Vector2(800, 50), 0, Vector2.One));
            gameObject.Add(new Text("Lives: ", ResourceManager.GetFont("default"), Color.White, Color.Black, false) { centerOfRotation = new Vector2(0, ResourceManager.GetFont("default").MeasureString("Lives: ").Y / 2) });
            gameObject.Add(new RenderedComponent());
            gameObject.Add(new UpdateLivesText(gameObject));

            return gameObject;


        }
    }
}


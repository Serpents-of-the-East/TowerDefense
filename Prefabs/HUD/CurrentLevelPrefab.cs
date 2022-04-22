using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class CurrentLevelPrefab
    {
        public static GameObject Create()
        {
            GameObject gameObject = new GameObject();
            gameObject.Add(new Transform(new Vector2(800, 950), 0, Vector2.One));
            gameObject.Add(new Text("Level: ", ResourceManager.GetFont("default"), Color.White, Color.Black, false) { centerOfRotation = new Vector2(0, ResourceManager.GetFont("default").MeasureString("Level: ").Y / 2) });
            gameObject.Add(new RenderedComponent());
            gameObject.Add(new UpdateWaveText(gameObject));

            return gameObject;


        }
    }
}


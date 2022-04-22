using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class CreepsKilledPrefab
    {
        public static GameObject Create()
        {
            GameObject gameObject = new GameObject();
            gameObject.Add(new Transform(new Vector2(-200, 950), 0, Vector2.One));
            gameObject.Add(new Text("Kills: ", ResourceManager.GetFont("default"), Color.White, Color.Black, false) { centerOfRotation = new Vector2(0, ResourceManager.GetFont("default").MeasureString("Kills: ").Y / 2) });
            gameObject.Add(new RenderedComponent());
            gameObject.Add(new UpdateCreepsText(gameObject));

            return gameObject;


        }
    }
}


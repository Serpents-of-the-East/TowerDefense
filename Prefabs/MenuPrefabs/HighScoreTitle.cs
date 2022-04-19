using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class HighScoresTitle
    {
        public static GameObject Create()
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new Transform(new Vector2(500, 100), 0, Vector2.One));
            gameObject.Add(new Text("High Scores", ResourceManager.GetFont("default"), Color.Red, Color.Black, true, 0));

            return gameObject;
        }

    }
}
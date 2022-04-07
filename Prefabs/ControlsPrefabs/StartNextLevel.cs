using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class StartNextLevel
    {
        public static GameObject CreateStartNextLevel(string currentControl, float windowWidth)
        {
            GameObject gameObject = new GameObject();
            gameObject.Add(new Text($"Start Next Level: {currentControl}", ResourceManager.GetFont("default"), Color.Black, Color.White));
            gameObject.Add(new Transform(new Vector2(windowWidth / 2 - ResourceManager.GetFont("default").MeasureString($"Start Next Level: {currentControl}").X / 2, 300), 0, Vector2.One));

            return gameObject;

        }
    }
}

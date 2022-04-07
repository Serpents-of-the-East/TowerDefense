using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class PointsPrefab
    {
        public static GameObject CreatePointsPrefab()
        {
            GameObject gameObject = new GameObject();
            gameObject.Add(new Transform(new Vector2(0, 100), 0, Vector2.One));
            gameObject.Add(new Text($"${PointsManager.GetPlayerPoints()}", ResourceManager.GetFont("default"), Color.Green, Color.Black, true));
            gameObject.Add(new RenderedComponent());
            gameObject.Add(new UpdatePointsScript(gameObject));

            return gameObject;


        }
    }
}


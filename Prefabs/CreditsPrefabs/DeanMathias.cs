using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class DeanMathias
    {
        public static GameObject Create()
        {
            GameObject gameObject = new GameObject();
            gameObject.Add(new Transform(new Vector2(500, 500), 0, Vector2.One));

            gameObject.Add(new Text("Dean Mathias", ResourceManager.GetFont("default"), Color.Green, Color.Black, true));

            return gameObject;


        }
    }
}

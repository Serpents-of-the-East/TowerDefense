using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class TaylorAnderson
    {
        public static GameObject CreateTaylorAnderson()
        {
            GameObject gameObject = new GameObject();
            gameObject.Add(new Transform(new Vector2(500, 400), 0, Vector2.One));

            gameObject.Add(new Text("Taylor Anderson", ResourceManager.GetFont("default"), Color.Green, Color.Black, true));

            return gameObject;
        }
    }
}

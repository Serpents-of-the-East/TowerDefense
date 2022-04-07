using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class RyanAnderson
    {
        public static GameObject CreateRyanAnderson()
        {
            GameObject gameObject = new GameObject();
            gameObject.Add(new Transform(new Vector2(500, 300), 0, Vector2.One));

            gameObject.Add(new Text("Ryan Anderson", ResourceManager.GetFont("default"), Color.Green, Color.Black, true));

            return gameObject;


        }
    }
}

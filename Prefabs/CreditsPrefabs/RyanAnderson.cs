using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class RyanAnderson
    {
        public static GameObject CreateRyanAnderson(float windowWidth)
        {
            GameObject gameObject = new GameObject();
            gameObject.Add(new Transform(new Vector2(windowWidth / 2 - ResourceManager.GetFont("default").MeasureString("Ryan Anderson").X / 2, 300), 0, Vector2.One));

            gameObject.Add(new Text("Ryan Anderson", ResourceManager.GetFont("default"), Color.Green, Color.Black, true));

            return gameObject;


        }
    }
}

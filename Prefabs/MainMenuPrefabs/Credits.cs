using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class Credits
    {
        public static GameObject CreateCredits(float windowWidth)
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new Rigidbody());
            gameObject.Add(new RectangleCollider(new Vector2(1000, 25)));
            gameObject.Add(new Transform(new Vector2(windowWidth, 500), 0, Vector2.One));
            gameObject.Add(new Text("Credits", ResourceManager.GetFont("default"), Color.White, Color.Black, true, 0));


            return gameObject;
        }
    }
}

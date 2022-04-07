using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class Credits
    {
        public static GameObject CreateCredits()
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new Rigidbody());
            gameObject.Add(new RectangleCollider(new Vector2(500, 100)));
            gameObject.Add(new Transform(new Vector2(700, 600), 0, Vector2.One));
            gameObject.Add(new Text("Credits", ResourceManager.GetFont("default"), Color.White, Color.Black, true, 0));
            gameObject.Add(new RenderedComponent());
            gameObject.Add(new MenuItem(ScreenEnum.Credits));

            return gameObject;
        }
    }
}

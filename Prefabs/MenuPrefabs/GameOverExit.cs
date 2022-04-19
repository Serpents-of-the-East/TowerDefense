using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class GameOverExit
    {
        public static GameObject Create()
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new Rigidbody());
            gameObject.Add(new RectangleCollider(new Vector2(500, 100)));
            gameObject.Add(new Transform(new Vector2(500, 600), 0, Vector2.One));
            gameObject.Add(new Text("Press Escape to go back to the Main Menu", ResourceManager.GetFont("default"), Color.Red, Color.Black, true, 0));
            gameObject.Add(new RenderedComponent());
            gameObject.Add(new MenuItem(ScreenEnum.MainMenu));


            return gameObject;
        }
    }
}
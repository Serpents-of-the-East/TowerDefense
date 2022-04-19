using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class PlayGame
    {
        public static GameObject CreatePlayGame()
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new Rigidbody());
            gameObject.Add(new RenderedComponent());
            gameObject.Add(new RectangleCollider(new Vector2(500, 100)));
            gameObject.Add(new Transform(new Vector2(500, 550), 0, Vector2.One));
            gameObject.Add(new Text("New Game", ResourceManager.GetFont("default"), Color.White, Color.Black, true, 0));
            gameObject.Add(new MenuItem(ScreenEnum.Game));


            return gameObject;
        }
    }
}

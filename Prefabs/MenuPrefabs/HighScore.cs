using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class HighScore
    {
        public static GameObject Create()
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new Rigidbody());
            gameObject.Add(new RectangleCollider(new Vector2(500, 100)));
            gameObject.Add(new Transform(new Vector2(500, 850), 0, Vector2.One));
            gameObject.Add(new Text("High Score", ResourceManager.GetFont("default"), Color.White, Color.Black, true, 0));
            gameObject.Add(new MenuItem(ScreenEnum.HighScore));

            return gameObject;
        }
    }
}

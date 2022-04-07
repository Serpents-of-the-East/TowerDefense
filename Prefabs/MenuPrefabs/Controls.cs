using System;
using Microsoft.Xna.Framework;
using CrowEngineBase;

namespace TowerDefense
{
    public static class Controls
    {
        public static GameObject CreateControls()
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new Rigidbody());
            gameObject.Add(new RectangleCollider(new Vector2(500, 100)));
            gameObject.Add(new Transform(new Vector2(700, 500), 0, Vector2.One));
            gameObject.Add(new Text("Controls", ResourceManager.GetFont("default") , Color.White, Color.Black, true, 0f));
            gameObject.Add(new RenderedComponent());
            gameObject.Add(new MenuItem(ScreenEnum.Controls));

            return gameObject;
        }

    }
}

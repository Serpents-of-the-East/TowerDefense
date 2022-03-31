using System;
using Microsoft.Xna.Framework;
using CrowEngineBase;

namespace TowerDefense
{
    public static class Controls
    {
        public static GameObject CreateControls(float windowWidth)
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new Rigidbody());
            gameObject.Add(new RectangleCollider(new Vector2(1000, 25)));
            gameObject.Add(new Transform(new Vector2(windowWidth, 600), 0, Vector2.One));
            gameObject.Add(new Text("Controls", ResourceManager.GetFont("default"), Color.White, Color.Black, true, 0f));


            return gameObject;
        }

    }
}

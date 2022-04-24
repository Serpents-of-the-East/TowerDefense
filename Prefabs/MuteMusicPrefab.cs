using System;
using System.Collections.Generic;
using System.Text;

using CrowEngineBase;

using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class MuteMusicPrefab
    {
        public static GameObject Create()
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new Text("Toggle Audio", ResourceManager.GetFont("default"), Color.White, Color.Black, true));

            gameObject.Add(new Transform(new Vector2(0, 50), 0, Vector2.One));
            gameObject.Add(new RectangleCollider(new Vector2(200, 50)));
            gameObject.Add(new Rigidbody());
            gameObject.Add(new MusicToggle());

            return gameObject;
        }
    }
}

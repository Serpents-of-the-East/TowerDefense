using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class Cursor
    {
        public static GameObject CreateCursor()
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new Transform(new Vector2(500, 500), 0f, Vector2.One));
            gameObject.Add(new Rigidbody());
            gameObject.Add(new CircleCollider(2));



            return gameObject;
        }
    }
}

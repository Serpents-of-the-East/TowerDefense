using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class DestinationColliders
    {
        public static GameObject Create(Vector2 position, PathGoal pathGoal)
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new DestinationGoal() { destination = pathGoal });
            gameObject.Add(new Transform(new Vector2(position.X - 2000, position.Y - 2000), 0, Vector2.Zero));
            gameObject.Add(new RectangleCollider(new Vector2(100, 100)));
            gameObject.Add(new Rigidbody());
            gameObject.Add(new RenderedComponent());

            return gameObject;
        }
    }
}

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

            if (pathGoal == PathGoal.Up || pathGoal == PathGoal.Down)
            {
                gameObject.Add(new RectangleCollider(new Vector2(300, 100)));
            }
            else
            {
                gameObject.Add(new RectangleCollider(new Vector2(100, 300)));
            }

            gameObject.Add(new Transform(new Vector2(position.X - 1900, position.Y - 1900), 0, Vector2.Zero));
            gameObject.Add(new Rigidbody());
            gameObject.Add(new RenderedComponent());

            return gameObject;
        }
    }
}

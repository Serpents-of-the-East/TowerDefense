using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class PointsTextPrefab
    {
        public static GameObject Create(Vector2 position, SystemManager systemManager, PointsComponent enemy)
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new Transform(new Vector2(position.X, position.Y), 0, Vector2.One));
            gameObject.Add(new Text($"{enemy.points}", ResourceManager.GetFont("default"), Color.Black, Color.White, usesCameraPosition: true));
            gameObject.Add(new Rigidbody() { velocity = new Vector2(0.0f, -100.0f)});
            gameObject.Add(new CircleCollider(0));
            gameObject.Add(new PointsTextScript(gameObject, systemManager));


            return gameObject;
        }
    }
}

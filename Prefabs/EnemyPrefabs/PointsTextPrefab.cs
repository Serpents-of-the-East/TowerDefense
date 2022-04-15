using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class PointsTextPrefab
    {
        public static GameObject Create(Vector2 position, SystemManager systemManager)
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new Transform(position, 0, Vector2.One));
            gameObject.Add(new Text("YEET", ResourceManager.GetFont("default"), Color.Black, Color.White));
            gameObject.Add(new Rigidbody());
            gameObject.Add(new PointsTextScript(gameObject, systemManager));


            return gameObject;
        }
    }
}

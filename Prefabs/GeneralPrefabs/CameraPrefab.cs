using System;
using Microsoft.Xna.Framework;

using CrowEngineBase;

namespace TowerDefense
{
    public static class CameraPrefab
    {
        public static GameObject Create()
        {
            GameObject camera = new GameObject();

            camera.Add(new Transform(new Vector2(500, 500), 0, Vector2.One));
            camera.Add(new Rigidbody());
            camera.Add(new CircleCollider(1));
            camera.Add(new MouseInput());

            camera.Add(new CameraScript(camera));


            return camera;
        }
    }
}

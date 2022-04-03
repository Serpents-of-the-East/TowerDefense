using System;

using Microsoft.Xna.Framework;
using CrowEngineBase;

namespace TowerDefense
{
    public class CameraScript : ScriptBase
    {
        private Transform transform;
        private MouseInput mouse;
        private Rigidbody rb;

        private static int maxDistanceFromCursor = 400;

        private Vector2 currentDirection = new Vector2();

        private float cameraSpeed = 100;


        public CameraScript(GameObject gameObject) : base(gameObject)
        {
        }

        public override void Start()
        {
            rb = gameObject.GetComponent<Rigidbody>();
            transform = gameObject.GetComponent<Transform>();
            mouse = gameObject.GetComponent<MouseInput>();
        }

        public override void Update(GameTime gameTime)
        {
        }

        public void OnMouseMove(Vector2 mousePosition)
        {
            Vector2 currentMousePosition = mouse.PhysicsPositionCamera(transform);
            if (Vector2.DistanceSquared(currentMousePosition, transform.position) > MathF.Pow(maxDistanceFromCursor, 2))
            {
                currentDirection = currentMousePosition - transform.position;
                currentDirection.Normalize();
            }
            else
            {
                currentDirection = Vector2.Zero;
            }
            rb.velocity = currentDirection * cameraSpeed;

        }
    }
}

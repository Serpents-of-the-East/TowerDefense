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

        private static int maxDistanceFromCursor = 450;

        float minX;
        float maxX;
        float minY;
        float maxY;

        private Vector2 currentDirection = new Vector2();

        private float cameraSpeed = 600;


        public CameraScript(GameObject gameObject) : base(gameObject)
        {
        }

        public override void Start()
        {
            rb = gameObject.GetComponent<Rigidbody>();
            transform = gameObject.GetComponent<Transform>();
            mouse = gameObject.GetComponent<MouseInput>();

            minX = Pathfinder.GridToTrueCoordinate(Pathfinder.leftEntrance).X;
            maxX = Pathfinder.GridToTrueCoordinate(Pathfinder.rightEntrance).X;
            minY = Pathfinder.GridToTrueCoordinate(Pathfinder.topEntrance).Y;
            maxY = Pathfinder.GridToTrueCoordinate(Pathfinder.bottomEntrance).Y;
        }

        public override void Update(GameTime gameTime)
        {
            if (transform.position.X < minX)
            {
                transform.position= new Vector2(minX, transform.position.Y);
            }
            else if (transform.position.X > maxX)
            {
                transform.position = new Vector2(maxX, transform.position.Y);
            }

            if (transform.position.Y < minY)
            {
                transform.position = new Vector2(transform.position.X, minY);
            }
            else if (transform.position.Y > maxY)
            {
                transform.position = new Vector2(transform.position.X, maxY);
            }

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

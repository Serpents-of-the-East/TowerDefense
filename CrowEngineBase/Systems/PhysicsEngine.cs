using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

using CrowEngineBase.General;

namespace CrowEngineBase
{
    public class PhysicsEngine : System
    {
        public static int PHYSICS_DIMENSION_WIDTH = 1000;
        public static int PHYSICS_DIMENSION_HEIGHT = 1000;
        public static float GRAVITY_CONSTANT = -9.81f;

        private Quadtree quadtree;

        public PhysicsEngine(SystemManager systemManager) : base(systemManager, typeof(Transform), typeof(Rigidbody), typeof(Collider))
        {
        }

        /// <summary>
        /// This section updates all the rigidbody positions, and calls the Collision events from a component's script, if it has one
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            quadtree = new Quadtree(PHYSICS_DIMENSION_WIDTH, PHYSICS_DIMENSION_HEIGHT);

            foreach (uint id in m_gameObjects.Keys)
            {
                Transform transform = m_gameObjects[id].GetComponent<Transform>();
                Rigidbody rb = m_gameObjects[id].GetComponent<Rigidbody>();


                // Update velocity from gravity
                rb.velocity += new Vector2(0, GRAVITY_CONSTANT * (gameTime.ElapsedGameTime.Milliseconds / 1000f) * rb.gravityScale);

                // Update velocity from accleration as well
                rb.velocity += new Vector2(rb.acceleration.X * gameTime.ElapsedGameTime.Milliseconds / 1000f, rb.acceleration.Y * MathF.Pow(gameTime.ElapsedGameTime.Milliseconds / 1000f, 2f));


                // Update position from velocity
                transform.position += rb.velocity * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000f);
            }

            foreach((uint id, GameObject gameObject) in m_gameObjects)
            {
                quadtree.Insert(gameObject);
            }

            foreach(uint id in m_gameObjects.Keys)
            {
                Rigidbody rb = m_gameObjects[id].GetComponent<Rigidbody>(); // No need for null check here, by nature of being in physics engine, there is one
                List<GameObject> possibleCollisions = quadtree.GetPossibleCollisions(m_gameObjects[id]);
                List<uint> currentCollisions = new List<uint>();
                foreach (GameObject gameObject in possibleCollisions)
                {
                    if (HasCollision(m_gameObjects[id], gameObject))
                    {
                        currentCollisions.Add(gameObject.id);
                        if (!rb.currentCollidedGameObjects.Contains(gameObject.id)) // First frame of colliding
                        {
                            if (m_gameObjects[id].ContainsComponent<ScriptBase>())
                            {
                                m_gameObjects[id].GetComponent<ScriptBase>().OnCollisionStart(gameObject);
                            }

                        }
                        if (m_gameObjects[id].ContainsComponent<ScriptBase>())
                        {
                            m_gameObjects[id].GetComponent<ScriptBase>().OnCollision(gameObject);
                        }
                    }
                    else
                    {
                        if (rb.currentCollidedGameObjects.Contains(gameObject.id)) // We used to be colliding with this
                        {
                            if (m_gameObjects[id].ContainsComponent<ScriptBase>())
                            {
                                m_gameObjects[id].GetComponent<ScriptBase>().OnCollisionEnd(gameObject);
                            }
                        }
                    }
                }
                rb.currentCollidedGameObjects = currentCollisions;
            }
        }

        private bool HasCollision(GameObject one, GameObject two)
        {
            if (one.ContainsComponent<CircleCollider>())
            {
                if (two.ContainsComponent<CircleCollider>())
                {
                    return CircleOnCircle(one, two);
                }
                else
                {
                    return CircleOnSquare(one, two);
                }
            }
            else
            {
                if (two.ContainsComponent<CircleCollider>())
                {
                    return CircleOnSquare(two, one);
                }
                else
                {
                    return SquareOnSquare(one, two);
                }
            }
        }


        private bool CircleOnCircle(GameObject circle1, GameObject circle2)
        {
            // Squared distance is less than the summed squared radius
            return (Vector2.DistanceSquared(circle1.GetComponent<Transform>().position, circle2.GetComponent<Transform>().position) < MathF.Pow(circle1.GetComponent<CircleCollider>().radius + circle2.GetComponent<CircleCollider>().radius, 2));
        }

        // Used http://jeffreythompson.org/collision-detection/circle-rect.php
        private bool CircleOnSquare(GameObject circle, GameObject square)
        {
            

            Transform squareTransform = square.GetComponent<Transform>();
            Transform circleTransform = circle.GetComponent<Transform>();
            RectangleCollider squareCollider = square.GetComponent<RectangleCollider>();

            Vector2 testLocation = circleTransform.position;

            if (circleTransform.position.X < squareTransform.position.X - squareCollider.size.X / 2)
            {
                testLocation.X = squareTransform.position.X - squareCollider.size.X / 2;
            }
            else if (circleTransform.position.X > squareTransform.position.X + squareCollider.size.X / 2)
            {
                testLocation.X = squareTransform.position.X + squareCollider.size.X / 2;
            }

            if (circleTransform.position.Y < squareTransform.position.Y - squareCollider.size.Y / 2)
            {
                testLocation.Y = squareTransform.position.Y - squareCollider.size.Y / 2;
            }
            else if (circleTransform.position.Y > squareTransform.position.Y + squareCollider.size.Y / 2)
            {
                testLocation.Y = squareTransform.position.Y + squareCollider.size.Y / 2;
            }

            float squaredDistance = Vector2.DistanceSquared(circleTransform.position, testLocation);

            return (squaredDistance <= MathF.Pow(circle.GetComponent<CircleCollider>().radius, 2));


        }

        private bool SquareOnSquare(GameObject square1, GameObject square2)
        {
            Transform square1Transform = square1.GetComponent<Transform>();
            Transform square2Transform = square2.GetComponent<Transform>();

            RectangleCollider square1Collider = square1.GetComponent<RectangleCollider>();
            RectangleCollider square2Collider = square2.GetComponent<RectangleCollider>();

            return !(
                    square1Transform.position.X - square1Collider.size.X / 2f > square2Transform.position.X + square2Collider.size.X / 2f || // sq1 left is greater than sq2 right
                    square1Transform.position.X + square1Collider.size.X / 2f < square2Transform.position.X - square2Collider.size.X / 2f || // sq1 right is less than sq2 left
                    square1Transform.position.Y - square1Collider.size.Y / 2f > square2Transform.position.Y + square2Collider.size.Y / 2f || // sq1 top is below sq2 bottom
                    square1Transform.position.Y + square1Collider.size.Y / 2f < square2Transform.position.Y - square2Collider.size.Y / 2f // sq1 bottom is above sq1 top
                    );
        }

    }
}

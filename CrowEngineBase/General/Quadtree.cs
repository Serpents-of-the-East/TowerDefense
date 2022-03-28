using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace CrowEngineBase.General
{
    public class Quadtree
    {
        // Number of rigidbodies allowed in a level. It will split once it exceeds this
        public static int MAX_OBJECTS_IN_LEVEL = 50;

        // Max number of levels that are allowed
        // (important for when there are objects exactly over each other that would be a infinite loop)
        // If you set this value to 1, it should behave as a n^2 physics engine would
        public static int MAX_LEVELS = 200;

        private int currentLevel;

        private List<GameObject> gameObjectsInLevel;

        private Quadtree[] children;

        // Upper left corner of this node
        public Vector2 position { get; private set; }

        // Width and height of this node
        public Vector2 size { get; private set; }

        public Quadtree(float x, float y) : this(Vector2.Zero, new Vector2(x, y), 0)
        {

        }

        public Quadtree(Vector2 position, Vector2 size, int currentLevel)
        {
            this.position = position;
            this.size = size;
            gameObjectsInLevel = new List<GameObject>();
            this.currentLevel = currentLevel;
            children = new Quadtree[4];
        }

        /// <summary>
        /// Returns the quadrants this game object falls in
        /// </summary>
        /// <param name="obj">The game object to check</param>
        /// <returns></returns>
        public bool[] GetQuadrants(GameObject obj)
        {
            CircleCollider circleCollider = obj.GetComponent<CircleCollider>();
            RectangleCollider rectangleCollider = obj.GetComponent<RectangleCollider>();
            Transform transform = obj.GetComponent<Transform>();

            if (circleCollider == null && rectangleCollider == null)
            {
                throw new Exception($"The GameObject {obj} is missing a collider component. Check the physics system, as it should not have been added here.");
            }

            bool[] quadrants = new bool[4];
            for (int i = 0; i < 4; i++)
            {
                if (children[i] == null)
                {
                    quadrants[i] = false;
                    continue;
                }

                // Circle check
                if (rectangleCollider == null)
                {
                    Vector2 testLocation = transform.position;
                    if (transform.position.X < children[i].position.X)
                    {
                        testLocation.X = children[i].position.X;
                    }
                    else if (transform.position.X > children[i].position.X + children[i].size.X)
                    {
                        testLocation.X = children[i].position.X + children[i].size.X;
                    }

                    if (transform.position.Y < children[i].position.Y)
                    {
                        testLocation.Y = children[i].position.Y;
                    }
                    else if (transform.position.Y > children[i].position.Y + children[i].size.Y)
                    {
                        testLocation.Y = children[i].position.Y + children[i].size.Y;
                    }

                    float squaredDistance = Vector2.DistanceSquared(transform.position, testLocation);

                    if (squaredDistance <= MathF.Pow(circleCollider.radius, 2))
                    {
                        quadrants[i] = true;
                    }
                }
                // Rect check
                else
                {
                    // Adapted from Dr. Mathias' lecture slides
                    if (!(
                        transform.position.X - rectangleCollider.size.X / 2f > children[i].position.X + children[i].size.X ||
                        transform.position.X + rectangleCollider.size.X / 2f < children[i].position.X ||
                        transform.position.Y - rectangleCollider.size.Y / 2f > children[i].position.Y + children[i].size.Y ||
                        transform.position.Y + rectangleCollider.size.Y / 2f < children[i].position.Y
                        ))
                    {
                        quadrants[i] = true;
                    }
                }
            }
            return quadrants;
        }

        public void Insert(GameObject obj)
        {
            bool[] quadrantsToInsert = GetQuadrants(obj);

            // No children yet, we should just insert this
            if (children[0] == null)
            {
                gameObjectsInLevel.Add(obj);

                if (gameObjectsInLevel.Count > MAX_OBJECTS_IN_LEVEL && currentLevel < MAX_LEVELS)
                {
                    Split();
                }
            }
            // Insert into each child that we should
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    if (quadrantsToInsert[i])
                    {
                        children[i].Insert(obj);
                    }
                }
            }

        }

        public void Split()
        {
            children[0] = new Quadtree(position, size / 2, currentLevel + 1);
            children[1] = new Quadtree(new Vector2(position.X + size.X / 2, position.Y), size / 2, currentLevel + 1);
            children[2] = new Quadtree(new Vector2(position.X, position.Y + size.Y / 2), size / 2, currentLevel + 1);
            children[3] = new Quadtree(position + size / 2, size / 2, currentLevel + 1);

            foreach (GameObject obj in gameObjectsInLevel)
            {
                bool[] quadrants = GetQuadrants(obj);
                for (int i = 0; i < 4; i++)
                {
                    if (quadrants[i])
                    {
                        children[i].Insert(obj);
                    }
                }
            }
        }

        public List<GameObject> GetPossibleCollisions(GameObject obj)
        {
            if (children[0] == null)
            {
                return gameObjectsInLevel;
            }

            HashSet<GameObject> results = new HashSet<GameObject>();

            GetPossibleCollisions(obj, ref results);

            return new List<GameObject>(results);
        }

        private void GetPossibleCollisions(GameObject obj, ref HashSet<GameObject> results)
        {
            if (children[0] == null)
            {
                results.UnionWith(gameObjectsInLevel);
            }
            bool[] possibleRegions = GetQuadrants(obj);

            // Add the possible collisions for each child
            for (int i = 0; i < 4; i++)
            {
                if (possibleRegions[i])
                {
                    children[i].GetPossibleCollisions(obj, ref results);
                }
            }
        }
    }
}

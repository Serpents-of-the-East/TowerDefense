using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using CrowEngineBase;

namespace TowerDefense
{
    public static class Pathfinder
    {
        public static int SIZE_PER_TOWER = 50; // The number of physics units each tower should take up

        public static int MAP_SIZE_IN_TOWERS = 25; // The number of towers you can play in width or height

        public static bool[,] gameMap = new bool[MAP_SIZE_IN_TOWERS, MAP_SIZE_IN_TOWERS];
        public static List<Vector2> leftRightPath { get; private set; }
        public static List<Vector2> upDownPath { get; private set; }

        public static Vector2 leftEntrance;
        public static Vector2 rightEntrance;
        public static Vector2 topEntrance;
        public static Vector2 bottomEntrance;

        /// <summary>
        /// Updates the path and returns if the current map is valid.
        /// </summary>
        /// <param name="addedTower">The location of the tower you'd like to add, in terms of physics units</param>
        /// <returns></returns>
        public static bool UpdatePaths(Vector2 addedTowerPosition)
        {
            float conversionFactor = PhysicsEngine.PHYSICS_DIMENSION_WIDTH / SIZE_PER_TOWER; // how to change the position from game coordinates to grid

            bool[,] oldMap = new bool[MAP_SIZE_IN_TOWERS, MAP_SIZE_IN_TOWERS];

            Array.Copy(gameMap, oldMap, gameMap.Length);

            Vector2 translatedPosition = addedTowerPosition / conversionFactor;

            translatedPosition += new Vector2(MAP_SIZE_IN_TOWERS, MAP_SIZE_IN_TOWERS) / 2; // Normalize, so 0,0 is actually the center of the grid, so we avoid negatives

            gameMap[(int)translatedPosition.X, (int)translatedPosition.Y] = true;

            List<Vector2> solvedHorizontal = SolveMaze(leftEntrance, rightEntrance);
            List<Vector2> solvedVertical = SolveMaze(topEntrance, bottomEntrance);

            if (solvedHorizontal != null && solvedVertical != null)
            {
                leftRightPath = solvedHorizontal;
                upDownPath = solvedVertical;
            }
            else
            {
                gameMap = oldMap;
            }

            return (solvedVertical != null && solvedHorizontal != null);
        }


        /// <summary>
        /// Takes a position and aligns it with the grid
        /// </summary>
        /// <param name="originalPosition"></param>
        /// <returns></returns>
        public static Vector2 Gridify(Vector2 originalPosition)
        {
            Vector2 result = new Vector2((int)((originalPosition.X + SIZE_PER_TOWER / 2) / SIZE_PER_TOWER), (int)((originalPosition.Y + SIZE_PER_TOWER / 2) / SIZE_PER_TOWER));
            result *= SIZE_PER_TOWER;
            return result;
        }

        /// <summary>
        /// Returns the solved list, or null if unsolvable
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static List<Vector2> SolveMaze(Vector2 start, Vector2 end)
        {
            // Horizontal Solving
            List<(Vector2 position, List<Vector2> history)> queue = new List<(Vector2, List<Vector2>)>();
            bool[,] visited = new bool[SIZE_PER_TOWER, SIZE_PER_TOWER];
            queue.Add((start, new List<Vector2>()));
            while (queue.Count > 0)
            {
                (Vector2 position, List<Vector2> history) currentPosition = queue[0];
                queue.RemoveAt(0);

                currentPosition.history.Add(currentPosition.position);


                int currentX = (int)currentPosition.position.X;
                int currentY = (int)currentPosition.position.Y;
                visited[currentY, currentX] = true;

                if (new Vector2(currentX, currentY) == end)
                {
                    return currentPosition.history;
                }

                for (int horizontal = -1; horizontal < 2; horizontal++)
                {
                    for (int vertical = -1; vertical < 2; vertical++)
                    {
                        if (currentX + horizontal >= 0 &&
                            currentX + horizontal < SIZE_PER_TOWER &&
                            currentY + vertical >= 0 &&
                            currentY + vertical < SIZE_PER_TOWER &&
                            !visited[currentX + horizontal, currentY + vertical]
                            && !gameMap[currentX + horizontal, currentY + vertical])
                        {
                            queue.Add((new Vector2(currentX + horizontal, currentY + vertical), new List<Vector2>(currentPosition.history)));
                        }
                    }
                }


            }

            return null; // no path found
        }

        /// <summary>
        /// Returns the position the given object should be going towards, if it is on the left-right path
        /// </summary>
        /// <param name="currentPosition">The current physics position of the object</param>
        /// <returns></returns>
        public static Vector2 GetNextRightTarget(Vector2 currentPosition)
        {
            float conversionFactor = PhysicsEngine.PHYSICS_DIMENSION_WIDTH / SIZE_PER_TOWER; // how to change the position to the vector

            Vector2 gridPosition = currentPosition / conversionFactor;

            gridPosition = new Vector2((int)gridPosition.X, (int)gridPosition.Y);

            int currentIndex = leftRightPath.IndexOf(gridPosition);
            if (currentIndex + 1 < SIZE_PER_TOWER)
            {
                return leftRightPath[currentIndex + 1];
            }
            else
            {
                return leftRightPath[SIZE_PER_TOWER-1];
            }
        }

        /// <summary>
        /// Returns the position the given object should be going towards, if it is on the right-left path
        /// </summary>
        /// <param name="currentPosition">The current physics position of the object</param>
        /// <returns></returns>
        public static Vector2 GetNextLeftTarget(Vector2 currentPosition)
        {
            float conversionFactor = PhysicsEngine.PHYSICS_DIMENSION_WIDTH / SIZE_PER_TOWER; // how to change the position to the vector

            Vector2 gridPosition = currentPosition / conversionFactor;

            gridPosition = new Vector2((int)gridPosition.X, (int)gridPosition.Y);

            int currentIndex = leftRightPath.IndexOf(gridPosition);
            if (currentIndex - 1 >= 0)
            {
                return leftRightPath[currentIndex - 1];
            }
            else
            {
                return leftRightPath[0];
            }
        }

        /// <summary>
        /// Returns the position the given object should be going towards, if it is on the bottom-top path
        /// </summary>
        /// <param name="currentPosition">The current physics position of the object</param>
        /// <returns></returns>
        public static Vector2 GetNextUpTarget(Vector2 currentPosition)
        {
            float conversionFactor = PhysicsEngine.PHYSICS_DIMENSION_WIDTH / SIZE_PER_TOWER; // how to change the position to the vector

            Vector2 gridPosition = currentPosition / conversionFactor;

            gridPosition = new Vector2((int)gridPosition.X, (int)gridPosition.Y);

            int currentIndex = upDownPath.IndexOf(gridPosition);
            if (currentIndex - 1 >= 0)
            {
                return upDownPath[currentIndex - 1];
            }
            else
            {
                return upDownPath[0];
            }
        }

        /// <summary>
        /// Returns the position the given object should be going towards, if it is on the top-bottom path
        /// </summary>
        /// <param name="currentPosition">The current physics position of the object</param>
        /// <returns></returns>
        public static Vector2 GetNextDownTarget(Vector2 currentPosition)
        {
            float conversionFactor = PhysicsEngine.PHYSICS_DIMENSION_WIDTH / SIZE_PER_TOWER; // how to change the position to the vector

            Vector2 gridPosition = currentPosition / conversionFactor;

            gridPosition = new Vector2((int)gridPosition.X, (int)gridPosition.Y);

            int currentIndex = upDownPath.IndexOf(gridPosition);
            if (currentIndex + 1 < SIZE_PER_TOWER)
            {
                return upDownPath[currentIndex + 1];
            }
            else
            {
                return upDownPath[SIZE_PER_TOWER - 1];
            }
        }
    }
}

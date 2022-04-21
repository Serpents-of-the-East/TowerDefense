using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using CrowEngineBase;

namespace TowerDefense
{
    public enum PathGoal
    {
        Left,
        Up,
        Right,
        Down
    }

    public static class Pathfinder
    {
        public static int MAP_SIZE_IN_TOWERS = 25; // The number of towers you can play in width or height

        public static int SIZE_PER_TOWER = PhysicsEngine.PHYSICS_DIMENSION_HEIGHT * 4 / MAP_SIZE_IN_TOWERS; // The number of physics units each tower should take up

        public static float conversionFactor = PhysicsEngine.PHYSICS_DIMENSION_HEIGHT * 4 / MAP_SIZE_IN_TOWERS;

        private static bool[,] gameMap = new bool[MAP_SIZE_IN_TOWERS, MAP_SIZE_IN_TOWERS];
        public static List<Vector2> leftRightPath { get; private set; }
        public static List<Vector2> upDownPath { get; private set; }

        public static Vector2 leftEntrance = new Vector2(0, MathF.Floor(MAP_SIZE_IN_TOWERS / 2));
        public static Vector2 rightEntrance = new Vector2(MAP_SIZE_IN_TOWERS - 1, MathF.Floor(MAP_SIZE_IN_TOWERS / 2));
        public static Vector2 topEntrance = new Vector2(MathF.Floor(MAP_SIZE_IN_TOWERS / 2), 0);
        public static Vector2 bottomEntrance = new Vector2(MathF.Floor(MAP_SIZE_IN_TOWERS / 2), MAP_SIZE_IN_TOWERS - 1);

        /// <summary>
        /// Can be used to lookup where to spawn an enemy, e.g. if the enemy's goal is right, it should spawn left
        /// </summary>
        private static Dictionary<PathGoal, Vector2> spawnPointLookup = new Dictionary<PathGoal, Vector2>()
        {
            {PathGoal.Right, leftEntrance },
            {PathGoal.Down, topEntrance },
            {PathGoal.Left, rightEntrance },
            {PathGoal.Up, bottomEntrance }
        };

        public static Vector2 SpawnPointLookup(PathGoal goal)
        {
            Vector2 position = GridToTrueCoordinate(spawnPointLookup[goal]);
            return position;
        }

        public static Func<bool> CheckPathsFunc;

        /// <summary>
        /// Updates the path and returns if the current map is valid.
        /// </summary>
        /// <param name="addedTower">The location of the tower you'd like to add, in terms of physics units</param>
        /// <returns></returns>
        /// 
        public static void SolvePaths()
        {
            List<Vector2> solvedHorizontal = SolveMaze(leftEntrance, rightEntrance);
            List<Vector2> solvedVertical = SolveMaze(topEntrance, bottomEntrance);
            leftRightPath = solvedHorizontal;
            upDownPath = solvedVertical;
        }

        public static bool SellTower(Vector2 sellTowerPosition)
        {
            bool[,] oldMap = new bool[MAP_SIZE_IN_TOWERS, MAP_SIZE_IN_TOWERS];
            bool updatedMap = false;
            Array.Copy(gameMap, oldMap, gameMap.Length);

            Vector2 translatedPosition = sellTowerPosition / (conversionFactor);
            translatedPosition += Vector2.One * MAP_SIZE_IN_TOWERS / 2;



            if (gameMap[(int)translatedPosition.X, (int)translatedPosition.Y])
            {
                gameMap[(int)translatedPosition.X, (int)translatedPosition.Y] = false;
                updatedMap = true;
            }


            List<Vector2> solvedHorizontal = SolveMaze(leftEntrance, rightEntrance);
            List<Vector2> solvedVertical = SolveMaze(topEntrance, bottomEntrance);


            if (solvedHorizontal != null && solvedVertical != null)
            {
                leftRightPath = solvedHorizontal;
                upDownPath = solvedVertical;
            }


            return (updatedMap);

        }

        public static bool UpdatePaths(Vector2 addedTowerPosition)
        {
            


            bool[,] oldMap = new bool[MAP_SIZE_IN_TOWERS, MAP_SIZE_IN_TOWERS];

            Array.Copy(gameMap, oldMap, gameMap.Length);

            Vector2 translatedPosition = addedTowerPosition / (conversionFactor);
            translatedPosition += Vector2.One * MAP_SIZE_IN_TOWERS / 2;

            if (translatedPosition.X >= MAP_SIZE_IN_TOWERS || translatedPosition.Y >= MAP_SIZE_IN_TOWERS || translatedPosition.X < 0 || translatedPosition.Y < 0) // out of bounds
            {
                return false;
            }

            if (gameMap[(int)translatedPosition.X, (int)translatedPosition.Y] || Vector2.Floor(translatedPosition) == leftEntrance || Vector2.Floor(translatedPosition) == rightEntrance || Vector2.Floor(translatedPosition) == topEntrance || Vector2.Floor(translatedPosition) == bottomEntrance) // Something is already there.
            {
                return false;
            }

            gameMap[(int)translatedPosition.X, (int)translatedPosition.Y] = true;

            List<Vector2> solvedHorizontal = SolveMaze(leftEntrance, rightEntrance);
            List<Vector2> solvedVertical = SolveMaze(topEntrance, bottomEntrance);


            bool allAreValid = true;
            if (CheckPathsFunc != null)
            {
                allAreValid = CheckPathsFunc.Invoke();

                if (!allAreValid)
                {
                    return false;
                }
            }

                if (solvedHorizontal != null && solvedVertical != null && allAreValid)
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
            
            Vector2 result = Vector2.Floor((originalPosition + Vector2.One * SIZE_PER_TOWER / 2) / conversionFactor);
            result *= conversionFactor;
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
            bool[,] visited = new bool[MAP_SIZE_IN_TOWERS, MAP_SIZE_IN_TOWERS];
            queue.Add((start, new List<Vector2>()));

            visited[(int)start.X, (int)start.Y] = true;
            while (queue.Count > 0)
            {
                (Vector2 position, List<Vector2> history) currentPosition = queue[0];
                queue.RemoveAt(0);

                currentPosition.history.Add(currentPosition.position);


                int currentX = (int)currentPosition.position.X;
                int currentY = (int)currentPosition.position.Y;
                visited[currentX, currentY] = true;

                if (new Vector2(currentX, currentY) == end)
                {
                    return currentPosition.history;
                }

                for (int horizontal = -1; horizontal < 2; horizontal++)
                {
                    for (int vertical = -1; vertical < 2; vertical++)
                    {
                        if (currentX + horizontal >= 0 &&
                            currentX + horizontal < MAP_SIZE_IN_TOWERS &&
                            currentY + vertical >= 0 &&
                            currentY + vertical < MAP_SIZE_IN_TOWERS &&
                            !visited[currentX + horizontal, currentY + vertical]
                            && !gameMap[currentX + horizontal, currentY + vertical]
                            && (horizontal + vertical == 1 || horizontal + vertical == -1))
                        {
                            queue.Add((new Vector2(currentX + horizontal, currentY + vertical), new List<Vector2>(currentPosition.history)));
                            visited[currentX + horizontal, currentY + vertical] = true;
                        }
                    }
                }


            }

            return null; // no path found
        }

        /// <summary>
        /// Gets the closest path to the given vector. Should already be converted out of physics
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <returns></returns>
        private static int GetClosestHorizontalPath(Vector2 currentPosition)
        {
            int closestPath = 0;
            float closestDistance = float.MaxValue;

            for (int i = 0; i < leftRightPath.Count; i++)
            {
                float distance = Vector2.DistanceSquared(currentPosition, leftRightPath[i]);
                if (distance < closestDistance)
                {
                    closestPath = i;
                    closestDistance = distance;
                }
            }

            return closestPath;
        }

        /// <summary>
        /// Gets the closest path to the given vector. Should already be converted out of physics
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <returns></returns>
        private static int GetClosestVerticalPath(Vector2 currentPosition)
        {
            int closestPath = 0;
            float closestDistance = float.MaxValue;

            for (int i = 0; i < upDownPath.Count; i++)
            {
                float distance = Vector2.DistanceSquared(currentPosition, upDownPath[i]);
                if (distance < closestDistance)
                {
                    closestPath = i;
                    closestDistance = distance;
                }
            }

            return closestPath;
        }

        public static Vector2 GridToTrueCoordinate(Vector2 coordinate)
        {
            Vector2 result = new Vector2(coordinate.X, coordinate.Y);
            result -= Vector2.One * MAP_SIZE_IN_TOWERS / 2;

            result = Vector2.Ceiling(result);

            result *= SIZE_PER_TOWER;

            return result;
        }

        public static List<Vector2> GetSolvedMazePath(Vector2 currentPos, PathGoal goal)
        {
            Vector2 gridPosition = Gridify(currentPos);
            gridPosition /= (conversionFactor);
            gridPosition += Vector2.One * MAP_SIZE_IN_TOWERS / 2;

            gridPosition = Vector2.Floor(gridPosition);

            switch (goal)
            {
                case (PathGoal.Down):
                    return SolveMaze(gridPosition, bottomEntrance);
                case (PathGoal.Up):
                    return SolveMaze(gridPosition, topEntrance);
                case (PathGoal.Left):
                    return SolveMaze(gridPosition, leftEntrance);
                case (PathGoal.Right):
                    return SolveMaze(gridPosition, rightEntrance);
            }

            return null; // if they gave an invalid goal
        }

        /// <summary>
        /// Returns the position the given object should be going towards, if it is on the left-right path
        /// </summary>
        /// <param name="currentPosition">The current physics position of the object</param>
        /// <returns></returns>
        public static Vector2 GetNextRightTarget(Vector2 currentPosition)
        {

            Vector2 gridPosition = Gridify(currentPosition);
            gridPosition /= (conversionFactor);
            gridPosition += Vector2.One * MAP_SIZE_IN_TOWERS / 2;

            gridPosition = Vector2.Floor(gridPosition);

            Vector2 result;

            int currentIndex = leftRightPath.IndexOf(gridPosition);

            if (currentIndex == -1)
            {
                currentIndex = GetClosestHorizontalPath(gridPosition);
                result = leftRightPath[currentIndex];
            }

            else if (currentIndex + 1 < MAP_SIZE_IN_TOWERS)
            {
                result = leftRightPath[currentIndex + 1];
            }
            else
            {
                result = leftRightPath[MAP_SIZE_IN_TOWERS - 1];
            }

            result -= Vector2.One * MAP_SIZE_IN_TOWERS / 2;

            result = Vector2.Ceiling(result);

            result *= SIZE_PER_TOWER;

            return result;
        }

        /// <summary>
        /// Returns the position the given object should be going towards, if it is on the right-left path
        /// </summary>
        /// <param name="currentPosition">The current physics position of the object</param>
        /// <returns></returns>
        public static Vector2 GetNextLeftTarget(Vector2 currentPosition)
        {

            Vector2 gridPosition = currentPosition / conversionFactor;

            gridPosition += Vector2.One * MAP_SIZE_IN_TOWERS / 2;

            gridPosition = Vector2.Floor(gridPosition);

            Vector2 result;

            int currentIndex = leftRightPath.IndexOf(gridPosition);

            if (currentIndex == -1)
            {
                currentIndex = GetClosestHorizontalPath(gridPosition);
                result = leftRightPath[currentIndex];
            }

            else if (currentIndex - 1 >= 0)
            {
                result = leftRightPath[currentIndex - 1];
            }
            else
            {
                result = leftRightPath[0];
            }

            result -= Vector2.One * MAP_SIZE_IN_TOWERS / 2;

            result *= SIZE_PER_TOWER;

            return result;
        }

        /// <summary>
        /// Returns the position the given object should be going towards, if it is on the bottom-top path
        /// </summary>
        /// <param name="currentPosition">The current physics position of the object</param>
        /// <returns></returns>
        public static Vector2 GetNextUpTarget(Vector2 currentPosition)
        {

            Vector2 gridPosition = currentPosition / conversionFactor;

            gridPosition += Vector2.One * MAP_SIZE_IN_TOWERS / 2;

            gridPosition = Vector2.Floor(gridPosition);

            Vector2 result;

            int currentIndex = upDownPath.IndexOf(gridPosition);

            if (currentIndex == -1)
            {
                currentIndex = GetClosestVerticalPath(gridPosition);
                result = upDownPath[currentIndex];
            }

            else if (currentIndex - 1 >= 0)
            {
                result = upDownPath[currentIndex - 1];
            }
            else
            {
                result = upDownPath[0];
            }

            result -= Vector2.One * MAP_SIZE_IN_TOWERS / 2;

            result *= SIZE_PER_TOWER;

            return result;
        }

        /// <summary>
        /// Returns the position the given object should be going towards, if it is on the top-bottom path
        /// </summary>
        /// <param name="currentPosition">The current physics position of the object</param>
        /// <returns></returns>
        public static Vector2 GetNextDownTarget(Vector2 currentPosition)
        {

            Vector2 gridPosition = currentPosition / conversionFactor;

            gridPosition += Vector2.One * MAP_SIZE_IN_TOWERS / 2;

            gridPosition = Vector2.Floor(gridPosition);

            Vector2 result;

            int currentIndex = upDownPath.IndexOf(gridPosition);

            if (currentIndex == -1)
            {
                currentIndex = GetClosestVerticalPath(gridPosition);
                result = upDownPath[currentIndex];
            }

            else if (currentIndex + 1 < MAP_SIZE_IN_TOWERS)
            {
                result = upDownPath[currentIndex + 1];
            }
            else
            {
                result = upDownPath[MAP_SIZE_IN_TOWERS - 1];
            }

            result -= Vector2.One * MAP_SIZE_IN_TOWERS / 2;

            result *= SIZE_PER_TOWER;

            return result;
        }
    }
}

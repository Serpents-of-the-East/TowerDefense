using System;

using System.Collections.Generic;

using CrowEngineBase;
using CrowEngineBase.Utilities;

using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public class PathSystem : CrowEngineBase.System
    {
        public static float distanceTolerance = 10;

        public static float ENEMY_TURN_SPEED = 2;

        public PathSystem(SystemManager systemManager) : base(systemManager, typeof(Path), typeof(Transform))
        {
            Pathfinder.CheckPathsFunc += CheckAndUpdateAllPaths;
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (uint id in m_gameObjects.Keys)
            {
                Path path = m_gameObjects[id].GetComponent<Path>();
                Transform transform = m_gameObjects[id].GetComponent<Transform>();

                if (path.correctPath == null)
                {
                    Pathfinder.CheckPathsFunc.Invoke();
                }

                if (Vector2.DistanceSquared(transform.position, path.currentTarget) < MathF.Pow(distanceTolerance, 2) && path.correctPath.Count > 1) // we've reached our current goal
                {
                    path.correctPath.RemoveAt(0);
                }
                if (path.correctPath != null)
                {
                    path.currentTarget = Pathfinder.GridToTrueCoordinate(path.correctPath[0]);

                    Vector2 direction = path.currentTarget - transform.position;
                    transform.rotation = CrowMath.Lerp(transform.rotation, MathF.Atan2(direction.Y, direction.X), ENEMY_TURN_SPEED * (gameTime.ElapsedGameTime.Milliseconds / 1000f));
                }
            }
        }

        protected override void Add(GameObject gameObject)
        {
            base.Add(gameObject);

            if (m_gameObjects.ContainsKey(gameObject.id)) // if this was actually interested
            {
                Path path = gameObject.GetComponent<Path>();
                Transform transform = gameObject.GetComponent<Transform>();
                List<Vector2> correctPath = new List<Vector2>();


                if (gameObject.GetComponent<EnemyTag>().enemyType == EnemyType.AIR)
                {
                    switch(path.goal)
                    {
                        case PathGoal.Up:
                            correctPath.Add(Pathfinder.topEntrance);
                            break;
                        case PathGoal.Down:
                            correctPath.Add(Pathfinder.bottomEntrance);

                            break;
                        case PathGoal.Left:
                            correctPath.Add(Pathfinder.leftEntrance);

                            break;
                        case PathGoal.Right:
                            correctPath.Add(Pathfinder.rightEntrance);
                            break;
                    }
                }
                else
                {
                    correctPath = Pathfinder.GetSolvedMazePath(transform.position, path.goal);
                }
                path.correctPath = correctPath;
            }
        }

        public bool CheckAndUpdateAllPaths()
        {

            foreach (uint id in m_gameObjects.Keys)
            {
                Path path = m_gameObjects[id].GetComponent<Path>();
                Transform transform = m_gameObjects[id].GetComponent<Transform>();
                List<Vector2> correctPath = new List<Vector2>();

                if (m_gameObjects[id].GetComponent<EnemyTag>().enemyType == EnemyType.AIR)
                {
                    switch (path.goal)
                    {
                        case PathGoal.Up:
                            correctPath.Add(Pathfinder.topEntrance);
                            break;
                        case PathGoal.Down:
                            correctPath.Add(Pathfinder.bottomEntrance);

                            break;
                        case PathGoal.Left:
                            correctPath.Add(Pathfinder.leftEntrance);

                            break;
                        case PathGoal.Right:
                            correctPath.Add(Pathfinder.rightEntrance);
                            break;
                    }


                }
                else
                {
                    correctPath = Pathfinder.GetSolvedMazePath(transform.position, path.goal);
                }

                if (correctPath == null) return false;
                else
                {
                    path.correctPath = correctPath;
                }
            }

            return true;
        }


    }
}

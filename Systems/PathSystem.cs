using System;

using System.Collections.Generic;

using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public class PathSystem : CrowEngineBase.System
    {
        public static float distanceTolerance = 10;

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

                if (Vector2.DistanceSquared(transform.position, path.currentTarget) < MathF.Pow(distanceTolerance, 2) && path.correctPath.Count > 1) // we've reached our current goal
                {
                    path.correctPath.RemoveAt(0);
                }
                path.currentTarget = Pathfinder.GridToTrueCoordinate(path.correctPath[0]);

            }
        }

        public bool CheckAndUpdateAllPaths()
        {

            foreach (uint id in m_gameObjects.Keys)
            {
                Path path = m_gameObjects[id].GetComponent<Path>();
                Transform transform = m_gameObjects[id].GetComponent<Transform>();

                List<Vector2> correctPath = Pathfinder.GetSolvedMazePath(transform.position, path.goal);

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

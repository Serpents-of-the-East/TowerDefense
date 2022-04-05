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
            Pathfinder.UpdatePathsAction += UpdatePaths;
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

        protected void UpdatePaths()
        {
            foreach (uint id in m_gameObjects.Keys)
            {
                Path path = m_gameObjects[id].GetComponent<Path>();
                Transform transform = m_gameObjects[id].GetComponent<Transform>();

                path.correctPath = Pathfinder.GetSolvedMazePath(transform.position, path.goal);
            }
        }
    }
}

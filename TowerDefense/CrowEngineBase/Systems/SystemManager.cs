using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace CrowEngineBase
{
    /// <summary>
    /// This is where GameObjects should be added to. It will automatically handle adding all gameobjects to all systems
    /// </summary>
    public static class SystemManager
    {
        public static event Action<GameObject> AddGameObject;
        public static event Action<uint> RemoveGameObject;
        public static event Action<GameTime> UpdateSystem;

        /// <summary>
        /// Adds a new gameobject to all systems
        /// </summary>
        /// <param name="gameObject"></param>
        public static void Add(GameObject gameObject)
        {
            Console.WriteLine("Adding gameObjects to all systems");
            AddGameObject?.Invoke(gameObject);
        }

        public static void Remove(uint id)
        {
            RemoveGameObject?.Invoke(id);
        }

        public static void Update(GameTime gameTime)
        {
            UpdateSystem?.Invoke(gameTime);
        }
    }
}

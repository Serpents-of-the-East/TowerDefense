using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace CrowEngineBase
{
    /// <summary>
    /// This is where GameObjects should be added to. It will automatically handle adding all gameobjects to all systems
    /// </summary>
    public class SystemManager
    {
        public event Action<GameObject> AddGameObject;
        public event Action<uint> RemoveGameObject;
        public event Action<GameTime> UpdateSystem;

        private Queue<GameObject> safeAddedObjects = new Queue<GameObject>();

        /// <summary>
        /// Adds a new gameobject to all systems
        /// </summary>
        /// <param name="gameObject"></param>
        public void Add(GameObject gameObject)
        {
            AddGameObject?.Invoke(gameObject);
        }

        public void Remove(uint id)
        {
            RemoveGameObject?.Invoke(id);
        }

        public void Update(GameTime gameTime)
        {
            UpdateSystem?.Invoke(gameTime);

            while (safeAddedObjects.Count > 0)
            {
                Add(safeAddedObjects.Dequeue());
            }
        }

        /// <summary>
        /// Adds a gameobject at the END of an update frame. Safer, but should only be used when necessary
        /// </summary>
        /// <param name="gameObject"></param>
        public void DelayedAdd(GameObject gameObject)
        {
            safeAddedObjects.Enqueue(gameObject);
        }
    }
}

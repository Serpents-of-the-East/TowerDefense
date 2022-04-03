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
        }
    }
}

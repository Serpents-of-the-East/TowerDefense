using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace CrowEngineBase
{
    /// <summary>
    /// Abtract implementation of a System for ECS architecture. Taken from Dr. Mathias' ECS example.
    /// </summary>
    public abstract class System
    {
        protected Dictionary<uint, GameObject> m_gameObjects = new Dictionary<uint, GameObject>();

        private bool isEnabled;

        /// <summary>
        /// The types this system is interested in (for example, rendering system wants to see a sprite and transform)
        /// </summary>
        private Type[] ComponentTypes { get; set; }

        public System(params Type[] componentTypes)
        {
            this.ComponentTypes = componentTypes;

            SystemManager.AddGameObject += Add;
            SystemManager.RemoveGameObject += Remove;
            SystemManager.UpdateSystem += Update;

            isEnabled = true;
        }

        /// <summary>
        /// When enabled, a system is part of the global update queue
        /// </summary>
        public void Enable()
        {
            if (!isEnabled)
            {
                SystemManager.UpdateSystem += Update;
            }
            isEnabled = true;
        }

        /// <summary>
        /// When disabled, a system is NOT part of the global update queue. Generally useful for turning off renderers
        /// </summary>
        public void Disable()
        {
            if (isEnabled)
            {
                SystemManager.UpdateSystem -= Update;
            }
            isEnabled = false;
        }

        /// <summary>
        /// Checks a game object for all require types to subscribe to this system
        /// </summary>
        /// <param name="gameObject">The game object to check</param>
        /// <returns></returns>
        protected virtual bool IsInterested(GameObject gameObject)
        {
            foreach (Type type in ComponentTypes)
            {
                if (!gameObject.ContainsComponentOfParentType(type))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Adds a game object to the system. This is private, as adding calls should be done through the system manager to ensure all systems recieve it
        /// </summary>
        /// <param name="gameObject">The game object to add</param>
        protected virtual void Add(GameObject gameObject)
        {
            Debug.WriteLine($"Checking object {gameObject} against system {GetType()}");
            if (IsInterested(gameObject))
            {
                Debug.WriteLine($"Added gameobject {gameObject} to {GetType()}");
                m_gameObjects.Add(gameObject.id, gameObject);
            }
        }

        /// <summary>
        /// Removes a gameobject from the system. Private, as removing calls should be done through the system manager to ensure all systems recieve it
        /// </summary>
        /// <param name="id">ID of the object to remove</param>
        protected virtual void Remove(uint id)
        {
            m_gameObjects.Remove(id);
        }

        /// <summary>
        /// The Update function for a system. This is protected, as the update will be called using the SystemManager.
        /// </summary>
        /// <param name="gameTime"></param>
        protected abstract void Update(GameTime gameTime);
    }
}

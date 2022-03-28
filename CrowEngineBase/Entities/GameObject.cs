using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CrowEngineBase
{
    /// <summary>
    /// Heavily referenced from Dr. Mathias' ECS example program
    /// </summary>
    public sealed class GameObject
    {
        private readonly Dictionary<Type, Component> components = new Dictionary<Type, Component>();

        private static uint m_nextId = 0;

        public uint id { get; private set; }

        public GameObject()
        {
            id = m_nextId++;
        }

        /// <summary>
        /// Used to get reference to a component on a game object, which will be useful for scripting
        /// </summary>
        /// <typeparam name="TComponent">The type of component to get</typeparam>
        /// <returns>The component if found, or null otherwise</returns>
        public TComponent GetComponent<TComponent>()
            where TComponent : Component
        {
            Component component;
            components.TryGetValue(typeof(TComponent), out component);

            if (component == null)
            {
                component = GetInheritedComponent<TComponent>();
            }

            return (TComponent) component;
        }

        /// <summary>
        /// Gets the first component which fulfills this condition, which is useful for scripting
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <returns></returns>
        private TComponent GetInheritedComponent<TComponent>()
            where TComponent : Component
        {
            // 1. Find the matching type
            Type type = null;
            foreach (Type componentType in components.Keys)
            {
                if (typeof(TComponent).IsAssignableFrom(componentType))
                {
                    type = componentType;
                    break;
                }
            }
            if (type == null)
            {
                return null;
            }
            else
            {
                Component component;
                components.TryGetValue(type, out component);
                return (TComponent)component;
            }
        }

        /// <summary>
        /// Checks if a game object contains a component of the given type. Used by systems to subscribe when they need to
        /// </summary>
        /// <typeparam name="TComponent">The type of component to check for</typeparam>
        /// <returns></returns>
        public bool ContainsComponent<TComponent>()
            where TComponent : Component
        {
            return ContainsComponent(typeof(TComponent));
        }

        public bool ContainsComponent(Type type)
        {
            return components.ContainsKey(type) && components[type] != null;
        }

        /// <summary>
        /// This is slower, but checks if the gameobject has a component which could satisfy the condition.
        /// This is useful for things like colliders having a 'parent' component
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool ContainsComponentOfParentType(Type type)
        {
            foreach (Type component in components.Keys)
            {
                if (type.IsAssignableFrom(component))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Adds a list of components to a game object
        /// </summary>
        /// <param name="newComponents">The list of components to add</param>
        public void Add(params Component[] newComponents)
        {
            Debug.Assert(components != null, "You cannot add an empty list of components");

            foreach (Component component in newComponents)
            {
                Type type = component.GetType();

                Debug.Assert(typeof(Component).IsAssignableFrom(type), $"The given type should be assignable to {typeof(Component)}");
                Debug.Assert(!this.components.ContainsKey(type), $"A component of type {type} is already attached to this game object");

                this.components.Add(type, component);
            }
        }

        /// <summary>
        /// Add a single component
        /// </summary>
        /// <param name="component">The component to add</param>
        public void Add(Component component)
        {
            Debug.Assert(component != null, "Cannot add a null component");
            Debug.Assert(!this.components.ContainsKey(component.GetType()), $"A component of type {component.GetType()} has already been attached to this game object");

            components.Add(component.GetType(), component);
        }

        /// <summary>
        /// Remove all components
        /// </summary>
        public void Clear()
        {
            components.Clear();
        }

        /// <summary>
        /// Remove a list of components from a game object
        /// </summary>
        /// <param name="removedComponents">The list of components to remove</param>
        public void Remove(params Component[] removedComponents)
        {
            foreach(Component component in removedComponents)
            {
                components.Remove(component.GetType());
            }
        }

        public void Remove<TComponent>()
            where TComponent : Component
        {
            this.components.Remove(typeof(TComponent));
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", id, string.Join(", ", from c in components.Values select c.GetType().Name));
        }
    }
}

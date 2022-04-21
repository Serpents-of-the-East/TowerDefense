using System;

using CrowEngineBase;

using Microsoft.Xna.Framework;

namespace TowerDefense
{
    /// <summary>
    /// An item which can be selected to rebind the given action
    /// </summary>
    public static class ControlItem
    {
        /// <summary>
        /// An item which can be selected to rebind the given action
        /// </summary>
        /// <param name="position">Location of the menu item</param>
        /// <param name="actionName">The action to rebind</param>
        /// <param name="collisionSize">How large the mouse collision area is</param>
        /// <param name="gameplayInput">The keyboard input to read current key from</param>
        /// <returns></returns>
        public static GameObject Create(Vector2 position, string actionName, Vector2 collisionSize, KeyboardInput gameplayInput)
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new Transform(position, 0, Vector2.One));
            gameObject.Add(new Rigidbody());
            gameObject.Add(new RectangleCollider(collisionSize));
            gameObject.Add(new RenderedComponent());
            gameObject.Add(new Text($"{actionName}", ResourceManager.GetFont("default"), Color.White, Color.Black, drawOutline: true));
            gameObject.Add(new RebindControlScript(gameObject, gameplayInput, actionName));

            return gameObject;
        }
    }
}

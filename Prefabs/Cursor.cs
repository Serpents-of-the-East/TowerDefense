using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class Cursor
    {
        public static GameObject CreateCursor(Screen.SetCurrentScreenDelegate setCurrentScreenDelegate)
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new Transform(new Vector2(500, 500), 0f, Vector2.One));
            gameObject.Add(new Rigidbody());
            gameObject.Add(new CircleCollider(10));
            MouseInput mouseInput = new MouseInput();
            mouseInput.actionButtonPairs.Add("Select", MouseButton.LeftButton);
            gameObject.Add(mouseInput);
            gameObject.Add(new CursorScript(gameObject, setCurrentScreenDelegate));


            return gameObject;
        }
    }
}

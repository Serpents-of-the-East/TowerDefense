using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using CrowEngineBase;

namespace TowerDefense
{
    public static class PlacementCursor
    {
        /// <summary>
        /// Takes a system manager to be able to add in the objects when it is clicked, as well as the camera so it can follow it
        /// </summary>
        /// <param name="systemManager"></param>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static GameObject Create(SystemManager systemManager, GameObject camera, ControlLoaderSystem sys) // control loader should BE REMOVED LATER THIS IS FOR TESTING
        {
            GameObject cursor = new GameObject();

            cursor.Add(new Sprite(ResourceManager.GetTexture("empty"), Color.White, 0));
            cursor.Add(new Transform(Vector2.Zero, 0, Vector2.One));
            cursor.Add(new Rigidbody());
            cursor.Add(new CircleCollider(1));

            MouseInput mouseInput = new MouseInput();
            mouseInput.actionButtonPairs.Add("PlaceTower", MouseButton.LeftButton);
            cursor.Add(mouseInput);

            KeyboardInput keyboardInput = new KeyboardInput();
            keyboardInput.actionKeyPairs.Add("SwitchUpTower", Keys.OemPeriod);
            keyboardInput.actionKeyPairs.Add("SwitchDownTower", Keys.OemComma);
            keyboardInput.actionKeyPairs.Add("ReloadScreen", Keys.R);

            keyboardInput.actionKeyPairs.Add("ShakeScreen", Keys.Space);

            cursor.Add(keyboardInput);

            cursor.Add(new PlacementCursorScript(cursor, systemManager, camera, sys));


            return cursor;
        }
    }
}

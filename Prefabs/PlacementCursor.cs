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
        public static GameObject Create(SystemManager systemManager, GameObject camera)
        {
            GameObject cursor = new GameObject();

            cursor.Add(new Sprite(ResourceManager.GetTexture("empty"), Color.White, 0));
            cursor.Add(new Transform(Vector2.Zero, 0, Vector2.One));
            cursor.Add(new Rigidbody());
            cursor.Add(new CircleCollider(1));
            cursor.Add(new MouseInput());

            KeyboardInput keyboardInput = new KeyboardInput();
            keyboardInput.actionKeyPairs.Add("SwitchUpTower", Keys.OemPeriod);
            keyboardInput.actionKeyPairs.Add("SwitchDownTower", Keys.OemComma);

            cursor.Add(keyboardInput);

            cursor.Add(new PlacementCursorScript(cursor, systemManager, camera));


            return cursor;
        }
    }
}

using System;

using CrowEngineBase;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TowerDefense
{
    public static class RebindMainInput
    {
        public static GameObject Create(GameObject[] menuItems, KeyboardInput rebindInput, Vector2 position, Screen.SetCurrentScreenDelegate setCurrentScreenDelegate)
        {
            GameObject gameObject = new GameObject();

            KeyboardInput keyboardInput = new KeyboardInput();
            keyboardInput.actionKeyPairs.Add("MoveUp", Keys.Up);
            keyboardInput.actionKeyPairs.Add("MoveDown", Keys.Down);
            keyboardInput.actionKeyPairs.Add("Select", Keys.Enter);
            keyboardInput.actionKeyPairs.Add("Cancel", Keys.Escape);

            gameObject.Add(keyboardInput);

            gameObject.Add(new Text("", ResourceManager.GetFont("default"), Color.White, Color.Black, true));

            gameObject.Add(new RebindScreenNavigation(gameObject, menuItems, rebindInput, setCurrentScreenDelegate));
            gameObject.Add(new Transform(position, 0, Vector2.One));

            return gameObject;
        }
    }
}

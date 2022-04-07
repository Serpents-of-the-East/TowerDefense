using System;
using CrowEngineBase;
using Microsoft.Xna.Framework.Input;

namespace TowerDefense
{
    public static class ControlsKeyboard
    {
        public static GameObject CreateControlsKeyboard(Screen.SetCurrentScreenDelegate setCurrentScreenDelegate)
        {
            GameObject gameObject = new GameObject();
            KeyboardInput keyboardInput = new KeyboardInput();

            keyboardInput.actionKeyPairs.Add("MoveToMainMenu", Keys.Escape);
            gameObject.Add(keyboardInput);
            gameObject.Add(new MenuNavigationScript(gameObject, setCurrentScreenDelegate));



            return gameObject;


        }
    }
}

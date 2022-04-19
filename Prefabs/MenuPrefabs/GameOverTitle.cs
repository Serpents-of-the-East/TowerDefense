using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TowerDefense
{
    public static class GameOverTitle
    {
        public static GameObject Create(Screen.SetCurrentScreenDelegate setCurrentScreenDelegate)
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new Transform(new Vector2(500, 100), 0, Vector2.One));
            gameObject.Add(new Text("Game Over", ResourceManager.GetFont("default"), Color.White, Color.Black, true, 0));
            KeyboardInput keyboardInput = new KeyboardInput();
            keyboardInput.actionKeyPairs.Add("MoveToMainMenu", Keys.Escape);
            gameObject.Add(keyboardInput);
            gameObject.Add(new MenuNavigationScript(gameObject, setCurrentScreenDelegate));



            return gameObject;



        }

    }
}
using System;
using CrowEngineBase;
using Microsoft.Xna.Framework.Input;

namespace TowerDefense
{
    public class KeyboardMainMenu
    {
        public KeyboardMainMenu()
        {
            KeyboardInput keyboardInput = new KeyboardInput();
            keyboardInput.actionKeyPairs.Add("MoveUp", Keys.W);
            keyboardInput.actionKeyPairs.Add("MoveDown", Keys.S);
            keyboardInput.actionKeyPairs.Add("MoveLeft", Keys.A);
            keyboardInput.actionKeyPairs.Add("MoveRight", Keys.D);

        }
    }
}

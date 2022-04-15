using System;

using CrowEngineBase;

using Microsoft.Xna.Framework.Input;

namespace TowerDefense
{
    /// <summary>
    /// See create method for more info
    /// </summary>
    public static class GameplayKeyboardControls
    {
        /// <summary>
        /// Creates the default control bindings for the gameplay screen.
        /// This is important for both the gameplay screen, as well as the rebinding screen
        /// </summary>
        /// <returns></returns>
        public static KeyboardInput Create()
        {
            KeyboardInput keyboard = new KeyboardInput();

            keyboard.actionKeyPairs.Add("UpgradeTower", Keys.U);
            keyboard.actionKeyPairs.Add("SellTower", Keys.S);
            keyboard.actionKeyPairs.Add("StartLevel", Keys.G);
            keyboard.actionKeyPairs.Add("SwitchUpTower", Keys.OemPeriod);
            keyboard.actionKeyPairs.Add("SwitchDownTower", Keys.OemComma);

            return keyboard;
        }
    }
}

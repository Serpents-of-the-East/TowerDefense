using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace CrowEngineBase
{
    /// <summary>
    /// Represents the possible buttons on a mouse. Left middle and right are self explanatory, while x1 and x2 are the potential side mouse buttons
    /// </summary>
    public enum MouseButton
    {
        LeftButton,
        MiddleButton,
        RightButton,
        x1Button,
        x2Button,
    }

    public class MouseInput : InputComponent
    {
        public Vector2 position { get; set; }
        public Vector2 previousPosition { get; set; }

        public Dictionary<string, MouseButton> actionButtonPairs { get; set; }
        public Dictionary<string, bool> actionStatePairs { get; set; }
        public Dictionary<string, bool> previousActionStatePairs { get; set; }

        public MouseInput()
        {
            position = new Vector2();
            previousPosition = new Vector2();
            actionButtonPairs = new Dictionary<string, MouseButton>();
            actionStatePairs = new Dictionary<string, bool>();
            previousActionStatePairs = new Dictionary<string, bool>();
        }
    }
}

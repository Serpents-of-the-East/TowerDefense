using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Input;

namespace CrowEngineBase
{
    public class KeyboardInput : InputComponent
    {
        public Dictionary<string, Keys> actionKeyPairs { get; set; }
        public Dictionary<string, bool> actionStatePairs { get; set; }
        public Dictionary<string, bool> previousActionStatePairs { get; set; }
        public KeyboardInput()
        {
            actionKeyPairs = new Dictionary<string, Keys>();
            actionStatePairs = new Dictionary<string, bool>();
            previousActionStatePairs = new Dictionary<string, bool>();
        }
    }
}

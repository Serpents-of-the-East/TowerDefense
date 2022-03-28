using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace CrowEngineBase
{
    /// <summary>
    /// All possible inputs from a controller. There may be a better way to do this
    /// </summary>
    public enum ControllerInputType
    {
        LeftStickVertical,
        LeftStickHorizontal,
        RightStickVertical,
        RightStickHorizontal,
        LeftTrigger,
        RightTrigger,
        LeftShoulder,
        RightShoulder,
        DpadUp,
        DpadDown,
        DpadLeft,
        DpadRight,
        A,
        B,
        Y,
        X,
        Start,
        Back,
        LeftStickPress,
        RightStickPress,
    }

    /// <summary>
    /// For the controller, all possible values are read as floats. This makes it consistent that all input types for controller use it, although
    /// it may be inefficient. Something to look at potentially changing in the future
    /// </summary>
    public class ControllerInput : InputComponent
    {
        public Dictionary<string, ControllerInputType> actionButtonPairs;
        public Dictionary<string, float> actionStatePairs;
        public Dictionary<string, float> previousActionStatePairs;
        public PlayerIndex controllerOwner;

        public ControllerInput(PlayerIndex controllerOwner = PlayerIndex.One)
        {
            actionButtonPairs = new Dictionary<string, ControllerInputType>();
            actionStatePairs = new Dictionary<string, float>();
            previousActionStatePairs = new Dictionary<string, float>();
            this.controllerOwner = controllerOwner;
        }
    }
}

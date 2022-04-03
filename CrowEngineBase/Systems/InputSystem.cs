using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;


namespace CrowEngineBase
{
    /// <summary>
    /// Updates Input components in the game (does not do anything with it, just updates state)
    /// </summary>
    public class InputSystem : System
    {
        public InputSystem(SystemManager systemManager) : base(systemManager, typeof(InputComponent))
        {
        }

        protected override void Update(GameTime gameTime)
        {
            foreach(uint id in m_gameObjects.Keys)
            {
                if (m_gameObjects[id].ContainsComponent<KeyboardInput>())
                {
                    KeyboardInput keyboardInput = m_gameObjects[id].GetComponent<KeyboardInput>();
                    KeyboardState keyboardState = Keyboard.GetState();
                    keyboardInput.previousActionStatePairs = new Dictionary<string, bool>(keyboardInput.actionStatePairs);

                    foreach(string action in keyboardInput.actionKeyPairs.Keys)
                    {
                        if (!keyboardInput.actionStatePairs.ContainsKey(action))
                        {
                            keyboardInput.actionStatePairs.Add(action, false);
                        }
                        if (!keyboardInput.previousActionStatePairs.ContainsKey(action))
                        {
                            keyboardInput.previousActionStatePairs.Add(action, false);
                        }
                        keyboardInput.actionStatePairs[action] = keyboardState.IsKeyDown(keyboardInput.actionKeyPairs[action]);
                    }

                }
                if (m_gameObjects[id].ContainsComponent<MouseInput>())
                {
                    MouseState mouseState = Mouse.GetState();
                    MouseInput mouseInput = m_gameObjects[id].GetComponent<MouseInput>();
                    mouseInput.previousPosition = new Vector2(mouseInput.position.X, mouseInput.position.Y);
                    mouseInput.previousActionStatePairs = new Dictionary<string, bool>(mouseInput.actionStatePairs);

                    mouseInput.position = new Vector2(mouseState.X, mouseState.Y);

                    foreach(string action in mouseInput.actionButtonPairs.Keys)
                    {
                        if (!mouseInput.actionStatePairs.ContainsKey(action))
                        {
                            mouseInput.actionStatePairs.Add(action, false);
                        }
                        if (!mouseInput.previousActionStatePairs.ContainsKey(action))
                        {
                            mouseInput.previousActionStatePairs.Add(action, false);
                        }
                        switch (mouseInput.actionButtonPairs[action])
                        {
                            case (MouseButton.LeftButton):
                                mouseInput.actionStatePairs[action] = mouseState.LeftButton == ButtonState.Pressed;
                                break;
                            case (MouseButton.MiddleButton):
                                mouseInput.actionStatePairs[action] = mouseState.MiddleButton == ButtonState.Pressed;
                                break;
                            case (MouseButton.RightButton):
                                mouseInput.actionStatePairs[action] = mouseState.RightButton == ButtonState.Pressed;
                                break;
                            case (MouseButton.x1Button):
                                mouseInput.actionStatePairs[action] = mouseState.XButton1 == ButtonState.Pressed;
                                break;
                            case (MouseButton.x2Button):
                                mouseInput.actionStatePairs[action] = mouseState.XButton2 == ButtonState.Pressed;
                                break;
                        }
                    }
                }

                
                if (m_gameObjects[id].ContainsComponent<ControllerInput>())
                {
                    ControllerInput controllerInput = m_gameObjects[id].GetComponent<ControllerInput>();
                    GamePadState gamePadState = GamePad.GetState(controllerInput.controllerOwner);

                    controllerInput.previousActionStatePairs = new Dictionary<string, float>(controllerInput.actionStatePairs);

                    foreach (string action in controllerInput.actionButtonPairs.Keys)
                    {
                        if (!controllerInput.actionStatePairs.ContainsKey(action))
                        {
                            controllerInput.actionStatePairs.Add(action, 0);
                        }
                        controllerInput.actionStatePairs[action] = GetControllerState(controllerInput.actionButtonPairs[action], gamePadState);
                    }
                }
            }
        }


        /// <summary>
        /// Very ugly way of reading controller input. Should be improved upon, but allows all input types to be treated as floats
        /// </summary>
        /// <param name="type"></param>
        /// <param name="gamePadState"></param>
        /// <returns></returns>
        private float GetControllerState(ControllerInputType type, GamePadState gamePadState)
        {
            switch (type)
            {
                case (ControllerInputType.A):
                    return gamePadState.IsButtonDown(Buttons.A) ? 1 : 0;
                case (ControllerInputType.B):
                    return gamePadState.IsButtonDown(Buttons.B) ? 1 : 0;
                case (ControllerInputType.Back):
                    return gamePadState.IsButtonDown(Buttons.Back) ? 1 : 0;
                case (ControllerInputType.DpadDown):
                    return gamePadState.DPad.Down == ButtonState.Pressed ? 1 : 0;
                case (ControllerInputType.DpadLeft):
                    return gamePadState.DPad.Left == ButtonState.Pressed ? 1 : 0;
                case (ControllerInputType.DpadRight):
                    return gamePadState.DPad.Right == ButtonState.Pressed ? 1 : 0;
                case (ControllerInputType.DpadUp):
                    return gamePadState.DPad.Up == ButtonState.Pressed ? 1 : 0;
                case (ControllerInputType.LeftShoulder):
                    return gamePadState.IsButtonDown(Buttons.LeftShoulder) ? 1 : 0;
                case (ControllerInputType.LeftStickHorizontal):
                    return gamePadState.ThumbSticks.Left.X;
                case (ControllerInputType.LeftStickPress):
                    return gamePadState.IsButtonDown(Buttons.LeftStick) ? 1 : 0;
                case (ControllerInputType.LeftStickVertical):
                    return gamePadState.ThumbSticks.Left.Y;
                case (ControllerInputType.LeftTrigger):
                    return gamePadState.Triggers.Left;
                case (ControllerInputType.RightShoulder):
                    return gamePadState.IsButtonDown(Buttons.RightShoulder) ? 1 : 0;
                case (ControllerInputType.RightStickHorizontal):
                    return gamePadState.ThumbSticks.Right.X;
                case (ControllerInputType.RightStickPress):
                    return gamePadState.IsButtonDown(Buttons.RightStick) ? 1 : 0;
                case (ControllerInputType.RightStickVertical):
                    return gamePadState.ThumbSticks.Right.Y;
                case (ControllerInputType.RightTrigger):
                    return gamePadState.Triggers.Right;
                case (ControllerInputType.Start):
                    return gamePadState.IsButtonDown(Buttons.Start) ? 1 : 0;
                case (ControllerInputType.X):
                    return gamePadState.IsButtonDown(Buttons.X) ? 1 : 0;
                case (ControllerInputType.Y):
                    return gamePadState.IsButtonDown(Buttons.Y) ? 1 : 0;
                default:
                    Debug.WriteLine("Found a controller input type that was non-existant, returning 0");
                    return 0;
            }

        }
    }
}

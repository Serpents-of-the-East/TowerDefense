using System;

using CrowEngineBase;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TowerDefense
{
    public class RebindScreenNavigation : ScriptBase
    {
        public string currentSelected { get; set; }
        public GameObject currentSelectedObject { get; private set; }

        public int currentSelectedIdx { get; private set; }

        private GameObject[] menuItems;

        private KeyboardInput rebindKeyboard; // the keyboard to rebind

        public bool rebindMode { get; private set; }

        private Text rebindingText;

        private MouseInput mouse;

        private Transform transform;

        private Transform cameraTransform;

        private Screen.SetCurrentScreenDelegate setCurrentScreenDelegate;

        public RebindScreenNavigation(GameObject gameObject, GameObject[] menuItems, KeyboardInput rebindKeyboard, Screen.SetCurrentScreenDelegate setCurrentScreenDelegate, Transform cameraTransform) : base(gameObject)
        {
            this.menuItems = menuItems;
            this.rebindKeyboard = rebindKeyboard;
            this.setCurrentScreenDelegate = setCurrentScreenDelegate;
            this.cameraTransform = cameraTransform;
        }


        public override void Start()
        {
            rebindingText = gameObject.GetComponent<Text>();
            mouse = gameObject.GetComponent<MouseInput>();
            transform = gameObject.GetComponent<Transform>();
        }
        /// <summary>
        /// Used by mouse to set the selected object
        /// </summary>
        /// <param name="gameObject"></param>
        public void SetSelected(GameObject gameObject)
        {
            if (currentSelectedObject != null)
            {
                currentSelectedObject.GetComponent<RebindControlScript>().isSelected = false;
            }

            if (gameObject != null)
            {
                currentSelectedObject = gameObject;
                currentSelected = gameObject.GetComponent<RebindControlScript>().myAction;
                for (int i = 0; i < menuItems.Length; i++)
                {
                    if (menuItems[i] == gameObject)
                    {
                        this.currentSelectedIdx = i;
                        break;
                    }
                }
                gameObject.GetComponent<RebindControlScript>().isSelected = true;
            }
            else
            {
                currentSelectedObject = null;
                currentSelected = "";
                currentSelectedIdx = -1;
            }
            
        }

        public override void OnCollisionStart(GameObject other)
        {
            if (!rebindMode && other.ContainsComponent<RebindControlScript>())
            {
                SetSelected(other);
            }
        }

        public override void OnCollisionEnd(GameObject other)
        {
            if (other == currentSelectedObject && !rebindMode)
            {
                other.GetComponent<RebindControlScript>().isSelected = false;

                SetSelected(null);
            }
        }

        public void OnMoveUp(float value)
        {
            if (value > 0 && !rebindMode)
            {
                currentSelectedIdx--;
                if (currentSelectedIdx < 0)
                {
                    currentSelectedIdx = menuItems.Length - 1;
                }
                SetSelected(menuItems[currentSelectedIdx]);
            }
        }

        public void OnMoveDown(float value)
        {
            if (value > 0 && !rebindMode)
            {
                currentSelectedIdx++;
                currentSelectedIdx %= menuItems.Length;
                SetSelected(menuItems[currentSelectedIdx]);
            }
        }

        public void OnSelect(float value)
        {

            if (value > 0 && currentSelected != "")
            {
                rebindMode = true;
            }
        }

        public void OnMouseMove(Vector2 value)
        {
            Vector2 currentMousePosition = mouse.PhysicsPositionCamera(cameraTransform);

            transform.position = currentMousePosition;

        }


        public void OnCancel(float value)
        {
            if (value > 0)
            {
                if (rebindMode)
                {
                    rebindMode = false;
                }
                else
                {
                    setCurrentScreenDelegate(ScreenEnum.MainMenu);
                }
            }
        }


        public override void Update(GameTime gameTime)
        {
            rebindingText.text = "";
            if (!rebindMode) // this logic should ONLY happen if we are rebinding. This is where we actually read a key
            {
                return;
            }

            rebindingText.text = $"Rebinding {currentSelected}";

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.GetPressedKeyCount() != 1) // only accept one key
            {
                return;
            }

            Keys pressedKey = keyboardState.GetPressedKeys()[0]; // safe, since we made sure there was at least one

            if (pressedKey == Keys.Enter || pressedKey == Keys.Escape)
            {
                return;
            }

            rebindKeyboard.actionKeyPairs[currentSelected] = pressedKey;

            rebindMode = false;
        }
    }
}

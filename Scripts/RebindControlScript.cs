using System;

using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public class RebindControlScript : ScriptBase
    {

        Text text;
        public string myAction { get; private set; }
        public bool isSelected { get; set; }
        KeyboardInput keyboardInput;

        public RebindControlScript(GameObject gameObject, KeyboardInput keyboardInput, string myAction) : base(gameObject)
        {
            this.keyboardInput = keyboardInput;
            this.myAction = myAction;
        }

        public override void Start()
        {
            text = gameObject.GetComponent<Text>();
        }

        public override void Update(GameTime gameTime)
        {
            string oldText = text.text;

            text.text = $"{myAction}: {keyboardInput.actionKeyPairs[myAction]}";

            if (text.text != oldText)
            {
                text.centerOfRotation = text.spriteFont.MeasureString(text.text) / 2;
            }

            if (isSelected)
            {
                text.color = Color.Red;
            }
            else
            {
                text.color = Color.White;
            }
        }



    }
}

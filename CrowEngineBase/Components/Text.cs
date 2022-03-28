using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrowEngineBase
{
    public class Text : Component
    {
        public string text { get; set; }
        public Color color { get; set; }
        public Color outlineColor { get; set; }
        public Vector2 centerOfRotation { get; set; }
        public SpriteFont spriteFont { get; set; }
        public SpriteEffects spriteEffect { get; set; }
        public float layerDepth { get; set; }
        public bool renderOutline { get; set; }



        public Text(string text, SpriteFont font, Color color) : this(text, font, new Vector2(0, 0), color, color)
        {
        }

        public Text(string text, SpriteFont font, Color color, Vector2 centerOfRotation) : this(text, font, centerOfRotation, color, Color.Black)
        {
        }

        public Text(string text, SpriteFont font, Color color, Color outlineColor, bool renderOutline = true) : this(text, font, new Vector2(0, 0), color, outlineColor, renderOutline)
        {
        }

        public Text(string text, SpriteFont font, Color color, Color outlineColor, Vector2 centerOfRotation, bool renderOutline = true) : this(text, font, centerOfRotation, color, outlineColor, renderOutline)
        {
        }


        public Text(string text, SpriteFont font, Vector2 centerOfRotation, Color color, Color outlineColor, bool drawOutline = false, float layerDepth = 0f)
        {
            this.text = text;
            this.color = color;
            this.outlineColor = outlineColor;
            this.centerOfRotation = centerOfRotation;
            this.spriteFont = font;
            this.spriteEffect = SpriteEffects.None;
            this.layerDepth = layerDepth;
            this.renderOutline = drawOutline;
        }


    }
}

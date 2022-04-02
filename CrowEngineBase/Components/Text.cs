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
        public bool usesCameraPosition { get; set; }



        public Text(string text, SpriteFont font, Color color, Color outlineColor, bool drawOutline = false, float layerDepth = 0f, bool usesCameraPosition = false)
        {
            this.text = text;
            this.color = color;
            this.outlineColor = outlineColor;
            this.spriteFont = font;
            this.centerOfRotation = this.spriteFont.MeasureString(this.text) / 2;
            this.spriteEffect = SpriteEffects.None;
            this.layerDepth = layerDepth;
            this.renderOutline = drawOutline;
            this.usesCameraPosition = usesCameraPosition;
        }


    }
}

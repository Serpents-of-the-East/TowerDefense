using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CrowEngineBase
{
    public class Sprite : RenderedComponent
    {
        public Texture2D sprite { get; set; }
        public Color color { get; set; }
        public Vector2 center { get; set; }
        public float renderDepth { get; set; }

        public Sprite(Texture2D sprite, Color color, Vector2 center, float renderDepth=0)
        {
            this.sprite = sprite;
            this.color = color;
            this.renderDepth = renderDepth;
            this.center = center;
        }

        public Sprite(Texture2D sprite, Color color, float renderDepth=0) : this(sprite, color, new Vector2((float)sprite.Width / 2, (float)sprite.Height / 2), renderDepth)
        {
            
        }
    }
}

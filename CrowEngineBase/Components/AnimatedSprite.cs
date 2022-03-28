using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrowEngineBase
{
    public class AnimatedSprite : RenderedComponent
    {
        // The texture to take the frame data from
        public Texture2D spritesheet { get; set; }

        // The current frame we're on
        public int currentFrame { get; set; }

        // The timing for each frame in milliseconds
        public int[] frameTiming { get; set; }

        // The current timer
        public TimeSpan currentTime { get; set; }

        // The size in pixels of each frame
        public Vector2 singleFrameSize { get; set; }

        // Depth to render
        public int layerDepth { get; set; }

        public AnimatedSprite(Texture2D spritesheet, int[] frameTiming, Vector2 singleFrameSize, int layerDepth=0)
        {
            this.spritesheet = spritesheet;
            this.frameTiming = frameTiming;
            this.singleFrameSize = singleFrameSize;
            this.currentFrame = 0;
            this.currentTime = new TimeSpan();
            this.layerDepth = layerDepth;
        }
    }
}

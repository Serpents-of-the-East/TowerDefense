using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace CrowEngineBase
{
    class Light : Component
    {
        /// <summary>
        /// Inverse transparency of this
        /// </summary>
        public float intensity { get; set; }

        /// <summary>
        /// Essentially translates to the size of the light
        /// </summary>
        public float range { get; set; }

        /// <summary>
        /// The color of the light, defaulted to white
        /// </summary>
        public Color color { get; set; }


        public Light(float intensity = 0.5f, float range = 10) : this(Color.White, intensity, range)
        {
        }

        public Light(Color color, float intensity = 0.5f, float range = 10)
        {
            this.intensity = intensity;
            this.range = range;
            this.color = color;
        }
    }
}

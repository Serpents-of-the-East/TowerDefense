using System;

using Microsoft.Xna.Framework;

namespace CrowEngineBase.Utilities
{
    /// <summary>
    /// Following Dr. Mathias' code for this random class. Thanks!
    /// </summary>
    public class CrowRandom : Random
    {
        public CrowRandom() : base()
        {

        }

        /// <summary>
        /// Keep this around to optimize gaussian calculation performance.
        /// </summary>
        private double y2;
        private bool usePrevious { get; set; }

        /// <summary>
        /// Generate a random number in a range
        /// </summary>
        /// <param name="min">Minimum number (inclusive)</param>
        /// <param name="max">Maximum number (inclusive)</param>
        /// <returns>random number in range [min, max]</returns>
        public float NextRange(float min, float max)
        {
            return CrowMath.Lerp(min, max, (float)this.NextDouble());
        }


        /// <summary>
        /// Returns a random vector in the unit circle
        /// </summary>
        /// <returns></returns>
        public Vector2 NextCircleVector()
        {
            float angle = (float)(this.NextDouble() * 2.0 * Math.PI);
            float x = (float)Math.Cos(angle);
            float y = (float)Math.Sin(angle);

            return new Vector2(x, y);
        }


        /// <summary>
        /// Gets a random number using a Gaussian distribution
        /// Taken from Dr. Mathias' code
        /// </summary>
        public double NextGaussian(double mean, double stdDev)
        {
            if (this.usePrevious)
            {
                this.usePrevious = false;
                return mean + y2 * stdDev;
            }
            this.usePrevious = true;

            double x1 = 0.0;
            double x2 = 0.0;
            double y1 = 0.0;
            double z = 0.0;

            do
            {
                x1 = 2.0 * this.NextDouble() - 1.0;
                x2 = 2.0 * this.NextDouble() - 1.0;
                z = (x1 * x1) + (x2 * x2);
            }
            while (z >= 1.0);

            z = Math.Sqrt((-2.0 * Math.Log(z)) / z);
            y1 = x1 * z;
            y2 = x2 * z;

            return mean + y1 * stdDev;
        }

    }
}

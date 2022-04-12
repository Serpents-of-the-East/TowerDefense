using System;

using Microsoft.Xna.Framework;

namespace CrowEngineBase.Utilities
{
    public static class CrowMath
    {
        public static float Lerp(float start, float end, float step)
        {
            return start + (end - start) * step;
        }

        public static double Lerp(double start, double end, double step)
        {
            return start + (end - start) * step;
        }

        /// <summary>
        /// This Lerp returns the about to step by, rather than the new value
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static float LerpAmount(float start, float end, float step)
        {
            return (end - start) * step;
        }

        public static bool Tolerance(float current, float target, float tolerance)
        {
            return MathF.Abs(current - target) < tolerance;
        }

        public static bool Tolerance(Vector2 current, Vector2 target, float tolerance)
        {
            return Vector2.DistanceSquared(current, target) < tolerance * tolerance;
        }

        public static float AngleBetweenVectors(Vector2 current, Vector2 target)
        {
            var distanceBetween = Vector2.Subtract(current, target);
            return MathF.Atan(distanceBetween.Y/distanceBetween.X);
        }

        public static float AngleBetweenVectorsDegrees(Vector2 current, Vector2 target)
        {
            var distanceBetween = Vector2.Subtract(current, target);
            return (180 / MathF.PI) * MathF.Atan(distanceBetween.Y / distanceBetween.X);
        }

    }
}

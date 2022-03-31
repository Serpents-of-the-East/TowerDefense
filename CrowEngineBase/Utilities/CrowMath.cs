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

        public static bool Tolerance(float current, float target, float tolerance)
        {
            return MathF.Abs(current - target) < tolerance;
        }

        public static bool Tolerance(Vector2 current, Vector2 target, float tolerance)
        {
            return Vector2.DistanceSquared(current, target) < tolerance * tolerance;
        }

        public static float Angle(Vector2 current, Vector2 target)
        {
            return MathF.Acos(Vector2.Dot(Vector2.Normalize(current), Vector2.Normalize(target)));
        }

        public static float AngleDegrees(Vector2 current, Vector2 target)
        {
            return (180 / MathF.PI) * MathF.Acos(Vector2.Dot(Vector2.Normalize(current), Vector2.Normalize(target)));
        }

    }
}

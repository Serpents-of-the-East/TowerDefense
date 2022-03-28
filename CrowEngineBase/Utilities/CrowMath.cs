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

    }
}

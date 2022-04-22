using System;
using CrowEngineBase;

namespace TowerDefense
{
    public class PointsComponent : Component
    {
        public int points;

        public float[] pointsPerUpgradeLevel = new float[] { 1.0f, 2f, 3f };

        public PointsComponent()
        {
            points = 0;
        }
    }
}

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using CrowEngineBase;

namespace TowerDefense
{
    public class Path : Component
    {
        public List<Vector2> correctPath { get; set; }
        public Vector2 currentTarget { get; set; }
        public PathGoal goal { get; set; }
    }
}

using System;
namespace CrowEngineBase
{
    /// <summary>
    /// An abtract class that both rectangles and circles components should extend
    /// Used by the physics system to check for a collider, but not caring which type
    /// </summary>
    public abstract class Collider : Component
    {
    }
}

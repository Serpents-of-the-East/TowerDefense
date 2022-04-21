using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public class PointsTextScript : ScriptBase
    {
        private TimeSpan lifetime;
        private SystemManager systemManager;

        public PointsTextScript(GameObject gameObject, SystemManager systemManager) : base(gameObject)
        {
            lifetime = TimeSpan.FromSeconds(1);
            this.systemManager = systemManager;
        }


        public override void Update(GameTime gameTime)
        {
            lifetime -= gameTime.ElapsedGameTime;

            if (lifetime <= TimeSpan.Zero)
            {
                systemManager.DelayedRemove(gameObject);
            }
            else
            {
                gameObject.GetComponent<Rigidbody>().velocity = new Vector2(0, -100);
            }

        }



    }
}

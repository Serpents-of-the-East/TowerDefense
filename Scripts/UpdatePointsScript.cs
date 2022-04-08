using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{ 
    public class UpdatePointsScript : ScriptBase
    {
        public UpdatePointsScript(GameObject gameObject) : base(gameObject)
        {
        }


        public override void Start()
        {
            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            gameObject.GetComponent<Text>().text = PointsManager.GetPlayerPoints().ToString();

        }

    }
}

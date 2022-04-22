using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public class UpdateCreepsText : ScriptBase
    {
        public UpdateCreepsText(GameObject gameObject) : base(gameObject)
        {
        }


        public override void Start()
        {
            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            gameObject.GetComponent<Text>().text = "Kills: " + GameStats.creepsDestroyed;

        }

    }
}

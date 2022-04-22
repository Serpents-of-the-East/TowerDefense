using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public class UpdateLivesText : ScriptBase
    {
        public UpdateLivesText(GameObject gameObject) : base(gameObject)
        {
        }


        public override void Start()
        {
            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            gameObject.GetComponent<Text>().text = "Lives: " + GameStats.lives;

        }

    }
}

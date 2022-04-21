using System;
using System.Collections.Generic;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public class GameOverSystem : CrowEngineBase.System
    {
        private Screen.SetCurrentScreenDelegate setCurrentScreenDelegate;

        public GameOverSystem(SystemManager systemManager, Screen.SetCurrentScreenDelegate setCurrentScreenDelegate) : base(systemManager)
        {
            this.setCurrentScreenDelegate = setCurrentScreenDelegate;
        }

        

        protected override void Update(GameTime gameTime)
        {
            if (GameStats.lives <= 0)
            {
                // Save all your stats and reset
                SavedStatePersistence.SaveNewScore(new TowerDefenseHighScores() { creepsKilled = GameStats.GetCreepsDestroyed(), levelsCompleted = GameStats.numberLevels, totalTowerValue = GameStats.GetTowerValue(), wavesCompleted = GameStats.numberWaves });
                this.setCurrentScreenDelegate.Invoke(ScreenEnum.GameOver);

            }




        }
    }
}

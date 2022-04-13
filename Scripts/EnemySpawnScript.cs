using System;
using System.Collections.Generic;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public enum SpawnLocation
    {
        Up,
        Down,
        Left,
        Right,
    }


    public class EnemySpawnScript : ScriptBase
    {
        
        Dictionary<string, (TimeSpan timeToSpawn, int spawnThisMany)> enemiesToSpawn;
        Dictionary<string, (TimeSpan timeTillNextSpawn, int remainingSpawn)> currentLevelEnemiesSpawn;
        SystemManager systemManager;
        SpawnLocation currentSpawnLocation;

        public EnemySpawnScript(GameObject gameObject, SystemManager systemManager) : base(gameObject)
        {
            enemiesToSpawn = new Dictionary<string, (TimeSpan timeToSpawn, int spawnThisMany)>();
            currentLevelEnemiesSpawn = new Dictionary<string, (TimeSpan timeTillNextSpawn, int remainingSpawn)>();
            enemiesToSpawn.Add("basic", (TimeSpan.FromMilliseconds(1000), 10));
            enemiesToSpawn.Add("heavy", (TimeSpan.FromMilliseconds(5000), 2));
            enemiesToSpawn.Add("flying", (TimeSpan.FromMilliseconds(10000), 5));
            currentLevelEnemiesSpawn.Add("basic", (TimeSpan.FromMilliseconds(1000), 10));
            currentLevelEnemiesSpawn.Add("heavy", (TimeSpan.FromMilliseconds(5000), 2));
            currentLevelEnemiesSpawn.Add("flying", (TimeSpan.FromMilliseconds(10000), 5));
            this.systemManager = systemManager;
            currentSpawnLocation = SpawnLocation.Left;
        }

        public void LevelUp()
        { 
            enemiesToSpawn["basic"] = (enemiesToSpawn["basic"].timeToSpawn - TimeSpan.FromMilliseconds(100), enemiesToSpawn["basic"].spawnThisMany + 5);
            enemiesToSpawn["heavy"] = (enemiesToSpawn["heavy"].timeToSpawn - TimeSpan.FromMilliseconds(100), enemiesToSpawn["heavy"].spawnThisMany + 5);
            enemiesToSpawn["flying"] = (enemiesToSpawn["flying"].timeToSpawn - TimeSpan.FromMilliseconds(100), enemiesToSpawn["flying"].spawnThisMany + 5);

            currentLevelEnemiesSpawn["basic"] = enemiesToSpawn["basic"];
            currentLevelEnemiesSpawn["heavy"] = enemiesToSpawn["heavy"];
            currentLevelEnemiesSpawn["flying"] = enemiesToSpawn["flying"];
        }

        public override void Update(GameTime gameTime)
        {

            if (currentLevelEnemiesSpawn["basic"].remainingSpawn > 0)
            {
                if (currentLevelEnemiesSpawn["basic"].timeTillNextSpawn <= TimeSpan.Zero)
                {
                    systemManager.DelayedAdd(BasicEnemy.CreateBasicEnemy(Vector2.One * 100));
                    currentLevelEnemiesSpawn["basic"] = (enemiesToSpawn["basic"].timeToSpawn, currentLevelEnemiesSpawn["basic"].remainingSpawn - 1);
                }
                else
                {
                    currentLevelEnemiesSpawn["basic"] = (currentLevelEnemiesSpawn["basic"].timeTillNextSpawn - gameTime.ElapsedGameTime, currentLevelEnemiesSpawn["basic"].remainingSpawn);
                }
            }

            if (currentLevelEnemiesSpawn["heavy"].remainingSpawn > 0)
            {
                if (currentLevelEnemiesSpawn["heavy"].timeTillNextSpawn <= TimeSpan.Zero)
                {
                    systemManager.DelayedAdd(TankyEnemy.CreateTankyEnemy(Vector2.One));

                    currentLevelEnemiesSpawn["heavy"] = (enemiesToSpawn["heavy"].timeToSpawn, currentLevelEnemiesSpawn["heavy"].remainingSpawn - 1);
                }
                else
                {
                    currentLevelEnemiesSpawn["heavy"] = (currentLevelEnemiesSpawn["heavy"].timeTillNextSpawn - gameTime.ElapsedGameTime, currentLevelEnemiesSpawn["heavy"].remainingSpawn);
                }
            }


            if (currentLevelEnemiesSpawn["flying"].remainingSpawn > 0)
            {
                if (currentLevelEnemiesSpawn["flying"].timeTillNextSpawn <= TimeSpan.Zero)
                {
                    systemManager.DelayedAdd(FlyingEnemy.CreateFlyingEnemy(Vector2.One));
                    currentLevelEnemiesSpawn["flying"] = (enemiesToSpawn["flying"].timeToSpawn, currentLevelEnemiesSpawn["flying"].remainingSpawn - 1);
                }
                else
                {
                    currentLevelEnemiesSpawn["flying"] = (currentLevelEnemiesSpawn["flying"].timeTillNextSpawn - gameTime.ElapsedGameTime, currentLevelEnemiesSpawn["flying"].remainingSpawn);
                }
            }

        }


    }
}

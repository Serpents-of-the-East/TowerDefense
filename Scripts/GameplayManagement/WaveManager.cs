using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using CrowEngineBase;
using CrowEngineBase.Utilities;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    /// <summary>
    /// Generates and inserts waves into the game
    /// The number of waves per level is decided based on the current level, e.g. level 1 has one wave, and so on
    /// </summary>
    public class WaveManager : ScriptBase
    {

        private SystemManager systemManager;

        private bool waveIsRunning = false;
        private PathGoal currentGoal = PathGoal.Right;

        private CrowRandom random = new CrowRandom();

        /// <summary>
        /// Enemy spawn timing criterea
        /// </summary>
        private TimeSpan timeBetweenEnemies = TimeSpan.FromSeconds(2);
        private TimeSpan currentTimeBetweenEnemies = TimeSpan.Zero;

        private static TimeSpan timeBetweenWaves = TimeSpan.FromSeconds(10);
        private TimeSpan currentTimeBetweenWaves = TimeSpan.Zero;

        /// <summary>
        /// How to generate the number of enemies per wave
        /// </summary>
        private int meanEnemiesPerWave = 10;
        private int sdEnemiesPerWave = 2;


        /// <summary>
        /// What to multiply the meanEnemiesPerWave by before each wave calculation
        /// </summary>
        private float waveScaling = 1;
        private float waveScalingAddPerLevel = 0.2f;

        private Queue<Enemy> currentWaveQueue;
        private Queue<Queue<Enemy>> currentLevelQueue;

        enum Enemy
        {
            Flying,
            Normal,
            Tank,
        }

        // A lookup for the generation
        private static Dictionary<Enemy, float> chancePerEnemy = new Dictionary<Enemy, float>()
        {
            {Enemy.Flying, 0.2f },
            {Enemy.Normal, 0.5f },
            {Enemy.Tank, 0.3f },
        };


        private Transform transform;

        private Vector2 currentSpawnTruePosition;
        private Transform cameraTransform;


        public WaveManager(GameObject gameObject, SystemManager systemManager, Transform cameraTranform) : base(gameObject)
        {
            this.systemManager = systemManager;
            this.cameraTransform = cameraTranform;
        }

        public override void Start()
        {
            currentLevelQueue = new Queue<Queue<Enemy>>();
            currentWaveQueue = new Queue<Enemy>();
            transform = gameObject.GetComponent<Transform>();

            currentSpawnTruePosition = Pathfinder.SpawnPointLookup(currentGoal);
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 foundPosition = cameraTransform.position + transform.position - (Vector2.One * 500); // convert our HUD position to the actual position

            Vector2 direction = currentSpawnTruePosition - foundPosition;
            direction.Normalize();

            transform.rotation = MathF.Atan2(direction.Y, direction.X);


            if (waveIsRunning && currentLevelQueue.Count == 0 && currentWaveQueue.Count == 0)
            {
                // Level Completed
                waveIsRunning = false;

                switch (currentGoal)
                {
                    case (PathGoal.Left):
                        currentGoal = PathGoal.Up;
                        transform.position = new Vector2(500, 950);
                        break;
                    case (PathGoal.Up):
                        currentGoal = PathGoal.Right;
                        transform.position = new Vector2(50, 500);
                        break;
                    case (PathGoal.Right):
                        currentGoal = PathGoal.Down;
                        transform.position = new Vector2(500, 50);
                        break;
                    case (PathGoal.Down):
                        currentGoal = PathGoal.Left;
                        transform.position = new Vector2(950, 500);
                        break;
                }
                currentSpawnTruePosition = Pathfinder.SpawnPointLookup(currentGoal);

                GameStats.AddLevel();
            }

            else if (waveIsRunning && currentWaveQueue.Count == 0)
            {
                // Wave Complete
                currentWaveQueue = currentLevelQueue.Dequeue();
                currentTimeBetweenWaves = timeBetweenWaves;
                GameStats.AddWave();
            }

            else if (waveIsRunning)
            {
                if (currentTimeBetweenWaves <= TimeSpan.Zero)
                {
                    if (currentTimeBetweenEnemies <= TimeSpan.Zero)
                    {
                        Enemy currentEnemy = currentWaveQueue.Dequeue();

                        switch (currentEnemy)
                        {
                            case (Enemy.Flying):
                                systemManager.DelayedAdd(FlyingEnemy.Create(Pathfinder.SpawnPointLookup(currentGoal), systemManager, currentGoal));
                                break;
                            case (Enemy.Normal):
                                systemManager.DelayedAdd(BasicEnemy.CreateBasicEnemy(Pathfinder.SpawnPointLookup(currentGoal), systemManager, currentGoal));
                                break;
                            case (Enemy.Tank):
                                systemManager.DelayedAdd(TankyEnemy.CreateTankyEnemy(Pathfinder.SpawnPointLookup(currentGoal), systemManager, currentGoal));
                                break;
                        }

                        currentTimeBetweenEnemies += timeBetweenEnemies;

                    }
                    else
                    {
                        currentTimeBetweenEnemies -= gameTime.ElapsedGameTime;
                    }
                }
                else
                {
                    currentTimeBetweenWaves -= gameTime.ElapsedGameTime;
                }
            }
        }


        public void OnStartLevel(float value)
        {
            if (value > 0 && !waveIsRunning)
            {
                waveIsRunning = true;
                currentLevelQueue = GenerateLevel();
                currentWaveQueue = currentLevelQueue.Dequeue();
                currentTimeBetweenEnemies = TimeSpan.Zero;
                currentTimeBetweenWaves = TimeSpan.Zero;
            }
        }

        private Queue<Queue<Enemy>> GenerateLevel()
        {
            Queue<Queue<Enemy>> queue = new Queue<Queue<Enemy>>();

            for (int i = 0; i < GameStats.numberLevels + 2; i++)
            {
                queue.Enqueue(GenerateWaveEnemies());
            }

            return queue;
        }


        private Queue<Enemy> GenerateWaveEnemies()
        {
            int numberThisWave = (int)random.NextGaussian(meanEnemiesPerWave * (waveScaling + (GameStats.numberLevels + 1) * waveScalingAddPerLevel), sdEnemiesPerWave);
            if (numberThisWave < 5)
            {
                numberThisWave = 5; // just in case we get negative or zero, or just any number under 5. Who likes fighting 1 enemy right?
            }

            Enemy[] enemyTypes = new Enemy[numberThisWave];

            for (int i = 0; i < numberThisWave; i++)
            {
                float randomValue = random.NextRange(0, 1);
                if (randomValue < chancePerEnemy[Enemy.Flying])
                {
                    if (GameStats.numberLevels < 2)
                    {
                        enemyTypes[i] = Enemy.Normal;
                    }
                    else
                    {
                        enemyTypes[i] = (Enemy.Flying);
                    }
                }
                else if (randomValue < chancePerEnemy[Enemy.Flying] + chancePerEnemy[Enemy.Normal])
                {
                    enemyTypes[i] = (Enemy.Normal);
                }
                else
                {
                    enemyTypes[i] = (Enemy.Tank);
                }
            }

            return new Queue<Enemy>(enemyTypes.OrderBy(x => random.Next()).ToArray());
        }
    }
}

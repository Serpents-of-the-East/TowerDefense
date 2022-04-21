using System;
using CrowEngineBase;
using CrowEngineBase.Utilities;
using System.Diagnostics;


using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public class TowerTargetingScript : ScriptBase
    {
        public enum BulletType
        {
            Basic,
            Bomb,
            Missile,
            AirGround,
        }

        public GameObject currentTarget;
        private EnemyType targetableEnemy;
        float toleranceAllowed = .3f;
        private TowerComponent towerComponent;

        private Transform transform;

        private BulletType bulletType;
        private TimeSpan timeBetweenShots;
        private TimeSpan currentTime;

        private SystemManager systemManager;
        

        public TowerTargetingScript(GameObject gameObject, BulletType bulletType, TimeSpan timeBetweenShots, SystemManager systemManager) : base(gameObject)
        {
            this.bulletType = bulletType;
            this.timeBetweenShots = timeBetweenShots;
            this.currentTime = new TimeSpan();
            this.systemManager = systemManager;
        }

        public override void Start()
        {
            targetableEnemy = gameObject.GetComponent<EnemyTag>().enemyType;
            towerComponent = gameObject.GetComponent<TowerComponent>();
            transform = gameObject.GetComponent<Transform>();

        }

        public override void OnCollision(GameObject other)
        {
            if (currentTarget == null && other.ContainsComponent<Enemy>() && (other.GetComponent<EnemyTag>().enemyType == targetableEnemy || targetableEnemy == EnemyType.MIXED))
            {
                currentTarget = other;
            }

        }

        public override void OnCollisionEnd(GameObject other)
        {
            if (other == currentTarget)
            {
                currentTarget = null;
            }
        }

        private void Shoot()
        {
            GameObject spawnedObject;

            switch (bulletType)
            {
                case (BulletType.Bomb):
                    spawnedObject = BombBullet.Create(transform.position, currentTarget.GetComponent<Transform>().position, gameObject, systemManager);
                    break;
                case (BulletType.Missile):
                    spawnedObject = MissileBullet.Create(transform.position, currentTarget.GetComponent<Transform>(), gameObject, systemManager);
                    break;
                case (BulletType.AirGround):
                    spawnedObject = AirGroundBullet.Create(transform.position, currentTarget.GetComponent<Transform>().position, gameObject);
                    break;
                default:
                    spawnedObject = BasicBullet.Create(transform.position, currentTarget.GetComponent<Transform>().position, gameObject);
                    break;
            }

            systemManager.DelayedAdd(spawnedObject);
            
        }

        public override void Update(GameTime gameTime)
        {
            if (currentTarget != null && !systemManager.gameObjectsDictionary.ContainsKey(currentTarget.id))
            {
                currentTarget = null;
            }


            if (currentTime > TimeSpan.Zero)
            {
                currentTime -= gameTime.ElapsedGameTime;
            }

            Transform transform = gameObject.GetComponent<Transform>();

            if (currentTarget != null)
            {
                Vector2 targetVector = currentTarget.GetComponent<Transform>().position - transform.position;

                float targetAngle = MathF.Atan2(targetVector.Y, targetVector.X);

                if (targetAngle < 0)
                {
                    targetAngle += 2 * MathF.PI;
                }

                if (CrowMath.Tolerance(transform.rotation, targetAngle, toleranceAllowed))
                {
                    if (currentTime <= TimeSpan.Zero)
                    {
                        Shoot();
                        currentTime += timeBetweenShots;
                    }
                    return;
                }

                transform.rotation = CrowMath.Lerp(transform.rotation, targetAngle, towerComponent.turnSpeed * (gameTime.ElapsedGameTime.Milliseconds / 1000f));

                if (transform.rotation < 0)
                {
                    transform.rotation += MathF.PI;
                }

                transform.rotation %= 2*MathF.PI;

                

            } 


        }


    }
}

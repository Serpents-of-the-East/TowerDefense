using System;
using CrowEngineBase;
using CrowEngineBase.Utilities;
using System.Diagnostics;


using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public class TowerTargetingScript : ScriptBase
    {
        private GameObject currentTarget;
        private EnemyType targetableEnemy;
        float toleranceAllowed = .001f;
        private TowerComponent towerComponent;

        public TowerTargetingScript(GameObject gameObject) : base(gameObject)
        {

        }

        public override void Start()
        {
            targetableEnemy = gameObject.GetComponent<EnemyTag>().enemyType;
            towerComponent = gameObject.GetComponent<TowerComponent>();

        }

        public override void OnCollision(GameObject other)
        {
            if (currentTarget == null && other.ContainsComponent<Enemy>())// && (other.GetComponent<EnemyTag>().enemyType == targetableEnemy || targetableEnemy == EnemyType.MIXED))
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

        public override void Update(GameTime gameTime)
        {

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

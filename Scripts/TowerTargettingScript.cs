using System;
using CrowEngineBase;
using CrowEngineBase.Utilities;


using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public class TowerTargettingScript : ScriptBase
    {
        private GameObject currentTarget;
        private EnemyType targetableEnemy;
        float toleranceAllowed = .001f;
        private TowerComponent towerComponent;

        public TowerTargettingScript(GameObject gameObject) : base(gameObject)
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
                float angleBetween = CrowMath.AngleBetweenVectors(transform.position, currentTarget.GetComponent<Transform>().position);

                if (angleBetween == -0) angleBetween = 360;

                if (!CrowMath.Tolerance(angleBetween, 0.0f, toleranceAllowed))
                {
                    transform.rotation = CrowMath.Lerp(transform.rotation, transform.rotation + angleBetween, towerComponent.turnSpeed * gameTime.ElapsedGameTime.Milliseconds / 1000f);
                }

            }
            // speed * elapsedTime.mill / 1000f for lerp
            // Angle currently, angle should go to

            // transform.rotation = 


        }


    }
}

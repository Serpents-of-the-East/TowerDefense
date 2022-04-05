using System;

using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public class BasicEnemyTestScript : ScriptBase
    {

        private Rigidbody rb;
        private float maxSpeed = 100;
        private Transform transform;

        public BasicEnemyTestScript(GameObject gameObject) : base(gameObject)
        {
        }

        public override void Start()
        {
            transform = gameObject.GetComponent<Transform>();
            rb = gameObject.GetComponent<Rigidbody>();
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 targetPosition = Pathfinder.GetNextRightTarget(transform.position);

            Vector2 heading = targetPosition - transform.position;
            heading.Normalize();

            rb.velocity = heading * maxSpeed;
        }
    }
}

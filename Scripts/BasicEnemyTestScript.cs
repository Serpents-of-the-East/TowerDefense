using System;

using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public class BasicEnemyTestScript : ScriptBase
    {

        private Rigidbody rb;
        private float maxSpeed = 50;
        private Transform transform;
        private Path path;

        public BasicEnemyTestScript(GameObject gameObject) : base(gameObject)
        {
        }

        public override void Start()
        {
            transform = gameObject.GetComponent<Transform>();
            rb = gameObject.GetComponent<Rigidbody>();
            path = gameObject.GetComponent<Path>();
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 heading = path.currentTarget - transform.position;

            if (heading == Vector2.Zero)
            {
                return;
            }
            heading.Normalize();

            rb.velocity = heading * maxSpeed;
        }
    }
}

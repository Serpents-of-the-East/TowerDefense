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
        SystemManager systemManager;

        public BasicEnemyTestScript(GameObject gameObject, SystemManager systemManager, float maxSpeed = 50) : base(gameObject)
        {
            this.maxSpeed = maxSpeed;
            this.systemManager = systemManager;
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

        public override void OnCollision(GameObject other)
        {
            if (other.ContainsComponent<Bullet>())
            {
                this.gameObject.GetComponent<EnemyHealth>().health -= other.GetComponent<Bullet>().damage;
                systemManager.Remove(other.id);
            }


        }


    }
}

﻿using System;

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
                systemManager.DelayedRemove(other);

                if (this.gameObject.GetComponent<EnemyHealth>().health <= 0.0f)
                {
                    systemManager.DelayedRemove(gameObject);
                }
            }
        }


        public override void Destroyed()
        {
            base.Destroyed();
            if (gameObject.ContainsComponent<EnemyHealth>())
            {
                foreach (var deathObject in gameObject.GetComponent<EnemyHealth>().instantiateOnDeathObject)
                {
                    if (deathObject.ContainsComponent<PointsTextScript>() && deathObject.ContainsComponent<CircleCollider>())
                    {
                        deathObject.GetComponent<Transform>().position = new Vector2(deathObject.GetComponent<Transform>().position.X, deathObject.GetComponent<Transform>().position.Y + deathObject.GetComponent<CircleCollider>().radius);
                    }
                    else
                    {
                        deathObject.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;
                    }
                    systemManager.DelayedAdd(deathObject);
                }
                    

            }
        }

    }
}

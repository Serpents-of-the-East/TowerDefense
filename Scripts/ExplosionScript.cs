using System;
using System.Collections.Generic;
using System.Text;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{ 
    public class ExplosionScript : ScriptBase
    {
        private float damage;
        SystemManager systemManager;
        private bool collisionsHappened;
        public ExplosionScript(GameObject gameObject, SystemManager systemManager, float damage) : base(gameObject)
        {
            this.damage = damage;
            this.systemManager = systemManager;
            collisionsHappened = false;
        }


        public override void OnCollisionStart(GameObject other)
        {
            if (other.ContainsComponent<EnemyHealth>() && other.GetComponent<EnemyTag>().enemyType == gameObject.GetComponent<EnemyTag>().enemyType)
            {
                EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
                enemyHealth.health -= damage;


                if (other.GetComponent<EnemyHealth>().health <= 0.0f)
                {
                    PointsComponent enemyPointsWorth = other.GetComponent<PointsComponent>();
                    PointsManager.AddPlayerPoints(enemyPointsWorth.points);
                    GameStats.DestroyedCreep();


                    if (enemyHealth.instantiateOnDeathObject != null)
                    {
                        foreach (var deathGameObject in enemyHealth.instantiateOnDeathObject)
                        {
                            deathGameObject.GetComponent<Transform>().position = this.gameObject.GetComponent<Transform>().position;
                            systemManager.DelayedAdd(deathGameObject);
                        }
                    }

                    systemManager.DelayedRemove(other);
                }

            }

            collisionsHappened = true;
        }

        public override void Destroyed()
        {
            AudioManager.PlaySoundEffect("explode", 0.7f);
        }

        public override void Update(GameTime gameTime)
        {


            if (collisionsHappened)
            {
                systemManager.DelayedRemove(gameObject);
            }
        }

    }
}

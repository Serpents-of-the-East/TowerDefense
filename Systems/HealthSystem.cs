using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public class HealthSystem : CrowEngineBase.System
    {
        public HealthSystem(SystemManager systemManager) : base(systemManager, typeof(EnemyHealth), typeof(Transform), typeof(PointsComponent))
        {
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (uint id in m_gameObjects.Keys)
            {
                EnemyHealth enemyHealth = m_gameObjects[id].GetComponent<EnemyHealth>();


                if (enemyHealth.health <= 0.0f)
                {
                    PointsComponent enemyPointsWorth = m_gameObjects[id].GetComponent<PointsComponent>();
                    PointsManager.AddPlayerPoints(enemyPointsWorth.points);

                    if (enemyHealth.instantiateOnDeathObject != null)
                    {

                        foreach (var deathGameObject in enemyHealth.instantiateOnDeathObject)
                        {
                            deathGameObject.GetComponent<Transform>().position = m_gameObjects[id].GetComponent<Transform>().position;
                            systemManager.Add(deathGameObject);
                        }
                    }


                    systemManager.Remove(id);
                }



            }



        }
    }
}

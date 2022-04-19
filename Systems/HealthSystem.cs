//using System;
//using CrowEngineBase;
//using Microsoft.Xna.Framework;

//namespace TowerDefense
//{
//    public class HealthSystem : CrowEngineBase.System
//    {
//        public HealthSystem(SystemManager systemManager) : base(systemManager, typeof(EnemyHealth), typeof(Transform), typeof(PointsComponent))
//        {
//        }

//        protected override void Update(GameTime gameTime)
//        {
//        }

//        protected override void Remove(uint id)
//        {
//            PointsComponent enemyPointsWorth = m_gameObjects[id].GetComponent<PointsComponent>();
//            PointsManager.AddPlayerPoints(enemyPointsWorth.points);

//            EnemyHealth enemyHealth = m_gameObjects[id].GetComponent<EnemyHealth>();


//            if (enemyHealth.instantiateOnDeathObject != null)
//            {

//                foreach (var deathGameObject in enemyHealth.instantiateOnDeathObject)
//                {
//                    deathGameObject.GetComponent<Transform>().position = m_gameObjects[id].GetComponent<Transform>().position;
//                    systemManager.Add(deathGameObject);
//                }
//            }



//            base.Remove(id);
//        }
//    }
//}

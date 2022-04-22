using System;
using System.Collections.Generic;
using System.Text;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class BombBullet
    {
        private static float SPEED = 500;

        public static GameObject Create(Vector2 position, Vector2 target, GameObject tower, SystemManager systemManager)
        {
            GameObject gameObject = new GameObject();
            Vector2 direction = (target - position);
            direction.Normalize();

            float rotation = MathF.Atan2(direction.Y, direction.X);
            Transform bulletTransform = new Transform(position, 0.0f, Vector2.One);

            gameObject.Add(new Transform(position, rotation, Vector2.One * 2));
            gameObject.Add(new Rigidbody() { velocity = direction * SPEED });
            gameObject.Add(new CircleCollider(20));
            Bullet bullet = new Bullet() { speed = SPEED, damage = tower.GetComponent<TowerComponent>().damage[tower.GetComponent<TowerComponent>().upgradeLevel] };
            gameObject.Add(bullet);
            gameObject.Add(new Sprite(ResourceManager.GetTexture("arrow"), Color.White));
            gameObject.Add(BombTrailParticles.Create());
            gameObject.Add(new EnemyTag(EnemyType.GROUND));

            gameObject.Add(new Explosion()
            {
                instantiateOnDeathObject = new List<GameObject>()
                {
                    ExplosionPrefab.Create(position, 100, systemManager, tower.GetComponent<TowerComponent>().damage[tower.GetComponent<TowerComponent>().upgradeLevel], EnemyType.GROUND),
                    BombExplosionParticles.Create(position)
        
                }
            });


            return gameObject;
        }


    }
}

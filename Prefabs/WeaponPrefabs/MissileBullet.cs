using System;
using System.Collections.Generic;
using CrowEngineBase;

using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class MissileBullet
    {
        private static float SPEED = 500;

        public static GameObject Create(Vector2 position, Transform target, GameObject tower, SystemManager systemManager)
        {
            GameObject gameObject = new GameObject();
            Vector2 targetPosition = target.position;
            Vector2 direction = (targetPosition - position);
            direction.Normalize();

            float rotation = MathF.Atan2(direction.Y, direction.X);

            gameObject.Add(new Transform(position, rotation, Vector2.One));
            gameObject.Add(new Rigidbody() { velocity = direction * SPEED });
            gameObject.Add(new CircleCollider(20));
            gameObject.Add(new Bullet() { speed = SPEED, damage = tower.GetComponent<TowerComponent>().damage[tower.GetComponent<TowerComponent>().upgradeLevel] });
            gameObject.Add(new EnemyTag(EnemyType.AIR));
            gameObject.Add(new GuidedMissile() { target = target });
            gameObject.Add(new AnimatedSprite(ResourceManager.GetTexture("magebolt"), new int[] { 100, 100, 100, 100 }, Vector2.One * 64));

            gameObject.Add(MissileTrailParticles.Create());

            gameObject.Add(new Explosion()
            {
                instantiateOnDeathObject = new List<GameObject>()
                {
                    ExplosionPrefab.Create(position, 40, systemManager, tower.GetComponent<TowerComponent>().damage[tower.GetComponent<TowerComponent>().upgradeLevel], EnemyType.AIR),
                    MissileExplosionParticles.Create(position)

                }
            });



            return gameObject;
        }
    }
}

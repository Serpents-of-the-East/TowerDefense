using System;

using CrowEngineBase;

using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class BasicBullet
    {
        private static float SPEED = 400;

        public static GameObject Create(Vector2 position, Vector2 target, TowerComponent towerComponent)
        {
            GameObject gameObject = new GameObject();

            Vector2 direction = (target - position);
            direction.Normalize();

            float rotation = MathF.Atan2(direction.Y, direction.X);

            gameObject.Add(new Transform(position, rotation, Vector2.One));

            gameObject.Add(new Rigidbody() { velocity = direction * SPEED });

            gameObject.Add(new CircleCollider(20));

            gameObject.Add(new Bullet() { speed = SPEED, damage = towerComponent.damage[towerComponent.upgradeLevel] });

            gameObject.Add(new Sprite(ResourceManager.GetTexture("empty"), Color.Red));

            gameObject.Add(BombTrailParticles.Create());


            return gameObject;
        }
    }
}

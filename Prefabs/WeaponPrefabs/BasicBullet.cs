using System;

using CrowEngineBase;

using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class BasicBullet
    {
        private static float SPEED = 500;

        public static GameObject Create(Vector2 position, Vector2 target, GameObject tower)
        {
            GameObject gameObject = new GameObject();
            Vector2 direction = (target - position);
            direction.Normalize();

            float rotation = MathF.Atan2(direction.Y, direction.X);

            gameObject.Add(new Transform(position, rotation, Vector2.One * 2));
            gameObject.Add(new Rigidbody() { velocity = direction * SPEED });
            gameObject.Add(new CircleCollider(20));
            gameObject.Add(new Bullet() { speed = SPEED, damage = tower.GetComponent<TowerComponent>().damage[tower.GetComponent<TowerComponent>().upgradeLevel] });

            gameObject.Add(new Sprite(ResourceManager.GetTexture("arrow"), Color.White));

            gameObject.Add(BombTrailParticles.Create());


            return gameObject;
        }
    }
}

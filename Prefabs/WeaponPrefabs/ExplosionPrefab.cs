using System;
using System.Collections.Generic;
using System.Text;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class ExplosionPrefab
    {
        public static GameObject Create(Vector2 position, float radius, SystemManager systemManager, float damage, EnemyType enemyType)
        {
            GameObject gameObject = new GameObject();
            gameObject.Add(new Rigidbody());
            gameObject.Add(new Transform(position, 0, Vector2.One));
            gameObject.Add(new CircleCollider(radius));
            gameObject.Add(new ExplosionScript(gameObject, systemManager, damage));
            gameObject.Add(new EnemyTag(enemyType));

            return gameObject;
        }
    }
}

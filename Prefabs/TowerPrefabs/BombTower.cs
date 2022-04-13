using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class BombTower
    {


        public static GameObject Create(SystemManager systemManager)
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new Bomb());
            gameObject.Add(new TowerComponent() { turnSpeed = 3 });
            gameObject.Add(new Sprite(ResourceManager.GetTexture("bombTower"), Color.White, 0));
            gameObject.Add(new PointsComponent() { points = 200 });
            gameObject.Add(new CircleCollider(Pathfinder.SIZE_PER_TOWER * 2));
            gameObject.Add(new Rigidbody());
            gameObject.Add(new Transform(Vector2.Zero, 0, Vector2.One));
            gameObject.Add(new EnemyTag(EnemyType.GROUND));

            gameObject.Add(new TowerTargetingScript(gameObject, TowerTargetingScript.BulletType.Bomb, TimeSpan.FromMilliseconds(500), systemManager));


            return gameObject;

        }
    }
}

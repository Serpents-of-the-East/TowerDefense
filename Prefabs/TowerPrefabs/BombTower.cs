using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class BombTower
    {

        public static float TowerRadius = Pathfinder.SIZE_PER_TOWER * 2;
        public static GameObject Create(SystemManager systemManager)
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new Bomb());
            gameObject.Add(new TowerComponent() { turnSpeed = 6 });
            gameObject.Add(new Sprite(ResourceManager.GetTexture("bombTower"), Color.White, 0));
            gameObject.Add(new PointsComponent() { points = 200 });
            gameObject.Add(new CircleCollider(TowerRadius));
            gameObject.Add(new RectangleCollider(new Vector2(Pathfinder.SIZE_PER_TOWER, Pathfinder.SIZE_PER_TOWER)));
            gameObject.Add(new Rigidbody());
            gameObject.Add(new Transform(Vector2.Zero, 0, Vector2.One * 2));
            gameObject.Add(new EnemyTag(EnemyType.GROUND));

            gameObject.Add(new TowerTargetingScript(gameObject, TowerTargetingScript.BulletType.Bomb, TimeSpan.FromMilliseconds(3000), systemManager));
            systemManager.Add(TowerColliderPrefab.Create(gameObject));


            return gameObject;

        }
    }
}

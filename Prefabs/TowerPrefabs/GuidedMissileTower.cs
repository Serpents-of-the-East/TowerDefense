using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class GuidedMissileTower
    {
        public static GameObject Create(SystemManager systemManager)
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new GuidedMissile());
            gameObject.Add(new TowerComponent() { turnSpeed = 3 });
            gameObject.Add(new Sprite(ResourceManager.GetTexture("guidedTower"), Color.White, 1));
            gameObject.Add(new AnimatedSprite(ResourceManager.GetTexture("guidedTowerBase"), new int[] { 250, 250, 250, 250 }, Vector2.One * 64, layerDepth:0));
            gameObject.Add(new CircleCollider(Pathfinder.SIZE_PER_TOWER*4));
            gameObject.Add(new RectangleCollider(new Vector2(Pathfinder.SIZE_PER_TOWER, Pathfinder.SIZE_PER_TOWER)));
            gameObject.Add(new PointsComponent() { points = 300 });
            gameObject.Add(new Rigidbody());
            gameObject.Add(new Transform(Vector2.Zero, 0, Vector2.One * 2));
            gameObject.Add(new EnemyTag(EnemyType.AIR));

            gameObject.Add(new TowerTargetingScript(gameObject, TowerTargetingScript.BulletType.Missile, TimeSpan.FromMilliseconds(1000), systemManager));

            systemManager.Add(TowerColliderPrefab.Create(gameObject));

            return gameObject;
        }
    }
}

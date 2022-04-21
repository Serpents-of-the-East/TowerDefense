using System;
using System.Collections.Generic;

using CrowEngineBase;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TowerDefense
{
    public static class BombTower
    {
        static List<Texture2D> towerTextures;

        static BombTower()
        {
            ResourceManager.RegisterTexture("Textures/ballista-1", "ballista-1");
            ResourceManager.RegisterTexture("Textures/ballista-2", "ballista-2");
            ResourceManager.RegisterTexture("Textures/ballista-3", "ballista-3");
            towerTextures = new List<Texture2D>() { ResourceManager.GetTexture("ballista-1"), ResourceManager.GetTexture("ballista-2"), ResourceManager.GetTexture("ballista-3"), };
        }

        public static float TowerRadius = Pathfinder.SIZE_PER_TOWER * 2;
        public static GameObject Create(SystemManager systemManager)
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new Bomb());
            gameObject.Add(new TowerComponent() { turnSpeed = 6, towerTextureByLevel = towerTextures });
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

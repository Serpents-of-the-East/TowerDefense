using System;
using System.Collections.Generic;
using CrowEngineBase;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace TowerDefense
{
    public static class AirGroundTower
    {

        static List<Texture2D> towerTextures;

        static AirGroundTower()
        {
            ResourceManager.RegisterTexture("Textures/cloudtower1", "cloudtower1");
            ResourceManager.RegisterTexture("Textures/cloudtower2", "cloudtower2");
            ResourceManager.RegisterTexture("Textures/cloudtower3", "cloudtower3");
            towerTextures = new List<Texture2D>() { ResourceManager.GetTexture("cloudtower1"), ResourceManager.GetTexture("cloudtower2"), ResourceManager.GetTexture("cloudtower3"), };
        }

        public static float TowerRadius = Pathfinder.SIZE_PER_TOWER * 3;
        public static GameObject Create(SystemManager systemManager)
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new TowerComponent() { turnSpeed = 6, towerTextureByLevel = towerTextures });
            gameObject.Add(new Sprite(ResourceManager.GetTexture("cloudtower1"), Color.White, 0));
            gameObject.Add(new CircleCollider(TowerRadius));
            gameObject.Add(new PointsComponent() { points = 100 });
            gameObject.Add(new Rigidbody());
            gameObject.Add(new Transform(Vector2.Zero, 0, Vector2.One * 2));
            gameObject.Add(new EnemyTag(EnemyType.MIXED));


            gameObject.Add(new TowerTargetingScript(gameObject, TowerTargetingScript.BulletType.AirGround, TimeSpan.FromMilliseconds(500), systemManager));

            systemManager.Add(TowerColliderPrefab.Create(gameObject));


            return gameObject;
        }



    }
}

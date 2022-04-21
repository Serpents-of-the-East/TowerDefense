using System;
using System.Collections.Generic;
using CrowEngineBase;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace TowerDefense
{
    public static class RegularTower
    {

        static List<Texture2D> towerTextures;

        static RegularTower()
        {
            ResourceManager.RegisterTexture("Textures/archer-tower0", "archer-tower0");
            ResourceManager.RegisterTexture("Textures/archer-tower1", "archer-tower1");
            ResourceManager.RegisterTexture("Textures/archer-tower2", "archer-tower2");
            towerTextures = new List<Texture2D>() { ResourceManager.GetTexture("archer-tower0"), ResourceManager.GetTexture("archer-tower1"), ResourceManager.GetTexture("archer-tower2"), };
        }

        public static float TowerRadius = Pathfinder.SIZE_PER_TOWER * 2;
        public static GameObject Create(SystemManager systemManager)
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new TowerComponent() { turnSpeed = 6, towerTextureByLevel = towerTextures });
            gameObject.Add(new Sprite(ResourceManager.GetTexture("regularTower"), Color.White, 0));
            gameObject.Add(new CircleCollider(TowerRadius));
            gameObject.Add(new PointsComponent() { points = 100 });
            gameObject.Add(new Rigidbody());
            gameObject.Add(new Transform(Vector2.Zero, 0, Vector2.One * 2));
            gameObject.Add(new EnemyTag(EnemyType.GROUND));


            gameObject.Add(new TowerTargetingScript(gameObject, TowerTargetingScript.BulletType.Basic, TimeSpan.FromMilliseconds(500), systemManager));

            systemManager.Add(TowerColliderPrefab.Create(gameObject));


            return gameObject;
        }



    }
}

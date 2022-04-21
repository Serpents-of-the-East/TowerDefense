using System;
using System.Collections.Generic;
using CrowEngineBase;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense
{
    public static class GuidedMissileTower
    {

        static List<Texture2D> towerTextures;

        static GuidedMissileTower()
        {
            ResourceManager.RegisterTexture("Textures/Level0GuidedMissile", "Level0GuidedMissile");
            ResourceManager.RegisterTexture("Textures/Leve1GuidedMissile", "Level1GuidedMissile");
            ResourceManager.RegisterTexture("Textures/Leve2GuidedMissile", "Level2GuidedMissile");
            towerTextures = new List<Texture2D>() { ResourceManager.GetTexture("Level0GuidedMissile"), ResourceManager.GetTexture("Level1GuidedMissile"), ResourceManager.GetTexture("Level2GuidedMissile"), };
        }

        public static float TowerRadius = Pathfinder.SIZE_PER_TOWER * 4;
        public static GameObject Create(SystemManager systemManager)
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new GuidedMissile());
            gameObject.Add(new TowerComponent() { turnSpeed = 6, changeAnimatedTexture = true, towerTextureByLevel = towerTextures });
            gameObject.Add(new Sprite(ResourceManager.GetTexture("guidedTower"), Color.White, 1));
            gameObject.Add(new AnimatedSprite(ResourceManager.GetTexture("Level0GuidedMissile"), new int[] { 250, 250, 250, 250 }, Vector2.One * 64, layerDepth:0));
            gameObject.Add(new CircleCollider(TowerRadius));
            gameObject.Add(new RectangleCollider(new Vector2(Pathfinder.SIZE_PER_TOWER, Pathfinder.SIZE_PER_TOWER)));
            gameObject.Add(new PointsComponent() { points = 300 });
            gameObject.Add(new Rigidbody());
            gameObject.Add(new Transform(Vector2.Zero, 0, Vector2.One * 2));
            gameObject.Add(new EnemyTag(EnemyType.AIR));

            gameObject.Add(new TowerTargetingScript(gameObject, TowerTargetingScript.BulletType.Missile, TimeSpan.FromMilliseconds(2000), systemManager));

            systemManager.Add(TowerColliderPrefab.Create(gameObject));

            return gameObject;
        }
    }
}

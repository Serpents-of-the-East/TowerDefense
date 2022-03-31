using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class RegularTower
    {
        public static GameObject Create()
        {
            GameObject gameObject = new();

            gameObject.Add(new Bullet());
            gameObject.Add(new TowerComponent());
            gameObject.Add(new Sprite(ResourceManager.GetTexture("crow"), Color.White, 0));
            gameObject.Add(new CircleCollider(10));
            gameObject.Add(new Rigidbody());

            return gameObject;
        }



    }
}

using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense
{
    public static class EnemyHealthBar
    {
        public static GameObject CreateEnemyHealthBar(GameObject parent, SystemManager systemManager)
        {
            GameObject gameObject = new GameObject();
            gameObject.Add(new Transform(Vector2.Zero, 0, Vector2.One));
            gameObject.Add(new Sprite(TextureCreation.CreateTexture((int)parent.GetComponent<EnemyHealth>().health, (int)parent.GetComponent<EnemyHealth>().maxHealth, 10, pixels => Color.Green, pixels => Color.Red), Color.White));
            gameObject.Add(new UpdateEnemyHealthBarScript(gameObject, parent, systemManager));



            return gameObject;
        }
    }
}

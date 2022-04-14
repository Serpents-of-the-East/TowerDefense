using System;
using System.Diagnostics;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public class UpdateEnemyHealthBarScript : ScriptBase
    {

        Transform transform;

        Sprite sprite;

        GameObject parent;
        SystemManager systemManager;
        float lastFrameHealth;

        public UpdateEnemyHealthBarScript(GameObject gameObject, GameObject parent, SystemManager systemManager) : base(gameObject)
        {
            this.parent = parent;
            this.systemManager = systemManager;
        }

        public override void Start()
        {
            this.transform = gameObject.GetComponent<Transform>();
            this.sprite = gameObject.GetComponent<Sprite>();
        }


        public override void Update(GameTime gameTime)
        {
            EnemyHealth enemyHealth = parent.GetComponent<EnemyHealth>();
            float currentHealth = enemyHealth.health;
            Transform parentTransform = parent.GetComponent<Transform>();

            if (currentHealth <= 0.0f)
            {
                systemManager.DelayedRemove(gameObject);
                return;
            }


            if (currentHealth != lastFrameHealth)
            {
                sprite.sprite = TextureCreation.CreateTexture((int) ((currentHealth / enemyHealth.maxHealth) * enemyHealth.maxHealth), (int)(enemyHealth.maxHealth), 10, pixel => Color.Green, pixel => Color.Red);
            }


            transform.position = new Vector2(parentTransform.position.X, parentTransform.position.Y - 64);
            lastFrameHealth = currentHealth;



        }


    }
}

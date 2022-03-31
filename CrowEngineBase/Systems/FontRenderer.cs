using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrowEngineBase
{
    public class FontRenderer : System
    {
        private Vector2 m_centerOfScreen;
        private float m_scalingRatio;



        public FontRenderer(SystemManager systemManager, float clientBoundsHeight, Vector2 screenSize) : base(systemManager, typeof(Text), typeof(Transform))
        {
            m_scalingRatio = clientBoundsHeight / PhysicsEngine.PHYSICS_DIMENSION_HEIGHT;
            this.m_centerOfScreen = screenSize / 2;

            systemManager.UpdateSystem -= Update;
        }

        private void DrawBackground(Text text, Transform transform, Vector2 trueRenderPosition, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(text.spriteFont, text.text, new Vector2(trueRenderPosition.X + 1, trueRenderPosition.Y), text.outlineColor, transform.rotation, text.centerOfRotation, transform.scale, text.spriteEffect, text.layerDepth + 1);
            spriteBatch.DrawString(text.spriteFont, text.text, new Vector2(trueRenderPosition.X - 1, trueRenderPosition.Y), text.outlineColor, transform.rotation, text.centerOfRotation, transform.scale, text.spriteEffect, text.layerDepth + 1);
            spriteBatch.DrawString(text.spriteFont, text.text, new Vector2(trueRenderPosition.X, trueRenderPosition.Y + 1), text.outlineColor, transform.rotation, text.centerOfRotation, transform.scale, text.spriteEffect, text.layerDepth + 1);
            spriteBatch.DrawString(text.spriteFont, text.text, new Vector2(trueRenderPosition.X, trueRenderPosition.Y - 1), text.outlineColor, transform.rotation, text.centerOfRotation, transform.scale, text.spriteEffect, text.layerDepth + 1);
        }

        protected override void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (uint id in m_gameObjects.Keys)
            {
                Text text = m_gameObjects[id].GetComponent<Text>();
                Transform transform = m_gameObjects[id].GetComponent<Transform>();

                Vector2 distanceFromCenter = m_gameObjects[id].GetComponent<Transform>().position - new Vector2(PhysicsEngine.PHYSICS_DIMENSION_WIDTH, PhysicsEngine.PHYSICS_DIMENSION_HEIGHT) / 2f;
                Vector2 renderDistanceFromCenter = distanceFromCenter * m_scalingRatio;
                Vector2 trueRenderPosition = renderDistanceFromCenter + m_centerOfScreen;


                if (text.renderOutline) DrawBackground(text, transform, trueRenderPosition, spriteBatch);
                spriteBatch.DrawString(text.spriteFont, text.text, trueRenderPosition, text.color, transform.rotation, text.centerOfRotation, transform.scale, text.spriteEffect, text.layerDepth);

            }
        }
    }
}

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrowEngineBase
{
    public class FontRenderer : System
    {
        private SpriteBatch m_spriteBatch;
        private Vector2 m_centerOfScreen;
        private float m_scalingRatio;



        public FontRenderer(SpriteBatch spriteBatch, float clientBoundsHeight, Vector2 screenSize) : base(typeof(Text), typeof(Transform))
        {
            this.m_spriteBatch = spriteBatch;
            m_scalingRatio = clientBoundsHeight / PhysicsEngine.PHYSICS_DIMENSION_HEIGHT;
            this.m_centerOfScreen = screenSize / 2;


        }

        private void drawBackground(Text text, Transform transform, Vector2 trueRenderPosition)
        {
            m_spriteBatch.DrawString(text.spriteFont, text.text, new Vector2(trueRenderPosition.X + 1, trueRenderPosition.Y), text.outlineColor, transform.rotation, text.centerOfRotation, transform.scale, text.spriteEffect, text.layerDepth);
            m_spriteBatch.DrawString(text.spriteFont, text.text, new Vector2(trueRenderPosition.X - 1, trueRenderPosition.Y), text.outlineColor, transform.rotation, text.centerOfRotation, transform.scale, text.spriteEffect, text.layerDepth);
            m_spriteBatch.DrawString(text.spriteFont, text.text, new Vector2(trueRenderPosition.X, trueRenderPosition.Y + 1), text.outlineColor, transform.rotation, text.centerOfRotation, transform.scale, text.spriteEffect, text.layerDepth);
            m_spriteBatch.DrawString(text.spriteFont, text.text, new Vector2(trueRenderPosition.X, trueRenderPosition.Y - 1), text.outlineColor, transform.rotation, text.centerOfRotation, transform.scale, text.spriteEffect, text.layerDepth);
        }


        protected override void Update(GameTime gameTime)
        {

            m_spriteBatch.Begin();
            foreach (uint id in m_gameObjects.Keys)
            {
                Text text = m_gameObjects[id].GetComponent<Text>();
                Transform transform = m_gameObjects[id].GetComponent<Transform>();

                Vector2 distanceFromCenter = m_gameObjects[id].GetComponent<Transform>().position - new Vector2(PhysicsEngine.PHYSICS_DIMENSION_WIDTH, PhysicsEngine.PHYSICS_DIMENSION_HEIGHT) / 2f;
                Vector2 renderDistanceFromCenter = distanceFromCenter * m_scalingRatio;
                Vector2 trueRenderPosition = renderDistanceFromCenter + m_centerOfScreen;


                if (text.renderOutline) drawBackground(text, transform, trueRenderPosition);
                m_spriteBatch.DrawString(text.spriteFont, text.text, transform.position, text.color, transform.rotation, text.centerOfRotation, transform.scale, text.spriteEffect, text.layerDepth);

            }
            m_spriteBatch.End();
        }
    }
}

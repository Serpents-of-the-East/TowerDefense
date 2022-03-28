using System;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;

namespace CrowEngineBase
{
    public class ParticleRenderer : System
    {
        private GameObject m_camera;

        private float m_scalingRatio;
        private Vector2 m_centerOfScreen;

        public ParticleRenderer(SystemManager systemManager, float clientBoundsHeight, GameObject camera, Vector2 screenSize) : base(systemManager, typeof(Particle))
        {
            m_scalingRatio = clientBoundsHeight / PhysicsEngine.PHYSICS_DIMENSION_HEIGHT;
            this.m_camera = camera;
            this.m_centerOfScreen = screenSize / 2;
            systemManager.UpdateSystem -= Update; // remove the automatically added update
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (uint id in m_gameObjects.Keys)
            {
                Particle particle = m_gameObjects[id].GetComponent<Particle>();
                foreach (SingleParticle singleParticle in particle.particles)
                {
                    Vector2 distanceFromCenter = singleParticle.position - new Vector2(PhysicsEngine.PHYSICS_DIMENSION_WIDTH, PhysicsEngine.PHYSICS_DIMENSION_HEIGHT) / 2f;
                    Vector2 renderDistanceFromCenter = distanceFromCenter * m_scalingRatio;
                    Vector2 trueRenderPosition = renderDistanceFromCenter + m_centerOfScreen;
                    spriteBatch.Draw(particle.texture, trueRenderPosition, null,
                        Color.White, singleParticle.rotation,
                        new Vector2(particle.texture.Width / 2, particle.texture.Height / 2), singleParticle.scale * m_scalingRatio,
                        SpriteEffects.None, particle.renderDepth);
                }
            }

        }

        protected override void Update(GameTime gameTime)
        {
        }
    }
}

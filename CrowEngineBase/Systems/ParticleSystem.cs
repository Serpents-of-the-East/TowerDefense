using System;
using Microsoft.Xna.Framework;

namespace CrowEngineBase
{
    public class ParticleSystem : System
    {
        public ParticleSystem(SystemManager systemManager) : base(systemManager, typeof(Particle), typeof(Transform))
        {
        }

        protected override void Update(GameTime gameTime)
        {
            // Update each particle group
            foreach (uint id in m_gameObjects.Keys)
            {
                Particle particleGroup = m_gameObjects[id].GetComponent<Particle>();

                if (particleGroup.maxSystemLifetime != null)
                {
                    // Update each particle of group
                    for (int i = 0; i < particleGroup.particles.Count; i++)
                    {
                        // Update time on particle
                        particleGroup.particles[i].lifeTime -= gameTime.ElapsedGameTime;

                        // Remove it if it has expired
                        if (particleGroup.particles[i].lifeTime <= TimeSpan.Zero)
                        {
                            particleGroup.particles.RemoveAt(i);
                        }
                        else
                        {
                            particleGroup.particles[i].position += (gameTime.ElapsedGameTime.Milliseconds / 1000f) * particleGroup.particles[i].velocity;
                            particleGroup.particles[i].rotation += (gameTime.ElapsedGameTime.Milliseconds / 1000f) * particleGroup.rotationSpeed;
                        }
                    }

                    if (particleGroup.maxSystemLifetime > TimeSpan.Zero)
                    {
                        particleGroup.maxSystemLifetime -= gameTime.ElapsedGameTime;


                        // Create new particles
                        particleGroup.currentTime += gameTime.ElapsedGameTime;

                        // number | time
                        // ------ | 
                        // second |
                        Transform particleTransform = m_gameObjects[id].GetComponent<Transform>();

                        while (particleGroup.currentTime > particleGroup.rate)
                        {
                            particleGroup.currentTime -= particleGroup.rate;

                            if (particleGroup.isEmitting)
                            {
                                SingleParticle singleParticle = new SingleParticle();
                                singleParticle.lifeTime = particleGroup.maxLifeTime;
                                singleParticle.position = new Vector2((float)particleGroup.random.NextGaussian(particleTransform.position.X, particleGroup.emissionArea.X), (float)particleGroup.random.NextGaussian(particleTransform.position.Y, particleGroup.emissionArea.Y));
                                singleParticle.rotation = particleGroup.random.NextRange(0, 360);
                                singleParticle.scale = particleGroup.random.NextRange(particleGroup.minScale, particleGroup.maxScale);

                                float emittedRotation = particleTransform.rotation; // start with the transform's rotation

                                // Next we need to modify the rotation using the bounds

                                float rotationModification = particleGroup.random.NextRange(particleGroup.emissionArc.X, particleGroup.emissionArc.Y);

                                emittedRotation +=  (MathF.PI / 180) * rotationModification;

                                Vector2 particleDirection = new Vector2(MathF.Cos(emittedRotation), MathF.Sin(emittedRotation));

                                singleParticle.velocity = particleDirection * particleGroup.random.NextRange(particleGroup.minSpeed, particleGroup.maxSpeed);




                                particleGroup.particles.Insert(0, singleParticle);
                            }
                        }
                    }

                }


                
            }
        }
    }
}

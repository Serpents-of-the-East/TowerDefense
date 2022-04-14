using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense
{
    public class CreditScreen : Screen
    {
        private Renderer renderSystem;
        private PhysicsEngine physicsEngine;
        private ParticleRenderer particleRenderer;
        private ParticleSystem particleSystem;
        private InputSystem inputSystem;
        private ScriptSystem scriptSystem;
        private LightRenderer lightRenderer;
        private RenderTarget2D renderTarget;
        private FontRenderer fontRenderer;
        private GameObject camera;

        public CreditScreen(ScreenEnum screen) : base(screen)
        {
        }


        public override void Initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, GameWindow window)
        {
            base.Initialize(graphicsDevice, graphics, window);

            physicsEngine = new PhysicsEngine(systemManager);
            inputSystem = new InputSystem(systemManager);
            scriptSystem = new ScriptSystem(systemManager);
            camera = new GameObject();
            camera.Add(new Transform(new Vector2(0, 0), 0, Vector2.One));
            particleSystem = new ParticleSystem(systemManager);


            systemManager.Add(camera);
        }


        public override void Draw(GameTime gameTime)
        {
            m_spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp);

            fontRenderer.Draw(gameTime, m_spriteBatch);
            renderSystem.Draw(gameTime, m_spriteBatch);
            particleRenderer.Draw(gameTime, m_spriteBatch);

            m_spriteBatch.End();
        }

        public override void LoadContent()
        {
            renderSystem = new Renderer(systemManager, m_window.ClientBounds.Height, camera, new Vector2(m_window.ClientBounds.Width, m_window.ClientBounds.Height));
            renderSystem.debugMode = true;
            particleRenderer = new ParticleRenderer(systemManager, m_window.ClientBounds.Height, camera, new Vector2(m_window.ClientBounds.Width, m_window.ClientBounds.Height));
            lightRenderer = new LightRenderer(systemManager, m_window.ClientBounds.Height, camera, new Vector2(m_window.ClientBounds.Width, m_window.ClientBounds.Height), m_graphicsDevice);
            lightRenderer.globalLightLevel = 0f;
            fontRenderer = new FontRenderer(systemManager, m_window.ClientBounds.Height, new Vector2(m_window.ClientBounds.Width, m_window.ClientBounds.Height), camera);
            renderTarget = new RenderTarget2D(m_graphicsDevice, m_graphicsDevice.PresentationParameters.BackBufferWidth, m_graphicsDevice.PresentationParameters.BackBufferHeight, false, m_graphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);

            ResourceManager.RegisterTexture("fire", "fire");
        }

        public override void OnScreenDefocus()
        {
        }

        public override void OnScreenFocus()
        {
            currentScreen = ScreenEnum.Credits;
            screenName = ScreenEnum.Credits;

        }

        public override void SetupGameObjects()
        {
            systemManager.Add(RyanAnderson.CreateRyanAnderson());
            systemManager.Add(TaylorAnderson.CreateTaylorAnderson());
            systemManager.Add(CreditsKeyboard.CreateCreditsKeyboard(SetCurrentScreen));


            // Temporary

            GameObject testParticles = new GameObject();

            Particle particle = new Particle(ResourceManager.GetTexture("fire"));

            testParticles.Add(new Sprite(ResourceManager.GetTexture("fire"), Color.White));

            particle.maxLifeTime = TimeSpan.FromMilliseconds(2000);
            particle.maxSpeed = 100;
            particle.emissionArc = new Vector2(-45, 45);
            particle.rate = TimeSpan.FromMilliseconds(10);
            particle.minScale = 0.5f;
            particle.maxScale = 1;
            particle.maxSystemLifetime = TimeSpan.MaxValue;

            testParticles.Add(particle);
            testParticles.Add(new Transform(Vector2.Zero, 0, Vector2.One));

            systemManager.Add(testParticles);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace CrowEngineBase
{
    class DefaultScreen : Screen
    {

        private Renderer renderSystem;
        private PhysicsEngine physicsEngine;
        private ParticleRenderer particleRenderer;
        private ParticleSystem particleSystem;
        private InputSystem inputSystem;
        private ScriptSystem scriptSystem;
        private LightRenderer lightRenderer;

        private GameObject camera;


        private RenderTarget2D renderTarget;
        private RenderTarget2D lightRenderTarget;

        public DefaultScreen(ScreenEnum screenEnum) : base(screenEnum) { }

        public override void Initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, GameWindow window)
        {
            base.Initialize(graphicsDevice, graphics, window);

            physicsEngine = new PhysicsEngine(systemManager);
            inputSystem = new InputSystem(systemManager);
            scriptSystem = new ScriptSystem(systemManager);
            camera = new GameObject();
            camera.Add(new Transform(Vector2.Zero, 0, Vector2.One));
            particleSystem = new ParticleSystem(systemManager);



            systemManager.Add(camera);
        }


        public override void Draw(GameTime gameTime)
        {

            m_graphicsDevice.SetRenderTarget(renderTarget);

            m_spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp);

            renderSystem.Draw(gameTime, m_spriteBatch);
            particleRenderer.Draw(gameTime, m_spriteBatch);

            m_spriteBatch.End();

            lightRenderer.Draw(gameTime, lightRenderTarget);

            m_graphicsDevice.SetRenderTarget(null);

            m_spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            m_spriteBatch.Draw(renderTarget, new Rectangle(0, 0, m_window.ClientBounds.Width, m_window.ClientBounds.Height), Color.White);

            m_spriteBatch.End();

            m_spriteBatch.Begin(SpriteSortMode.Immediate, LightRenderer.multiplyBlend);

            m_spriteBatch.Draw(lightRenderTarget, new Rectangle(0, 0, m_window.ClientBounds.Width, m_window.ClientBounds.Height), Color.White);

            m_spriteBatch.End();
        }

        public override void LoadContent()
        {
            ResourceManager.RegisterTexture("crow", "crow");
            ResourceManager.RegisterTexture("fire", "fire");

            renderSystem = new Renderer(systemManager, m_window.ClientBounds.Height, camera, new Vector2(m_window.ClientBounds.Width, m_window.ClientBounds.Height));
            particleRenderer = new ParticleRenderer(systemManager, m_window.ClientBounds.Height, camera, new Vector2(m_window.ClientBounds.Width, m_window.ClientBounds.Height));
            lightRenderer = new LightRenderer(systemManager, m_window.ClientBounds.Height, camera, new Vector2(m_window.ClientBounds.Width, m_window.ClientBounds.Height), m_graphicsDevice);
            lightRenderer.globalLightLevel = 0f;

            renderTarget = new RenderTarget2D(m_graphicsDevice, m_graphicsDevice.PresentationParameters.BackBufferWidth, m_graphicsDevice.PresentationParameters.BackBufferHeight, false, m_graphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
            lightRenderTarget = new RenderTarget2D(m_graphicsDevice, m_graphicsDevice.PresentationParameters.BackBufferWidth, m_graphicsDevice.PresentationParameters.BackBufferHeight, false, m_graphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
        }

        public override void OnScreenDefocus()
        {
            Debug.WriteLine("Default Screen was unloaded");
        }

        public override void OnScreenFocus()
        {
            Debug.WriteLine("Default Screen was loaded");
        }

        /// <summary>
        /// Note, while this one creates gameObjects manually inline, this should really be done in a separate file, in a static class.
        /// The reason this is done this way here, is so that any naming conventions you'd like to have don't conflict
        /// </summary>
        public override void SetupGameObjects()
        {
            GameObject player = new GameObject();
            KeyboardInput keyboardInput = new KeyboardInput();
            keyboardInput.actionKeyPairs.Add("MoveUp", Keys.W);
            keyboardInput.actionKeyPairs.Add("MoveDown", Keys.S);
            keyboardInput.actionKeyPairs.Add("MoveLeft", Keys.A);
            keyboardInput.actionKeyPairs.Add("MoveRight", Keys.D);

            Particle particleGroup = new Particle(ResourceManager.GetTexture("fire"));
            particleGroup.rate = TimeSpan.FromMilliseconds(50);
            particleGroup.maxSpeed = 100;
            particleGroup.minSpeed = 100;
            particleGroup.minScale = 1;
            particleGroup.maxScale = 4;
            particleGroup.rotationSpeed = 1;
            particleGroup.renderDepth = 1;
            particleGroup.maxLifeTime = TimeSpan.FromSeconds(1);
            particleGroup.emissionArea = Vector2.One * 5;

            player.Add(particleGroup);
            player.Add(keyboardInput);
            player.Add(new BasicTestScript(player));
            player.Add(new Transform(Vector2.One * 500, 0, Vector2.One * 2));
            player.Add(new Sprite(ResourceManager.GetTexture("crow"), Color.White, 0));
            player.Add(new Rigidbody());
            player.Add(new CircleCollider(10));
            player.Add(new Light(Color.White, 1f, 100));

            systemManager.Add(player);


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

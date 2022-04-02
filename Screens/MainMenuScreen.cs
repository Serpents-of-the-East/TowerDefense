using System;
using System.Diagnostics;
using CrowEngine;
using CrowEngineBase;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense
{
    public class MainMenuScreen : Screen
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


        public MainMenuScreen(ScreenEnum screenEnum) : base(screenEnum)
        {
        }

        public override void Initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, GameWindow window)
        {
            base.Initialize(graphicsDevice, graphics, window);

            physicsEngine = new PhysicsEngine(systemManager);
            inputSystem = new InputSystem(systemManager);
            scriptSystem = new ScriptSystem(systemManager);
            camera = new GameObject();
            camera.Add(new Transform(new Vector2(500, 500), 0, Vector2.One));
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
        }

        public override void OnScreenDefocus()
        {
            Debug.WriteLine("Main Menu Screen was unloaded");
        }

        public override void OnScreenFocus()
        {
            Debug.WriteLine("Main Menu Screen was loaded");
        }

        public override void SetupGameObjects()
        {
            systemManager.Add(Controls.CreateControls(m_window.ClientBounds.Width));
            systemManager.Add(PlayGame.CreatePlayGame(m_window.ClientBounds.Width));
            systemManager.Add(Credits.CreateCredits(m_window.ClientBounds.Width));
        }
    }
}

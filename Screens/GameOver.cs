using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CrowEngineBase;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense
{
    public class GameOver : Screen
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

        List<GameObject> menuItems = new List<GameObject>();



        private GameObject camera;


        public GameOver(ScreenEnum screenEnum) : base(screenEnum)
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
            m_spriteBatch.Begin(SpriteSortMode.Deferred, samplerState: SamplerState.PointClamp);

            renderSystem.Draw(gameTime, m_spriteBatch);
            particleRenderer.Draw(gameTime, m_spriteBatch);
            fontRenderer.Draw(gameTime, m_spriteBatch);


            m_spriteBatch.End();
        }

        public override void LoadContent()
        {
            renderSystem = new Renderer(systemManager, m_window.ClientBounds.Height, camera, new Vector2(m_window.ClientBounds.Width, m_window.ClientBounds.Height));
            renderSystem.debugMode = false;
            particleRenderer = new ParticleRenderer(systemManager, m_window.ClientBounds.Height, camera, new Vector2(m_window.ClientBounds.Width, m_window.ClientBounds.Height));
            lightRenderer = new LightRenderer(systemManager, m_window.ClientBounds.Height, camera, new Vector2(m_window.ClientBounds.Width, m_window.ClientBounds.Height), m_graphicsDevice);
            lightRenderer.globalLightLevel = 0f;
            fontRenderer = new FontRenderer(systemManager, m_window.ClientBounds.Height, new Vector2(m_window.ClientBounds.Width, m_window.ClientBounds.Height), camera);

            renderTarget = new RenderTarget2D(m_graphicsDevice, m_graphicsDevice.PresentationParameters.BackBufferWidth, m_graphicsDevice.PresentationParameters.BackBufferHeight, false, m_graphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);

            ResourceManager.RegisterSong("A-Lonely-Cherry-Tree", "Music/A-Lonely-Cherry-Tree");
        }

        public override void OnScreenDefocus()
        {
            Debug.WriteLine("Default Screen was unloaded");

            foreach (GameObject menuItem in menuItems)
            {
                systemManager.Remove(menuItem.id);
            }

            menuItems.Clear();
        }

        public override void OnScreenFocus()
        {
            currentScreen = ScreenEnum.GameOver;
            screenName = ScreenEnum.GameOver;

            AudioManager.PlaySong("A-Lonely-Cherry-Tree");

        }

        public override void SetupGameObjects()
        {
            systemManager.Add(LargeAltScreenBackground.Create());
            systemManager.Add(GameOverTitle.Create(SetCurrentScreen));
            systemManager.Add(Cursor.CreateCursor(SetCurrentScreen));
            systemManager.Add(GameOverExit.Create());
        }
    }
}
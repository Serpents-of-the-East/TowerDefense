using System;

using CrowEngineBase;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense
{
    public class GameplayScreen : Screen
    {

        private Renderer renderer;
        private PhysicsEngine physics;
        private ScriptSystem scriptSystem;
        private FontRenderer fontRenderer;
        private AnimationSystem animationSystem;
        private InputSystem inputSystem;
        private ParticleSystem particleSystem;
        private ParticleRenderer particleRenderer;
        private AudioSystem audioSystem;
        private PathSystem pathSystem;
        private ControlLoaderSystem controlLoaderSystem;

        private GameObject camera;

        public GameplayScreen(ScreenEnum screenEnum) : base (screenEnum)
        {
        }

        public override void Initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, GameWindow window)
        {
            base.Initialize(graphicsDevice, graphics, window);
            physics = new PhysicsEngine(systemManager);
            scriptSystem = new ScriptSystem(systemManager);
            inputSystem = new InputSystem(systemManager);
            pathSystem = new PathSystem(systemManager);
            controlLoaderSystem = new ControlLoaderSystem(systemManager);
            Pathfinder.SolvePaths();
            ParticleEmitter.systemManager = systemManager;

        }

        public override void Draw(GameTime gameTime)
        {
            m_spriteBatch.Begin(samplerState:SamplerState.PointClamp);

            if (controlLoaderSystem.controlsLoaded)
            {
                renderer.Draw(gameTime, m_spriteBatch);
                fontRenderer.Draw(gameTime, m_spriteBatch);
            }
            else
            {
                m_spriteBatch.DrawString(ResourceManager.GetFont("default"), "Loading...", Vector2.One * 300, Color.White); // temporary loading screen, this should really be done better
            }
            m_spriteBatch.End();
        }

        public override void LoadContent()
        {
            ResourceManager.RegisterTexture("Textures/empty", "empty");
            ResourceManager.RegisterTexture("circle", "bombTower");
            ResourceManager.RegisterTexture("fire", "guidedTower");
            ResourceManager.RegisterTexture("crow", "regularTower");

            camera = CameraPrefab.Create();
            camera.GetComponent<Transform>().position = Vector2.Zero;
            renderer = new Renderer(systemManager, m_window.ClientBounds.Height, camera, new Vector2(m_window.ClientBounds.Width, m_window.ClientBounds.Height));
            fontRenderer = new FontRenderer(systemManager, m_window.ClientBounds.Height, new Vector2(m_window.ClientBounds.Width, m_window.ClientBounds.Height), camera);

            systemManager.Add(camera);
        }

        public override void OnScreenDefocus()
        {
        }

        public override void OnScreenFocus()
        {
        }

        public override void SetupGameObjects()
        {
            systemManager.Add(BackgroundPrefab.Create());
            systemManager.Add(BasicEnemy.CreateBasicEnemy(Vector2.Zero));
            systemManager.Add(PlacementCursor.Create(systemManager, camera, controlLoaderSystem));
            systemManager.Add(TestEnemy.Create(Vector2.Zero));
            systemManager.Add(PointsPrefab.CreatePointsPrefab());

            Pathfinder.UpdatePathsAction.Invoke();
        }
    }
}

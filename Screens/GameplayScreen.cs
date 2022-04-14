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
            animationSystem = new AnimationSystem(systemManager);
            pathSystem = new PathSystem(systemManager);
            controlLoaderSystem = new ControlLoaderSystem(systemManager);
            TextureCreation.device = graphicsDevice;
            particleSystem = new ParticleSystem(systemManager);
            ParticleEmitter.systemManager = systemManager;

            Pathfinder.SolvePaths();
        }

        public override void Draw(GameTime gameTime)
        {
            m_spriteBatch.Begin(samplerState:SamplerState.PointClamp);

            if (controlLoaderSystem.controlsLoaded)
            {
                renderer.Draw(gameTime, m_spriteBatch);
                particleRenderer.Draw(gameTime, m_spriteBatch);
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
            ResourceManager.RegisterTexture("Textures/archer-tower0", "regularTower");
            ResourceManager.RegisterTexture("Textures/goblin", "goblin");
            ResourceManager.RegisterTexture("Textures/orc", "orc");
            ResourceManager.RegisterTexture("Textures/enemy-death-particle", "enemy-death-particle");
            ResourceManager.RegisterTexture("Textures/bomb-particle-trail", "bomb-particle-trail");
            ResourceManager.RegisterTexture("Textures/missile-explosion-particle", "missile-explosion-particle");
            ResourceManager.RegisterTexture("Textures/bomb-particle-explosion", "bomb-particle-explosion");


            camera = CameraPrefab.Create();
            camera.GetComponent<Transform>().position = Vector2.Zero;
            renderer = new Renderer(systemManager, m_window.ClientBounds.Height, camera, new Vector2(m_window.ClientBounds.Width, m_window.ClientBounds.Height));

            renderer.debugMode = true;

            fontRenderer = new FontRenderer(systemManager, m_window.ClientBounds.Height, new Vector2(m_window.ClientBounds.Width, m_window.ClientBounds.Height), camera);
            particleRenderer = new ParticleRenderer(systemManager, m_window.ClientBounds.Height, camera, new Vector2(m_window.ClientBounds.Width, m_window.ClientBounds.Height));

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
            GameObject basicEnemy = BasicEnemy.CreateBasicEnemy(Vector2.One, systemManager);
            systemManager.Add(basicEnemy);

            systemManager.Add(EnemyHealthBar.CreateEnemyHealthBar(basicEnemy, systemManager));

            

            GameObject tankyEnemyTest = TankyEnemy.CreateTankyEnemy(new Vector2(-100, 0), systemManager);
            systemManager.Add(tankyEnemyTest);

            systemManager.Add(EnemyHealthBar.CreateEnemyHealthBar(tankyEnemyTest, systemManager));

            systemManager.Add(PlacementCursor.Create(systemManager, camera, controlLoaderSystem));

            GameObject actualBasicEnemy = TestEnemy.Create(Vector2.Zero, systemManager);

            systemManager.Add(actualBasicEnemy);

            systemManager.Add(EnemyHealthBar.CreateEnemyHealthBar(actualBasicEnemy, systemManager));

            systemManager.Add(PointsPrefab.CreatePointsPrefab());

            systemManager.Add(PointsPrefab.CreatePointsPrefab());

            systemManager.Add(EnemySpawner.CreateEnemySpawner(systemManager));

            Pathfinder.CheckPathsFunc.Invoke();
        }
    }
}

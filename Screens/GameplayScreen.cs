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
            Pathfinder.SolvePaths();
        }

        public override void Draw(GameTime gameTime)
        {
            m_spriteBatch.Begin(samplerState:SamplerState.PointClamp);
            renderer.Draw(gameTime, m_spriteBatch);
            fontRenderer.Draw(gameTime, m_spriteBatch);
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
            systemManager.Add(BasicEnemy.CreateBasicEnemy(Vector2.Zero));
            systemManager.Add(PlacementCursor.Create(systemManager, camera));
            systemManager.Add(TestEnemy.Create(Vector2.Zero));
            systemManager.Add(PointsPrefab.CreatePointsPrefab());
            /*GameObject mouseDebug = new GameObject();
            mouseDebug.Add(new MouseInput());
            mouseDebug.Add(new Text("Position", ResourceManager.GetFont("default"), Color.White, Color.Black));*/

            Pathfinder.UpdatePathsAction.Invoke();
        }
    }
}

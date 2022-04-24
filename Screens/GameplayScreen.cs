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
        private WeaponSystem weaponSystem;
        private UpgradeSystem upgradeSystem;
        private GameOverSystem gameOverSystem;
        private GameObject camera;
        private KeyboardInput keyboardInput;
        private KeyboardInput spawnWavesInput;

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
            weaponSystem = new WeaponSystem(systemManager);
            upgradeSystem = new UpgradeSystem(systemManager);
            audioSystem = new AudioSystem(systemManager);
            TextureCreation.device = graphicsDevice;
            particleSystem = new ParticleSystem(systemManager);
            ParticleEmitter.systemManager = systemManager;
            keyboardInput = GameplayKeyboardControls.Create();
            spawnWavesInput = GameplayKeyboardControls.Create();
            gameOverSystem = new GameOverSystem(systemManager, SetCurrentScreen);
            Pathfinder.SolvePaths();
            SavedStatePersistence.LoadScores();
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
            ResourceManager.RegisterTexture("Textures/ballista-1", "bombTower");
            ResourceManager.RegisterTexture("Textures/mage", "guidedTower");
            ResourceManager.RegisterTexture("Textures/archer-tower0", "regularTower");
            ResourceManager.RegisterTexture("Textures/cloudtower1", "cloudtower1");
            ResourceManager.RegisterTexture("Textures/goblin", "goblin");
            ResourceManager.RegisterTexture("Textures/orc", "orc");
            ResourceManager.RegisterTexture("Textures/enemy-death-particle", "enemy-death-particle");
            ResourceManager.RegisterTexture("Textures/bomb-particle-trail", "bomb-particle-trail");
            ResourceManager.RegisterTexture("Textures/missile-explosion-particle", "missile-explosion-particle");
            ResourceManager.RegisterTexture("Textures/bomb-particle-explosion", "bomb-particle-explosion");
            ResourceManager.RegisterTexture("Textures/Level0GuidedMissile", "guidedTowerBase");
            ResourceManager.RegisterTexture("Textures/ballista-arrow", "arrow");
            ResourceManager.RegisterTexture("Textures/fire-particle", "fire-particle");
            ResourceManager.RegisterTexture("Textures/magebolt", "magebolt");
            ResourceManager.RegisterTexture("Textures/wyvern", "wyvern");

            ResourceManager.RegisterSong("Hellish-Revenge", "Music/Hellish-Revenge");

            ResourceManager.RegisterSoundEffect("sell", "SoundEffects/sell");
            ResourceManager.RegisterSoundEffect("death", "SoundEffects/death");
            ResourceManager.RegisterSoundEffect("build", "SoundEffects/build");

            camera = CameraPrefab.Create();
            camera.GetComponent<Transform>().position = Vector2.Zero;
            renderer = new Renderer(systemManager, m_window.ClientBounds.Height, camera, new Vector2(m_window.ClientBounds.Width, m_window.ClientBounds.Height));

            renderer.debugMode = false;

            fontRenderer = new FontRenderer(systemManager, m_window.ClientBounds.Height, new Vector2(m_window.ClientBounds.Width, m_window.ClientBounds.Height), camera);
            particleRenderer = new ParticleRenderer(systemManager, m_window.ClientBounds.Height, camera, new Vector2(m_window.ClientBounds.Width, m_window.ClientBounds.Height));

            systemManager.Add(camera);
        }

        public override void OnScreenDefocus()
        {
        }

        public override void OnScreenFocus()
        {
            currentScreen = ScreenEnum.Game;
            screenName = ScreenEnum.Game;
            InputPersistence.LoadSavedKeyboard(ref keyboardInput);
            InputPersistence.LoadSavedKeyboard(ref spawnWavesInput);

            AudioManager.PlaySong("Hellish-Revenge");

            AudioManager.SetVolume(0.5f);
        }

        public override void SetupGameObjects()
        {


            systemManager.Add(BackgroundPrefab.Create());



            systemManager.Add(PlacementCursor.Create(systemManager, camera, controlLoaderSystem, keyboardInput, SetCurrentScreen));


            systemManager.Add(InfoBackground.Create(new Vector2(0, 50)));
            systemManager.Add(PointsPrefab.CreatePointsPrefab());

            systemManager.Add(InfoBackground.Create(new Vector2(1000, 50)));
            systemManager.Add(LivesPrefab.Create());
            systemManager.Add(InfoBackground.Create(new Vector2(1000, 950)));
            systemManager.Add(CurrentLevelPrefab.Create());
            systemManager.Add(InfoBackground.Create(new Vector2(0, 950)));
            systemManager.Add(CreepsKilledPrefab.Create());


            systemManager.Add(WaveController.Create(systemManager, spawnWavesInput, camera.GetComponent<Transform>()));

            systemManager.Add(DestinationColliders.Create(Pathfinder.leftEntrance * Pathfinder.conversionFactor, PathGoal.Left));
            systemManager.Add(DestinationColliders.Create(Pathfinder.rightEntrance * Pathfinder.conversionFactor, PathGoal.Right));
            systemManager.Add(DestinationColliders.Create(Pathfinder.topEntrance * Pathfinder.conversionFactor, PathGoal.Up));
            systemManager.Add(DestinationColliders.Create(Pathfinder.bottomEntrance * Pathfinder.conversionFactor, PathGoal.Down));

            

            Pathfinder.CheckPathsFunc.Invoke();
        }
    }
}

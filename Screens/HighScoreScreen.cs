using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CrowEngineBase;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense
{
    public class HighScoreScreen : Screen
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
        private List<TowerDefenseHighScores> highScores;

        List<GameObject> menuItems = new List<GameObject>();

        private List<TowerDefenseHighScores> scores;

        private bool layoutLoaded = false;


        public HighScoreScreen(ScreenEnum screen) : base(screen)
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

            scores = new List<TowerDefenseHighScores>();
            highScores = new List<TowerDefenseHighScores>();

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
            renderSystem.debugMode = true;
            particleRenderer = new ParticleRenderer(systemManager, m_window.ClientBounds.Height, camera, new Vector2(m_window.ClientBounds.Width, m_window.ClientBounds.Height));
            lightRenderer = new LightRenderer(systemManager, m_window.ClientBounds.Height, camera, new Vector2(m_window.ClientBounds.Width, m_window.ClientBounds.Height), m_graphicsDevice);
            lightRenderer.globalLightLevel = 0f;
            fontRenderer = new FontRenderer(systemManager, m_window.ClientBounds.Height, new Vector2(m_window.ClientBounds.Width, m_window.ClientBounds.Height), camera);
            renderTarget = new RenderTarget2D(m_graphicsDevice, m_graphicsDevice.PresentationParameters.BackBufferWidth, m_graphicsDevice.PresentationParameters.BackBufferHeight, false, m_graphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
        }

        public override void OnScreenDefocus()
        {
            foreach (GameObject menuItem in menuItems)
            {
                systemManager.Remove(menuItem.id);
            }

            menuItems.Clear();

        }

        public override void OnScreenFocus()
        {
            currentScreen = ScreenEnum.HighScore;
            screenName = ScreenEnum.HighScore;
            highScores.Clear();
            SavedStatePersistence.LoadScoresIntoDictionary(ref highScores);

            Debug.WriteLine("Default Screen was loaded");
            Vector2 currentPos = new Vector2(500, 200);

            if (highScores.Count == 0)
            {
                GameObject menuItem = HighScoreItem.Create(currentPos, "No Available Scores", new Vector2(100, 50));
                systemManager.Add(menuItem);
                menuItems.Add(menuItem);
            }
            List<TowerDefenseHighScores> sortedHighScores = highScores.OrderBy(o => o.creepsKilled).ToList();
            sortedHighScores.Reverse();


            for (int i = 0, j = 0; i < sortedHighScores.Count; i++)
            {
                GameObject menuItem = HighScoreItem.Create(currentPos, "Creeps Killed: " + sortedHighScores[i].creepsKilled + " Levels Beaten: " + sortedHighScores[i].levelsCompleted, new Vector2(100, 50));
                systemManager.Add(menuItem);
                //GameObject menuItem1 = HighScoreItem.Create(new Vector2(currentPos.X, currentPos.Y + 100), "Levels Beaten: " + sortedHighScores[i].levelsCompleted, new Vector2(100, 50));
                //systemManager.Add(menuItem1);
                GameObject menuItem2 = HighScoreItem.Create(new Vector2(currentPos.X, currentPos.Y + 100), "Waves Complete: " + sortedHighScores[i].wavesCompleted + " Total Tower Value: " + sortedHighScores[i].totalTowerValue, new Vector2(100, 50));
                systemManager.Add(menuItem2);
                //GameObject menuItem3 = HighScoreItem.Create(new Vector2(currentPos.X, currentPos.Y + 300), "Total Tower Value: " + sortedHighScores[i].totalTowerValue, new Vector2(100, 50));
                //systemManager.Add(menuItem3);
                currentPos = new Vector2(currentPos.X, currentPos.Y + 250);
                menuItems.Add(menuItem);
                j++;
                if (j >= 3)
                { break; }
            }



        }

        public override void SetupGameObjects()
        {
            systemManager.Add(LargeAltScreenBackground.Create());
            systemManager.Add(CreditsKeyboard.CreateCreditsKeyboard(SetCurrentScreen));
            systemManager.Add(HighScoresTitle.Create());



        }
    }
}
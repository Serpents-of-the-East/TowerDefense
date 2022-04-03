﻿using System;

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

        }

        public override void Draw(GameTime gameTime)
        {
            m_spriteBatch.Begin();
            renderer.Draw(gameTime, m_spriteBatch);
            m_spriteBatch.End();
        }

        public override void LoadContent()
        {
            ResourceManager.RegisterTexture("Textures/empty", "empty");
            ResourceManager.RegisterTexture("circle", "bombTower");
            ResourceManager.RegisterTexture("fire", "guidedTower");
            ResourceManager.RegisterTexture("crow", "regularTower");

            camera = CameraPrefab.Create();
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
            systemManager.Add(BasicEnemy.CreateBasicEnemy(Vector2.One * 500));
            systemManager.Add(PlacementCursor.Create(systemManager, camera));
        }
    }
}

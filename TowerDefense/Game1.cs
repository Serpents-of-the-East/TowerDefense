using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

using CrowEngineBase;

namespace CrowEngine
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Renderer renderSystem;
        private PhysicsEngine physicsEngine;
        private ParticleRenderer particleRenderer;
        private ParticleSystem particleSystem;
        private InputSystem inputSystem;
        private ScriptSystem scriptSystem;

        private GameObject camera;
        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            ResourceManager.manager = Content;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

/*            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();*/

            physicsEngine = new PhysicsEngine();
            inputSystem = new InputSystem();
            scriptSystem = new ScriptSystem();
            camera = new GameObject();
            camera.Add(new Transform(Vector2.Zero, 0, Vector2.One));
            particleSystem = new ParticleSystem();
            

            SystemManager.Add(camera);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D crow = Content.Load<Texture2D>("crow");

            renderSystem = new Renderer(Window.ClientBounds.Height, camera, new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height));
            particleRenderer = new ParticleRenderer(Window.ClientBounds.Height, camera, new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height));
            

            GameObject player = new GameObject();
            KeyboardInput keyboardInput = new KeyboardInput();
            keyboardInput.actionKeyPairs.Add("MoveUp", Keys.W);
            keyboardInput.actionKeyPairs.Add("MoveDown", Keys.S);
            keyboardInput.actionKeyPairs.Add("MoveLeft", Keys.A);
            keyboardInput.actionKeyPairs.Add("MoveRight", Keys.D);

            Particle particleGroup = new Particle(crow);
            particleGroup.rate = TimeSpan.FromMilliseconds(20);
            particleGroup.maxSpeed = 100;
            particleGroup.minSpeed = 60;
            particleGroup.minScale = 1;
            particleGroup.maxScale = 4;
            particleGroup.rotationSpeed = 1;
            particleGroup.renderDepth = 0;
            particleGroup.maxLifeTime = TimeSpan.FromSeconds(1);
            particleGroup.emissionArea = Vector2.One * 5;

            player.Add(particleGroup);
            player.Add(keyboardInput);
            player.Add(new BasicTestScript(player));
            player.Add(new Transform(Vector2.One * 500, 0, Vector2.One * 2));
            player.Add(new Sprite(crow, Color.White, 1));
            player.Add(new Rigidbody());
            player.Add(new CircleCollider(10));

            SystemManager.Add(player);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            SystemManager.Update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            renderSystem.Draw(gameTime, _spriteBatch);
            particleRenderer.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);
        }
    }
}

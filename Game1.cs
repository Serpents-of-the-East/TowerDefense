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
        private LightRenderer lightRenderer;
        private SystemManager systemManager;

        private GameObject camera;


        private RenderTarget2D renderTarget;
        private RenderTarget2D lightRenderTarget;

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

            systemManager = new SystemManager();

            physicsEngine = new PhysicsEngine(systemManager);
            inputSystem = new InputSystem(systemManager);
            scriptSystem = new ScriptSystem(systemManager);
            camera = new GameObject();
            camera.Add(new Transform(Vector2.Zero, 0, Vector2.One));
            particleSystem = new ParticleSystem(systemManager);



            systemManager.Add(camera);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D crow = Content.Load<Texture2D>("crow");
            Texture2D fire = Content.Load<Texture2D>("fire");

            renderSystem = new Renderer(systemManager, Window.ClientBounds.Height, camera, new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height));
            particleRenderer = new ParticleRenderer(systemManager, Window.ClientBounds.Height, camera, new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height));
            lightRenderer = new LightRenderer(systemManager, Window.ClientBounds.Height, camera, new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height), GraphicsDevice);
            lightRenderer.globalLightLevel = 0f;

            GameObject player = new GameObject();
            KeyboardInput keyboardInput = new KeyboardInput();
            keyboardInput.actionKeyPairs.Add("MoveUp", Keys.W);
            keyboardInput.actionKeyPairs.Add("MoveDown", Keys.S);
            keyboardInput.actionKeyPairs.Add("MoveLeft", Keys.A);
            keyboardInput.actionKeyPairs.Add("MoveRight", Keys.D);

            Particle particleGroup = new Particle(fire);
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
            player.Add(new Sprite(crow, Color.White, 0));
            player.Add(new Rigidbody());
            player.Add(new CircleCollider(10));
            player.Add(new Light(Color.White, 1f, 100));

            systemManager.Add(player);

            renderTarget = new RenderTarget2D(GraphicsDevice, GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight, false, GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
            lightRenderTarget = new RenderTarget2D(GraphicsDevice, GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight, false, GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            systemManager.Update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GraphicsDevice.SetRenderTarget(renderTarget);

            _spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState:SamplerState.PointClamp);

            renderSystem.Draw(gameTime, _spriteBatch);
            particleRenderer.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            lightRenderer.Draw(gameTime, lightRenderTarget);

            GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            _spriteBatch.Draw(renderTarget, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);

            _spriteBatch.End();

            _spriteBatch.Begin(SpriteSortMode.Immediate, LightRenderer.multiplyBlend);

            _spriteBatch.Draw(lightRenderTarget, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

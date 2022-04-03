using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TowerDefense;

using CrowEngineBase;

namespace CrowEngine
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Dictionary<ScreenEnum, Screen> screens;
        private Screen currentScreen;
        private ScreenEnum nextScreen;
        private bool newScreenFocused;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            ResourceManager.manager = Content;
            screens = new Dictionary<ScreenEnum, Screen>();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            /*            _graphics.PreferredBackBufferWidth = 1920;
                        _graphics.PreferredBackBufferHeight = 1080;
                        _graphics.ApplyChanges();*/

            screens.Add(ScreenEnum.Default, new DefaultScreen(ScreenEnum.Default));
            screens.Add(ScreenEnum.MainMenu, new MainMenuScreen(ScreenEnum.MainMenu));
            screens.Add(ScreenEnum.CameraTest, new CameraTestScreen(ScreenEnum.CameraTest));
<<<<<<< HEAD
            screens.Add(ScreenEnum.PauseScreen, new PauseMenu(ScreenEnum.PauseScreen));
=======
>>>>>>> 3c7074b5cf6adb6efdbb0cd8de9ddadd9d7c9d4d
            currentScreen = screens[ScreenEnum.MainMenu];


            ResourceManager.RegisterFont("Fonts/DefaultFont", "default");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (ScreenEnum screen in screens.Keys)
            {
                screens[screen].Initialize(GraphicsDevice, _graphics, Window);
            }

            foreach (ScreenEnum screen in screens.Keys)
            {
                screens[screen].LoadContent();
                screens[screen].SetupGameObjects();
            }

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (newScreenFocused)
            {
                currentScreen.OnScreenFocus();
                newScreenFocused = false;
            }

            nextScreen = currentScreen.Update(gameTime);

            if (screens[nextScreen] != currentScreen)
            {
                currentScreen.OnScreenDefocus();
                newScreenFocused = true;
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            currentScreen.Draw(gameTime);

            currentScreen = screens[nextScreen];

            base.Draw(gameTime);
        }
    }
}

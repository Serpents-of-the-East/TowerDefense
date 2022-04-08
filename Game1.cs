using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TowerDefense;

using System.Diagnostics;

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

            InputPersistence.LoadControls();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            screens.Add(ScreenEnum.Default, new DefaultScreen(ScreenEnum.Default));
            screens.Add(ScreenEnum.MainMenu, new MainMenuScreen(ScreenEnum.MainMenu));
            screens.Add(ScreenEnum.Controls, new ControlScreen(ScreenEnum.Controls));
            screens.Add(ScreenEnum.Credits, new CreditScreen(ScreenEnum.Credits));
            screens.Add(ScreenEnum.CameraTest, new CameraTestScreen(ScreenEnum.CameraTest));
            screens.Add(ScreenEnum.PauseScreen, new PauseMenu(ScreenEnum.PauseScreen));
            screens.Add(ScreenEnum.Quit, new Quit(ScreenEnum.Quit));
            screens.Add(ScreenEnum.Game, new GameplayScreen(ScreenEnum.Game));
            currentScreen = screens[ScreenEnum.MainMenu];


            ResourceManager.RegisterFont("Fonts/DefaultFont", "default");
            PointsManager.AddPlayerPoints(1000);

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || nextScreen == ScreenEnum.Quit)
                Exit();

            if (newScreenFocused)
            {
                currentScreen.OnScreenFocus();
                newScreenFocused = false;
            }

            nextScreen = currentScreen.Update(gameTime);


            if (nextScreen != ScreenEnum.Quit && screens[nextScreen] != currentScreen)
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

using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CrowEngineBase
{
    public enum ScreenEnum
    {
        Default,
        Game,
        MainMenu,
        Controls,
        Credits,
        PauseScreen,
        Quit,
        CameraTest,
    }

    /// <summary>
    /// A representative of a screen. Add the names of your screens to this, in the enum. When you want to add a gameObject to this screen, you should call systemManager.Add(gameObject).
    /// </summary>
    public abstract class Screen
    {
        protected SystemManager systemManager;

        protected ScreenEnum screenName;

        protected ScreenEnum currentScreen;

        protected GraphicsDeviceManager m_graphics;
        protected SpriteBatch m_spriteBatch;

        protected GraphicsDevice m_graphicsDevice;

        protected GameWindow m_window;

        public Screen(ScreenEnum screenEnum)
        {
            this.currentScreen = screenEnum;
            this.screenName = screenEnum;
        }

        /// <summary>
        /// This MUST be invoked with a base() call.
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <param name="graphics"></param>
        /// <param name="window"></param>
        /// <param name="screen"></param>
        public virtual void Initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, GameWindow window)
        {
            this.systemManager = new SystemManager();
            this.m_graphics = graphics;
            m_spriteBatch = new SpriteBatch(graphicsDevice);
            m_window = window;
            m_graphicsDevice = graphicsDevice;
        }

        public abstract void LoadContent();

        /// <summary>
        /// Updates all registered systems. You shouldn't need to modify this
        /// </summary>
        /// <param name="gameTime"></param>
        public ScreenEnum Update(GameTime gameTime)
        {
            systemManager.Update(gameTime);

            return currentScreen;
        }

        public abstract void Draw(GameTime gameTime);

        /// <summary>
        /// Function that is called the first frame this screen is switched to. Useful for loading and such
        /// </summary>
        public abstract void OnScreenFocus();

        /// <summary>
        /// Function that is called on the last frame this screen is switched to.
        /// </summary>
        public abstract void OnScreenDefocus();


        /// <summary>
        /// Sets up all entities for the given screen. This is called AFTER the onLoad
        /// </summary>
        public abstract void SetupGameObjects();



        protected void SetCurrentScreen(ScreenEnum screenEnum)
        {
            currentScreen = screenEnum;
        }


        public delegate void SetCurrentScreenDelegate(ScreenEnum screenEnum);
    }
}

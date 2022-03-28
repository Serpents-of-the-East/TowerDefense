using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Graphics;

namespace CrowEngineBase
{
    /// <summary>
    /// Light renderer is different from the other classes, as it should have it's own spritebatch, since the effects are handled specially
    /// http://rbwhitaker.wikidot.com/render-to-texture followed this to learn how to use renderTargets
    /// https://gist.github.com/Jjagg/db34a25897e20dbdb0f16cb5bcb75493 for the custom blend state
    /// https://community.monogame.net/t/trying-to-use-a-multiplying-blendstate-to-get-results-like-image-editors/14248/2 for actually getting multiply to work
    /// </summary>
    class LightRenderer : System
    {
        private Texture2D blackBackground;
        private Texture2D lightTexture;
        private Texture2D whiteBackground;
        private SpriteBatch spriteBatch;
        private GraphicsDevice graphicsDevice;

        private float m_scalingRatio;
        private Vector2 m_centerOfScreen;

        private float lightScaleFactor;

        public float globalLightLevel { get; set; }

        public static BlendState multiplyBlend = new BlendState
        {
            ColorBlendFunction = BlendFunction.Add,
            ColorSourceBlend = Blend.DestinationColor,
            ColorDestinationBlend = Blend.Zero,
        };

        public LightRenderer(SystemManager systemManager, float clientBoundsHeight, GameObject camera, Vector2 screenSize, GraphicsDevice graphicsDevice) : base(systemManager, typeof(Transform), typeof(Light))
        {
            systemManager.UpdateSystem -= Update;
            m_scalingRatio = clientBoundsHeight / PhysicsEngine.PHYSICS_DIMENSION_HEIGHT;
            ResourceManager.RegisterTexture("light", "light");
            ResourceManager.RegisterTexture("black", "black");
            ResourceManager.RegisterTexture("white", "white");

            blackBackground = ResourceManager.GetTexture("black");
            lightTexture = ResourceManager.GetTexture("light");
            whiteBackground = ResourceManager.GetTexture("white");
            this.spriteBatch = new SpriteBatch(graphicsDevice);
            this.graphicsDevice = graphicsDevice;
            this.m_centerOfScreen = screenSize / 2;


            lightScaleFactor = 2f / lightTexture.Width;
        }
        protected override void Update(GameTime gameTime)
        {
            
        }

        public void Draw(GameTime gameTime, RenderTarget2D renderTarget)
        {

            graphicsDevice.SetRenderTarget(renderTarget);
            graphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };

            spriteBatch.Begin(blendState:BlendState.Additive);

            spriteBatch.Draw(blackBackground, Vector2.One * 500, null, Color.White, 0, Vector2.Zero, 40, SpriteEffects.None, 1); // start with background
            spriteBatch.Draw(whiteBackground, new Vector2(graphicsDevice.PresentationParameters.BackBufferWidth, graphicsDevice.PresentationParameters.BackBufferHeight) / 2f, null, new Color(Color.White, globalLightLevel), 0, new Vector2(whiteBackground.Width, whiteBackground.Height) / 2f, 40, SpriteEffects.None, 1);

            foreach(uint id in m_gameObjects.Keys)
            {
                Light light = m_gameObjects[id].GetComponent<Light>();

                Vector2 distanceFromCenter = m_gameObjects[id].GetComponent<Transform>().position - new Vector2(PhysicsEngine.PHYSICS_DIMENSION_WIDTH, PhysicsEngine.PHYSICS_DIMENSION_HEIGHT) / 2f;
                Vector2 renderDistanceFromCenter = distanceFromCenter * m_scalingRatio;
                Vector2 trueRenderPosition = renderDistanceFromCenter + m_centerOfScreen;

                spriteBatch.Draw(lightTexture, trueRenderPosition, null, new Color(light.color, light.intensity), 0, new Vector2(lightTexture.Width, lightTexture.Height) / 2f, lightScaleFactor * light.range * m_scalingRatio, SpriteEffects.None, 1);
            }

            spriteBatch.End();
        }
    }
}

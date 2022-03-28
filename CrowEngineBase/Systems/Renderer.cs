using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrowEngineBase
{
    public class Renderer : System
    {
        // Used for moving the camera
        private GameObject m_camera;

        private float m_scalingRatio;
        private Vector2 m_centerOfScreen;

        public bool debugMode = true;

        /// <summary>
        /// Renderer system. It's update method is NOT part of the normal system update
        /// </summary>
        /// <param name="spriteBatch">Spritebatch that will be used</param>
        /// <param name="clientBoundsHeight">The height of the client's window, which can be found with GameWindow.ClientBounds.Height</param>
        /// <param name="camera">The camera game object, which really just has a transform position currently. Future will have scale</param>
        public Renderer(SystemManager systemManager, float clientBoundsHeight, GameObject camera, Vector2 screenSize) : base(systemManager, typeof(Transform), typeof(RenderedComponent))
        {
            m_scalingRatio = clientBoundsHeight / PhysicsEngine.PHYSICS_DIMENSION_HEIGHT;
            this.m_camera = camera;
            this.m_centerOfScreen = screenSize / 2;
            systemManager.UpdateSystem -= Update; // remove the automatically added update

            ResourceManager.RegisterTexture("circle", "circle");
            ResourceManager.RegisterTexture("box", "box");
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (uint id in m_gameObjects.Keys)
            {

                Vector2 distanceFromCenter = m_gameObjects[id].GetComponent<Transform>().position - new Vector2(PhysicsEngine.PHYSICS_DIMENSION_WIDTH, PhysicsEngine.PHYSICS_DIMENSION_HEIGHT) / 2f;
                Vector2 renderDistanceFromCenter = distanceFromCenter * m_scalingRatio;
                Vector2 trueRenderPosition = renderDistanceFromCenter + m_centerOfScreen;

                if (m_gameObjects[id].ContainsComponent<Sprite>())
                {
                    Sprite sprite = m_gameObjects[id].GetComponent<Sprite>();
                    spriteBatch.Draw(sprite.sprite, trueRenderPosition, null,
                        sprite.color, m_gameObjects[id].GetComponent<Transform>().rotation,
                        sprite.center, m_gameObjects[id].GetComponent<Transform>().scale * m_scalingRatio,
                        SpriteEffects.None, sprite.renderDepth);
                }
                else if (m_gameObjects[id].ContainsComponent<AnimatedSprite>())
                {
                    AnimatedSprite animatedSprite = m_gameObjects[id].GetComponent<AnimatedSprite>();
                    Transform transform = m_gameObjects[id].GetComponent<Transform>();
                    int currentX = (int)(animatedSprite.currentFrame * animatedSprite.singleFrameSize.X);
                    spriteBatch.Draw(animatedSprite.spritesheet, trueRenderPosition, new Rectangle(currentX, 0, (int)animatedSprite.singleFrameSize.X, (int)animatedSprite.singleFrameSize.Y), Color.White, transform.rotation, animatedSprite.singleFrameSize / 2, transform.scale * m_scalingRatio, SpriteEffects.None, animatedSprite.layerDepth);
                }

                if (debugMode)
                {
                    CircleCollider circleCollider = m_gameObjects[id].GetComponent<CircleCollider>();
                    RectangleCollider rectangleCollider = m_gameObjects[id].GetComponent<RectangleCollider>();

                    if (circleCollider != null)
                    {
                        Texture2D circleTexture = ResourceManager.GetTexture("circle");
                        spriteBatch.Draw(circleTexture, trueRenderPosition, null,
                        Color.Green, m_gameObjects[id].GetComponent<Transform>().rotation,
                        new Vector2(circleTexture.Width, circleTexture.Height) / 2f, 2f / circleTexture.Width * circleCollider.radius * m_scalingRatio,
                        SpriteEffects.None, 0);
                    }
                    if (rectangleCollider != null)
                    {
                        Texture2D boxTexture = ResourceManager.GetTexture("box");
                        spriteBatch.Draw(boxTexture, trueRenderPosition, null,
                        Color.Green, m_gameObjects[id].GetComponent<Transform>().rotation,
                        new Vector2(boxTexture.Width, boxTexture.Height) / 2f, new Vector2(2f / boxTexture.Width * (rectangleCollider.size.X / 2), 2f / boxTexture.Height * (rectangleCollider.size.Y / 2)) * m_scalingRatio,
                        SpriteEffects.None, 0);
                    }
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
        }
    }
}

using System;
using Microsoft.Xna.Framework;

namespace CrowEngineBase
{
    public class AnimationSystem : System
    {
        public AnimationSystem() : base(typeof(AnimatedSprite))
        {
        }

        protected override void Update(GameTime gameTime)
        {
            foreach(uint id in m_gameObjects.Keys)
            {
                AnimatedSprite animatedSprite = m_gameObjects[id].GetComponent<AnimatedSprite>();
                animatedSprite.currentTime += gameTime.ElapsedGameTime;

                while (animatedSprite.currentTime.Milliseconds > animatedSprite.frameTiming[animatedSprite.currentFrame])
                {
                    animatedSprite.currentTime.Subtract(TimeSpan.FromMilliseconds(animatedSprite.frameTiming[animatedSprite.currentFrame]));
                    animatedSprite.currentFrame += 1;
                    animatedSprite.currentFrame %= animatedSprite.frameTiming.Length;
                }
            }
        }
    }
}

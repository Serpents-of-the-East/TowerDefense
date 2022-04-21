using System;
using System.Collections.Generic;
using System.Text;

using CrowEngineBase;

using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class WaveController
    {
        static WaveController()
        {
            ResourceManager.RegisterTexture("Textures/wavearrow", "wavearrow");
        }
        public static GameObject Create(SystemManager systemManager, KeyboardInput gameplayKeyboard, Transform camera)
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new Transform(new Vector2(50, 500), 0, Vector2.One));
            gameObject.Add(new Sprite(ResourceManager.GetTexture("wavearrow"), Color.White, HUDelement: true));
            gameObject.Add(new WaveManager(gameObject, systemManager, camera));
            gameObject.Add(gameplayKeyboard);

            return gameObject;
        }
    }
}

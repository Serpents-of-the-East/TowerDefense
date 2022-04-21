using System;

using CrowEngineBase;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace TowerDefense
{
    public static class RebindMainInput
    {
        private static Texture2D texture;

        static RebindMainInput()
        {
            texture = TextureCreation.CreateCircleWithRadius(10);
        }

        public static GameObject Create(GameObject[] menuItems, KeyboardInput rebindInput, Vector2 position, Transform cameraTransform, Screen.SetCurrentScreenDelegate setCurrentScreenDelegate)
        {
            GameObject gameObject = new GameObject();

            KeyboardInput keyboardInput = new KeyboardInput();
            keyboardInput.actionKeyPairs.Add("MoveUp", Keys.Up);
            keyboardInput.actionKeyPairs.Add("MoveDown", Keys.Down);
            keyboardInput.actionKeyPairs.Add("Select", Keys.Enter);
            keyboardInput.actionKeyPairs.Add("Cancel", Keys.Escape);

            gameObject.Add(keyboardInput);

            MouseInput mouseInput = new MouseInput();
            mouseInput.actionButtonPairs.Add("Select", MouseButton.LeftButton);

            gameObject.Add(mouseInput);

            gameObject.Add(new CircleCollider(10));
            gameObject.Add(new Rigidbody());

            gameObject.Add(new RenderedComponent());

            gameObject.Add(new Text("", ResourceManager.GetFont("default"), Color.White, Color.Black, true));

            gameObject.Add(new RebindScreenNavigation(gameObject, menuItems, rebindInput, setCurrentScreenDelegate, cameraTransform));
            gameObject.Add(new Transform(position, 0, Vector2.One));

            return gameObject;
        }
    }
}

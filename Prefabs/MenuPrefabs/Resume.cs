using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class Resume
    {
        public static GameObject CreateResumeItem(float windowWidth)
        {
            GameObject gameObject = new GameObject();
            gameObject.Add(new Rigidbody());
            gameObject.Add(new RectangleCollider(new Vector2(500 + ResourceManager.GetFont("default").MeasureString("Resume").X, 100)));
            gameObject.Add(new Transform(new Vector2(windowWidth / 2 + ResourceManager.GetFont("default").MeasureString("Resume").X/2, 500), 0, Vector2.One));
            gameObject.Add(new Text("Resume", ResourceManager.GetFont("default"), Color.White, Color.Black, true, 0));
            gameObject.Add(new RenderedComponent());
            gameObject.Add(new MenuItem(ScreenEnum.MainMenu));

            return gameObject;
        }
    }
}

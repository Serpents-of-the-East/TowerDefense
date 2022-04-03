using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{ 
    public static class PauseTitle
    {
        public static GameObject CreatePauseTitle(float windowWidth)
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new Transform(new Vector2(windowWidth / 2 + ResourceManager.GetFont("default").MeasureString("Game Paused").X / 2, 100), 0, Vector2.One));
            gameObject.Add(new Text("Game Paused", ResourceManager.GetFont("default"), Color.White, Color.Black, true, 0));


            return gameObject;



        }
        
    }
}

using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{ 
    public static class PauseTitle
    {
        public static GameObject CreatePauseTitle()
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new Transform(new Vector2(500, 100), 0, Vector2.One));
            gameObject.Add(new Text("Game Paused", ResourceManager.GetFont("default"), Color.White, Color.Black, true, 0));


            return gameObject;



        }
        
    }
}

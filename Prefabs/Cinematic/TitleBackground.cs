using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

using CrowEngineBase;

namespace TowerDefense
{
    public static class TitleBackground
    {
        public static GameObject Create()
        {
            GameObject gameObject = BackgroundPrefab.Create();

            gameObject.Add(new TitleScreenBackgroundMovement(gameObject));
            gameObject.Add(new Rigidbody());
            gameObject.Add(new CircleCollider(0));


            return gameObject;
        }
    }
}

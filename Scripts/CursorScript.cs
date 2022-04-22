using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public class CursorScript : ScriptBase
    {
        private MouseInput mouseInput;
        private Transform transform;
        public GameObject currentlyCollidingWith;
        private Screen.SetCurrentScreenDelegate setCurrentScreenDelegate;

        public CursorScript(GameObject gameObject, Screen.SetCurrentScreenDelegate setCurrentScreenDelegate) : base(gameObject)
        {
            this.setCurrentScreenDelegate = setCurrentScreenDelegate;
        }

        public override void Start()
        {
            base.Start();
            mouseInput = gameObject.GetComponent<MouseInput>();
            transform = gameObject.GetComponent<Transform>();
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            gameObject.GetComponent<Transform>().position = gameObject.GetComponent<MouseInput>().GetCurrentPhysicsPosition();


        }

        public override void OnCollision(GameObject other)
        {
            if (currentlyCollidingWith == null && other.ContainsComponent<Text>())
            {
                other.GetComponent<Text>().color = Color.Red;
                currentlyCollidingWith = other;
                
            }

        }
        public override void OnCollisionEnd(GameObject other)
        {
            base.OnCollisionEnd(other);
            currentlyCollidingWith = null;
            other.GetComponent<Text>().color = Color.White;
        }

        public void OnSelect(float buttonState)
        {
            if (buttonState > 0.5f && currentlyCollidingWith != null)
            {
                this.setCurrentScreenDelegate.Invoke(currentlyCollidingWith.GetComponent<MenuItem>().screenEnum);
            }
        }

    }
}

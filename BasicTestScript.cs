using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

using CrowEngineBase;

namespace CrowEngine
{
    public class BasicTestScript : ScriptBase
    {
        private Transform transform;
        private Rigidbody rb;

        private float playerSpeed = 200f;

        public BasicTestScript(GameObject owner) : base(owner)
        {
        }

        public override void Start()
        {
            transform = gameObject.GetComponent<Transform>();
            rb = gameObject.GetComponent<Rigidbody>();
            Debug.WriteLine($"Player is at position {transform.position}");
        }

        public void OnMoveUp(float direction)
        {
            rb.velocity = new Vector2(rb.velocity.X, -direction * playerSpeed);
        }
        public void OnMoveDown(float direction)
        {
            rb.velocity = new Vector2(rb.velocity.X, direction * playerSpeed);
        }

        public void OnMoveLeft(float direction)
        {
            rb.velocity = new Vector2(-direction * playerSpeed, rb.velocity.Y);
        }

        public void OnMoveRight(float direction)
        {
            rb.velocity = new Vector2(direction * playerSpeed, rb.velocity.Y);
        }
    }
}

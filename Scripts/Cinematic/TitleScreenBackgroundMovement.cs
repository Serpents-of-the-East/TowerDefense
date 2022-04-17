using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

using CrowEngineBase;

using CrowEngineBase.Utilities;

namespace TowerDefense
{
    class TitleScreenBackgroundMovement : ScriptBase
    {

        private float speed = 100;
        private float acceleration = 3;
        Transform transform;
        Rigidbody rb;

        private Vector2 currentTarget;

        private Vector2 leftTarget = new Vector2(1750, 0);
        private Vector2 rightTarget = new Vector2(-1750, 0);
        private Vector2 downTarget = new Vector2(0, 1750);
        private Vector2 upTarget = new Vector2(0, -1750);

        private CurrentDirection currentDirection;

        enum CurrentDirection
        {
            Up,
            Right,
            Down,
            Left
        }

        public TitleScreenBackgroundMovement(GameObject gameObject) : base(gameObject)
        {
        }

        public override void Start()
        {
            transform = gameObject.GetComponent<Transform>();
            rb = gameObject.GetComponent<Rigidbody>();

            currentTarget = rightTarget;
            currentDirection = CurrentDirection.Right;

            Vector2 target = currentTarget - transform.position;
            target.Normalize();

            rb.velocity = target * speed;
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 heading = currentTarget - transform.position;
            heading.Normalize();

            rb.velocity = Vector2.SmoothStep(rb.velocity, heading * speed, acceleration * gameTime.ElapsedGameTime.Milliseconds / 1000f);

            if (Vector2.DistanceSquared(transform.position, currentTarget) < 100)
            {
                switch (currentDirection)
                {
                    case (CurrentDirection.Right):
                        currentDirection = CurrentDirection.Up;
                        currentTarget = upTarget;
                        break;
                    case (CurrentDirection.Up):
                        currentDirection = CurrentDirection.Left;
                        currentTarget = leftTarget;
                        break;
                    case (CurrentDirection.Left):
                        currentDirection = CurrentDirection.Down;
                        currentTarget = downTarget;
                        break;
                    default:
                        currentDirection = CurrentDirection.Right;
                        currentTarget = rightTarget;
                        break;
                }
            }
        }
    }
}

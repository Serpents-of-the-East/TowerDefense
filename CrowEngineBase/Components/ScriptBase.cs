using System;
using System.Reflection;

using CrowEngineBase.General;

using Microsoft.Xna.Framework;

namespace CrowEngineBase
{
    /// <summary>
    /// Script base should ONLY be used for things which are small. Overarching systems should
    /// have their OWN components and systems created. Really, this should be a VERY small system.
    ///
    /// For Input, you should create a method called OnEVENT, where EVENT is the name of the action you registered with
    /// an input handler. For example, if you had an event in the input handler called MoveUp, a function called OnMoveUp
    /// would attempt to be called (you don't necessarily need to write one). For most, it accepts a float, except for the OnMouseMove event, which is automatically called,
    /// and passed a Vector2 (the mouse's new position). All input events are only called when the value changes.
    /// </summary>
    public abstract class ScriptBase : Component
    {
        protected GameObject gameObject;
        public ScriptBase(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }


        /// <summary>
        /// Called the frame this starts existing
        /// </summary>
        public virtual void Start()
        {
        }

        /// <summary>
        /// Called the last frame this is in the system, right before removal
        /// </summary>
        public virtual void Destroyed()
        {
        }

        /// <summary>
        /// Function called every frame that the game object this is attached to is colliding with another object
        /// </summary>
        /// <param name="other"></param>
        public virtual void OnCollision(GameObject other)
        {
        }

        /// <summary>
        /// Called the frame which a game object has stopped colliding with a given object
        /// </summary>
        /// <param name="other"></param>
        public virtual void OnCollisionEnd(GameObject other)
        {
        }

        /// <summary>
        /// Called the frame which a game object first starts colliding with a given object
        /// </summary>
        /// <param name="other"></param>
        public virtual void OnCollisionStart(GameObject other)
        {
        }

        /// <summary>
        /// Overridable update function
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
        }

        /// <summary>
        /// Allows you to try to call a function from a script, useful for the input system and such
        /// Enforces correct types and parameter counts
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="values"></param>
        public void SendMessage(string functionName, params Object[] parameters)
        {

            Type thisType = this.GetType();
            MethodInfo theMethod = thisType.GetMethod(functionName);

            if (theMethod != null)
            {
                var parametersNeeded = theMethod.GetParameters();
                if (parametersNeeded.Length != parameters.Length)
                {
                    throw new Exception($"Error calling the function {functionName}. An incorrect number of parameters were passed");
                }
                for (int i = 0; i < parameters.Length; i++)
                {
                    // Make sure the parameter types are the same
                    if (parameters[i].GetType() != parametersNeeded[i].ParameterType)
                    {
                        throw new Exception($"Error with parameter {i} of function {functionName}. An incorrect type {parameters[i].GetType()} was supplied, but expected {parametersNeeded[i].ParameterType}");
                    }
                }

                theMethod.Invoke(this, parameters);
            }
        }

        
    }
}

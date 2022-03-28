using System;
using Microsoft.Xna.Framework;

namespace CrowEngineBase
{
    public class ScriptSystem : System
    {
        public ScriptSystem(SystemManager systemManager) : base(systemManager, typeof(ScriptBase))
        {
            foreach (uint id in m_gameObjects.Keys)
            {
                m_gameObjects[id].GetComponent<ScriptBase>().Start();
            }
        }

        protected override void Add(GameObject gameObject)
        {
            if (IsInterested(gameObject))
            {
                gameObject.GetComponent<ScriptBase>().Start();
            }
            base.Add(gameObject);
        }

        protected override void Remove(uint id)
        {
            if (m_gameObjects.ContainsKey(id))
            {
                m_gameObjects[id].GetComponent<ScriptBase>().Destroyed();
            }
            base.Remove(id);
        }

        protected override void Update(GameTime gameTime)
        {
            foreach(uint id in m_gameObjects.Keys)
            {
                CallInputScripts(id);
                m_gameObjects[id].GetComponent<ScriptBase>().Update(gameTime);
            }
        }

        private void CallInputScripts(uint id)
        {
            if (m_gameObjects[id].ContainsComponent<KeyboardInput>())
            {
                KeyboardInput keyboardInput = m_gameObjects[id].GetComponent<KeyboardInput>();
                foreach (string action in keyboardInput.actionStatePairs.Keys)
                {
                    if (keyboardInput.actionStatePairs[action] != keyboardInput.previousActionStatePairs[action])
                    {
                        m_gameObjects[id].GetComponent<ScriptBase>().SendMessage($"On{action}", keyboardInput.actionStatePairs[action] ? 1f : 0f);
                    }
                }
            }
            if (m_gameObjects[id].ContainsComponent<ControllerInput>())
            {
                ControllerInput controllerInput = m_gameObjects[id].GetComponent<ControllerInput>();
                foreach (string action in controllerInput.actionStatePairs.Keys)
                {
                    if (controllerInput.actionStatePairs[action] != controllerInput.previousActionStatePairs[action])
                    {
                        m_gameObjects[id].GetComponent<ScriptBase>().SendMessage($"On{action}", controllerInput.actionStatePairs[action]);
                    }
                }
            }
            if (m_gameObjects[id].ContainsComponent<MouseInput>())
            {
                MouseInput mouseInput = m_gameObjects[id].GetComponent<MouseInput>();
                foreach (string action in mouseInput.actionStatePairs.Keys)
                {
                    if (mouseInput.actionStatePairs[action] != mouseInput.previousActionStatePairs[action])
                    {
                        m_gameObjects[id].GetComponent<ScriptBase>().SendMessage($"On{action}", mouseInput.actionStatePairs[action] ? 1f : 0f);
                    }
                }
                if (mouseInput.position != mouseInput.previousPosition)
                {
                    m_gameObjects[id].GetComponent<ScriptBase>().SendMessage($"OnMouseMove", mouseInput.position);
                }
            }
        }
    }
}

using CrowEngineBase;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefense
{
    public class ControlLoaderSystem : CrowEngineBase.System
    {

        public bool controlsLoaded { get; private set; }
        public ControlLoaderSystem(SystemManager systemManager) : base(systemManager, typeof(KeyboardInput))
        {
            controlsLoaded = false;
        }

        protected override void Update(GameTime gameTime)
        {
            if (!InputPersistence.controlsLoaded || controlsLoaded) // if the saved state is not ready, or we have already loaded the controls for this screen
            {
                return;
            }

            foreach (uint id in m_gameObjects.Keys)
            {
                KeyboardInput keyboardInput = m_gameObjects[id].GetComponent<KeyboardInput>();
                InputPersistence.LoadSavedKeyboard(ref keyboardInput);
            }


            controlsLoaded = true;
            systemManager.UpdateSystem -= Update; // remove this system from the update calls once it has finished loading
        }

        public void ReloadControls()
        {
            if (controlsLoaded)
            {
                controlsLoaded = false;
                systemManager.UpdateSystem += Update;
            }
        }
    }
}

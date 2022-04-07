using System;
using CrowEngineBase;

namespace TowerDefense
{
    public class ControlNavigation : ScriptBase
    {
        private Screen.SetCurrentScreenDelegate setCurrentScreenDelegate;


        public ControlNavigation(GameObject gameObject, Screen.SetCurrentScreenDelegate setCurrentScreenDelegate) : base(gameObject)
        {
            this.setCurrentScreenDelegate = setCurrentScreenDelegate;

        }


        public void OnMoveToMainMenu(float buttonState)
        {
            if (buttonState > 0)
            {
                Console.WriteLine("OnMainMenuMove Called");
                this.setCurrentScreenDelegate.Invoke(ScreenEnum.MainMenu);
            }
        }


        public void OnMoveMenuUp()
        {

        }

        public void OnMoveMenuDown()
        {

        }


    }
}

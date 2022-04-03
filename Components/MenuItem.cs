using System;
using CrowEngineBase;

namespace TowerDefense
{
    public class MenuItem : Component
    {
        public ScreenEnum screenEnum;


        public MenuItem(ScreenEnum screenEnum)
        {
            this.screenEnum = screenEnum;
        }
    }
}

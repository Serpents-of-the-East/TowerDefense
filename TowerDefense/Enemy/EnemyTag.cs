using System;
using CrowEngineBase;

namespace TowerDefense
{
    public class EnemyTag : Component
    {
        public EnemyType enemyType { get; set; }

        public EnemyTag(EnemyType enemyType)
        {
            this.enemyType = enemyType;
        }
    }


    public enum EnemyType
    {
        AIR,
        GROUND,
        MIXED,
    }

}

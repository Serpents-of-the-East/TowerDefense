using System;
using System.Collections.Generic;
using System.Text;

using CrowEngineBase;

namespace TowerDefense
{
    public static class TowerColliderPrefab
    {
        public static GameObject Create(GameObject parentTower)
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new CircleCollider(Pathfinder.SIZE_PER_TOWER / 2));

            gameObject.Add(parentTower.GetComponent<Transform>());

            gameObject.Add(new TowerColliderComponent() { parentAttach = parentTower });

            gameObject.Add(new Rigidbody());


            return gameObject;
        }
    }
}

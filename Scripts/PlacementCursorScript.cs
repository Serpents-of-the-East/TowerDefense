using System;

using CrowEngineBase;

using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public class PlacementCursorScript : ScriptBase
    {

        MouseInput mouse;
        Transform transform;

        Sprite sprite; // TODO: This should probably switch to an animated sprite

        private uint currentSelected = 0;
        private uint numberOfTowers = 3;

        private GameObject camera;

        private SystemManager systemManager;



        public PlacementCursorScript(GameObject gameObject, SystemManager systemManager, GameObject camera) : base(gameObject)
        {
            this.systemManager = systemManager;
            this.camera = camera;
        }

        public override void Start()
        {
            mouse = gameObject.GetComponent<MouseInput>();
            transform = gameObject.GetComponent<Transform>();
            sprite = gameObject.GetComponent<Sprite>();
        }

        public void OnMouseMove(Vector2 mousePosition)
        {
            transform.position = Pathfinder.Gridify(mouse.PhysicsPositionCamera(camera.GetComponent<Transform>()));
        }

        public void OnSwitchUpTower(float input)
        {
            if (input > 0.5f)
            {
                currentSelected += 1;
                currentSelected %= numberOfTowers + 1; // To allow for the empty one
            }
            SetTowerTexture();
        }

        public void OnSwitchDownTower(float input)
        {
            if (input > 0.5f)
            {
                if (currentSelected == 0)
                {
                    currentSelected = numberOfTowers;
                }
                else
                {
                    currentSelected -= 1;
                }
            }
            SetTowerTexture();
        }

        public void OnPlaceTower(float input)
        {
            if (input > 0.5f)
            {
                if (Pathfinder.UpdatePaths(transform.position))
                {
                    switch (currentSelected)
                    {
                        case (1):
                            GameObject bombTower = BombTower.Create();
                            bombTower.GetComponent<Transform>().position = transform.position;
                            systemManager.Add(bombTower);
                            break;
                        case (2):
                            GameObject guidedTower = GuidedMissileTower.Create();
                            guidedTower.GetComponent<Transform>().position = transform.position;
                            systemManager.Add(guidedTower);
                            break;
                        case (3):
                            GameObject regularTower = RegularTower.Create();
                            regularTower.GetComponent<Transform>().position = transform.position;
                            systemManager.Add(regularTower);
                            break;
                    }
                }
            }
        }

        private void SetTowerTexture()
        {
            switch (currentSelected)
            {
                case (1):
                    sprite.sprite = ResourceManager.GetTexture("bombTower");
                    transform.scale = Vector2.One / 2;
                    break;
                case (2):
                    sprite.sprite = ResourceManager.GetTexture("guidedTower");
                    transform.scale = Vector2.One * 2;
                    break;
                case (3):
                    sprite.sprite = ResourceManager.GetTexture("regularTower");
                    transform.scale = Vector2.One * 2;
                    break;
                default:
                    sprite.sprite = ResourceManager.GetTexture("empty");
                    break;
            }
            sprite.center = new Vector2(sprite.sprite.Width, sprite.sprite.Height) / 2;

        }
    }
}

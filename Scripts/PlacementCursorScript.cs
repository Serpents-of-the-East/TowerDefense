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
        private GameObject selectedTower;


        private GameObject camera;

        private SystemManager systemManager;


        private ControlLoaderSystem ControlLoaderSystem; // this is just for testing it SHOULD BE REMOVED LATER

        public PlacementCursorScript(GameObject gameObject, SystemManager systemManager, GameObject camera, ControlLoaderSystem controlSystem) : base(gameObject)
        {
            this.systemManager = systemManager;
            this.camera = camera;
            this.ControlLoaderSystem = controlSystem;
        }

        public override void Start()
        {
            mouse = gameObject.GetComponent<MouseInput>();
            transform = gameObject.GetComponent<Transform>();
            sprite = gameObject.GetComponent<Sprite>();
            selectedTower = null;
        }

        public void OnMouseMove(Vector2 mousePosition)
        {
            transform.position = Pathfinder.Gridify(mouse.PhysicsPositionCamera(camera.GetComponent<Transform>()));
        }

        public void OnSellTower(float input)
        {

            if (input > 0f)
            {
                if (selectedTower != null)
                {
                    if (Pathfinder.SellTower(transform.position))
                    {
                        if (selectedTower.ContainsComponent<PointsComponent>())
                        {
                            PointsManager.AddPlayerPoints((int) (selectedTower.GetComponent<PointsComponent>().points * 0.8f));
                            ParticleEmitter.EmitSellParticles(selectedTower.GetComponent<Transform>().position);
                        }
                        systemManager.Remove(selectedTower.id);
                        selectedTower = null;
                    }

                }

            }

        }


        public override void OnCollisionStart(GameObject other)
        {
            if (selectedTower == null && other.ContainsComponent<TowerComponent>())
            {
                selectedTower = other;
            }

        }

        public override void OnCollisionEnd(GameObject other)
        {
            if (selectedTower == other)
            {
                selectedTower = null;
            }
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

        public void OnReloadScreen(float input)
        {
            if (input == 1 && ControlLoaderSystem.controlsLoaded)
            {
                ControlLoaderSystem.ReloadControls();
            }
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

                GameObject newTower = null;

                switch (currentSelected)
                {
                    case (1):
                        newTower = BombTower.Create();
                        break;
                    case (2):
                        newTower = GuidedMissileTower.Create();
                        break;
                    case (3):
                        newTower = RegularTower.Create();
                        break;
                }

                if (newTower != null && PointsManager.GetPlayerPoints() >= newTower.GetComponent<PointsComponent>().points)
                {
                    if (Pathfinder.UpdatePaths(transform.position))
                    {
                        newTower.GetComponent<Transform>().position = transform.position;
                        systemManager.DelayedAdd(newTower);
                        PointsManager.SubtractPlayerPoints(newTower.GetComponent<PointsComponent>().points);
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

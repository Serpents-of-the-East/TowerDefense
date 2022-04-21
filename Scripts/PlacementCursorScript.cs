using System;

using CrowEngineBase;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense
{
    public class PlacementCursorScript : ScriptBase
    {

        MouseInput mouse;
        Transform transform;

        Sprite sprite; // TODO: This should probably switch to an animated sprite
        AnimatedSprite animatedSprite;

        private uint currentSelected = 0;
        private uint numberOfTowers = 3;
        private GameObject selectedTower;
        Screen.SetCurrentScreenDelegate setCurrentScreenDelegate;




        private GameObject camera;

        private SystemManager systemManager;


        private ControlLoaderSystem ControlLoaderSystem; // this is just for testing it SHOULD BE REMOVED LATER

        public PlacementCursorScript(GameObject gameObject, SystemManager systemManager, GameObject camera, ControlLoaderSystem controlSystem, Screen.SetCurrentScreenDelegate setCurrentScreenDelegate) : base(gameObject)
        {
            this.systemManager = systemManager;
            this.camera = camera;
            this.ControlLoaderSystem = controlSystem;
            this.setCurrentScreenDelegate = setCurrentScreenDelegate;
        }

        public override void Start()
        {
            mouse = gameObject.GetComponent<MouseInput>();
            transform = gameObject.GetComponent<Transform>();
            sprite = gameObject.GetComponent<Sprite>();
            animatedSprite = gameObject.GetComponent<AnimatedSprite>();
            selectedTower = null;
        }

        public void OnPause(float buttonState)
        {
            if (buttonState > 0)
            {
                Console.WriteLine("OnPause Called");
                this.setCurrentScreenDelegate.Invoke(ScreenEnum.PauseScreen);
            }
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
                        if (selectedTower.GetComponent<TowerColliderComponent>().parentAttach.ContainsComponent<PointsComponent>())
                        {
                            PointsManager.AddPlayerPoints((int) (selectedTower.GetComponent<TowerColliderComponent>().parentAttach.GetComponent<PointsComponent>().points * 0.8f));
                            ParticleEmitter.EmitSellParticles(selectedTower.GetComponent<Transform>().position); // THIS IS THE ORIGINAL, CHANGE BACK AFTER
                            GameStats.RemoveTowerValue(selectedTower.GetComponent<TowerColliderComponent>().parentAttach.GetComponent<PointsComponent>().points);
                        }
                        systemManager.Remove(selectedTower.GetComponent<TowerColliderComponent>().parentAttach.id);
                        systemManager.Remove(selectedTower.id);
                        selectedTower = null;
                    }

                }

            }

        }




        public override void OnCollision(GameObject other)
        {
            if (selectedTower == null && other.ContainsComponent<TowerColliderComponent>())
            {
                selectedTower = other;
                selectedTower.GetComponent<TowerColliderComponent>().parentAttach.GetComponent<Sprite>().color = Color.Red;
            }
        }

        public override void OnCollisionEnd(GameObject other)
        {
            if (selectedTower == other)
            {
                selectedTower.GetComponent<TowerColliderComponent>().parentAttach.GetComponent<Sprite>().color = Color.White;
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
                        newTower = BombTower.Create(systemManager);
                        break;
                    case (2):
                        newTower = GuidedMissileTower.Create(systemManager);
                        break;
                    case (3):
                        newTower = RegularTower.Create(systemManager);
                        break;
                }

                if (newTower != null && PointsManager.GetPlayerPoints() >= newTower.GetComponent<PointsComponent>().points)
                {
                    if (Pathfinder.UpdatePaths(transform.position))
                    {
                        newTower.GetComponent<Transform>().position = transform.position;
                        systemManager.DelayedAdd(newTower);
                        GameStats.AddTowerValue(newTower.GetComponent<PointsComponent>().points);

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
                    animatedSprite.spritesheet = TextureCreation.CreateCircleWithRadius(BombTower.TowerRadius / 2);
                    transform.scale = Vector2.One * 2;
                    break;
                case (2):
                    sprite.sprite = ResourceManager.GetTexture("guidedTower");
                    animatedSprite.spritesheet = TextureCreation.CreateCircleWithRadius(GuidedMissileTower.TowerRadius / 2);
                    transform.scale = Vector2.One * 2;
                    break;
                case (3):
                    sprite.sprite = ResourceManager.GetTexture("regularTower");
                    animatedSprite.spritesheet = TextureCreation.CreateCircleWithRadius(RegularTower.TowerRadius / 2);
                    transform.scale = Vector2.One * 2;
                    break;
                default:
                    sprite.sprite = ResourceManager.GetTexture("empty");
                    animatedSprite.spritesheet = ResourceManager.GetTexture("empty");
                    break;
            }
            sprite.center = new Vector2(sprite.sprite.Width, sprite.sprite.Height) / 2;
            animatedSprite.singleFrameSize = Vector2.One * animatedSprite.spritesheet.Height;

        }
    }
}

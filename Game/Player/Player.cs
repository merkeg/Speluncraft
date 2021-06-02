// <copyright file="Player.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Player
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using Engine.GameObject;
    using Engine.Renderer;
    using Engine.Renderer.Sprite;
    using Game.Scenes;
    using OpenTK.Windowing.GraphicsLibraryFramework;

    /// <summary>
    /// The thing the Player COntrolls.
    /// </summary>
    public class Player : GameObject, Gun.ILookDirection
    {
        private int jumpcounter;
        private int jumpCounterMax = 1;

        private float accelaration = 18f;
        private float idealBreacking = 25f;
        private float activeBreacking = 20f;
        private float jumpPower = 10f;

        private ISprite spriteWalking;
        private ISprite spriteHurt;
        private ISprite spriteIdle;
        private ISprite spriteJump;
        private ISprite spriteFall;
        private ISprite spriteBack;
        private ISprite spriteGun;

        private int isFaceing;
        private Gun.IGun gun;
        private GameComponents.AnimationScheduler animationScheduler;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="minX">the X-Coordinate of bottom left point, of the Player.</param>
        /// <param name="minY">the Y-Coordinate of bottom left point, of the Player.</param>
        /// <param name="sizeX">Player width.</param>
        /// <param name="sizeY">Player height.</param>
        /// <param name="sprite">Player sprite.</param>
        public Player(float minX, float minY, float sizeX, float sizeY, ISprite sprite)
            : base(minX, minY, sizeX, sizeY, sprite)
        {
            Engine.Component.Physics physics = new Engine.Component.Physics();
            physics.SetIsAffectedByGravity(true);
            physics.SetGravityMultiplier(2);
            this.AddComponent(physics);
            this.AddComponent(new Engine.Component.UndoOverlapCollisionResponse());

            this.AddComponent(new Engine.Component.HealthPoints(100, 100));

            this.jumpcounter = this.jumpCounterMax;

            this.InitializeSprites();
            this.animationScheduler = new GameComponents.AnimationScheduler();
            this.AddComponent(this.animationScheduler);

            Engine.Engine.Colliders.Add(this);

            // For Demo 2.0
            // this.AddComponent(new Engine.Component.DoDamageCollisionResponse(10, 1));
            this.gun = new Gun.Pistol();
            this.AddComponent(this.gun.GetAsComponent());
            this.ChangeGun(new Gun.MachineGun());

            GameManager.OnPauseStateChange += this.OnPauseStateChange;
        }

        /// <summary>
        /// Change the Gun of the Player ( the old Gun will be deleted ).
        /// </summary>
        /// <param name="gun">The new Gun.</param>
        public void ChangeGun(Gun.IGun gun)
        {
            Engine.Component.Component oldGun = null;
            foreach (Engine.Component.Component c in this.GetComponents())
            {
                if (c is Gun.IGun)
                {
                    oldGun = c;
                }
            }

            this.RemoveComponent(oldGun);

            this.gun = gun;
            if (gun is Engine.Component.Component)
            {
                this.AddComponent((Engine.Component.Component)gun);
            }

            this.SetGunSprite(gun);
        }

        /// <inheritdoc/>
        public int GetDirection()
        {
            return this.isFaceing;
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            KeyboardState keyboardState = Engine.Engine.GameWindow.KeyboardState;
            Engine.Component.Physics physics = this.GetComponent<Engine.Component.Physics>();

            if (GameManager.UpdatesPaused)
            {
                return;
            }

            this.Walk(frameTime, keyboardState, physics);

            base.OnUpdate(frameTime);
            this.Shoot(keyboardState);
            this.UpdateAnimations();

            // Check death
            if (this.GetComponent<Engine.Component.HealthPoints>().GetIsDeadFlag())
            {
                Engine.Engine.RemoveGameObject(this);
                Engine.Engine.ChangeScene(GameManager.SceneDeath);
            }

            // Reset jump counter when ground was touched
            Engine.Component.UndoOverlapCollisionResponse collider = this.GetComponent<Engine.Component.UndoOverlapCollisionResponse>();
            if (collider == null)
            {
                return;
            }

            if (collider.GetGroundTouchedFlag())
            {
                this.jumpcounter = this.jumpCounterMax;
            }

            // Jump
            if (keyboardState.IsKeyPressed(Keys.Space) && this.jumpcounter > 0)
            {
                physics.AddVelocitY(this.jumpPower);
                this.jumpcounter--;
            }

            this.Mirrored = this.animationScheduler.GetIfMustBeMirrored();
        }

        private void Shoot(KeyboardState keyboardState)
        {
            // if (keyboardState.IsKeyDown(Keys.Left))
            if (keyboardState.IsKeyPressed(Keys.Left))
            {
                this.isFaceing = Gun.ILookDirection.Left;
                this.gun.PullTrigger();
                if (this.gun.ShotFired())
                {
                    this.Mirrored = true;
                }
            }
            else if (keyboardState.IsKeyPressed(Keys.Right))
            {
                this.isFaceing = Gun.ILookDirection.Right;
                this.gun.PullTrigger();
                if (this.gun.ShotFired())
                {
                    this.Mirrored = false;
                }
            }
        }

        private void SetGunSprite(Gun.IGun gun)
        {
            if (gun is Gun.GrenadeLauncher)
            {
                this.spriteGun = TextureAtlas.Sprites["adventurer_weapon_grenadelauncher"];
            }

            if (gun is Gun.MachineGun)
            {
                this.spriteGun = TextureAtlas.Sprites["adventurer_weapon_machinegun"];
            }

            if (gun is Gun.Pistol)
            {
                this.spriteGun = TextureAtlas.Sprites["adventurer_weapon_pistol"];
            }

            if (gun is Gun.ShotGun)
            {
                this.spriteGun = TextureAtlas.Sprites["adventurer_weapon_shotgun"];
            }

            if (gun is Gun.Sniper)
            {
                this.spriteGun = TextureAtlas.Sprites["adventurer_weapon_sniper"];
            }
        }

        private void WalkLeft(float frameTime, Engine.Component.Physics physics)
        {
            this.isFaceing = Gun.ILookDirection.Left;
            if (physics.GetVelocity().X > 0)
            { // Player is breaking since he is going right
                physics.AddVelocityX(-this.activeBreacking * frameTime);
            }

            physics.AddVelocityX(-this.accelaration * frameTime);
        }

        private void WalkRight(float frameTime, Engine.Component.Physics physics)
        {
            this.isFaceing = Gun.ILookDirection.Right;
            if (physics.GetVelocity().X < 0)
            { // Player is breaking since he going left
                physics.AddVelocityX(this.activeBreacking * frameTime);
            }

            physics.AddVelocityX(this.accelaration * frameTime);
        }

        private void Idle(float frameTime, Engine.Component.Physics physics)
        {
            if (physics.GetVelocity().X > 0)
            { // Player is going right
                physics.AddVelocityX(-this.idealBreacking * frameTime);
            }
            else
            {
                physics.AddVelocityX(this.idealBreacking * frameTime);
            }

            if (Math.Abs(physics.GetVelocity().X) <= this.idealBreacking * frameTime)
            {
                physics.SetVelocity(0f, physics.GetVelocity().Y);
            }
        }

        private void Walk(float frameTime, KeyboardState keyboardState, Engine.Component.Physics physics)
        {
            if (keyboardState.IsKeyDown(Keys.A))
            { // Player wants to go left
                this.animationScheduler.AddAnimation(49, 0.03f, this.spriteWalking, true);
                this.Mirrored = true;
                this.WalkLeft(frameTime, physics);
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            { // Player wants to go right
                this.animationScheduler.AddAnimation(49, 0.03f, this.spriteWalking, false);
                this.Mirrored = false;
                this.WalkRight(frameTime, physics);
            }
            else
            { // Player is not breaking or accelerating
                this.animationScheduler.AddAnimation(51, 0.001f, this.spriteIdle, this.Mirrored);
                this.Idle(frameTime, physics);
            }

            if (keyboardState.IsKeyDown(Keys.W))
            {
                this.animationScheduler.AddAnimation(50, 0.03f, this.spriteBack, this.Mirrored);
            }
        }

        private void UpdateAnimations()
        {
            Engine.Component.Physics phys = this.GetComponent<Engine.Component.Physics>();

            // Falling
            if (phys.GetVelocity().Y < -0.5)
            {
                this.animationScheduler.AddAnimation(40, 0.001f, this.spriteFall, this.Mirrored);
            }

            // Jumping
            if (phys.GetVelocity().Y > 0 && this.jumpcounter < this.jumpCounterMax)
            {
                this.animationScheduler.AddAnimation(35, 0.0001f, this.spriteJump, this.Mirrored);
            }

            // Took Damage
            if (this.GetComponent<Engine.Component.HealthPoints>().GetTookDmgThisFrame())
            {
                this.animationScheduler.AddAnimation(20, 0.3f, this.spriteHurt, this.Mirrored);
            }

            // Shooting
            if (this.gun.ShotFired())
            {
                this.animationScheduler.AddAnimation(15, 0.2f, this.spriteGun, this.Mirrored);
            }
        }

        private void InitializeSprites()
        {
            this.spriteWalking = TextureAtlas.Sprites["adventurer_walking"];
            this.spriteHurt = TextureAtlas.Sprites["adventurer_hurt"];
            this.spriteIdle = TextureAtlas.Sprites["adventurer_idle"];
            this.spriteJump = TextureAtlas.Sprites["adventurer_jump"];
            this.spriteFall = TextureAtlas.Sprites["adventurer_fall"];
            this.spriteBack = TextureAtlas.Sprites["adventurer_back"];
            this.spriteGun = TextureAtlas.Sprites["adventurer_weapon_pistol"];
        }

        private void OnPauseStateChange(bool isPaused)
        {
            Engine.Component.Physics physics = this.GetComponent<Engine.Component.Physics>();
            if (isPaused)
            {
                physics.SetVelocity(0, 0);
                physics.SetIsAffectedByGravity(false);
            }
            else
            {
                physics.SetIsAffectedByGravity(true);
            }
        }
    }
}

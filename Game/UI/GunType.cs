// <copyright file="GunType.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.UI
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using Engine.Renderer.Sprite;
    using Game.Gun;

    /// <summary>
    /// Class for GunTypes and their information.
    /// </summary>
    public class GunType
    {
        private static Assembly assembly;

        private GunType(string gunName, IGun gun, Sprite sprite, float dmg, float price, string info)
        {
            this.GunName = gunName;
            this.Gun = gun;
            this.GunSprite = sprite;
            this.GunDMG = dmg;
            this.GunPrice = price;
            this.GunInfo = info;
        }

        /// <summary>
        /// Gets and sets an Array where every Gun and additional information about it is saved.
        /// </summary>
        public static GunType[] GunTypeArray { get; private set; }

        /// <summary>
        /// Gets Gunname as a String.
        /// </summary>
        public string GunName { get; private set; }

        /// <summary>
        /// Gets reference to gun instance.
        /// </summary>
        public IGun Gun { get; private set; }

        /// <summary>
        /// Gets and sets Sprite for gun.
        /// </summary>
        public Sprite GunSprite { get; private set; }

        /// <summary>
        /// Gets and sets wha damage the gun makes.
        /// </summary>
        public float GunDMG { get; private set; }

        /// <summary>
        /// Gets and sets the price of the gun in hearts.
        /// </summary>
        public float GunPrice { get; private set; }

        /// <summary>
        /// Gets and sets genral info aobut the gun.
        /// </summary>
        public string GunInfo { get; private set; }

        /// <summary>
        /// Initialize Array of GunTypes. For now it has to be done manually.
        /// </summary>
        public static void InitGunArray()
        {
            assembly = Assembly.GetExecutingAssembly();

            GunTypeArray = new GunType[5];

            Stream spriteStream = assembly.GetManifestResourceStream("Game.Resources.Sprite.UI.ItemShop.grenadelauncher.png");
            GunTypeArray[0] = new GunType("Grenade Launcher", new Gun.GrenadeLauncher(), new Sprite(spriteStream, true), 10, 5, "Launches grenades which explode.");

            spriteStream = assembly.GetManifestResourceStream("Game.Resources.Sprite.UI.ItemShop.machinegun.png");
            GunTypeArray[1] = new GunType("Machine Gun", new Gun.MachineGun(), new Sprite(spriteStream, true), 10, 5, "Fires normal bullets at an astonishing rate.");

            spriteStream = assembly.GetManifestResourceStream("Game.Resources.Sprite.UI.ItemShop.pistol.png");
            GunTypeArray[2] = new GunType("Pistol", new Gun.Pistol(), new Sprite(spriteStream, true), 10, 0, "Fires normale bullets with normal speed.");

            spriteStream = assembly.GetManifestResourceStream("Game.Resources.Sprite.UI.ItemShop.shotgun.png");
            GunTypeArray[3] = new GunType("Shot Gun", new Gun.ShotGun(), new Sprite(spriteStream, true), 20, 3, "Fires multiple bullets at once.");

            spriteStream = assembly.GetManifestResourceStream("Game.Resources.Sprite.UI.ItemShop.sniper.png");
            GunTypeArray[4] = new GunType("Sniper", new Gun.Sniper(), new Sprite(spriteStream, true), 70, 7, "Fires fast Bullets which. Kills \"oneshots\".");

            return;
        }
    }
}

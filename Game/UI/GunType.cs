// <copyright file="GunType.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.UI
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Game.Gun;

    /// <summary>
    /// Class for GunTypes and their information.
    /// </summary>
    public class GunType
    {
        private GunType(string gunName, IGun gun)
        {
            this.GunName = gunName;
            this.Gun = gun;
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
        /// Initialize Array of GunTypes. For now it has to be done manually.
        /// </summary>
        public static void InitGunArray()
        {
            GunTypeArray = new GunType[5];
            GunTypeArray[0] = new GunType("Grenade Launcher", new Gun.GrenadeLauncher());
            GunTypeArray[1] = new GunType("Machine Gun", new Gun.MachineGun());
            GunTypeArray[2] = new GunType("Pistol", new Gun.Pistol());
            GunTypeArray[3] = new GunType("Shot Gun", new Gun.ShotGun());
            GunTypeArray[4] = new GunType("Sniper", new Gun.Sniper());
            return;
        }
    }
}

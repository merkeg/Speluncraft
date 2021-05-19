using Engine.Renderer.Sprite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Player.Items
{
    public abstract class Item : Engine.GameObject.GameObject
    {
        public Item(float minX, float minY, float sizeX, float sizeY, ISprite sprite) : base(minX, minY, sizeX, sizeY, sprite)
        {
        }

        public abstract void OnPickUp();

    }
}
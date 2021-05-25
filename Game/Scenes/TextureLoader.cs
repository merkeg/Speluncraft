// <copyright file="TextureLoader.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Scenes
{
    using Engine.Renderer;
    using Engine.Renderer.Sprite;
    using Engine.Renderer.Text;
    using Engine.Renderer.Text.Parser;
    using Engine.Renderer.Tile;
    using Engine.Renderer.Tile.Parser;

    /// <summary>
    /// Texture loader class.
    /// </summary>
    public class TextureLoader
    {
        /// <summary>
        /// Load textures.
        /// </summary>
        public static void LoadTextures()
        {
            // Fonts
            TextureAtlas.Set("defaultFont", new Font(FontModel.Parse("Game.Resources.Font.semicondensed.font.fnt"), new Sprite("Game.Resources.Font.semicondensed.font.png")));
            TextureAtlas.Set("debugFont", new Font(FontModel.Parse("Game.Resources.Font.hack.font.fnt"), new Sprite("Game.Resources.Font.hack.font.png")));

            // Tilesheets
            TextureAtlas.Set("defaultTilesheet", new Tilesheet("Game.Resources.Sprite.tilesheet.png", 32, 32));

            // Sprites
            // Player
            TextureAtlas.Set("adventurer_back", new Sprite("Game.Resources.Sprite.Player.adventurer_back.png"));
            TextureAtlas.Set("adventurer_fall", new Sprite("Game.Resources.Sprite.Player.adventurer_fall.png"));
            TextureAtlas.Set("adventurer_hurt", new AnimatedSprite(new Tilesheet("Game.Resources.Sprite.Player.adventurer_hurt.png", 80, 110), Keyframe.RangeX(0, 1, 0, 0.1f)));
            TextureAtlas.Set("adventurer_idle", new Sprite("Game.Resources.Sprite.Player.adventurer_idle.png"));
            TextureAtlas.Set("adventurer_jump", new Sprite("Game.Resources.Sprite.Player.adventurer_jump.png"));
            TextureAtlas.Set("adventurer_walking", new AnimatedSprite(new Tilesheet("Game.Resources.Sprite.Player.adventurer_walking.png", 80, 110), Keyframe.RangeX(0, 1, 0, 0.1f)));
            TextureAtlas.Set("adventurer_weapon_grenadelauncher", new Sprite("Game.Resources.Sprite.Player.adventurer_weapon_grenadelauncher.png"));
            TextureAtlas.Set("adventurer_weapon_machinegun", new Sprite("Game.Resources.Sprite.Player.adventurer_weapon_machinegun.png"));
            TextureAtlas.Set("adventurer_weapon_pistol", new Sprite("Game.Resources.Sprite.Player.adventurer_weapon_pistol.png"));
            TextureAtlas.Set("adventurer_weapon_shotgun", new Sprite("Game.Resources.Sprite.Player.adventurer_weapon_shotgun.png"));
            TextureAtlas.Set("adventurer_weapon_sniper", new Sprite("Game.Resources.Sprite.Player.adventurer_weapon_sniper.png"));

            // Enemies
            TextureAtlas.Set("zombie_attack", new AnimatedSprite(new Tilesheet("Game.Resources.Sprite.Enemy.zombie_hit.png", 80, 110), Keyframe.RangeX(0, 1, 0, 0.1f)));
            TextureAtlas.Set("zombie_hurt", new AnimatedSprite(new Tilesheet("Game.Resources.Sprite.Enemy.zombie_hurt.png", 80, 110), Keyframe.RangeX(0, 1, 0, 0.1f)));
            TextureAtlas.Set("zombie_walking", new AnimatedSprite(new Tilesheet("Game.Resources.Sprite.Enemy.zombie_walking.png", 80, 110), Keyframe.RangeX(0, 1, 0, 0.1f)));
            TextureAtlas.Set("leaper_attack", new AnimatedSprite(new Tilesheet("Game.Resources.Sprite.Enemy.leaper_hit.png", 80, 110), Keyframe.RangeX(0, 1, 0, 0.1f)));
            TextureAtlas.Set("leaper_hurt", new AnimatedSprite(new Tilesheet("Game.Resources.Sprite.Enemy.leaper_hurt.png", 80, 110), Keyframe.RangeX(0, 1, 0, 0.1f)));
            TextureAtlas.Set("leaper_walking", new AnimatedSprite(new Tilesheet("Game.Resources.Sprite.Enemy.leaper_walking.png", 80, 110), Keyframe.RangeX(0, 1, 0, 0.1f)));
            TextureAtlas.Set("leaper_jump", new Sprite("Game.Resources.Sprite.Enemy.leaper_jump.png"));
            TextureAtlas.Set("leaper_slide", new Sprite("Game.Resources.Sprite.Enemy.leaper_slide.png"));

            // Animated Sprites
            TextureAtlas.Set("netherPortal", new AnimatedSprite(new Tilesheet("Game.Resources.Animated.nether_portal.png", 32, 32), Keyframe.RangeY(0, 0, 31, 0.03f)));
            TextureAtlas.Set("waterFlow", new AnimatedSprite(new Tilesheet("Game.Resources.Animated.water_flow.png", 32, 32), Keyframe.RangeY(0, 0, 35, 0.1f)));
            TextureAtlas.Set("waterStill", new AnimatedSprite(new Tilesheet("Game.Resources.Animated.water_still.png", 32, 32), Keyframe.RangeY(0, 0, 50, 0.05f)));
            TextureAtlas.Set("waterCut", new AnimatedSprite(new Tilesheet("Game.Resources.Animated.water_still_cut.png", 32, 32), Keyframe.RangeY(0, 0, 50, 0.05f)));
            TextureAtlas.Set("lavaFlow", new AnimatedSprite(new Tilesheet("Game.Resources.Animated.lava_flow.png", 32, 32), Keyframe.RangeY(0, 0, 245, 0.03f)));
            TextureAtlas.Set("lavaStill", new AnimatedSprite(new Tilesheet("Game.Resources.Animated.lava_still.png", 32, 32), Keyframe.RangeY(0, 0, 122, 0.03f)));
            TextureAtlas.Set("lavaCut", new AnimatedSprite(new Tilesheet("Game.Resources.Animated.lava_still_cut.png", 32, 32), Keyframe.RangeY(0, 0, 122, 0.03f)));
            TextureAtlas.Set("fire", new AnimatedSprite(new Tilesheet("Game.Resources.Animated.fire.png", 32, 32), Keyframe.RangeY(0, 0, 23, 0.04f)));
            TextureAtlas.Set("heart", new AnimatedSprite(new Tilesheet("Game.Resources.Animated.heart.png", 16, 16), Keyframe.RangeX(0, 23, 0, 0.1f)));

            // UI
            TextureAtlas.Set("startscreen", new Sprite("Game.Resources.UI.startmenu.png", false));
            TextureAtlas.Set("gitgudcasul", new Sprite("Game.Resources.UI.gitgud.png", false));
            TextureAtlas.Set("healthbar_hearts", new Sprite("Game.Resources.UI.Healthbar.heartsheet_new.png", false));
            TextureAtlas.Set("healthbar_background", new Sprite("Game.Resources.UI.Healthbar.background.png", false));

            // Ammunition
            TextureAtlas.Set("ammunition_grenade", new AnimatedSprite(new Tilesheet("Game.Resources.Animated.explosion.png", 635, 635), Keyframe.RangeX(0, 8, 0, 0.001f)));
            TextureAtlas.Set("ammunition_bullet", new AnimatedSprite(new Tilesheet("Game.Resources.Animated.bullet.png", 32, 32), Keyframe.RangeY(0, 0, 1, 0.5f)));

            // TilemapModel
            TextureAtlas.Set("level01", new Tilemap(TextureAtlas.Tilesheets["defaultTilesheet"], TilemapParser.ParseTilemap("Game.Resources.Level.level01.json")));

            // Sheet Animations
            Tilesheet sheet = TextureAtlas.Tilesheets["defaultTilesheet"];
            sheet.SetCustomSprite(36, TextureAtlas.Sprites["netherPortal"]);
            sheet.SetCustomSprite(81, TextureAtlas.Sprites["waterFlow"]);
            sheet.SetCustomSprite(91, TextureAtlas.Sprites["waterStill"]);
            sheet.SetCustomSprite(92, TextureAtlas.Sprites["waterCut"]);
            sheet.SetCustomSprite(83, TextureAtlas.Sprites["lavaFlow"]);
            sheet.SetCustomSprite(93, TextureAtlas.Sprites["lavaStill"]);
            sheet.SetCustomSprite(94, TextureAtlas.Sprites["lavaCut"]);
            sheet.SetCustomSprite(85, TextureAtlas.Sprites["fire"]);
        }
    }
}
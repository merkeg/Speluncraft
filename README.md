# Introduction

We programmed Speluncraft with inspiration from Spelunky. We proceeded to implement Minecraft-like texturing and using Textures provided by one of Minecraft's famous Resourcepacks.
Our goal was to create a game with a few levels and different weapons whilst maintaining a simple playthrough experience.

We used .NET 3.1 and OpenTK as a low-level binding for OpenGL to create the game. Using practices learned from Software Engineering we used a CI pipeline with automated testing to ensure our Engine is working correctly and sprint planning with 6 Sprint phases.

# Getting Started

1. Make sure you have at least [.NET](https://dotnet.microsoft.com/download/dotnet/3.1) (>= 3.1) libraries installed
2. Click on Game.exe
3. Enjoy

# Trailer

[![Video](https://rwuwu.de/0f240h)](https://zer0.pw/mueIYFM "Trailer")

# Tutorial

![Screenshot Game](https://rwuwu.de/gNLveI)

1. Player - You control the Player via [A], [D] and [Space]
2. Healthbar - Shows your current HP
3. Weapon - Shows the selected Weapon, its cooldown when fired. Fired via [Left] and [Right]

# Build and Test

Before you can compile the game, you must ensure the [.NET](https://dotnet.microsoft.com/download/dotnet/3.1) (>= 3.1) libraries have been installed on your system.

Open the project in your favourite .NET IDE of choice and press build. Same procedure for automated tests.

# Credits

Below you can find Links to assets, tools, and tips we used to create our project

## Tools

[Tiled](https://github.com/mapeditor/tiled) - Tile map editor

[Hiero](https://github.com/libgdx/libgdx/wiki/Hiero) - Bitmap font packing tool

## Libraries / Nugets

[StyleCop.Analyzers](https://www.nuget.org/packages/StyleCop.Analyzers) - Enforce a set of style and consistency rules

[Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json) - JSON framework for .NET

[SixLabors.ImageSharp](https://www.nuget.org/packages/SixLabors.ImageSharp) - Fully featured 2D graphics API for .NET

[OpenTK](https://www.nuget.org/packages/OpenTK) - Low-level C# bindings for OpenGL

[MSTest.TestFramework](https://www.nuget.org/packages/MSTest.TestFramework) - Microsoft's Test framework for automated testing

[Microsoft.CodeCoverage](https://www.nuget.org/packages/Microsoft.CodeCoverage) - Collects code coverage from tests

## Resources

[Dokucraft](https://dokucraft.co.uk) - Textures used for map and hearts

[Kenney's Platformer Characters](https://kenney.nl/assets/platformer-characters) - Used for Player and Enemysprites

[Kenney's Blaster Kit](https://kenney.nl/assets/blaster-kit) - Used for weapons

[Kenney's Smoke Particles](https://kenney.nl/assets/smoke-particles) - Used for Explosion Sprite

[Font Hack](https://sourcefoundry.org/hack/) - Used for Tooltips

[Font Semicondensed](https://fonts.google.com/specimen/Barlow+Semi+Condensed) - Used for Start Screen

## Codeinspiration

[Prof. Scherzer's 06 Camera Example](https://fbe-nextcloud.rwu.de/s/y8xGNNx7AA7BZtL) - used to center the camera with the player

[davudk OpenGL TileMap Demo](https://github.com/davudk/OpenGL-TileMap-Demos#1-immediate-rendering) - TileMap rendering

[ThinMatrix Font Rendering Tutorial](https://www.youtube.com/watch?v=mnIQEQoHHCU) - Font rendering

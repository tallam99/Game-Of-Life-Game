# The Game of Life Game

The Game of Life Game is a 3D platformer based on John Conway's Game of Life.
The controls are WASD for movement, space bar for jumping, and mouse for camera control.
The goal is to survive on the evolving grid as long as possible, and the score is a multiplicative function of time survived and horizontal distance traveled.
10 custom levels are included based on classic Game of Life oscillators and spaceships.

In the spirit of Game of Life, the melody of the soundtrack was generated using glider configurations.
The live recording of the soundtrack can be viewed [here.](https://youtu.be/nF3_FcRNiag)

Levels are specified by text input files with examples found in the Level Templates directory.
To add custom levels, drop the files in the directory appropriate for your OS:
- Windows Builds/1.0/The Game of Life Game_Data/StreamingAssets/Levels
- macOS Builds.app/Contents/Resources/Data/StreamingAssets/Levels

For more details about the project and development, see "Project Report.pdf".

**How to Play**
This repository includes builds for Windows and macOS in "Windows Builds" and "macOS Builds.app", respectively. Simply download the repository to run the game!

**Known Bugs:**
- Occassionally the level will drop the player in the wrong place. Restart the level with R until this doesn't occur.
- The dropdown menu for level selection of the options screen does not display text properly.

# Developers
Tarek Allam

Jackson Lightfoot
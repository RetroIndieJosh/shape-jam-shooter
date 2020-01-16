# Joshua's Shape Jam II Shooter

## The Game

Keyboard: Move with WASD and shoot with arrow keys
Gamepad: Move with left stick and shoot with right stick

## How to Customize Assets

The following assets can be customized:
- player
- bullet
- enemy

Inside the [INSERT FOLDER NAME] directory, add a directory called anim. In this directory, add the following:

- player/idle
- player/walk
- player/shoot
- player/die
- bullet/move
- bullet/contact
- enemy/idle
- enemy/walk
- enemy/die

In each of these folders, add as many frames as you like numbered starting with 0. Only .png format is supported.

In other words, name the files 0.png, 1.png, 2.png, etc.

For example, the second frame of animation for the player shoot animation will be the file:
[INSERT FOLDER NAME]/anim/player/walk/1.png

All animations *must* be present for the game to run correctly.
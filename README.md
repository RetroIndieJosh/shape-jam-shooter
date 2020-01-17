# Joshua's Shape Jam II Shooter

## The Game

Keyboard: Move with WASD and shoot with arrow keys
Gamepad: Move with left stick and shoot with right stick

## How to Customize Assets

The following assets can be customized:
- player
- bullet
- enemy

Inside the [INSERT FOLDER NAME] directory, add the following:

- anim/player/idle/*.png
- anim/player/walk/*.png
- anim/player/shoot/*.png
- anim/player/die/*.png
- anim/bullet/move/*.png
- anim/bullet/contact/*.png
- anim/enemy/idle/*.png
- anim/enemy/walk/*.png
- anim/enemy/die/*.png
- texture/floor.png
- texture/walls.png

For animations (with *.png) each frame must be named in sequence, starting with 0.

In other words: 0.png, 1.png, 2.png for a 3-frame animation.

Only .png format is supported.

For example, the second frame of animation for the player shoot animation will be the file:
[INSERT FOLDER NAME]/anim/player/walk/1.png

If any animation is missing, the game will fall back to the default image. You must include *all* animations for a given object before any will show.
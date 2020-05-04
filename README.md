# Shape Jam Shooter

(c)2020 Joshua McLean

For the 8 Bits to Infinity Shape Jam II: Art Edition, Week 1

Created in one week (well, about 7 hours)

## The Game

Keyboard: Move with WASD and shoot with arrow keys
Gamepad: Move with left stick and shoot with right stick (untested)
Mouse: Move with WASD and shoot by clicking (will fire in direction you click)

## How to Customize Assets

Inside the `Shape Jam Shooter_Data` directory, add the following:

- anim/player/idle/^.png
- anim/player/walk/^.png
- anim/player/shoot/^.png
- anim/player/die/^.png
- anim/bullet/move/^.png
- anim/bullet/contact/^.png
- anim/enemy/idle/^.png
- anim/enemy/walk/^.png
- anim/enemy/die/^.png
- icon/health.png
- texture/floor.png
- texture/walls.png

Note that the player, enemies, and bullet have no sense of direction/rotation,
so you should use vaguely circular or square images. A top-down perspective is
strongly recommended. Collisions are also circular and slightly smaller than
the visual, so ideally these sprites will have some amount of transparency on
the border.

Player and enemies will match the scale of the sprites you import. The default
size is 100x100 for player and 200x200 for enemies, but you can adjust the game
difficulty by using differently sized sprites.

The textures will be tiled in the x- and y-axes.

Icons will be squashed into a square shape, so square is your best bet.

For animations (with ^.png), name each frame in sequence, starting with 0.

In other words: 0.png, 1.png, 2.png for a 3-frame animation.

Only .png format is supported.

For example, the second frame of animation for the player shoot animation will
be the file: `Shape Jam Shooter_Data/anim/player/walk/1.png`

## License

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program. If not, see <https://www.gnu.org/licenses/>.

(c)2019 Joshua McLean

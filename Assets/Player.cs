using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightBitsToInfinity
{
    public class Player : Unit
    {
        private void Update() {
            var moveX = Input.GetAxis("Horizontal");
            var moveY = Input.GetAxis("Vertical");
            var move = new Vector2(moveX, moveY).normalized;
            Move(move);
        }
    }
}

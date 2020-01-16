using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightBitsToInfinity {
    [RequireComponent(typeof(AnimatedSpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int m_damage = 1;

        public int Damage => m_damage;

        private AnimatedSpriteRenderer m_animator = null;
        private Rigidbody2D m_body = null;

        public void Collide() {
            m_body.velocity = Vector2.zero;
            m_animator.SetAnimation("contact");
        }

        private void Awake() {
            m_animator = GetComponent<AnimatedSpriteRenderer>();
            m_body = GetComponent<Rigidbody2D>();
        }

        private void Start() {
            var animList = new List<string>() { "move", "contact" };
            var missingList = m_animator.GetListOfMissingAnimationsExpecting(animList);
            if (missingList.Count > 0) {
                var missingStr = string.Join(", ", missingList);
                Debug.LogError($"Missing animations in {name}: {missingStr}");
                Destroy(gameObject);
                return;
            }

            var contactAnim = m_animator.FindAnimation("contact");
            contactAnim.loop = false;
            contactAnim.AddFinishEvent(() => {
                Destroy(gameObject);
            });

            // created bullet is moving automatically
            m_animator.SetAnimation("move");
        }
    }
}

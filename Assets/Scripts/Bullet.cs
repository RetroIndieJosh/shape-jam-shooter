using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightBitsToInfinity {
    [RequireComponent(typeof(AnimatedSpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int m_damage = 1;
        [SerializeField] private float m_moveLimit = 100f;

        public int Damage => m_damage;

        private AnimatedSpriteRenderer m_animator = null;
        private Rigidbody2D m_body = null;
        private Vector2 m_origin = Vector2.zero;

        public void Collide() {
            m_body.velocity = Vector2.zero;

            var contactAnim = m_animator.FindAnimation("contact");
            if (contactAnim == null) {
                Destroy(gameObject);
                return;
            }

            contactAnim.loop = false;
            contactAnim.AddFinishEvent(() => Destroy(gameObject));
            m_animator.SetAnimation("contact");

        }

        private void Awake() {
            m_animator = GetComponent<AnimatedSpriteRenderer>();
            m_body = GetComponent<Rigidbody2D>();
        }

        private void Update() {
            if (Vector2.Distance(transform.position, m_origin) > m_moveLimit)
                Destroy(gameObject);
        }

        private void Start() {
            // created bullet is moving automatically
            m_animator.SetAnimation("move");
            m_origin = transform.position;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightBitsToInfinity {
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(AnimatedSpriteRenderer))]
    public class Unit : MonoBehaviour
    {
        [SerializeField] private int m_healthMax = 5;
        [SerializeField] private float m_speed = 10f;
        [SerializeField] private float m_invincibleFrames = 60;

        private AnimatedSpriteRenderer m_animator = null;
        private Rigidbody2D m_body = null;
        private int m_health = 0;

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.CompareTag("bullet") == false)
                return;

            Damage(1);
        }

        private void Awake() {
            m_animator = GetComponent<AnimatedSpriteRenderer>();
            m_body = GetComponent<Rigidbody2D>();
            m_health = m_healthMax;
        }

        private void Start() {
            var animationList = new List<string>() { "die", "idle", "walk" };
            if (m_animator.VerifyAnimations(animationList) == false) {
                Debug.LogError($"Missing animations in {name}; require {animationList}");
                Destroy(gameObject);
                return;
            }

            var deathAnimation = m_animator.FindAnimation("die");
            deathAnimation.AddFinishEvent(() => {
                Destroy(gameObject);
            });
        }

        protected void Damage(int a_amount) {
            if (m_isInvincible)
                return;

            m_health -= a_amount;
            if (m_health <= 0) {
                Die();
                return;
            }
        }

        protected void Die() {
            m_animator.SetAnimation("die");
        }

        protected void Move(Vector2 a_direction) {
            a_direction = a_direction.normalized;
            if (a_direction.magnitude <= Mathf.Epsilon) {
                m_body.velocity = Vector2.zero;
                m_animator.SetAnimation("idle");
                return;
            }
            m_animator.SetAnimation("walk");
            m_body.velocity = a_direction * m_speed;
        }

        private bool m_isInvincible = false;

        private IEnumerator RunInvincibleFrames() {
            m_isInvincible = true;
            GetComponent<SpriteRenderer>().color = Color.red;

            var timeElapsed = 0f;
            var invincibleTime = m_invincibleFrames / 60f;
            while (timeElapsed < invincibleTime)
                yield return null;

            m_isInvincible = false;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}

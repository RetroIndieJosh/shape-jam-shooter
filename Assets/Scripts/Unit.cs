using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightBitsToInfinity {
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(AnimatedSpriteRenderer))]
    public class Unit : MonoBehaviour
    {
        [SerializeField] private float m_invincibleFrames = 60;
        [SerializeField] private AudioClip m_dieSound = null;

        [Header("Limits")]
        [SerializeField] private bool m_renderBoundsInEditor = false;
        [SerializeField] protected Vector2 m_lowerBound = new Vector2(-100, -100);
        [SerializeField] protected Vector2 m_upperBound = new Vector2(100, 100);

        public bool IsDead => m_health <= 0;

        protected int m_healthMax = 5;
        protected float m_speed = 10f;

        private AnimatedSpriteRenderer m_animator = null;
        private Rigidbody2D m_body = null;
        private int m_health = 0;

        virtual protected void OnCollisionEnter2D(Collision2D collision) {
            var bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet == null)
                return;

            Damage(bullet.Damage);
            bullet.Collide();
        }

        virtual protected void OnTriggerEnter2D(Collider2D collision) {
            var bullet = collision.GetComponent<Bullet>();
            if (bullet == null)
                return;

            Damage(bullet.Damage);
            bullet.Collide();
        }

        private void Awake() {
            m_animator = GetComponent<AnimatedSpriteRenderer>();
            m_body = GetComponent<Rigidbody2D>();
        }

        private void OnDrawGizmos() {
            if (m_renderBoundsInEditor == false)
                return;
            Gizmos.color = Color.red;
            var rect = new Rect {
                min = m_lowerBound,
                max = m_upperBound
            };
            Gizmos.DrawWireCube(rect.center, rect.size);
        }

        protected virtual void Start() {
            var animationList = new List<string>() { "die", "idle", "walk" };
            var missingList = m_animator.GetListOfMissingAnimationsExpecting(animationList);
            if( missingList.Count > 0) {
                var missingStr = string.Join(", ", missingList);
                Debug.LogError($"Missing animations in {name}; missing:  {missingStr}");
                Destroy(gameObject);
                return;
            }

            var deathAnimation = m_animator.FindAnimation("die");
            deathAnimation.loop = false;
            deathAnimation.AddFinishEvent(() => {
                Destroy(gameObject);
            });

            m_health = m_healthMax;
        }

        virtual protected void Update() {
        }

        protected void Damage(int a_amount) {
            if (m_isInvincible)
                return;

            m_health -= a_amount;
            if (m_health <= 0) {
                Die();
                return;
            }

            _ = StartCoroutine(RunInvincibleFrames());
        }

        protected void Die() {
            m_body.velocity = Vector2.zero;
            if (m_dieSound != null)
                AudioSource.PlayClipAtPoint(m_dieSound, Vector3.zero);
            m_animator.SetAnimation("die");
        }

        protected void Move(Vector2 a_moveVec) {
            if (a_moveVec.magnitude <= Mathf.Epsilon) {
                m_body.velocity = Vector2.zero;
                m_animator.SetAnimation("idle");
                return;
            }
            m_animator.SetAnimation("walk");
            m_body.velocity = a_moveVec * m_speed;
        }

        private bool m_isInvincible = false;

        private IEnumerator RunInvincibleFrames() {
            m_isInvincible = true;
            var renderer = GetComponent<SpriteRenderer>();
            var isRed = false;
            for (var frameCount = 0; frameCount < m_invincibleFrames; ++frameCount) {
                if (frameCount % 5 == 0)
                    isRed = !isRed;
                renderer.color = isRed ? Color.red : Color.white;
                yield return null;
            }

            m_isInvincible = false;
            renderer.color = Color.white;
        }
    }
}

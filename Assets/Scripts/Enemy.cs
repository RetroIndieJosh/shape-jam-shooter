using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightBitsToInfinity
{
    public class Enemy : Unit
    {
        [Header("Health")]
        [SerializeField] private int m_healthRandomMin = 1;
        [SerializeField] private int m_healthRandomMax = 10;

        [Header("Speed")]
        [SerializeField] private int m_speedRandomMin = 1;
        [SerializeField] private int m_speedRandomMax = 5;

        [Header("Direction Change")]
        [SerializeField] private float m_dirChangeSecMin = 1f;
        [SerializeField] private float m_dirChangeSecMax = 3f;

        private Vector2 m_moveDir = Vector2.zero;
        private float m_dirChangeSec = 0f;
        private float m_timeSinceLastDirChange = 0f;

        protected override void Start() {
            m_healthMax = Random.Range(m_healthRandomMin, m_healthRandomMax);
            m_speed = Random.Range(m_speedRandomMin, m_speedRandomMax);

            base.Start();
        }

        protected override void Update() {
            if (IsDead)
                return;

            // turn if we're going out of bounds
            var containmentRect = new Rect() {
                min = m_lowerBound,
                max = m_upperBound
            };
            var nextPos = (Vector2)transform.position + ( m_moveDir * m_speed * 0.1f );
            if( containmentRect.Contains(nextPos) == false)
                m_moveDir = new Vector2(-m_moveDir.y, m_moveDir.x);

            m_timeSinceLastDirChange += Time.deltaTime;
            if (m_timeSinceLastDirChange >= m_dirChangeSec) {
                var x = Random.Range(-1f, 1f);
                var y = Random.Range(-1f, 1f);
                m_moveDir = new Vector2(x, y);

                m_timeSinceLastDirChange = 0f;
                m_dirChangeSec = Random.Range(m_dirChangeSecMin, m_dirChangeSecMax);
            }

            Move(m_moveDir.normalized);
            base.Update();
        }
    }
}

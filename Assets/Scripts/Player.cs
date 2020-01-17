using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace EightBitsToInfinity
{
    public class Player : Unit
    {
        [SerializeField] private TextMeshProUGUI m_healthDisplay = null;

        [SerializeField] private int m_playerHealthMax = 10;
        [SerializeField] private float m_playerSpeed = 10f;

        [SerializeField] private GameObject m_playerBulletPrefab = null;
        [SerializeField] private float m_fireVelocity = 20f;
        [SerializeField] private float m_firePerSecond = 2f;

        [SerializeField] private AudioClip m_shootSound = null;

        private float SecondsPerFire => 1f / m_firePerSecond;

        private float m_timeSinceLastFire = 0f;

        protected override void OnCollisionEnter2D(Collision2D collision) {
            var enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
                Damage(1);

            base.OnCollisionEnter2D(collision);
        }

        protected override void OnTriggerEnter2D(Collider2D collision) {
            var enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
                Damage(1);

            base.OnTriggerEnter2D(collision);
        }

        protected override void Start() {
            m_healthMax = m_playerHealthMax;
            m_speed = m_playerSpeed;
            base.Start();
        }

        override protected void Update() {
            if (IsDead)
                return;

            if (m_healthDisplay != null)
                m_healthDisplay.text = $"{Health}/{m_healthMax}";

            var moveX = Input.GetAxis("Horizontal");
            var moveY = Input.GetAxis("Vertical");
            var move = new Vector2(moveX, moveY).normalized;
            Move(move);

            var fireX = Input.GetAxis("Fire Horizontal");
            var fireY = Input.GetAxis("Fire Vertical");
            var fire = new Vector2(fireX, fireY).normalized;
            if(fire.magnitude > Mathf.Epsilon)
                Fire(fire);

            m_timeSinceLastFire += Time.deltaTime;

            base.Update();

            var x = Mathf.Clamp(transform.position.x, m_lowerBound.x, m_upperBound.y);
            var y = Mathf.Clamp(transform.position.y, m_lowerBound.x, m_upperBound.y);
            transform.position = new Vector2(x, y);

            var z = Camera.main.transform.position.z;
            Camera.main.transform.position = new Vector3(x, y, z);
        }

        private void Fire(Vector2 a_fireVec) {
            if (m_timeSinceLastFire < SecondsPerFire)
                return;

            if (m_shootSound != null)
                AudioSource.PlayClipAtPoint(m_shootSound, Camera.main.transform.position + Vector3.forward);

            Debug.Log("Fire " + a_fireVec);
            var bullet = Instantiate(m_playerBulletPrefab, transform.position, Quaternion.identity);
            var body = bullet.GetComponent<Rigidbody2D>();
            body.velocity = a_fireVec * m_fireVelocity;
            m_timeSinceLastFire = 0f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightBitsToInfinity
{
    public class Player : Unit
    {
        [SerializeField] private GameObject m_playerBulletPrefab = null;
        [SerializeField] private float m_fireVelocity = 20f;
        [SerializeField] private float m_firePerSecond = 2f;

        [SerializeField] private AudioClip m_shootSound = null;

        private float SecondsPerFire => 1f / m_firePerSecond;

        private float m_timeSinceLastFire = 0f;

        override protected void Update() {
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

            var x = transform.position.x;
            var y = transform.position.y;
            var z = Camera.main.transform.position.z;
            Camera.main.transform.position = new Vector3(x, y, z);

            base.Update();
        }

        private void Fire(Vector2 a_fireVec) {
            if (m_timeSinceLastFire < SecondsPerFire)
                return;

            if (m_shootSound != null)
                AudioSource.PlayClipAtPoint(m_shootSound, Vector3.zero);

            Debug.Log("Fire " + a_fireVec);
            var bullet = Instantiate(m_playerBulletPrefab, transform.position, Quaternion.identity);
            var body = bullet.GetComponent<Rigidbody2D>();
            body.velocity = a_fireVec * m_fireVelocity;
            m_timeSinceLastFire = 0f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace EightBitsToInfinity {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class EnemyCounter : MonoBehaviour {
        [SerializeField] private TimeDisplay m_timer = null;
        [SerializeField] private int m_wave = 0;
        [SerializeField] private EnemySpawner m_spawner = null;

        [Header("Wave Definition")]
        [SerializeField] private int m_baseEnemies = 10;
        [SerializeField] private int m_addEnemiesPerWave = 5;

        private TextMeshProUGUI m_textMesh = null;

        private void Awake() {
            m_textMesh = GetComponent<TextMeshProUGUI>();
        }

        private void Start() {
            m_spawner.Spawn((m_wave * m_addEnemiesPerWave) + m_baseEnemies);
        }

        private void Update() {
            var enemyCount = FindObjectsOfType<Enemy>().Length;
            m_textMesh.text = $"Wave: {m_wave}\nEnemies: {enemyCount}";
            if (enemyCount == 0) {
                ++m_wave;
                m_spawner.Spawn((m_wave * m_addEnemiesPerWave) + m_baseEnemies);
                m_timer.Lap();
            }
        }
    }
}

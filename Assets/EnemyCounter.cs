using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace EightBitsToInfinity {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class EnemyCounter : MonoBehaviour
    {
        [SerializeField]
        private TimeDisplay m_timer = null;

        private TextMeshProUGUI m_textMesh = null;

        private void Awake() {
            m_textMesh = GetComponent<TextMeshProUGUI>();
        }

        private void Update() {
            var enemyCount = FindObjectsOfType<Enemy>().Length;
            m_textMesh.text = $"Enemies: {enemyCount}";
            if (enemyCount == 0)
                m_timer.Stop();
        }
    }
}

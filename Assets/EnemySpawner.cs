using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int m_enemyCount = 10;
    [SerializeField] private GameObject m_enemyPrefab = null;

    [Header("Limits")]
    [SerializeField] private bool m_renderBoundsInEditor = false;
    [SerializeField] protected Vector2 m_lowerBound = new Vector2(-100, -100);
    [SerializeField] protected Vector2 m_upperBound = new Vector2(100, 100);

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

    private void Start() {
        for (var i = 0; i < m_enemyCount; ++i) {
            var x = Random.Range(m_lowerBound.x, m_upperBound.x);
            var y = Random.Range(m_lowerBound.y, m_upperBound.y);
            _ = Instantiate(m_enemyPrefab, new Vector3(x, y, 0), Quaternion.identity);
        }
    }
}

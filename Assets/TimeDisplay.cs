using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TimeDisplay : MonoBehaviour
{
    private TextMeshProUGUI m_textMesh = null;
    private System.TimeSpan m_elapsedTime = System.TimeSpan.Zero;

    private void Awake() {
        m_textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        m_elapsedTime += System.TimeSpan.FromSeconds(Time.deltaTime);
        m_textMesh.text = string.Format("{0:D2}:{1:D2}.{2:D3}", 
            m_elapsedTime.Minutes, m_elapsedTime.Seconds, m_elapsedTime.Milliseconds);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TimeDisplay : MonoBehaviour
{
    private bool m_isRunning = true;
    private TextMeshProUGUI m_textMesh = null;
    private System.TimeSpan m_elapsedTime = System.TimeSpan.Zero;

    public void Stop() {
        m_isRunning = false;
    }

    private void Awake() {
        m_textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        if (m_isRunning == false)
            return;

        m_elapsedTime += System.TimeSpan.FromSeconds(Time.deltaTime);
        m_textMesh.text = string.Format("Time: {0:D2}:{1:D2}.{2:D3}", 
            m_elapsedTime.Minutes, m_elapsedTime.Seconds, m_elapsedTime.Milliseconds);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TimeDisplay : MonoBehaviour
{
    private string PrevTimeString => string.Format("{0:D2}:{1:D2}.{2:D3}", 
        m_prevTime.Minutes, m_prevTime.Seconds, m_prevTime.Milliseconds);
    private string TimeString => string.Format("{0:D2}:{1:D2}.{2:D3}", 
        m_elapsedTime.Minutes, m_elapsedTime.Seconds, m_elapsedTime.Milliseconds);
    private string TotalTimeString => string.Format("{0:D2}:{1:D2}.{2:D3}", 
        m_totalTime.Minutes, m_totalTime.Seconds, m_totalTime.Milliseconds);

    private bool m_isRunning = true;
    private TextMeshProUGUI m_textMesh = null;

    private System.TimeSpan m_elapsedTime = System.TimeSpan.Zero;
    private System.TimeSpan m_prevTime = System.TimeSpan.Zero;
    private System.TimeSpan m_totalTime = System.TimeSpan.Zero;

    public void Stop() {
        m_isRunning = false;
    }

    public void Lap() {
        m_totalTime += m_elapsedTime;
        m_prevTime = m_elapsedTime;
        m_elapsedTime = System.TimeSpan.Zero;
    }

    private void Awake() {
        m_textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        if (m_isRunning == false)
            return;

        m_elapsedTime += System.TimeSpan.FromSeconds(Time.deltaTime);
        m_textMesh.text = $"Time: {TimeString}\nTotal: {TotalTimeString}\nPrev Wave: {PrevTimeString}";
    }
}

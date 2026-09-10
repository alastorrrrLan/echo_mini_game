using UnityEngine;

public class EchoController : MonoBehaviour
{
    [Header("Echo Settings")]
    public float maxRadius = 20f;
    public float pulseSpeed = 12f;
    public float cooldown = 1f;

    private float currentRadius = 0f;
    private bool isPulsing = false;
    private float lastPulseTime = -999f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryPulse();
        }

        if (isPulsing)
        {
            UpdatePulse();
        }
    }

    void TryPulse()
    {
        if (Time.time - lastPulseTime < cooldown) return;
        isPulsing = true;
        currentRadius = 0f;
        lastPulseTime = Time.time;
    }

    void UpdatePulse()
    {
        currentRadius += pulseSpeed * Time.deltaTime;
        if (currentRadius >= maxRadius)
        {
            isPulsing = false;
            currentRadius = 0f;
        }
    }
}

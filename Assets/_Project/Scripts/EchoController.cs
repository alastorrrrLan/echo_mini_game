using UnityEngine;

public class EchoController : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource echoSound;

    [Header("Echo Settings")]
    public float maxRadius = 20f;
    public float pulseSpeed = 12f;
    public float cooldown = 1f;



    private float currentRadius = 0f;
    private bool isPulsing = false;
    private float lastPulseTime = -999f;

    private static readonly int EchoOriginID = Shader.PropertyToID("_EchoOrigin");
    private static readonly int EchoRadiusID = Shader.PropertyToID("_EchoRadius");

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
        Shader.SetGlobalVector(EchoOriginID, transform.position);
        Shader.SetGlobalFloat(EchoRadiusID, currentRadius);
    }


    void TryPulse()
    {
        if (Time.time - lastPulseTime < cooldown) return;
        isPulsing = true;
        currentRadius = 0f;
        lastPulseTime = Time.time;
        
        echoSound?.Play();
    }

    void UpdatePulse()
    {
        currentRadius += pulseSpeed * Time.deltaTime;
        if (currentRadius >= maxRadius)
        {
            StopPulse();
        }
    }

    void StopPulse()
    {
        isPulsing = false;
        currentRadius = 0f;
    }
}

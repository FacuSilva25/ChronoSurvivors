using UnityEngine;

public class PulseAnimation : MonoBehaviour
{
    public float minScale = 2f;
    public float maxScale = 2.5f;
    public float pulseSpeed = 4f;

    void Update()
    {
        // PingPong genera un valor que sube y baja suavemente
        float currentScale = Mathf.PingPong(Time.time * pulseSpeed, maxScale - minScale) + minScale;
        transform.localScale = new Vector3(currentScale, currentScale, 1f);
    }
}
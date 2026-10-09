using UnityEngine;

public class OrbitalRotation : MonoBehaviour
{
    public float rotationSpeed = 150f;

    void Update()
    {
        // Gira sobre el eje Z (2D) constantemente
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
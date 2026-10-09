using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;

    [Header("Configuración")]
    public float smoothTime = 0.15f; // Tiempo que tarda en alcanzar al jugador
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    // Variable interna requerida por SmoothDamp
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = player.position + offset;

        // SmoothDamp elimina los tirones al calcular la velocidad en tiempo real
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
using UnityEngine;

public class InfiniteFloor : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;

    [Header("Configuración")]
    // Este valor debe ser igual al tamaño físico de tu baldosa en el mundo de Unity.
    // El cuadrado por defecto mide exactamente 1 unidad.
    public float tileSize = 1f;

    void LateUpdate()
    {
        if (player == null) return;

        // La magia: redondeamos la posición del jugador para encontrar la baldosa más cercana
        float snapX = Mathf.Round(player.position.x / tileSize) * tileSize;
        float snapY = Mathf.Round(player.position.y / tileSize) * tileSize;

        // Teletransportamos este objeto gigante a ese punto exacto.
        // Como salta en fracciones idénticas a su patrón de dibujo, el jugador no ve el salto.
        transform.position = new Vector2(snapX, snapY);
    }
}
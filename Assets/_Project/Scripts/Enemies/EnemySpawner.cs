using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject enemyPrefab;
    public Transform player;

    [Header("Configuración de Oleada")]
    public float spawnRate = 2f; // Cantidad de enemigos que aparecen por segundo
    public float spawnRadius = 12f; // Distancia desde el jugador (debe ser mayor a lo que ve la cámara)

    private float nextSpawnTime;

    void Update()
    {
        // Medida de seguridad por si el jugador muere o no está asignado
        if (player == null) return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            // Calculamos el próximo tiempo de aparición
            nextSpawnTime = Time.time + 1f / spawnRate;
        }
    }

    void SpawnEnemy()
    {
        // Random.insideUnitCircle genera un punto (X, Y) dentro de un círculo de radio 1.
        // Al usar .normalized, forzamos a que el punto esté exactamente en el borde del círculo.
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        // Multiplicamos esa dirección por nuestro radio y se lo sumamos a la posición actual del jugador
        Vector2 spawnPosition = (Vector2)player.position + randomDirection * spawnRadius;

        // Generamos al enemigo en ese punto
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    // Dibuja un círculo verde en el editor para que puedas ajustar visualmente dónde nacen los enemigos
    void OnDrawGizmosSelected()
    {
        if (player != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(player.position, spawnRadius);
        }
    }
}
using UnityEngine;

public class FlaskWeapon : WeaponBase
{
    [Header("Configuración")]
    public GameObject puddlePrefab;
    public float throwCooldown = 2.5f;
    public float spawnRange = 5f;
    public LayerMask enemyLayer;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= throwCooldown)
        {
            SpawnPuddleNearEnemies();
            timer = 0f;
        }
    }

    void SpawnPuddleNearEnemies()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, spawnRange, enemyLayer);
        Vector2 targetPos;

        if (hitEnemies.Length > 0)
        {
            // Elige un enemigo al azar dentro del radio y tira el frasco sobre él
            targetPos = hitEnemies[Random.Range(0, hitEnemies.Length)].transform.position;
        }
        else
        {
            // Si no hay enemigos cerca, lo tira a una posición aleatoria alrededor del jugador
            targetPos = (Vector2)transform.position + Random.insideUnitCircle * (spawnRange * 0.7f);
        }

        Instantiate(puddlePrefab, targetPos, Quaternion.identity);
    }
}
using UnityEngine;

public class FlaskWeapon : Weapon
{
    [Header("Configuración Específica")]
    public float spawnRange = 5f;
    public LayerMask enemyLayer;

    // Se ejecuta automáticamente cada vez que expira el cooldown configurado en WeaponData
    public override bool Attack()
    {
        SpawnPuddleNearEnemies();
        return true;
    }

    void SpawnPuddleNearEnemies()
    {
        GameObject prefabToSpawn = currentStats.projectilePrefab;
        if (prefabToSpawn == null) return;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, spawnRange, enemyLayer);
        Vector2 targetPos;

        if (hitEnemies.Length > 0)
        {
            targetPos = hitEnemies[Random.Range(0, hitEnemies.Length)].transform.position;
        }
        else
        {
            targetPos = (Vector2)transform.position + Random.insideUnitCircle * (spawnRange * 0.7f);
        }

        Instantiate(prefabToSpawn, targetPos, Quaternion.identity);
    }
}
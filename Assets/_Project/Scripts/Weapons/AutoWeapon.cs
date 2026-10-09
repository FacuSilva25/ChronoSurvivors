using UnityEngine;

public class AutoWeapon : WeaponBase
{
    [Header("Configuración")]
    public LayerMask enemyLayer;  // Filtro para buscar solo enemigos

    private float nextFireTime;

    void Update()
    {
        // Verificamos si ya pasó el tiempo de recarga
        if (Time.time >= nextFireTime)
        {
            Transform target = FindClosestEnemy();
            if (target != null)
            {
                Shoot(target);
                // Calculamos el próximo disparo usando el fireRate del ScriptableObject
                nextFireTime = Time.time + 1f / weaponData.fireRate;
            }
        }
    }

    Transform FindClosestEnemy()
    {
        // Escaneamos el área de ataque buscando colliders que estén en la capa "Enemy"
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, weaponData.attackRange, enemyLayer);

        Transform closest = null;
        float minDistance = Mathf.Infinity;

        // Iteramos sobre todos los enemigos detectados para encontrar el más cercano
        foreach (Collider2D enemy in enemiesInRange)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy.transform;
            }
        }
        return closest;
    }

    void Shoot(Transform target)
    {
        // Instanciamos el proyectil que asignamos en el ScriptableObject
        GameObject bullet = Instantiate(weaponData.projectilePrefab, transform.position, Quaternion.identity);

        Vector2 direction = (target.position - transform.position).normalized;

        // Inicializamos la bala enviándole los datos de daño y velocidad
        bullet.GetComponent<Projectile>().Initialize(direction, weaponData.projectileSpeed, weaponData.damage);
    }

    // Dibuja un círculo rojo en el editor para visualizar el rango de ataque
    void OnDrawGizmosSelected()
    {
        if (weaponData != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, weaponData.attackRange);
        }
    }
}
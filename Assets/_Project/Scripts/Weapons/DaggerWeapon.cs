using UnityEngine;

public class DaggerWeapon : ProjectileWeapon
{
    public LayerMask enemyLayer;

    protected override void SpawnProjectile()
    {
        if (currentStats.projectilePrefab == null) return;

        // Busca el enemigo más cercano o dispara hacia el frente
        Vector2 targetDir = FindTargetDirection();

        GameObject obj = Instantiate(currentStats.projectilePrefab, transform.position, Quaternion.identity);
        Projectile proj = obj.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.Initialize(
                targetDir,
                currentStats.speed,
                GetDamage(),
                currentStats.pierce,
                GetArea()
            );
        }
    }

    Vector2 FindTargetDirection()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 10f, enemyLayer);
        if (enemies.Length == 0) return transform.right;

        Transform nearest = null;
        float minDist = Mathf.Infinity;
        foreach (var col in enemies)
        {
            float dist = Vector2.Distance(transform.position, col.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = col.transform;
            }
        }
        return nearest != null ? (nearest.position - transform.position).normalized : (Vector2)transform.right;
    }
}
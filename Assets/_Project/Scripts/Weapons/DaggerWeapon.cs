using UnityEngine;

public class DaggerWeapon : WeaponBase
{
    [Header("Configuración")]
    public GameObject daggerPrefab;
    public float attackCooldown = 1.2f;
    public float detectionRadius = 8f;
    public LayerMask enemyLayer;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= attackCooldown)
        {
            FireAtNearestEnemy();
            timer = 0f;
        }
    }

    void FireAtNearestEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);
        if (enemies.Length == 0) return;

        // Busca el enemigo más cercano
        Transform nearest = null;
        float minDistance = Mathf.Infinity;
        foreach (var col in enemies)
        {
            float dist = Vector2.Distance(transform.position, col.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                nearest = col.transform;
            }
        }

        if (nearest != null)
        {
            Vector2 dir = nearest.position - transform.position;
            GameObject dagger = Instantiate(daggerPrefab, transform.position, Quaternion.identity);
            dagger.GetComponent<PiercingProjectile>().Setup(dir);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
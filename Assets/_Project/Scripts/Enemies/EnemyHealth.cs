using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Estadísticas")]
    public float maxHealth = 30f;
    private float currentHealth;

    [Header("Recompensas")]
    public GameObject xpGemPrefab; // Arrastra el prefab Gem_XP aquí en el editor

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Instanciamos la gema en la misma posición donde muere el enemigo
        if (xpGemPrefab != null)
        {
            Instantiate(xpGemPrefab, transform.position, Quaternion.identity);
        }

        GameManager.Instance.RegisterKill();

        Destroy(gameObject);
    }
}
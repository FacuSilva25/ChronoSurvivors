using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Estadísticas")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI")]
    public Slider healthSlider; // Reemplazamos Image por Slider

    [Header("Detección de Daño")]
    public LayerMask enemyLayer;
    public float hurtboxRadius = 0.4f;

    [Header("Invulnerabilidad")]
    public float invulnerabilityTime = 0.5f;
    private float lastDamageTime;

    void Start()
    {
        currentHealth = maxHealth;

        // Configuramos los límites del slider al iniciar
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    void Update()
    {
        if (Time.time >= lastDamageTime + invulnerabilityTime)
        {
            CheckForEnemies();
        }
    }

    void CheckForEnemies()
    {
        Collider2D enemyCollider = Physics2D.OverlapCircle(transform.position, hurtboxRadius, enemyLayer);
        if (enemyCollider != null)
        {
            TakeDamage(10f);
        }
    }

    public void TakeDamage(float damage)
    {
        // Consultamos la armadura del stat controller si existe
        PlayerStatController stats = GetComponent<PlayerStatController>();
        float finalDamage = damage;

        if (stats != null)
        {
            finalDamage = Mathf.Max(1f, damage - stats.armor); // Al menos siempre recibe 1 de daño mínimo
        }

        currentHealth -= finalDamage;
        lastDamageTime = Time.time;

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthSlider != null)
        {
            // El Slider se ajusta directamente al valor actual
            healthSlider.value = currentHealth;
        }
    }

    void Die()
    {
        Time.timeScale = 0f;
        Debug.Log("¡El jugador ha muerto!");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, hurtboxRadius);
    }

    public void UpdateMaxHealth(float currentMultiplier)
    {
        float oldMaxHealth = maxHealth;
        maxHealth = 100f * currentMultiplier; // Asumiendo que 100 es tu base

        // Curamos al jugador por la diferencia para que el aumento de vida máxima sea útil al instante
        float healthDifference = maxHealth - oldMaxHealth;
        currentHealth += healthDifference;

        UpdateHealthBar();
    }
}
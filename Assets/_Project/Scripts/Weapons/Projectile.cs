using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed;
    private float projectileDamage;
    public float lifeTime = 3f; // Tiempo máximo de vida para evitar que consuma memoria

    // Este método recibirá los datos que configuraste en tu ScriptableObject
    public void Initialize(Vector2 direction, float speed, float damage)
    {
        moveDirection = direction;
        moveSpeed = speed;
        projectileDamage = damage;

        // Destrucción de seguridad si la bala no choca con nada
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Mueve el proyectil continuamente en la dirección asignada
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Buscamos el componente de salud en el enemigo impactado
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(projectileDamage);
            }

            // El proyectil siempre se destruye tras impactar
            Destroy(gameObject);
        }
    }
}
using UnityEngine;

public class PiercingProjectile : MonoBehaviour
{
    public float speed = 14f;
    public float damage = 25f;
    public int pierceCount = 3; // Cuántos enemigos puede atravesar
    public float lifetime = 3f;

    private Vector2 moveDirection;

    public void Setup(Vector2 direction)
    {
        moveDirection = direction.normalized;
        // Rota el proyectil hacia la dirección de vuelo
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>()?.TakeDamage(damage);
            pierceCount--;

            if (pierceCount <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
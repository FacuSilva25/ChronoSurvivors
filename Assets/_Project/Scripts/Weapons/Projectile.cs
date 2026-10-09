using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Vector2 moveDirection;
    protected float speed;
    protected float damage;
    protected int pierce;

    public virtual void Initialize(Vector2 direction, float speed, float damage, int pierce = 1, float area = 1f, float lifeTime = 3f)
    {
        this.moveDirection = direction.normalized;
        this.speed = speed;
        this.damage = damage;
        this.pierce = pierce;

        // Escala con la estadística de área
        transform.localScale = Vector3.one * area;

        // Rota hacia el movimiento
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Destroy(gameObject, lifeTime);
    }

    protected virtual void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>()?.TakeDamage(damage);
            pierce--;
            if (pierce <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
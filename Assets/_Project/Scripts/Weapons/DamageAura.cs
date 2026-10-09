using UnityEngine;
using System.Collections.Generic;

public class DamageAura : MonoBehaviour
{
    public float damage = 15f;
    public float tickRate = 0.5f; // Aplica daño cada medio segundo

    // Diccionario para recordar cuándo se golpeó por última vez a cada enemigo
    private Dictionary<Collider2D, float> damageTimers = new Dictionary<Collider2D, float>();

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Si el enemigo no está en la lista, o ya pasó el tiempo del cooldown
            if (!damageTimers.ContainsKey(other) || Time.time >= damageTimers[other] + tickRate)
            {
                other.GetComponent<EnemyHealth>()?.TakeDamage(damage);
                damageTimers[other] = Time.time;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Limpiamos el diccionario cuando el enemigo sale del área para ahorrar memoria
        if (damageTimers.ContainsKey(other))
        {
            damageTimers.Remove(other);
        }
    }
}
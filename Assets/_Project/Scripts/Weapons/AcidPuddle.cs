using UnityEngine;
using System.Collections.Generic;

public class AcidPuddle : MonoBehaviour
{
    public float damagePerTick = 8f;
    public float tickInterval = 0.4f;
    public float puddleDuration = 4f;

    private Dictionary<Collider2D, float> hitTimers = new Dictionary<Collider2D, float>();

    void Start()
    {
        Destroy(gameObject, puddleDuration);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!hitTimers.ContainsKey(other) || Time.time >= hitTimers[other] + tickInterval)
            {
                other.GetComponent<EnemyHealth>()?.TakeDamage(damagePerTick);
                hitTimers[other] = Time.time;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (hitTimers.ContainsKey(other))
        {
            hitTimers.Remove(other);
        }
    }
}
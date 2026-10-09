using UnityEngine;

public class AcidPuddle : MonoBehaviour
{
    public float damagePerTick = 8f;
    public float tickInterval = 0.4f;
    public float puddleDuration = 4f;

    private float timer;

    void Start()
    {
        Destroy(gameObject, puddleDuration);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            timer += Time.deltaTime;
            if (timer >= tickInterval)
            {
                other.GetComponent<EnemyHealth>()?.TakeDamage(damagePerTick);
                timer = 0f;
            }
        }
    }
}
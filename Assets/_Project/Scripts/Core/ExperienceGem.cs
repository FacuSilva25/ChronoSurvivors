using UnityEngine;

public class ExperienceGem : MonoBehaviour
{
    [Header("Valor")]
    public int xpValue = 10;

    [Header("Efecto Imán")]
    public float magnetRadius = 3.5f; // Distancia a la que el jugador atrae la gema
    public float moveSpeed = 12f;     // Velocidad de vuelo hacia el jugador

    private Transform player;
    private bool isMagnetic = false;

    void Start()
    {
        // Buscamos al jugador una sola vez al instanciar la gema
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        if (isMagnetic)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            // Obtenemos el multiplicador actual del jugador
            float currentMultiplier = player.GetComponent<PlayerStatController>().magnetRadiusMultiplier;

            // Multiplicamos el radio base de la gema por el bono del jugador
            if (Vector2.Distance(transform.position, player.position) <= magnetRadius * currentMultiplier)
            {
                isMagnetic = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // La recolección final sigue ocurriendo al tocar físicamente al jugador
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();
            if (stats != null)
            {
                stats.AddExperience(xpValue);
                Destroy(gameObject);
            }
        }
    }
}
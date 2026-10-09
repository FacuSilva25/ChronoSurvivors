using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Estadísticas")]
    public float speed = 2.5f;

    private Transform player;

    void Start()
    {
        // Buscamos al jugador al aparecer. 
        // Nota: En un juego final con Object Pool, esto se optimiza, pero es perfecto para prototipar.
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        // Movemos al enemigo constantemente hacia la posición del jugador
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}
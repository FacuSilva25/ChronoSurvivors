using UnityEngine;
using TMPro; // Para usar TextMeshPro

public class GameManager : MonoBehaviour
{
    // Patrón Singleton para acceder al GameManager desde cualquier script
    public static GameManager Instance;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI killCountText;

    [Header("Configuración de Partida")]
    public float maxGameTime = 1800f; // 30 minutos (30 * 60 segundos)

    private float currentTime = 0f;
    private int killCount = 0;
    private bool isGameOver = false;

    void Awake()
    {
        // Configuramos la instancia única
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        killCountText.text = "0";
    }

    void Update()
    {
        if (isGameOver) return;

        currentTime += Time.deltaTime;
        UpdateTimerUI();

        if (currentTime >= maxGameTime)
        {
            EndGameTimeLimit();
        }
    }

    public void RegisterKill()
    {
        killCount++;
        killCountText.text = "" + killCount;
    }

    void UpdateTimerUI()
    {
        // Convertimos los segundos a formato de Minutos:Segundos
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void EndGameTimeLimit()
    {
        isGameOver = true;
        Time.timeScale = 0f; // Congela el juego
        Debug.Log("¡Límite de tiempo alcanzado (30:00)!");
        // Próximamente: Aparecerá "La Muerte" o la pantalla de Victoria
    }
}
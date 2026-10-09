using UnityEngine;
using UnityEngine.UI; // Para el Slider
using TMPro; // Para el texto avanzado de Unity

public class PlayerStats : MonoBehaviour
{
    [Header("Progresión")]
    public int currentLevel = 1;
    public int currentExperience = 0;
    public int experienceToNextLevel = 100;

    [Header("UI de Pantalla")]
    public Slider xpSlider;
    public TextMeshProUGUI levelText;

    [Header("Referencias UI")]
    public LevelUpUI levelUpUI; // Asignar en el Inspector

    void Start()
    {
        UpdateUI();
    }

    public void AddExperience(int amount)
    {
        currentExperience += amount;

        // Verificamos si alcanzamos la experiencia requerida
        if (currentExperience >= experienceToNextLevel)
        {
            LevelUp();
        }

        UpdateUI(); // Actualizamos los gráficos siempre que ganamos XP
    }

    void LevelUp()
    {
        currentExperience -= experienceToNextLevel;
        currentLevel++;
        experienceToNextLevel = Mathf.RoundToInt(experienceToNextLevel * 1.5f);

        Debug.Log("¡Subiste al nivel " + currentLevel + "!");

        // Abrimos la pantalla de mejoras
        if (levelUpUI != null)
        {
            levelUpUI.OpenLevelUpScreen();
        }
    }

    void UpdateUI()
    {
        // Actualizamos la barra
        if (xpSlider != null)
        {
            xpSlider.maxValue = experienceToNextLevel;
            xpSlider.value = currentExperience;
        }

        // Actualizamos el texto
        if (levelText != null)
        {
            levelText.text = "Nivel: " + currentLevel;
        }
    }
}
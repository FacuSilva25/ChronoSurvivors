using UnityEngine;
using System.Collections.Generic;

public class LevelUpUI : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject upgradePanel; // El panel gris oscuro que cubre la pantalla
    public UpgradeCardUI[] upgradeCards; // Las 3 cartas
    public PlayerInventory playerInventory;

    [Header("Bases de Datos de Opciones")]
    public List<WeaponData> allAvailableWeapons = new List<WeaponData>();
    public List<PassiveItemData> allAvailablePassives = new List<PassiveItemData>();

    void Start()
    {
        upgradePanel.SetActive(false); // Oculto al inicio
    }

    public void OpenLevelUpScreen()
    {
        // 1. Pausamos el tiempo de juego
        Time.timeScale = 0f;
        upgradePanel.SetActive(true);

        // 2. Filtramos todas las opciones válidas que el inventario aún puede recibir
        List<WeaponData> validWeapons = new List<WeaponData>();
        foreach (var w in allAvailableWeapons)
        {
            if (playerInventory.CanEquipOrUpgradeWeapon(w))
            {
                validWeapons.Add(w);
            }
        }

        List<PassiveItemData> validPassives = new List<PassiveItemData>();
        foreach (var p in allAvailablePassives)
        {
            if (playerInventory.CanEquipPassive(p))
            {
                validPassives.Add(p);
            }
        }

        // 3. Configuramos las 3 cartas seleccionando al azar
        for (int i = 0; i < upgradeCards.Length; i++)
        {
            // Decidimos aleatoriamente entre arma (0) o pasiva (1) si ambas tienen opciones
            bool pickWeapon = (validWeapons.Count > 0 && validPassives.Count > 0)
                              ? (Random.value > 0.5f)
                              : (validWeapons.Count > 0);

            if (pickWeapon && validWeapons.Count > 0)
            {
                WeaponData selected = validWeapons[Random.Range(0, validWeapons.Count)];

                // Comprobamos si ya la tiene para marcarla como mejora de nivel
                WeaponBase existing = playerInventory.equippedWeapons.Find(w => w.weaponData == selected);
                bool isUpgrade = existing != null;
                int targetLevel = isUpgrade ? existing.currentLevel + 1 : 1;

                upgradeCards[i].SetupWeapon(selected, this, isUpgrade, targetLevel);
                validWeapons.Remove(selected); // Evitamos duplicados en la misma tanda
            }
            else if (validPassives.Count > 0)
            {
                PassiveItemData selected = validPassives[Random.Range(0, validPassives.Count)];
                upgradeCards[i].SetupPassive(selected, this);
                validPassives.Remove(selected);
            }
            else
            {
                // Si no quedan opciones, desactivamos la carta sobrante
                upgradeCards[i].gameObject.SetActive(false);
            }
        }
    }

    public void ApplyWeaponSelection(WeaponData weapon)
    {
        playerInventory.AddOrUpgradeWeapon(weapon);
        CloseLevelUpScreen();
    }

    public void ApplyPassiveSelection(PassiveItemData passive)
    {
        playerInventory.AddPassive(passive);
        CloseLevelUpScreen();
    }

    void CloseLevelUpScreen()
    {
        upgradePanel.SetActive(false);
        Time.timeScale = 1f; // Reanudamos el juego
    }
}
using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public const int MAX_WEAPONS = 5;
    public const int MAX_PASSIVES = 5;

    [Header("Inventario Activo")]
    public List<Weapon> equippedWeapons = new List<Weapon>();

    // Almacenamos ScriptableObjects nativos y sus niveles en listas estándar de Unity
    public List<PassiveItemData> equippedPassives = new List<PassiveItemData>();
    public List<int> passiveLevels = new List<int>();

    private PlayerStatController statController;

    void Awake()
    {
        statController = GetComponent<PlayerStatController>();
    }

    #region Gestión de Armas
    public bool CanEquipOrUpgradeWeapon(WeaponData weaponData)
    {
        if (weaponData == null) return false;
        equippedWeapons.RemoveAll(w => w == null);

        Weapon existing = equippedWeapons.Find(w => w != null && w.weaponData == weaponData);
        if (existing != null)
        {
            return existing.weaponData != null && existing.currentLevel < existing.weaponData.maxLevel;
        }

        return equippedWeapons.Count < MAX_WEAPONS;
    }

    public void AddOrUpgradeWeapon(WeaponData weaponData)
    {
        if (weaponData == null) return;
        equippedWeapons.RemoveAll(w => w == null);

        Weapon existing = equippedWeapons.Find(w => w != null && w.weaponData == weaponData);
        if (existing != null)
        {
            existing.LevelUp();
        }
        else if (equippedWeapons.Count < MAX_WEAPONS)
        {
            if (weaponData.weaponPrefab == null) return;

            GameObject newWeaponObj = Instantiate(weaponData.weaponPrefab, transform);
            Weapon weaponComponent = newWeaponObj.GetComponent<Weapon>();
            if (weaponComponent != null)
            {
                weaponComponent.weaponData = weaponData;
                equippedWeapons.Add(weaponComponent);
            }
        }
    }
    #endregion

    #region Gestión de Pasivas
    public bool CanEquipOrUpgradePassive(PassiveItemData passiveData)
    {
        if (passiveData == null) return false;

        int index = equippedPassives.IndexOf(passiveData);
        if (index != -1)
        {
            return passiveLevels[index] < passiveData.maxLevel;
        }

        return equippedPassives.Count < MAX_PASSIVES;
    }

    public void AddOrUpgradePassive(PassiveItemData passiveData)
    {
        if (passiveData == null) return;

        int index = equippedPassives.IndexOf(passiveData);
        if (index != -1)
        {
            if (passiveLevels[index] < passiveData.maxLevel)
            {
                passiveLevels[index]++;
                statController?.RecalculateStats(equippedPassives, passiveLevels);
            }
        }
        else if (equippedPassives.Count < MAX_PASSIVES)
        {
            equippedPassives.Add(passiveData);
            passiveLevels.Add(1);
            statController?.RecalculateStats(equippedPassives, passiveLevels);
        }
    }

    // Métodos para compatibilidad con llamadas anteriores de LevelUpUI
    public bool CanEquipPassive(PassiveItemData passiveData) => CanEquipOrUpgradePassive(passiveData);
    public void AddPassive(PassiveItemData passiveData) => AddOrUpgradePassive(passiveData);
    #endregion
}
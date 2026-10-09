using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    [Header("Límites de Ranuras")]
    public const int MAX_WEAPONS = 5;
    public const int MAX_PASSIVES = 5;

    [Header("Inventario Activo")]
    public List<WeaponBase> equippedWeapons = new List<WeaponBase>();
    public List<PassiveItemData> equippedPassives = new List<PassiveItemData>();

    private PlayerStatController statController;

    void Awake()
    {
        statController = GetComponent<PlayerStatController>();
    }

    #region Gestión de Armas

    public bool CanEquipOrUpgradeWeapon(WeaponData weaponData)
    {
        if (weaponData == null) return false;

        // Limpiamos referencias nulas accidentales en la lista
        equippedWeapons.RemoveAll(w => w == null);

        // Buscamos si ya la tiene, ignorando armas que no tengan weaponData asignado
        WeaponBase existing = equippedWeapons.Find(w => w != null && w.weaponData == weaponData);
        if (existing != null)
        {
            return existing.currentLevel < existing.maxLevel;
        }

        // Si no la tiene, verificamos si queda espacio en el inventario
        return equippedWeapons.Count < MAX_WEAPONS;
    }

    public void AddOrUpgradeWeapon(WeaponData weaponData)
    {
        if (weaponData == null) return;

        equippedWeapons.RemoveAll(w => w == null);
        WeaponBase existing = equippedWeapons.Find(w => w != null && w.weaponData == weaponData);

        if (existing != null)
        {
            existing.LevelUp();
        }
        else
        {
            if (equippedWeapons.Count < MAX_WEAPONS)
            {
                if (weaponData.weaponPrefab == null)
                {
                    Debug.LogError($"El arma {weaponData.weaponName} no tiene asignado su Weapon Prefab en el ScriptableObject.");
                    return;
                }

                GameObject newWeaponObj = Instantiate(weaponData.weaponPrefab, transform);
                WeaponBase weaponComponent = newWeaponObj.GetComponent<WeaponBase>();

                if (weaponComponent != null)
                {
                    weaponComponent.weaponData = weaponData;
                    equippedWeapons.Add(weaponComponent);
                    Debug.Log($"Equipada nueva arma: {weaponData.weaponName} ({equippedWeapons.Count}/{MAX_WEAPONS})");
                }
            }
        }
    }

    #endregion

    #region Gestión de Habilidades Pasivas

    public bool CanEquipPassive(PassiveItemData passiveData)
    {
        // Límite de 5 pasivas
        return equippedPassives.Count < MAX_PASSIVES;
    }

    public void AddPassive(PassiveItemData passiveData)
    {
        if (equippedPassives.Count < MAX_PASSIVES)
        {
            equippedPassives.Add(passiveData);
            statController?.ApplyPassiveItem(passiveData);
            Debug.Log($"Equipada nueva pasiva: {passiveData.itemName} ({equippedPassives.Count}/{MAX_PASSIVES})");
        }
        else
        {
            Debug.LogWarning("Inventario de pasivas lleno (5/5).");
        }
    }

    #endregion
}
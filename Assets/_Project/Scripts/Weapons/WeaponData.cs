using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Chrono Survivors/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public Sprite icon;
    public GameObject weaponPrefab; // El prefab que tiene el script del arma (ej. DaggerWeapon)
    public int maxLevel = 5;

    [Header("Estadísticas Base (Nivel 1)")]
    public Weapon.Stats baseStats;

    [Header("Crecimiento Lineal (Nivel 2 en adelante)")]
    [Tooltip("Índice 0 = Nivel 2, Índice 1 = Nivel 3, etc.")]
    public Weapon.Stats[] linearGrowth;
}
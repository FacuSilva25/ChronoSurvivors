using UnityEngine;

// Esto crea un atajo en el menú de Unity para fabricar nuevas armas con dos clics
[CreateAssetMenu(fileName = "NewWeapon", menuName = "Chrono Survivors/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Información General")]
    [Header("Prefab del Arma (Componente en el Jugador)")]
    public GameObject weaponPrefab; // El prefab que tiene el script AutoWeapon, DaggerWeapon, etc.
    public string weaponName;
    [TextArea]
    public string description;

    [Header("Referencias Visuales")]
    public GameObject projectilePrefab; // El prefabricado de la bala o rayo

    [Header("Estadísticas Base")]
    public float damage = 10f;
    public float fireRate = 1.5f; // Cantidad de ataques por segundo
    public float projectileSpeed = 12f;
    public float attackRange = 8f; // Radio de búsqueda de enemigos

    // Más adelante agregaremos las reglas de evolución de armas aquí
}
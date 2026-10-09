using UnityEngine;

[CreateAssetMenu(fileName = "NewPassiveItem", menuName = "Chrono Survivors/Passive Item")]
public class PassiveItemData : ScriptableObject
{
    [Header("Información")]
    public string itemName;
    [TextArea] public string description;
    public Sprite itemIcon; // Ícono temporal (puedes usar cuadrados de colores)

    [Header("Efecto")]
    public StatToBoost statBoosted;
    public float boostValue;
    // Ej: Si es 0.2f y es Daño, significa +20% de Daño.

    public enum StatToBoost
    {
        Damage,
        AttackSpeed,
        MoveSpeed,
        MagnetRadius,
        MaxHealth,
        Armor,
        CooldownReduction
    }
}
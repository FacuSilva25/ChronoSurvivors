using UnityEngine;

[CreateAssetMenu(fileName = "NewPassiveItem", menuName = "Chrono Survivors/Passive Item")]
public class PassiveItemData : ScriptableObject
{
    public string itemName;
    [TextArea] public string description;
    public Sprite itemIcon;
    public StatToBoost statBoosted;

    [Tooltip("Incremento por cada nivel (ej. Nivel 1: 0.1 (+10%), Nivel 2: 0.2 (+20%), etc.)")]
    public float[] boostPerLevel = new float[5] { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f };

    public int maxLevel => boostPerLevel != null ? boostPerLevel.Length : 1;

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

    public float GetBoostAtLevel(int level)
    {
        if (boostPerLevel == null || boostPerLevel.Length == 0) return 0f;
        int index = Mathf.Clamp(level - 1, 0, boostPerLevel.Length - 1);
        return boostPerLevel[index];
    }
}
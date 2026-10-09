using UnityEngine;
using System.Collections.Generic;

public class PlayerStatController : MonoBehaviour
{
    [Header("Estadísticas Base (Sin pasivas)")]
    public float baseDamageMultiplier = 1f;
    public float baseAttackSpeedMultiplier = 1f;
    public float baseMoveSpeedMultiplier = 1f;
    public float baseMagnetRadiusMultiplier = 1f;
    public float baseMaxHealthMultiplier = 1f;
    public float baseArmor = 0f;
    public float baseCooldownReduction = 0f;

    [Header("Estadísticas Actuales Calculadas")]
    public float damageMultiplier { get; private set; } = 1f;
    public float attackSpeedMultiplier { get; private set; } = 1f;
    public float moveSpeedMultiplier { get; private set; } = 1f;
    public float magnetRadiusMultiplier { get; private set; } = 1f;
    public float maxHealthMultiplier { get; private set; } = 1f;
    public float armor { get; private set; } = 0f;
    public float cooldownReduction { get; private set; } = 0f;

    void Awake()
    {
        ResetToBaseStats();
    }

    public void ResetToBaseStats()
    {
        damageMultiplier = baseDamageMultiplier;
        attackSpeedMultiplier = baseAttackSpeedMultiplier;
        moveSpeedMultiplier = baseMoveSpeedMultiplier;
        magnetRadiusMultiplier = baseMagnetRadiusMultiplier;
        maxHealthMultiplier = baseMaxHealthMultiplier;
        armor = baseArmor;
        cooldownReduction = baseCooldownReduction;
    }

    // Se ejecuta cada vez que adquieres o subes de nivel una pasiva
    public void RecalculateStats(List<PassiveItemData> passives, List<int> levels)
    {
        ResetToBaseStats();

        for (int i = 0; i < passives.Count; i++)
        {
            PassiveItemData data = passives[i];
            if (data == null) continue;

            int level = levels[i];
            float boost = data.GetBoostAtLevel(level);

            switch (data.statBoosted)
            {
                case PassiveItemData.StatToBoost.Damage:
                    damageMultiplier += boost;
                    break;
                case PassiveItemData.StatToBoost.AttackSpeed:
                    attackSpeedMultiplier += boost;
                    break;
                case PassiveItemData.StatToBoost.MoveSpeed:
                    moveSpeedMultiplier += boost;
                    break;
                case PassiveItemData.StatToBoost.MagnetRadius:
                    magnetRadiusMultiplier += boost;
                    break;
                case PassiveItemData.StatToBoost.MaxHealth:
                    maxHealthMultiplier += boost;
                    GetComponent<PlayerHealth>()?.UpdateMaxHealth(maxHealthMultiplier);
                    break;
                case PassiveItemData.StatToBoost.Armor:
                    armor += boost;
                    break;
                case PassiveItemData.StatToBoost.CooldownReduction:
                    cooldownReduction = Mathf.Clamp(cooldownReduction + boost, 0f, 0.5f);
                    break;
            }
        }
    }
}
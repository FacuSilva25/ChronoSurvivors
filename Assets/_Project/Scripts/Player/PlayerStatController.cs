using UnityEngine;
using System.Collections.Generic;

public class PlayerStatController : MonoBehaviour
{
    [Header("Multiplicadores (1 = 100%)")]
    public float damageMultiplier = 1f;
    public float attackSpeedMultiplier = 1f;
    public float moveSpeedMultiplier = 1f;
    public float magnetRadiusMultiplier = 1f;
    public float maxHealthMultiplier = 1f;

    [Header("Defensas y Tiempos")]
    public float armor = 0f;                   // Puntos planos de daño absorbidos
    public float cooldownReduction = 0f;       // Porcentaje de reducción (ej. 0.15 = 15% menos cooldown)

    [Header("Inventario Pasivo")]
    public List<PassiveItemData> acquiredPassives = new List<PassiveItemData>();

    public void ApplyPassiveItem(PassiveItemData newItem)
    {
        acquiredPassives.Add(newItem);

        switch (newItem.statBoosted)
        {
            case PassiveItemData.StatToBoost.Damage:
                damageMultiplier += newItem.boostValue;
                break;
            case PassiveItemData.StatToBoost.AttackSpeed:
                attackSpeedMultiplier += newItem.boostValue;
                break;
            case PassiveItemData.StatToBoost.MoveSpeed:
                moveSpeedMultiplier += newItem.boostValue;
                break;
            case PassiveItemData.StatToBoost.MagnetRadius:
                magnetRadiusMultiplier += newItem.boostValue;
                break;
            case PassiveItemData.StatToBoost.MaxHealth:
                maxHealthMultiplier += newItem.boostValue;
                GetComponent<PlayerHealth>()?.UpdateMaxHealth(maxHealthMultiplier);
                break;
            case PassiveItemData.StatToBoost.Armor:
                armor += newItem.boostValue;
                break;
            case PassiveItemData.StatToBoost.CooldownReduction:
                cooldownReduction = Mathf.Clamp(cooldownReduction + newItem.boostValue, 0f, 0.5f); // Tope de 50% de reducción
                break;
        }

        Debug.Log($"¡{newItem.itemName} adquirido! Se aplicó un boost a {newItem.statBoosted}.");
    }
}
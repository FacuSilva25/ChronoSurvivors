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

    [Header("Inventario Pasivo")]
    // Lista para llevar el registro de qué items hemos recolectado
    public List<PassiveItemData> acquiredPassives = new List<PassiveItemData>();

    // Este método será llamado desde la pantalla de subir de nivel
    public void ApplyPassiveItem(PassiveItemData newItem)
    {
        acquiredPassives.Add(newItem);

        // Aumentamos el multiplicador correspondiente sumando el valor del objeto
        // Ej: Si el multiplicador es 1f y el boost es 0.15f, el nuevo valor es 1.15f (115%)
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
                // Si aumentamos la vida máxima, avisamos al script de salud
                GetComponent<PlayerHealth>()?.UpdateMaxHealth(maxHealthMultiplier);
                break;
        }

        Debug.Log($"¡{newItem.itemName} adquirido! Se aplicó un boost a {newItem.statBoosted}.");
    }
}
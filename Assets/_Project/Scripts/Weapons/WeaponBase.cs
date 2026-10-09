using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public WeaponData weaponData;
    public int currentLevel = 1;
    public int maxLevel = 5;

    // Método virtual que cada arma implementará según cómo escale
    public virtual void LevelUp()
    {
        if (currentLevel < maxLevel)
        {
            currentLevel++;
            Debug.Log($"Arma {weaponData.weaponName} mejorada al Nivel {currentLevel}");
        }
    }
}
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [System.Serializable]
    public struct Stats
    {
        public string name;
        [TextArea] public string description;
        public GameObject projectilePrefab;
        public float damage;
        public float cooldown;
        public float speed;
        public float area;
        public int amount;       // Cantidad de proyectiles por ataque
        public int pierce;       // Perforación
        public float interval;   // Retraso entre proyectiles dentro de una ráfaga

        // Sobrecarga del operador + para sumar niveles automáticamente
        public static Stats operator +(Stats a, Stats b)
        {
            Stats res = a;
            res.damage += b.damage;
            res.cooldown += b.cooldown;
            res.speed += b.speed;
            res.area += b.area;
            res.amount += b.amount;
            res.pierce += b.pierce;
            // Si el nuevo nivel trae un prefab de proyectil, lo actualiza; si no, conserva el base
            if (b.projectilePrefab != null) res.projectilePrefab = b.projectilePrefab;
            return res;
        }
    }

    public WeaponData weaponData;
    public int currentLevel = 1;
    [SerializeField] protected Stats currentStats;
    protected float currentCooldown;
    protected PlayerStatController ownerStats;

    protected virtual void Awake()
    {
        ownerStats = GetComponentInParent<PlayerStatController>();
    }

    protected virtual void Start()
    {
        if (weaponData != null)
        {
            currentStats = weaponData.baseStats;
            currentCooldown = GetCooldown();
        }
    }

    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        {
            if (Attack())
            {
                currentCooldown = GetCooldown();
            }
        }
    }

    public virtual bool Attack() => true;

    // Métodos escalables con los pasivos del jugador
    public virtual float GetDamage()
    {
        float might = ownerStats != null ? ownerStats.damageMultiplier : 1f;
        return currentStats.damage * might;
    }

    public virtual float GetCooldown()
    {
        float cdReduction = ownerStats != null ? ownerStats.cooldownReduction : 0f;
        return currentStats.cooldown * (1f - cdReduction);
    }

    public virtual float GetArea()
    {
        // Si tienes pasivo de área en PlayerStatController, lo multiplicas aquí
        return currentStats.area > 0 ? currentStats.area : 1f;
    }

    public virtual void LevelUp()
    {
        if (weaponData != null && currentLevel < weaponData.maxLevel)
        {
            currentLevel++;
            int index = currentLevel - 2; // Nivel 2 -> índice 0
            if (index >= 0 && index < weaponData.linearGrowth.Length)
            {
                currentStats += weaponData.linearGrowth[index];
            }
            Debug.Log($"Arma {weaponData.weaponName} mejorada al Nivel {currentLevel}");
        }
    }

    public virtual bool CanLevelUp()
    {
        return weaponData != null && currentLevel < weaponData.maxLevel;
    }
}
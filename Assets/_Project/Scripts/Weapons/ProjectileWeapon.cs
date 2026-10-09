using System.Collections;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    public override bool Attack()
    {
        StartCoroutine(FireBurstRoutine());
        return true;
    }

    protected virtual IEnumerator FireBurstRoutine()
    {
        int shotsToFire = Mathf.Max(1, currentStats.amount);
        for (int i = 0; i < shotsToFire; i++)
        {
            SpawnProjectile();
            if (currentStats.interval > 0f)
                yield return new WaitForSeconds(currentStats.interval);
        }
    }

    protected virtual void SpawnProjectile()
    {
        // Se sobrescribe en cada arma para determinar la dirección del disparo
    }
}
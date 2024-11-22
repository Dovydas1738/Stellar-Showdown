using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class EnemyWeapon : Weapon
    {
        float fireTimer;

        private void Update()
        {
            fireTimer += Time.deltaTime;

            if(fireTimer >= weaponStrategy.FireRate)
            {
                weaponStrategy.Fire(firePoint, layer);
                fireTimer = 0f;
            }
        }
    }
}

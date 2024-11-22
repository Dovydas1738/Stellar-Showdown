using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class PlayerWeapon : Weapon
    {
        InputReader input;
        float fireTimer;

        private void Awake()
        {
            input = GetComponent<InputReader>();
        }

        private void Update()
        {
            fireTimer += Time.deltaTime;

            if (input.Fire && fireTimer >= weaponStrategy.FireRate)
            {
                weaponStrategy.Fire(firePoint, layer);
                fireTimer = 0f;
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public abstract class Spaceship : MonoBehaviour
    {
        [SerializeField] int maxHealth;
        float health;

        protected virtual void Awake() => health = maxHealth;

        public void SetMaxHealth(int amount) => maxHealth = amount;

        public void TakeDamage(int amount)
        {
            health -= amount;
            if (health <= 0)
            {
                Die();
            }
        }

        public void AddHealth(int amount)
        {
            health += amount;
            if(health > maxHealth)
            {
                health = maxHealth;
            }
        }

        public float GetHealthNormalized() => health / maxHealth;

        protected abstract void Die();
    }

}

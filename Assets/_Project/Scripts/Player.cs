using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shmup
{
    public class Player : Spaceship
    {
        [SerializeField] float maxFuel;
        [SerializeField] float fuelConsumptionRate;

        PlayerInput playerInput;

        float fuel;

        private void Start()
        {
            playerInput = GetComponent<PlayerInput>();
            fuel = maxFuel;
        }

        private void Update()
        {
            fuel -= fuelConsumptionRate * Time.deltaTime; 
        }

        public void AddFuel(int amount)
        {
            fuel += amount;
            if(fuel > maxFuel)
            {
                fuel = maxFuel;
            }
        }

        public float GetFuelNormalized() => fuel / maxFuel;

        protected override void Die()
        {
            playerInput.DeactivateInput();
        }
    }
}

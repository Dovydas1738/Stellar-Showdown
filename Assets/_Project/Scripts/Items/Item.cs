using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] protected float amount = 10f;
    }
}

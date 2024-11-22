using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public static class Vector3Extensions
    {
        // Sets any value of a Vector3

        public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
        }
    }
}

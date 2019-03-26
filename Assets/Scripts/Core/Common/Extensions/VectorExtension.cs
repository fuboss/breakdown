using UnityEngine;

namespace UnityCore.Extensions
{
    public static class VectorExtension
    {
        public static bool EqualsRound(this Vector3 v1, Vector3 v2)
        {
            return Mathf.Abs(v1.x) - Mathf.Abs(v2.x) < 0.02f && Mathf.Abs(v1.y) - Mathf.Abs(v2.y) < 0.02f &&
                   Mathf.Abs(v1.y) - Mathf.Abs(v2.y) < 0.02f;
       
        }
    }
}


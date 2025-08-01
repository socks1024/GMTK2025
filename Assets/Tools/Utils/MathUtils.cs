using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.Utils
{
    public static class MathUtils
    {
        public static Vector2 Rotate(this Vector2 vector, float angle)
        {
            Vector2 vec = Quaternion.Euler(0, 0, angle) * vector;

            return vec;
        }

        public static Vector2 RotateRoundToInt(this Vector2 vector, float angle)
        {
            Vector2 vec = Quaternion.Euler(0, 0, angle) * vector;

            vec.x = Mathf.RoundToInt(vec.x);
            vec.y = Mathf.RoundToInt(vec.y);

            return vec;
        }

        public static Vector2 RoundToInt(this Vector2 vec)
        {
            vec.x = Mathf.RoundToInt(vec.x);
            vec.y = Mathf.RoundToInt(vec.y);

            return vec;
        }

        public static Vector2 DiscardZ(this Vector3 vec)
        {
            return new Vector2(vec.x, vec.y);
        }
    }
}
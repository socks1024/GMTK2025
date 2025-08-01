using Tools.Utils;
using UnityEngine;

public class SnakePaint : MonoBehaviour
{
    #region Inspector Field

    public Sprite StraightBody; // 上下

    public Sprite BentBody; // UpToRight

    #endregion

    private bool IsNearVector(Vector2 vec, Vector2 another, float f = 45f)
    {
        return Vector2.Angle(vec, another) < f;
    }

    public void RepaintBody(Vector2 prevVec, Vector2 nextVec, SpriteRenderer sr)
    {
        sr.transform.localRotation = Quaternion.identity;

        if (Vector2.Angle(prevVec, nextVec) > 135f) // straight
        {
            sr.sprite = StraightBody;

            if (Mathf.Abs(prevVec.x) > Mathf.Abs(prevVec.y))
            {
                sr.transform.Rotate(0, 0, 90);
            }
        }
        else // bent
        {
            sr.sprite = BentBody;

            if (IsNearVector(prevVec, Vector2.up))
            {
                if (IsNearVector(nextVec, Vector2.left))
                {
                    sr.transform.Rotate(0, 0, 90);
                }
            }
            if (IsNearVector(prevVec, Vector2.right))
            {
                if (IsNearVector(nextVec, Vector2.down))
                {
                    sr.transform.Rotate(0, 0, -90);
                }
            }
            if (IsNearVector(prevVec, Vector2.left))
            {
                if (IsNearVector(nextVec, Vector2.up))
                {
                    sr.transform.Rotate(0, 0, 90);
                }
                else
                {
                    sr.transform.Rotate(0, 0, 180);
                }
            }
            if (IsNearVector(prevVec, Vector2.down))
            {
                if (IsNearVector(nextVec, Vector2.right))
                {
                    sr.transform.Rotate(0, 0, -90);
                }
                else
                {
                    sr.transform.Rotate(0, 0, 180);
                }
            }
        }
    }

    public void RepaintHead(Vector2 nextVec, SpriteRenderer sr)
    {
        sr.transform.localRotation = Quaternion.identity;

        if (Vector2.Angle(nextVec, Vector2.up) < 45)
        {
            sr.transform.Rotate(0, 0, 180);
        }

        if (Vector2.Angle(nextVec, Vector2.left) < 45)
        {
            sr.transform.Rotate(0, 0, -90);
        }

        if (Vector2.Angle(nextVec, Vector2.right) < 45)
        {
            sr.transform.Rotate(0, 0, 90);
        }
    }

    public void RepaintTail(Vector2 prevVec, SpriteRenderer sr)
    {
        sr.transform.localRotation = Quaternion.identity;

        if (Vector2.Angle(prevVec, Vector2.down) < 45)
        {
            sr.transform.Rotate(0, 0, 180);
        }

        if (Vector2.Angle(prevVec, Vector2.left) < 45)
        {
            sr.transform.Rotate(0, 0, 90);
        }

        if (Vector2.Angle(prevVec, Vector2.right) < 45)
        {
            sr.transform.Rotate(0, 0, -90);
        }
    }
}

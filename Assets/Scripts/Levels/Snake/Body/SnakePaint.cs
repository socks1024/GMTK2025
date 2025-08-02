using Tools.Utils;
using UnityEngine;

public class SnakePaint : MonoBehaviour
{
    #region Inspector Field

    public Sprite StraightBody; // 上下

    public Sprite BentBody; // UpToRight

    #endregion

    [HideInInspector]
    public BodyPosture Posture = BodyPosture.NONE;

    private bool IsNearVector(Vector2 vec, Vector2 another, float f = 45f)
    {
        return Vector2.Angle(vec, another) < f;
    }

    public void RepaintBody(Vector2 prevVec, Vector2 nextVec, SpriteRenderer sr)
    {
        RepaintBody(GetPosture(prevVec, nextVec), sr);
    }

    public BodyPosture GetPosture(Vector2 prevVec, Vector2 nextVec)
    {
        if (Vector2.Angle(prevVec, nextVec) > 135f) // straight
        {
            if (Mathf.Abs(prevVec.x) > Mathf.Abs(prevVec.y))
            {
                return BodyPosture.LEFT_RIGHT;
            }
            else
            {
                return BodyPosture.UP_DOWN;
            }
        }
        else // bent
        {
            if (IsNearVector(prevVec, Vector2.up))
            {
                if (IsNearVector(nextVec, Vector2.left))
                {
                    return BodyPosture.LEFT_UP;
                }
                else
                {
                    return BodyPosture.UP_RIGHT;
                }
            }
            if (IsNearVector(prevVec, Vector2.right))
            {
                if (IsNearVector(nextVec, Vector2.down))
                {
                    return BodyPosture.RIGHT_DOWN;
                }
                else
                {
                    return BodyPosture.UP_RIGHT;
                }
            }
            if (IsNearVector(prevVec, Vector2.left))
            {
                if (IsNearVector(nextVec, Vector2.up))
                {
                    return BodyPosture.LEFT_UP;
                }
                else
                {
                    return BodyPosture.DOWN_LEFT;
                }
            }
            if (IsNearVector(prevVec, Vector2.down))
            {
                if (IsNearVector(nextVec, Vector2.right))
                {
                    return BodyPosture.RIGHT_DOWN;
                }
                else
                {
                    return BodyPosture.DOWN_LEFT;
                }
            }
        }

        return BodyPosture.NONE;
    }

    public void RepaintBody(SpriteRenderer sr)
    {
        RepaintBody(Posture, sr);
    }

    public void RepaintBody(BodyPosture posture, SpriteRenderer sr)
    {
        Posture = posture;

        sr.transform.localRotation = Quaternion.identity;

        switch (posture)
        {
            case BodyPosture.UP_DOWN:
                sr.sprite = StraightBody;
                break;
            case BodyPosture.LEFT_RIGHT:
                sr.sprite = StraightBody;
                sr.transform.Rotate(0, 0, 90);
                break;
            case BodyPosture.UP_RIGHT:
                sr.sprite = BentBody;
                break;
            case BodyPosture.RIGHT_DOWN:
                sr.transform.Rotate(0, 0, -90);
                sr.sprite = BentBody;
                break;
            case BodyPosture.DOWN_LEFT:
                sr.transform.Rotate(0, 0, 180);
                sr.sprite = BentBody;
                break;
            case BodyPosture.LEFT_UP:
                sr.transform.Rotate(0, 0, 90);
                sr.sprite = BentBody;
                break;
        }
    }

    public void RepaintHead(Vector2 nextVec, SpriteRenderer sr)
    {
        sr.transform.localRotation = Quaternion.identity;

        if (Vector2.Angle(-nextVec, Vector2.down) < 45)
        {
            sr.transform.Rotate(0, 0, 180);
        }

        if (Vector2.Angle(-nextVec, Vector2.left) < 45)
        {
            sr.transform.Rotate(0, 0, 90);
        }

        if (Vector2.Angle(-nextVec, Vector2.right) < 45)
        {
            sr.transform.Rotate(0, 0, -90);
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

public enum BodyPosture
{
    UP_DOWN,
    LEFT_RIGHT,
    UP_RIGHT,
    RIGHT_DOWN,
    DOWN_LEFT,
    LEFT_UP,
    NONE,
}

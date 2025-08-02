using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feel;
using UnityEngine;

public class Portal : MonoBehaviour
{
    #region Inspector Field

    public Portal AnotherPortal;

    #endregion

    public virtual bool HasBody()
    {
        LayerMask mask = 1 << LayerMask.NameToLayer("SnakeBody") | 1 << LayerMask.NameToLayer("SnakeHead") | 1 << LayerMask.NameToLayer("SnakeTail");

        Collider2D col = Physics2D.OverlapCircle(GetComponentInChildren<SpriteRenderer>().transform.position, 0.2f, mask);

        return col != null;
    }

    void Start()
    {
        
    }
}

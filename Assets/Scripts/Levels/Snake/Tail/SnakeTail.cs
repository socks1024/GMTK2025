using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : SnakeBody
{
    public void RepaintTail(Vector3 nextPos)
    {
        _snakePaint.RepaintTail(nextPos - transform.position, _sr);
    }
}

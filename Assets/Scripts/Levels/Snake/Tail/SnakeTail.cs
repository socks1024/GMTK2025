using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : SnakeBody
{
    public void RepaintTail(Vector3 prevPos)
    {
        _snakePaint.RepaintTail(prevPos - transform.position, _sr);
    }
}

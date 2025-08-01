using System.Collections;
using System.Collections.Generic;
using Tools.GameProgrammingPatterns.Command;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SnakeInput), typeof(SnakeSense))]
public class SnakeHead : SnakeBody
{
    public List<SnakeBody> _bodies = new();
    public SnakeTail _tail;

    private SnakeInput _snakeInput;
    private SnakeSense _snakeView;

    private SnakeEatCommandInvoker _eatCommandInvoker = new();

    public UnityAction OnEatTail;

    private int portalMoveCount = 0;

    protected override void Awake()
    {
        base.Awake();

        _snakeInput = GetComponent<SnakeInput>();
        _snakeInput.TriggerMove += MoveHead;
        _snakeInput.TriggerUndo += Undo;

        _snakeView = GetComponent<SnakeSense>();
        _snakeView._eyeTransform = GetComponentInChildren<SpriteRenderer>().transform;

        for (int i = 0; i < _bodies.Count; i++)
        {
            SnakeBody body = _bodies[i];
            if (i == 0) body.PrevBody = this;
            else body.PrevBody = _bodies[i - 1];
        }

        if (_bodies.Count > 0)
        {
            _tail.PrevBody = _bodies[_bodies.Count - 1];
        }
        else
        {
            _tail.PrevBody = this;
        }

        LevelManager.Instance.CurrSnakeHead = this;

        OnEatTail += LevelManager.Instance.CompleteCurrLevel;
    }

    void Update()
    {
        Repaint();
    }

    public void Undo()
    {
        this.UndoBehaviour();

        _bodies.ForEach(_b => _b.UndoBehaviour());

        _tail.UndoBehaviour();

        _eatCommandInvoker.UndoCommand();
    }

    public void MoveHead(Vector2 direction)
    {
        if (_snakeView.SnakeEatTail(direction))
        {
            if (_bodies.Count != 0) OnEatTail?.Invoke();
        }

        bool blocked = _snakeView.Blocked(direction);

        #region Portal

        Portal portal = _snakeView.TryTeleport();

        bool readyToTeleport = false;

        if (portal != null)
        {
            if (portalMoveCount == 0)
            {
                direction.x = portal.AnotherPortal.transform.position.x - transform.position.x;
                direction.y = portal.AnotherPortal.transform.position.y - transform.position.y;

                readyToTeleport = true;
            }

            portalMoveCount += 1;
        }
        else
        {
            portalMoveCount = 0;
        }

        Debug.Log(portalMoveCount);

        #endregion

        if (readyToTeleport)
        {
            _snakeBehaviour.Move(direction, false, true);
            MoveBodyAndTail(false, true);

            _eatCommandInvoker.ExecuteCommand(new WaitCommand());
        }
        else if (blocked)
        {
            _snakeBehaviour.Move(direction, true, false);
            MoveBodyAndTail(true, false);
        }
        else
        {
            AbstractFood food = _snakeView.Eat(direction);

            if (food is null)
            {
                _snakeBehaviour.Move(direction, false, true);
                MoveBodyAndTail(false, true);

                _eatCommandInvoker.ExecuteCommand(new WaitCommand());
            }
            else
            {
                _eatCommandInvoker.ExecuteCommand(food);

                _snakeBehaviour.Move(direction, false, true);
                SnakeBody body = SpawnBody();
                MoveBodyAndTail(true, true);
                _bodies.Insert(0, body);
            }
        }
    }

    public SnakeBody SpawnBody()
    {
        SnakeBody newBody = Instantiate(BodyPrefab, transform.position, Quaternion.identity);

        newBody.transform.SetParent(transform.parent);

        if (_bodies.Count > 0)
        {
            newBody.OnSpawn(this, _bodies[0], transform.position);
        }
        else
        {
            newBody.OnSpawn(this, _tail, transform.position);
        }

        newBody.onDestroyAction += () => _bodies.Remove(newBody);

        return newBody;
    }

    public void MoveBodyAndTail(bool justBump, bool canUndo)
    {
        _bodies.ForEach(b => b.MoveToPrevBody(justBump, canUndo));
        _tail.MoveToPrevBody(justBump, canUndo);
    }

    public void Repaint()
    {
        if (_bodies.Count > 0)
        {
            this.RepaintHead(_bodies[0].transform.position);

            if (_bodies.Count == 1)
            {
                _bodies[0].RepaintBody(transform.position, _tail.transform.position);
            }
            else
            {
                for (int i = 0; i < _bodies.Count; i++)
                {
                    if (i == 0)
                    {
                        _bodies[i].RepaintBody(transform.position, _bodies[i + 1].transform.position);
                    }
                    else if (i == _bodies.Count - 1)
                    {
                        _bodies[i].RepaintBody(_bodies[i - 1].transform.position, _tail.transform.position);
                    }
                    else
                    {
                        _bodies[i].RepaintBody(_bodies[i - 1].transform.position, _bodies[i + 1].transform.position);
                    }
                }
            }

            _tail.RepaintTail(_bodies[_bodies.Count - 1].transform.position);
        }
        else
        {
            this.RepaintHead(_tail.transform.position);

            _tail.RepaintTail(transform.position);
        }
    }

    public void RepaintHead(Vector3 nextPos)
    {
        _snakePaint.RepaintHead(nextPos - transform.position, _sr);
    }

    #region Inspector Field

    [Header("Snake Body Parts")]
    public SnakeBody BodyPrefab;

    #endregion
}

public class SnakeEatCommandInvoker : CommandInvoker
{

}

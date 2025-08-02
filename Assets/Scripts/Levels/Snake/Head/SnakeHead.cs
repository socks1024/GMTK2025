using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Tools.GameProgrammingPatterns.Command;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SnakeInput), typeof(SnakeSense))]
public class SnakeHead : SnakeBody
{
    public List<SnakeBody> _bodies = new();
    public SnakeTail _tail;

    private SnakeInput _snakeInput;
    private SnakeSense _snakeSense;
    private MMF_Player _mmf_Player;

    private SnakeEatCommandInvoker _eatCommandInvoker = new();

    public UnityAction<int> OnEatTail;

    protected override void Awake()
    {
        base.Awake();

        _snakeInput = GetComponent<SnakeInput>();
        _snakeInput.TriggerMove += SnakeTurnProcess;
        _snakeInput.TriggerUndo += Undo;

        _snakeSense = GetComponent<SnakeSense>();
        _snakeSense._eyeTransform = GetComponentInChildren<SpriteRenderer>().transform;

        _mmf_Player = GetComponent<MMF_Player>();

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

        OnEatTail += FindAnyObjectByType<FinishScreen>().PopUp;
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

    public void SnakeTurnProcess(Vector2 direction)
    {
        bool blocked = _snakeSense.CheckBlocked(direction);

        if (_snakeSense.SnakeEatTail(direction))
        {
            if (_bodies.Count != 0)
            {
                _snakeBehaviour.Move(direction, false, true);
                SnakeBody body = SpawnBody();
                MoveBodyAndTail(true, true);
                _bodies.Insert(0, body);

                OnEatTail?.Invoke(_bodies.Count + 2);

                return;
            }
            else
            {
                blocked = true;
            }
        }

        #region Portal

        Portal currPortal = _snakeSense.TryTeleport();

        bool readyToTeleport = false;

        if (currPortal != null)
        {
            Portal anotherPortal = currPortal.AnotherPortal;

            if (!anotherPortal.HasBody())
            {
            
                direction.x = anotherPortal.transform.position.x - transform.position.x;
                direction.y = anotherPortal.transform.position.y - transform.position.y;

                readyToTeleport = true;

            }
            
        }

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

            // 此时不会向任何命令队列压入 WaitCommand 或其他 Command，即不记录此次输入的 Command
        }
        else
        {
            AbstractFood food = _snakeSense.Eat(direction);

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

                _mmf_Player.PlayFeedbacks();
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

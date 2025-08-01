using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SnakeBehaviour), typeof(SnakePaint))]
public class SnakeBody : MonoBehaviour
{
    [HideInInspector]
    public SnakeBody PrevBody;

    public UnityAction onDestroyAction;

    protected SnakeBehaviour _snakeBehaviour;

    protected SnakePaint _snakePaint;

    protected SpriteRenderer _sr;

    protected virtual void Awake()
    {
        _snakeBehaviour = GetComponent<SnakeBehaviour>();
        _snakePaint = GetComponent<SnakePaint>();
        _sr = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void OnDestroy()
    {
        onDestroyAction?.Invoke();
    }

    public void UndoBehaviour()
    {
        _snakeBehaviour.UndoCommand();
    }

    public void OnSpawn(SnakeBody head, SnakeBody nextBody, Vector3 position)
    {
        _snakeBehaviour.OnSpawn(head, nextBody, position);
    }

    public void MoveToPrevBody(bool wait, bool canUndo = false)
    {
        Vector2 vec = PrevBody.transform.position - transform.position;

        _snakeBehaviour.Move(vec, wait, canUndo);
    }

    public void RepaintBody(Vector3 prevPos, Vector3 nextPos)
    {
        _snakePaint.RepaintBody(prevPos - transform.position, nextPos - transform.position, _sr);
    }
}

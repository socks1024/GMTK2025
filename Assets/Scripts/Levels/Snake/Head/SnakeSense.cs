using UnityEngine;

public class SnakeSense : MonoBehaviour
{
    public Transform _eyeTransform;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public virtual bool CheckBlocked(Vector2 direction)
    {
        LayerMask mask = 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("SnakeBody");

        RaycastHit2D hit = Physics2D.Raycast(_eyeTransform.position, direction.normalized, 1f, mask);

        bool blocked = hit.collider != null;

        return blocked;
    }

    public virtual Portal TryTeleport()
    {
        LayerMask mask = 1 << LayerMask.NameToLayer("Portal");

        Collider2D col = Physics2D.OverlapCircle(_eyeTransform.position, 0.2f, mask);

        if (col != null && col.gameObject.GetComponentInParent<Portal>() != null)
        {
            return col.gameObject.GetComponentInParent<Portal>();
        }

        return null;
    }

    public virtual AbstractFood Eat(Vector2 direction)
    {
        LayerMask mask = 1 << LayerMask.NameToLayer("Food");

        RaycastHit2D hit = Physics2D.Raycast(_eyeTransform.position, direction.normalized, 1f, mask);

        if (hit.collider != null && hit.collider.gameObject.GetComponentInParent<AbstractFood>() != null)
        {
    
            _animator.Play("Eat");
            
            return hit.collider.gameObject.GetComponentInParent<AbstractFood>();
        }

        return null;
    }

    public virtual bool SnakeEatTail(Vector2 direction)
    {
        LayerMask mask = 1 << LayerMask.NameToLayer("SnakeTail");

        RaycastHit2D hit = Physics2D.Raycast(_eyeTransform.position, direction.normalized, 1f, mask);

        _animator.Play("Eat");

        return hit.collider != null && hit.collider.gameObject.GetComponentInParent<SnakeTail>();
    }
}

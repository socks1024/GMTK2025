using UnityEngine;

public class SnakeSense : MonoBehaviour
{
    public Transform _eyeTransform;

    public virtual bool Blocked(Vector2 direction)
    {
        LayerMask mask = 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("SnakeBody");

        RaycastHit2D hit = Physics2D.Raycast(_eyeTransform.position, direction.normalized, 1f, mask);

        bool blocked = hit.collider != null;

        return blocked;
    }

    public virtual AbstractFood Eat(Vector2 direction)
    {
        LayerMask mask = 1 << LayerMask.NameToLayer("Food");

        RaycastHit2D hit = Physics2D.Raycast(_eyeTransform.position, direction.normalized, 1f, mask);

        if (hit.collider != null && hit.collider.gameObject.GetComponentInParent<AbstractFood>() != null)
        {
            return hit.collider.gameObject.GetComponentInParent<AbstractFood>();
        }

        return null;
    }

    public virtual bool SnakeEatTail(Vector2 direction)
    {
        LayerMask mask = 1 << LayerMask.NameToLayer("SnakeTail");

        RaycastHit2D hit = Physics2D.Raycast(_eyeTransform.position, direction.normalized, 1f, mask);

        return hit.collider != null && hit.collider.gameObject.GetComponentInParent<SnakeTail>();
    }
}

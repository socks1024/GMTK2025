using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class PlayerMMControl : MonoBehaviour
{
    void Update()
    {
        HandleInput();
    }

    protected virtual void HandleInput()
    {
        bool input;

#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
        input = Keyboard.current.leftArrowKey.wasPressedThisFrame;
#else
        input = Input.GetKeyDown(KeyCode.LeftArrow);
#endif
        if (input) { Move(Vector2.left); }

#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
        input = Keyboard.current.rightArrowKey.wasPressedThisFrame;
#else
        input = Input.GetKeyDown(KeyCode.RightArrow);
#endif
        if (input) { Move(Vector2.right); }


#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
        input = Keyboard.current.downArrowKey.wasPressedThisFrame;
#else
        input = Input.GetKeyDown(KeyCode.DownArrow);
#endif
        if (input) { Move(Vector2.down); }

#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
        input = Keyboard.current.upArrowKey.wasPressedThisFrame;
#else
        input = Input.GetKeyDown(KeyCode.UpArrow);
#endif
        if (input) { Move(Vector2.up); }
    }

    protected virtual void Move(Vector2 direction)
    {
        bool blocked = CheckBlocked(direction, CellSize);

        if (blocked)
        {
            Bump(direction);
        }
        else
        {

            Vector3 movePos = new Vector3(
                transform.position.x + direction.x * CellSize,
                transform.position.y + direction.y * CellSize,
                transform.position.z
            );

            MovementSpring.MoveTo(movePos);

            ScaleSpring.Bump(5f * Vector3.one);

        }
    }

    protected virtual void Bump(Vector2 direction)
    {
        MovementSpring.Bump(new Vector3(4f * -direction.x, 4f * -direction.y, 0f));

        float rotationBumpZ = 450f;
        if (direction.x != 0f) rotationBumpZ += 450f;
        if (direction.x < 0f || direction.y < 0f) rotationBumpZ *= -1;

        RotationSpring.Bump(new Vector3(0, 0, rotationBumpZ));
    }

    protected virtual bool CheckBlocked(Vector2 direction, float length)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, length);

        bool blocked = hit.collider != null && hit.collider.gameObject.CompareTag("Obstacles");

        return blocked;
    }

    #region Inspector Field

    [Header("Spring")]
    public MMSpringPosition MovementSpring;
    public MMSpringRotation RotationSpring;
    public MMSpringScale ScaleSpring;

    [Header("Move Properties")]
    public float CellSize;
    
    #endregion
}

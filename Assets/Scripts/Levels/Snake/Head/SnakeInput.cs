using UnityEngine;
using UnityEngine.Events;

public class SnakeInput : MonoBehaviour
{
    public UnityAction<Vector2> TriggerMove;

    public UnityAction TriggerUndo;

    public bool IsActive = true;

    void Update()
    {
        if (IsActive)
        {

            HandleUndoInput();
            
            HandleMoveInput();

        }
    }

    protected virtual void HandleMoveInput()
    {
        bool input;

#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
        input = Keyboard.current.leftArrowKey.wasPressedThisFrame;
#else
        input = Input.GetKeyDown(KeyCode.LeftArrow);
#endif
        if (input) { TriggerMove(Vector2.left); return; }

#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
        input = Keyboard.current.rightArrowKey.wasPressedThisFrame;
#else
        input = Input.GetKeyDown(KeyCode.RightArrow);
#endif
        if (input) { TriggerMove(Vector2.right); return; }


#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
        input = Keyboard.current.downArrowKey.wasPressedThisFrame;
#else
        input = Input.GetKeyDown(KeyCode.DownArrow);
#endif
        if (input) { TriggerMove(Vector2.down); return; }

#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
        input = Keyboard.current.upArrowKey.wasPressedThisFrame;
#else
        input = Input.GetKeyDown(KeyCode.UpArrow);
#endif
        if (input) { TriggerMove(Vector2.up); return; }
    }

    public void HandleUndoInput()
    {
        bool input = false;
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
        input = Keyboard.current.zKey.wasPressedThisFrame;
#else
        input = Input.GetKeyDown(KeyCode.Z);
#endif
        if (input) { TriggerUndo(); }
    }
}

using System;
using DG.Tweening;
using MoreMountains.Feedbacks;
using Tools.GameProgrammingPatterns.Command;
using UnityEngine;

[RequireComponent(typeof(MMSpringPosition), typeof(MMSpringRotation), typeof(MMSpringScale))]
public class SnakeBehaviour : MonoBehaviour
{
	protected bool _isMoving;

	protected SnakeMMSpringMoveCmdInvoker _invoker = new();

	public MMSpringPosition _movementSpring;
	protected MMSpringRotation _rotationSpring;
	protected MMSpringScale _scaleSpring;

	protected virtual void Awake()
	{
		_movementSpring = GetComponent<MMSpringPosition>();
		_rotationSpring = GetComponent<MMSpringRotation>();
		_scaleSpring = GetComponent<MMSpringScale>();

		_movementSpring.OnEquilibriumReached.AddListener(PositionAlign);
	}

	public void UndoCommand()
	{
		_invoker.UndoCommand();
	}

	#region Move

	/// <summary>
	/// 触发当前身体的移动效果
	/// </summary>
	/// <param name="direction">移动方向</param>
	/// <param name="wait">是否只是震动而不移动位置</param>
	public virtual void Move(Vector2 direction, bool wait, bool canUndo = false)
	{
		if (wait)
		{
			if (canUndo) _invoker.ExecuteCommand(new WaitCommand());

			Bump(direction);
		}
		else
		{
			_invoker.ExecuteCommand(new SnakeMMSpringMoveCommand(transform.position, _movementSpring, direction, CellSize));

			_isMoving = true;

			_scaleSpring.Bump(5f * Vector3.one);
		}
	}

	protected void PositionAlign()
	{
		transform.position = new Vector3(
			(int)Math.Round(transform.position.x),
			(int)Math.Round(transform.position.y),
			transform.position.z
		);

		_isMoving = false;
	}

	protected virtual void Bump(Vector2 direction)
	{
		_movementSpring.Bump(new Vector3(4f * -direction.x, 4f * -direction.y, 0f));

		float rotationBumpZ = 450f;
		if (direction.x != 0f) rotationBumpZ += 450f;
		if (direction.x < 0f || direction.y < 0f) rotationBumpZ *= -1;

		_rotationSpring.Bump(new Vector3(0, 0, rotationBumpZ));
	}

	#endregion Move

	#region Spawn

	public virtual void OnSpawn(SnakeBody head, SnakeBody nextBody, Vector3 position)
	{
		_invoker.ExecuteCommand(new SnakeBodySpawnCommand(GetComponent<SnakeBody>(), head, nextBody, position, _movementSpring));
	}

	#endregion

	#region Inspector Field

	[Header("Move Properties")]
	public float CellSize = 1f;

	#endregion
}

public class SnakeMMSpringMoveCmdInvoker : CommandInvoker
{

}

public class SnakeMMSpringMoveCommand : ICommand
{
	private Vector3 _originalPosition;
	private Vector3 _newPosition;
	private MMSpringPosition _movementSpring;

	public SnakeMMSpringMoveCommand(Vector3 originalPosition, MMSpringPosition movementSpring, Vector2 direction, float distance)
	: this(originalPosition, new Vector3(originalPosition.x + direction.x * distance, originalPosition.y + direction.y * distance, originalPosition.z), movementSpring)
	{
		
	}

	public SnakeMMSpringMoveCommand(Vector3 originalPosition, Vector3 newPosition, MMSpringPosition movementSpring)
	{
		_originalPosition = originalPosition;
		_newPosition = newPosition;
		_movementSpring = movementSpring;
	}

	public virtual void Execute()
	{
		_movementSpring.Finish();
		_movementSpring.OnEquilibriumReached.Invoke();
		_movementSpring.MoveTo(_newPosition);
	}

	public virtual void Undo()
	{
		_movementSpring.Finish();
		_movementSpring.OnEquilibriumReached.Invoke();
		_movementSpring.MoveTo(_originalPosition);
    }
}

public class WaitCommand : ICommand
{
	public void Execute()
	{
		
	}

	public void Undo()
	{
		
	}
}

public class SnakeBodySpawnCommand : ICommand
{
	private SnakeBody _head;
	private SnakeBody _nextBody;
	private SnakeBody _newBody;

	public SnakeBodySpawnCommand(SnakeBody newBody, SnakeBody head, SnakeBody nextBody, Vector3 position, MMSpringPosition springPosition)
	{
		_newBody = newBody;
		_head = head;
		_nextBody = nextBody;

		springPosition.MoveToInstant(position);
	}

	public void Execute()
	{
		_nextBody.PrevBody = _newBody;
		_newBody.PrevBody = _head;
	}

	public void Undo()
	{
		_nextBody.PrevBody = _head;
        UnityEngine.Object.Destroy(_newBody.gameObject);
	}
}


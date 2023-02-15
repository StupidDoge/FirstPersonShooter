using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHolder : MonoBehaviour
{
	public static Action OnInventoryUsed;
	public static Action OnReloadButtonPressed;
	public static Action OnMouseLeftButtonClicked;
	public static Action<bool> OnMouseRightButtonHold;
	public static Action<bool> OnMouseLeftButtonHold;

	[Header("Character Input Values")]
	public Vector2 move;
	public Vector2 look;
	public bool jump;
	public bool sprint;
	public bool interact;
	public bool inventory;
	public bool leftMouseClick;
	public bool rightMouseClick;
	public bool reload;

	[Header("Movement Settings")]
	public bool analogMovement;

	[Header("Mouse Cursor Settings")]
	public bool cursorLocked = true;
	public bool cursorInputForLook = true;

	[Header("Interaction settings")]
	[SerializeField] private float _interactionTimeout = 0.1f;

	private bool _canInteract = true;

    public void OnMove(InputAction.CallbackContext context)
	{
		MoveInput(context.ReadValue<Vector2>());
	}

	public void OnLook(InputAction.CallbackContext context)
	{
		if (cursorInputForLook)
		{
			LookInput(context.ReadValue<Vector2>());
		}
	}

	public void OnJump(InputAction.CallbackContext context)
	{
		if (context.started)
			JumpInput(true);
	}

	public void OnSprint(InputAction.CallbackContext context)
	{
		if (context.started)
			SprintInput(true);

		if (context.canceled)
			SprintInput(false);
	}

	public void OnInteraction(InputAction.CallbackContext context)
	{
		if (context.started)
			InteractionInput(true);

		if (context.canceled)
			InteractionInput(false);
	}

	public void OnInventory(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			inventory = true;
		}

		if (context.canceled)
		{
			inventory = false;
			OnInventoryUsed?.Invoke();
		}
	}

	public void OnReload(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			reload = true;
		}

		if (context.canceled)
		{
			reload = false;
			OnReloadButtonPressed?.Invoke();
		}
	}

	public void OnMouseLeftClick(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			leftMouseClick = true;
			OnMouseLeftButtonHold?.Invoke(true);
		}

		if (context.canceled)
		{
			leftMouseClick = false;
			OnMouseLeftButtonClicked?.Invoke();
			OnMouseLeftButtonHold?.Invoke(false);
		}
	}

	public void OnMouseRightClick(InputAction.CallbackContext context)
    {
		if (context.started)
		{
			rightMouseClick = true;
			OnMouseRightButtonHold?.Invoke(true);
		}

		if (context.canceled)
        {
			rightMouseClick = false;
			OnMouseRightButtonHold?.Invoke(false);
		}
    }

	public void MoveInput(Vector2 newMoveDirection)
	{
		move = newMoveDirection;
	}

	public void LookInput(Vector2 newLookDirection)
	{
		look = newLookDirection;
	}

	public void JumpInput(bool newJumpState)
	{
		jump = newJumpState;
	}

	public void SprintInput(bool newSprintState)
	{
		sprint = newSprintState;
	}

	public void InteractionInput(bool input)
	{
		interact = input;
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		SetCursorState(cursorLocked);
	}

	private void SetCursorState(bool newState)
	{
		Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
	}
}
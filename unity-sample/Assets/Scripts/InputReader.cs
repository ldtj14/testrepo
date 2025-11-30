using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Captures input actions and exposes them as events. Hook these up to your Input Actions asset.
/// </summary>
public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public event Action<Vector2> MoveEvent;
    public event Action JumpEvent;
    public event Action FireEvent;

    private Controls controls;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpEvent?.Invoke();
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            FireEvent?.Invoke();
        }
    }
}

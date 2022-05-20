using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 CameraValue {get; private set;}
    public Vector2 MovementValue {get; private set;}
    public event Action JumpEvent;
    public event Action DodgeEvent;
    public event Action TargetEvent;
    public event Action CancelEvent;

    private Controls controls;

    private void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Enable();
    }

    private void OnDestroy()
    {
        controls.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        JumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        DodgeEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnCamera(InputAction.CallbackContext context)
    {
        // CameraValue = context.ReadValue<Vector2>();
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        TargetEvent?.Invoke();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        CancelEvent?.Invoke();
    }
}

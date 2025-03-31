using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Vector2 move;
    public Vector2 aim;
    [SerializeField] private bool shooting;
    [SerializeField] private bool throwing;
    [SerializeField] private bool jumping;
    [SerializeField] private bool teleporting;
    
    [Header("Events")]
    public UnityEvent onJump;
    [Space(25)] 
    public UnityEvent onTeleport;
    [Space(25)]
    public UnityEvent onShoot;
    public UnityEvent onShootStart;
    public UnityEvent onShootEnd;
    [Space(25)]
    public UnityEvent onThrow;

    private void Update()
    {
        Aim();
        if(shooting) onShoot?.Invoke();
    }

    private void Aim() => aim = Gamepad.current.leftStick.ReadValue();
    
    public void Move(InputAction.CallbackContext ctx) => move = ctx.ReadValue<Vector2>();

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            onJump?.Invoke();
            jumping = true;
        }
        else if (ctx.canceled)
        {
            jumping = false;
        }
    }

    public void Teleport(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            onTeleport?.Invoke();
            teleporting = true;
        }
        else if (ctx.canceled)
        {
            teleporting = false;
        }
    }

    public void Throw(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            onThrow?.Invoke();
            throwing = true;
        }
        else
        {
            throwing = false;
        }
    }

    public void Shoot(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            onShootStart?.Invoke();
            shooting = true;
        }
        else if (ctx.canceled)
        {
            onShootEnd?.Invoke();
            shooting = false;
        }
    }
}

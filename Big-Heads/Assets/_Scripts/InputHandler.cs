using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputHandler : MonoBehaviour
{
    public bool isLeftStickAim = true;
    [Space(10)]
    public Vector2 move;
    public Vector2 aim;
    [SerializeField] private bool shooting;
    [SerializeField] private bool jumping;
    [SerializeField] private bool teleportThrowing;
    
    [Header("Events")]
    public UnityEvent onJump;
    [Space(25)] 
    public UnityEvent onTeleportThrow;
    [Space(25)]
    public UnityEvent onThrow;
    [Space(25)]
    public UnityEvent onShoot;
    public UnityEvent onShootStart;
    public UnityEvent onShootEnd;

    private void Update()
    {
        Aim();
        if(shooting) onShoot?.Invoke();
    }

    private void Aim()
    {
        aim = Gamepad.current.rightStick.ReadValue() != Vector2.zero ?
            Gamepad.current.rightStick.ReadValue() :
            Gamepad.current.leftStick.ReadValue();
    }
    
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

    public void TeleportThrow(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            onTeleportThrow?.Invoke();
            teleportThrowing = true;
        }
        else if (ctx.canceled)
        {
            teleportThrowing = false;
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

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public Vector2 aim;
    public bool headAttached = true;

    [SerializeField] private Transform head;
    [SerializeField] private Transform body;
    
    public void Aim(InputAction.CallbackContext ctx) => aim = ctx.ReadValue<Vector2>().normalized;
    
    public void Teleport(InputAction.CallbackContext ctx)
    { 
        if (ctx.started && !headAttached)
        {
            body.position = head.GetChild(0).position;
            headAttached = true;
        }
    }
}

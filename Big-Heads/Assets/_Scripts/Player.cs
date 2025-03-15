using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public Vector2 aim;
    
    public void Aim(InputAction.CallbackContext ctx) => aim = ctx.ReadValue<Vector2>().normalized;

}

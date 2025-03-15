using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Aim : MonoBehaviour
{
    public Vector2 dir;
    
    public void PlayerAim(InputAction.CallbackContext ctx) => dir = ctx.ReadValue<Vector2>().normalized;

}

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BodyMovement : MonoBehaviour
{
   [SerializeField] private Rigidbody2D rb;
   [SerializeField] private float moveSpeed = 5f, jumpForce = 100f;
   [SerializeField] private float gravity = 9.81f;
   [SerializeField] private bool isGrounded;
   private Vector2 direction, Movement;
   [SerializeField] private InputActionAsset actionAsset;

   void Start()
   {
       
   }

    // Update is called once per frame
    void Update()
    { 
        Movement = new Vector2(direction.x* moveSpeed, rb.linearVelocity.y);
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Movement* Time.deltaTime);
    }

    public void MoveCube(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 PlayerInput = context.ReadValue<Vector2>();
            direction.x = PlayerInput.x;
            direction.y = PlayerInput.y;
        }
        else
        {
            direction = Vector2.zero;
        }
    }
}
